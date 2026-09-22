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
