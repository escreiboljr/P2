using DatosDecod;

TiraDatosDecod048 p48 = new TiraDatosDecod048();
List<int> listaBits = new List<int>() { 0, 0, 1, 1, 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2 };
p48.DecDataSourceID(listaBits);
Console.WriteLine(p48.DataSourceID[0] + " " + p48.DataSourceID[1]);
