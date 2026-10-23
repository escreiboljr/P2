using Archivos;
using DatosDecod;
using DatosDecod21;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Archvios
{
    public class LeerDatos
    {
        private readonly Dictionary<string, double> bpsCandidatos = new Dictionary<string, double>();
        private readonly Dictionary<string, int> repeticionesBPS = new Dictionary<string, int>();
        private readonly Dictionary<string, double> ultimosQNH = new Dictionary<string, double>();
        public List<Mensaje> DatosProcesados(string nombreArchivo, IProgress<int> progreso = null)
        {
            ultimosBPS.Clear();
            ultimosTiemposBPS.Clear();
            bpsCandidatos.Clear();
            repeticionesBPS.Clear();
            ultimosQNH.Clear();
            List<Mensaje> listaTiras = new List<Mensaje>();
            Queue<byte> cola = new Queue<byte>();

            using (FileStream fs = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[8 * 1024 * 1024];
                int leidos;

                long bytesTotales = fs.Length;
                int ultimoPorcentaje = -1;

                progreso?.Report(0);

                while ((leidos = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < leidos; i++)
                        cola.Enqueue(buffer[i]);

                    ProcesarMensajes(cola, listaTiras);

                    // Calcular el porcentaje de carga
                    int porcentaje = (int)(fs.Position * 100 / bytesTotales);

                    // Actualizar solo si cambia el porcentaje
                    if (porcentaje != ultimoPorcentaje)
                    {
                        progreso?.Report(porcentaje);
                        ultimoPorcentaje = porcentaje;
                    }
                }
            }

            // Procesar los mensajes restantes
            ProcesarMensajes(cola, listaTiras);

            progreso?.Report(100);

            return listaTiras;
        }

        private void ProcesarMensajes(Queue<byte> cola, List<Mensaje> listaTiras)
    {
        while (true)
        {
            // ¿Hay al menos CAT + LEN?
            if (cola.Count < 3)
                return;

            byte[] cabecera = cola.Take(3).ToArray();
            int longitud = (cabecera[1] << 8) | cabecera[2];
            int bytesMensaje = longitud - 3;

            // ¿Hay suficientes bytes para el mensaje completo?
            if (cola.Count < longitud)
                return; // mensaje partido → esperar más datos

            // Extraer CAT + LEN
            byte categoria = cola.Dequeue();
            cola.Dequeue(); // MSB
            cola.Dequeue(); // LSB

            // Extraer el mensaje completo
            byte[] mensajeBytes = new byte[bytesMensaje];
            for (int i = 0; i < bytesMensaje; i++)
                mensajeBytes[i] = cola.Dequeue();

            Queue<byte> colaMensaje = new Queue<byte>(mensajeBytes);

            // Decodificar
            if (categoria == 48)
            {
                var tira48 = DecodificarCAT048(colaMensaje);
                    if (tira48.PositionCorrectedLatLonAlt[0] > 40.99 &&
                        tira48.PositionCorrectedLatLonAlt[0] < 41.7 &&
                        tira48.PositionCorrectedLatLonAlt[1] > 1.5 &&
                        tira48.PositionCorrectedLatLonAlt[1] < 2.6)
                    {
                        if (tira48.mode3.reply =="7777")
                        { listaTiras.Add(new Mensaje { categoria = categoria, tira = tira48, TransFijo=true }); }
                        else
                        { listaTiras.Add(new Mensaje { categoria = categoria, tira = tira48, TransFijo = false }); }
                    }
                }
            else if (categoria == 21)
            {
                var tira21 = DecodificarCAT021(colaMensaje);
                DetectarCambioBPS(tira21);

    if (!double.IsNaN(tira21.FlightLevel) &&
    tira21.FlightLevel < 60 &&
    !string.IsNullOrEmpty(tira21.TargetAddress))
                    {
                        /*Debug.WriteLine(
                            "PRUEBA QNH | " +
                            "ADDR: " + tira21.TargetAddress +
                            " | TIEMPO: " + tira21.TimeReceptionPosition.ToString("F2") +
                            " | FL: " + tira21.FlightLevel.ToString("F2") +
                            " | BPS: " + tira21.BPS.ToString("F1") +
                            " | STD: " + tira21.ajusteEstandar +
                            " | QNH GUARDADO: " + tira21.qnhUtilizado.ToString("F1") +
                            " | ALT PRESION: " + tira21.altitudPresionFt.ToString("F1") +
                            " | ALT QNH: " + tira21.altitudQNHFt.ToString("F1")
                        );*/
                    }
                    if (tira21.TargetRep.GBS == 0)
                    {
                        if (tira21.PosWGS84HighRes[0] > 40.9 && //Descartar mensajes de aeronaves que no se encuentren dentro del filtro geográfico
                            tira21.PosWGS84HighRes[0] < 41.7 &&
                            tira21.PosWGS84HighRes[1] > 1.5 &&
                            tira21.PosWGS84HighRes[1] < 2.6)
                        {
                            listaTiras.Add(new Mensaje { categoria = categoria, tira = tira21, TransFijo = false });
                        }
                    }
                }
            }
    }

        private void SkipUnknownFRN(Queue<byte> q)
        {
            while (q.Count > 0)
                q.Dequeue();
        }

        private static readonly Dictionary<int, Action<Queue<byte>, TiraDatosDecod048>> decoders =
    new Dictionary<int, Action<Queue<byte>, TiraDatosDecod048>>
    {
        { 1,  (q, td) => td.DecDataSourceID(q) },
        { 2,  (q, td) => td.DecodeTimeOfDayFromBits(q) },
        { 3,  (q, td) => td.DecodeTargetRep(q) },
        { 4, (q, td) => td.DecodePosSlantPolarCoord(q) },
        { 5, (q, td)  => td.DecodeMode3A(q) },
        { 6,  (q, td) => td.DecodeFL(q) },
        { 7,  (q, td) => td.DecodeRadarPlot(q) },
        { 8,  (q, td) => td.DecodeAircraftAddress(q) },
        { 9,  (q, td) => td.DecodeAircraftID(q) },
        { 10, (q, td) => td.DecodeI048_250(q) },
        { 11, (q, td) => td.DecodeTrack(q) },
        { 12, (q, td) =>
            {
                q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
            }
        },
        { 13, (q, td) => td.DecodeTrackVelocityPolar(q) },
        { 14, (q, td) => td.DecodeTrackStatus(q) },
        { 15, (q, td) =>
            {
                q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
            }
        },
        { 16, (q, td) =>
            {
                byte b = q.Dequeue();
                List<int> bits = TiraDatosDecod048.ByteToBits(b);

                // FX = bit 7
                while (bits[7] == 1)
                {
                    b = q.Dequeue();
                    bits = TiraDatosDecod048.ByteToBits(b);
                }
            }
        },
        { 17, (q, td) =>
            {
                q.Dequeue(); q.Dequeue();
            }
        },
        { 18, (q, td) =>
            {
                q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
            }
        },
        { 19, (q, td) =>
            {
                q.Dequeue(); q.Dequeue();
            }
        },
        { 20, (q, td) =>
            {
                byte octInfo = q.Dequeue();
                List<int> bits = TiraDatosDecod048.ByteToBits(octInfo);

                if (bits[7] == 1)
                   { q.Dequeue(); }

                if (bits[0] == 1)
                {
                    q.Dequeue();
                    q.Dequeue();
                }
                if (bits[1] == 1)
                {
                    q.Dequeue(); q.Dequeue(); q.Dequeue();
                    q.Dequeue(); q.Dequeue(); q.Dequeue();
                    q.Dequeue();
                }
            }
        },
        { 21, (q, td) => td.DecodeCommACAScapability(q) }
    };


        public TiraDatosDecod048 DecodificarCAT048(Queue<byte> listaBytes)
        {
            TiraDatosDecod048 td = new TiraDatosDecod048();

            List<int> listaFRN = SacarFRNpresentes(listaBytes);

            foreach (int frn in listaFRN)
            {
                if (decoders.TryGetValue(frn, out var accion))
                    accion(listaBytes, td);
                else if (frn > 21)
                {
                    SkipUnknownFRN(listaBytes);
                    break;   // ya no hay nada más que decodificar
                }
            }
            if (!double.IsNaN(td.PosSlantPolarCoord[0]) &&!double.IsNaN(td.PosSlantPolarCoord[1]))
            {
                td.ConvertToWGS84(41.3007023333, 2.1020581944, 27.257);
            }
            AplicarCorreccionQNH(td);
            return td;
        }

        private static readonly Dictionary<int, Action<Queue<byte>, TiraDatosDecod021>>
            decodersCAT021 =
            new Dictionary<int, Action<Queue<byte>, TiraDatosDecod021>>
            {
                { 1, (q, td) => td.DecDataSourceID(q) },

                { 2, (q, td) => td.DecodeTargetReportDescriptor(q)},

                { 3, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 4, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 5, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 6, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 7, (q, td) => td.DecodePosWGS84HighRes(q) },

                { 8, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 9, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 10, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 11, (q, td) => td.DecodeTargetAddress(q) },

                { 12, (q, td) => td.DecodeTimeReceptionPosition(q) },

                { 13, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 14, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 15, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 16, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 17, (q, td) =>
                    {
                        byte b = q.Dequeue();
                        List<int> listaBits = td.ByteToBits(b);
                        if (listaBits[7]==1)
                        {
                            b = q.Dequeue();
                            listaBits = td.ByteToBits(b);
                            if (listaBits[7]==1)
                            {
                                b = q.Dequeue();
                                listaBits = td.ByteToBits(b);
                                if (listaBits[7]==1)
                                {
                                    b = q.Dequeue();
                                    listaBits = td.ByteToBits(b);
                                }
                            }
                        }
                    }
                },

                { 18, (q, td) => q.Dequeue() },

                { 19, (q, td) => td.DecodeMode3A(q) },

                { 20, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 21, (q, td) => td.DecodeFlightLevel(q) },

                { 22, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 23, (q, td) => q.Dequeue() },

                { 24, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 25, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 26, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 27, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 28, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 29, (q, td) => td.DecodeTargetIdentification(q) },

                { 30, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 31, (q, td) =>
                    {
                        byte b = q.Dequeue();
                        List<int> listaBits = td.ByteToBits(b);
                        if (listaBits[7]==1)
                        {
                            q.Dequeue();
                        }
                        if (listaBits[3]==1)
                        {
                            q.Dequeue();
                        }
                        if (listaBits[2]==1)
                        {
                            q.Dequeue();
                            q.Dequeue();
                        }
                        if (listaBits[1]==1)
                        {
                            q.Dequeue();
                            q.Dequeue() ;
                        }
                        if (listaBits[0]==1)
                        {
                            q.Dequeue();
                            q.Dequeue();
                        }
                    }
                },

                { 32, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 33, (q, td) =>
                    {
                        q.Dequeue(); q.Dequeue();
                    }
                },

                { 34, (q, td) =>            //DUDA --------------------------------------_______________-----__-____________----__--
                    {
                        byte b = q.Dequeue();
                        List<int> listaBits = td.ByteToBits(b);
                        if (listaBits[7]==1)
                        {
                            q.Dequeue();
                        }
                        if (listaBits[1]==1)
                        {
                            q.Dequeue();
                        }
                        if (listaBits[0]==1)
                        {
                            q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();
                            q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();
                        }
                    }
                },

                { 35, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 36, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 37, (q, td) =>
                    {
                        byte b = q.Dequeue();
                        List<int> listaBits = td.ByteToBits(b);
                        if (listaBits[7]==1)
                        {
                            q.Dequeue();
                        }
                    }
                },

                { 38, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 39, (q, td) =>
                    {
                        q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();q.Dequeue();
                        q.Dequeue();
                    }
                },

                { 40, (q, td) => // I021/260: 7 bytes
                    {
                        q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue(); q.Dequeue();
                    }
                },

                { 41, (q, td) =>
                    {
                        q.Dequeue();
                    }
                },

                { 42, (q, td) =>
                    {
                        int contador = 0;
                        while (q.Count > 0)
                        {
                            byte b = q.Dequeue();
                            List<int> listaBits = td.ByteToBits(b);
                            int fx = listaBits[7];
                            listaBits.RemoveAt(7);
                            foreach (int bit in listaBits)
                            {
                                if (bit == 1)
                                {contador++; }
                            }
                            if (fx == 0)
                            {
                                break;
                            }
                        }
                        int j = 0;
                        while (j<contador)
                        {
                            q.Dequeue();
                            j ++;
                        }
                    }
                },

                { 48, (q, td) =>
                    {
                        td.DecodeReservedExp(q);
                    }
                },

                { 49, (q, td) =>
                    {
                        while (q.Count()>0)
                        {
                            q.Dequeue();
                        }
                    }
                }

            };

        public TiraDatosDecod021 DecodificarCAT021(Queue<byte> listaBytes)
        {
            TiraDatosDecod021 td = new TiraDatosDecod021();

            List<int> listaFRN = SacarFRNpresentes(listaBytes);

            foreach (int frn in listaFRN)
            {
                if (decodersCAT021.TryGetValue(frn, out var accion))
                    accion(listaBytes, td);
                else if (frn > 48)
                {
                    SkipUnknownFRN(listaBytes);
                    break;
                }
            }
            td.IdentificarAjusteBarometrico();
            AplicarCorreccionQNH(td);

            return td;
        }

        public List<int> SacarFRNpresentes(Queue<byte> ColaBytes)
        {
            List<int> listaBits = new List<int>();
            bool seguir = true;

            while (seguir)
            {
                byte b = ColaBytes.Dequeue();
                listaBits.AddRange(TiraDatosDecod048.ByteToBits(b));

                if (listaBits[listaBits.Count - 1] == 0)
                    seguir = false;
            }
            List<int> FRNpresentes = new List<int>();
            int frnBase = 1;   // FRN 1–7 en el primer octeto

            // Procesar cada octeto FSPEC
            for (int i = 0; i < listaBits.Count; i += 8)
            {
                List<int> octeto = listaBits.GetRange(i, 8);
                for (int bit = 0; bit < 7; bit++)
                {
                    if (octeto[bit] == 1)
                        FRNpresentes.Add(frnBase + bit);
                }
                bool fx = octeto[7] == 1;

                if (!fx)
                    break;

                frnBase += 7;  
            }

            return FRNpresentes;
        }
        private readonly Dictionary<string, double> ultimosBPS = new Dictionary<string, double>();


        private readonly Dictionary<string, double> ultimosTiemposBPS =
            new Dictionary<string, double>();


        private void DetectarCambioBPS(TiraDatosDecod021 td)
        {
            if (td == null || string.IsNullOrEmpty(td.TargetAddress))
                return;

            if (double.IsNaN(td.BPS) || double.IsInfinity(td.BPS))
                return;

            string direccion = td.TargetAddress;
            double tiempo = td.TimeReceptionPosition;

            if (!ultimosBPS.TryGetValue(direccion, out double bpsReferencia))
            {
                ultimosBPS[direccion] = td.BPS;

                if (tiempo >= 0 && !double.IsInfinity(tiempo))
                    ultimosTiemposBPS[direccion] = tiempo;

                return;
            }

            double diferencia = td.BPS - bpsReferencia;

            if (Math.Abs(diferencia) < 3.0)
                return;

            string tiempoTranscurrido = "N/A";

            if (tiempo >= 0 &&
                !double.IsInfinity(tiempo) &&
                ultimosTiemposBPS.TryGetValue(direccion, out double tiempoAnterior))
            {
                double dt = tiempo - tiempoAnterior;

                if (dt < -43200)
                    dt += 86400;

                if (dt >= 0)
                    tiempoTranscurrido = dt.ToString("F2") + " s";
            }
            ultimosBPS[direccion] = td.BPS;

            if (tiempo >= 0 && !double.IsInfinity(tiempo))
                ultimosTiemposBPS[direccion] = tiempo;
        }



        private void AplicarCorreccionQNH(TiraDatosDecod021 td)
        {
            if (td == null)
                return;

            string direccion = td.TargetAddress;
            double bps = td.BPS;

            bool bpsValido =
                !double.IsNaN(bps) &&
                !double.IsInfinity(bps) &&
                bps >= 900.0 &&
                bps <= 1100.0;

            // Mismo criterio STD que IdentificarAjusteBarometrico()
            bool esEstandar =
                                bpsValido &&
                                bps >= 1013.0 &&
                                bps <= 1013.5;

            double qnh = double.NaN;

            if (!string.IsNullOrWhiteSpace(direccion))
            {
                if (bpsValido && !esEstandar)
                {
                    // Confirmamos cualquier nuevo BPS no estándar,
                    // sin exigir una diferencia mínima de 3 hPa.
                    ConfirmarCandidatoBPS(direccion, bps);
                }
                else
                {
                    // STD o BPS ausente: no actualizamos el QNH.
                    // Descartamos una confirmación incompleta.
                    bpsCandidatos.Remove(direccion);
                    repeticionesBPS.Remove(direccion);
                }

                // Recuperar la última referencia confirmada.
                if (ultimosQNH.TryGetValue(direccion, out double qnhGuardado))
                {
                    qnh = qnhGuardado;
                }
            }

            // Calcular la altitud usando el QNH disponible.
            td.CalcularAltitudBarometrica(qnh);
        }
        private void AplicarCorreccionQNH(TiraDatosDecod048 td)
        {
            td.altitudQNHFt = double.NaN;
            td.qnhUtilizado = double.NaN;

            if (double.IsNaN(td.altitudPresionFt))
                return;

            // A partir de 6000 ft utilizamos la altitud de presión
            if (td.altitudPresionFt >= 6000)
            {
                td.altitudQNHFt = td.altitudPresionFt;
                return;
            }

            // Identificar el avión
            string direccion = td.AircrftAddrs;

            if (string.IsNullOrWhiteSpace(direccion))
                return;

            double bps = td.BPS;

            // Comprobar si tenemos una presión válida y no estándar
            if (!double.IsNaN(bps) &&
                bps >= 900 &&
                bps <= 1100 &&
                (bps < 1013.0 || bps > 1013.5))
            {
                ConfirmarCandidatoBPS(direccion, bps);
            }
            else
            {
                bpsCandidatos.Remove(direccion);
                repeticionesBPS.Remove(direccion);
            }

            // Recuperar el último QNH confirmado de este avión
            if (ultimosQNH.TryGetValue(direccion, out double qnh))
            {
                td.qnhUtilizado = qnh;

                td.altitudQNHFt =
                    td.altitudPresionFt + (qnh - 1013.25) * 30.0;
            }
            else
            {
                td.altitudQNHFt = td.altitudPresionFt;
            }
            double altitudEsperada = td.altitudPresionFt;

            if (td.altitudPresionFt < 6000 &&
                !double.IsNaN(td.qnhUtilizado))
            {
                altitudEsperada +=
                    (td.qnhUtilizado - 1013.25) * 30.0;
            }

            bool correcto =
                !double.IsNaN(td.altitudQNHFt) &&
                Math.Abs(td.altitudQNHFt - altitudEsperada) < 0.01;
        }
        private void ConfirmarCandidatoBPS(string direccion, double bps)
        {
            const double toleranciaLecturas = 0.5;
            const int lecturasNecesarias = 3;

            if (bpsCandidatos.TryGetValue(direccion, out double candidato) &&
                Math.Abs(bps - candidato) <= toleranciaLecturas)
            {
                repeticionesBPS[direccion]++;
            }
            else
            {
                bpsCandidatos[direccion] = bps;
                repeticionesBPS[direccion] = 1;
            }

            // Esperar tres lecturas consecutivas similares.
            if (repeticionesBPS[direccion] < lecturasNecesarias)
                return;

            double qnhNuevo = bpsCandidatos[direccion];

            bool existeAnterior =
                ultimosQNH.TryGetValue(direccion, out double qnhAnterior);

            // Actualizar si no existe QNH o si ha cambiado
            // al menos una décima de hPa.
            if (!existeAnterior ||
                Math.Abs(qnhNuevo - qnhAnterior) >= 0.1 - 0.000001)
            {
                ultimosQNH[direccion] = qnhNuevo;
            }

            // Preparados para detectar un cambio posterior.
            bpsCandidatos.Remove(direccion);
            repeticionesBPS.Remove(direccion);
        }
    }
}
