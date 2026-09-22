using Archivos;
using DatosDecod;
using DatosDecod21;

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

        public TiraDatosDecod048 DecodificarCAT048(Queue<byte> listaBytes)
        {
            TiraDatosDecod048 td = new TiraDatosDecod048();
            List<int> listaFRN = SacarFRNpresentes(listaBytes);
            int i = 0;

            if (listaFRN[i]==1)
            {
                td.DecDataSourceID(listaBytes);
                i ++;
            }
            if (listaFRN[i] == 2)
            {
                td.DecodeTimeOfDayFromBits(listaBytes);
                i++;
            }
            if (listaFRN[i] == 3)
            {
                td.DecodeTargetRep(listaBytes);
                i++;
            }
            if (listaFRN[i] == 4)
            {
                td.DecodePosSlantPolarCoord(listaBytes);
                i++;
            }
            if (listaFRN[i] ==51)
            {
                td.DecodeMode3A(listaBytes);
                i++;
            }
            if (listaFRN[i] == 6)
            {
                td.DecDataSourceID(listaBytes);
                i++;
            }
            if (listaFRN[i] == 7)
            {
                td.DecodeRadarPlot(listaBytes);
                i++;
            }
            if (listaFRN[i] == 8)
            {
                td.DecodeAircraftAddress(listaBytes);
                i++;
            }
            if (listaFRN[i] == 9)
            {
                td.DecodeAircraftID(listaBytes);
                i++;
            }
            if (listaFRN[i] == 10)
            {
                td.DecodeI048_250(listaBytes);
                i++;
            }
            if (listaFRN[i] == 11)
            {
                td.DecodeTrack(listaBytes);
                i++;
            }
            if (listaFRN[i] == 12)
            {
                for (int j = 0; j < 4; j++)
                    { listaBytes.Dequeue(); }
                i++;
            }
            if (listaFRN[i] == 13)
            {
                td.DecodeTrackVelocityPolar(listaBytes);
                i++;
            }
            if (listaFRN[i] == 14)
            {
                td.DecodeTrackStatus(listaBytes);
                i++;
            }
            if (listaFRN[i] == 15)
            {
                for (int j = 0; j < 4; j++)
                { listaBytes.Dequeue(); }
                i++;
            }
            if (listaFRN[i] == 16)    
            {
                listaBytes.Dequeue();
                i++;
            }
            if (listaFRN[i] == 17)
            {
                for (int j = 0; j < 2; j++)
                    { listaBytes.Dequeue(); }
                i++;
            }
            if (listaFRN[i] == 18)
            {
                for (int j = 0; j < 4; j++)
                    { listaBytes.Dequeue(); }
                i++;
            }
            if (listaFRN[i] == 19)
            {
                for (int j = 0; j < 2; j++)
                   { listaBytes.Dequeue(); }
                i++;
            }
            if (listaFRN[i] == 20)
            {
                byte octInfo = listaBytes.Dequeue();
                List<int> listaBits = TiraDatosDecod048.ByteToBits(octInfo);
                if (listaBits[7] == 1)
                {
                    listaBytes.Dequeue();
                }
                if (listaBits[1] == 1)
                {
                    for (int j = 0; j < 7; j++)
                        listaBytes.Dequeue();
                }
                if (listaBits[0]==1)
                {
                    listaBytes.Dequeue();
                    listaBytes.Dequeue();
                }
                i++;
            }
            if (listaFRN[i]==21)
            {
                td.DecodeCommACAScapability(listaBytes);
            }
            foreach(byte b in listaBytes)
            {
                listaBytes.Dequeue();
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
