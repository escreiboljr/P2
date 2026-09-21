using DatosDecod;


/*TiraDatosDecod048 p48 = new TiraDatosDecod048();
List<int> listaBits = new List<int>() { 0, 0, 1, 1, 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2 };
p48.DecDataSourceID(listaBits);
Console.WriteLine(p48.DataSourceID[0] + " " + p48.DataSourceID[1]);
*/

using (FileStream fs = new FileStream("asterix_radar.ast", FileMode.Open, FileAccess.Read))
{
    byte[] buffer = new byte[4096]; // 4 KB por bloque
    int leidos;

    while ((leidos = fs.Read(buffer, 0, buffer.Length)) > 0)
    {
        for (int i = 0; i < leidos; i++)
        {
            byte b = buffer[i];
            // Procesar cada byte aquí
        }
    }
}


