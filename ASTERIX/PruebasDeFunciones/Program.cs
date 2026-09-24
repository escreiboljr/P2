using Archivos;
using Archvios;

LeerDatos lector = new LeerDatos();
List<Mensaje> mensajes = lector.DatosProcesados("asterix_combinado.ast");

// Mostrar los primeros 10
for (int i = 0; i <mensajes.Count; i++)
{
    var msg = mensajes[i];
    Console.WriteLine($"CAT{msg.categoria}");
}
