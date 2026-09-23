using Archivos;
using DatosDecod;
using DatosDecod21;

namespace Archvios
{
    public class LeerDatos
    {
        //preuba2
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
                    for (int i = 0; i < leidos; i++)
                        cola.Enqueue(buffer[i]);
                }
            }

            while (cola.Count > 0)
            {
                byte categoria = cola.Dequeue();

                byte lenMSB = cola.Dequeue();
                byte lenLSB = cola.Dequeue();
                int longitud = (lenMSB << 8) | lenLSB;

                byte[] mensajeBytes = new byte[longitud - 3];
                for (int i = 0; i < mensajeBytes.Length; i++)
                    mensajeBytes[i] = cola.Dequeue();

                Queue<byte> colaMensaje = new Queue<byte>(mensajeBytes);

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

            return listaTiras;
        }

        private static readonly Dictionary<int, Action<Queue<byte>, TiraDatosDecod048>> decoders =
    new Dictionary<int, Action<Queue<byte>, TiraDatosDecod048>>
    {
        { 1,  (q, td) => td.DecDataSourceID(q) },
        { 2,  (q, td) => td.DecodeTimeOfDayFromBits(q) },
        { 3,  (q, td) => td.DecodeTargetRep(q) },
        { 4,  (q, td) => td.DecodePosSlantPolarCoord(q) },
        { 51, (q, td) => td.DecodeMode3A(q) },
        { 6,  (q, td) => td.DecDataSourceID(q) },
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
        { 16, (q, td) => q.Dequeue() },
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
                    q.Dequeue();

                if (bits[1] == 1)
                {
                    q.Dequeue(); q.Dequeue(); q.Dequeue();
                    q.Dequeue(); q.Dequeue(); q.Dequeue();
                    q.Dequeue();
                }

                if (bits[0] == 1)
                {
                    q.Dequeue();
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
