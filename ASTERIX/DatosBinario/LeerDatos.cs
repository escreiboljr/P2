namespace Archvios
{
    public class LeerDatos
    {
        public Queue<byte> ColaBytes(string nombreArchivo)
        {
            Queue<byte> cola = new Queue<byte>();

            using (FileStream fs = new FileStream(nombreArchivo, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[4096];
                int leidos;

                while ((leidos = fs.Read(buffer,0,buffer.Length))>0)
                {
                    for (int i = 0; i<leidos; i++)
                    {
                        cola.Enqueue(buffer[i]);
                    }
                }
            }
            return cola;
        }
    }
}
