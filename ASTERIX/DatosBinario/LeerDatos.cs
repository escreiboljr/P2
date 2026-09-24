using Archivos;
using DatosDecod;
using DatosDecod21;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archvios
{
    public class LeerDatos
    {
        public List<Mensaje> DatosProcesados(string nombreArchivo)
        {
            List<Mensaje> listaTiras = new List<Mensaje>();
            Queue<byte> cola = new Queue<byte>();

            using (FileStream fs = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[4096];
                int leidos;

                while ((leidos = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Añadir bloque a la cola principal
                    for (int i = 0; i < leidos; i++)
                        cola.Enqueue(buffer[i]);

                    // Intentar procesar mensajes mientras haya suficientes bytes
                    ProcesarMensajes(cola, listaTiras);
                }
            }

            // Procesar lo que quede al final
            ProcesarMensajes(cola, listaTiras);

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
                listaTiras.Add(new Mensaje { categoria = categoria, tira = tira48 });
            }
            else if (categoria == 21)
            {
                var tira21 = DecodificarCAT021(colaMensaje);
                listaTiras.Add(new Mensaje { categoria = categoria, tira = tira21 });
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
        { 4,  (q, td) => td.DecodePosSlantPolarCoord(q) },
        { 5, (q, td) => td.DecodeMode3A(q) },
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
                    break;   // ya no hay nada más que decodificar
                }
            }

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
    }
}
