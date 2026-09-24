using System;
using System.Collections.Generic;
using Archivos;
using Archvios;      // Tu namespace
using DatosDecod;
using DatosDecod21;    // Donde está TiraDatosDecod048

namespace PruebasDeFunciones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ruta del archivo CAT048

            string rutaArchivo = @"C:\Users\user\Desktop\P2\ASTERIX\PruebasDeFunciones\bin\Debug\net10.0\asterix_adsb.ast"; // cámbiala

            // Crear lector
            LeerDatos lector = new LeerDatos();

            // Procesar archivo
            List<Mensaje> mensajes = lector.DatosProcesados(rutaArchivo);

            // Mostrar solo los 10 primeros mensajes
            int maxMensajes = Math.Min(10, mensajes.Count);

            for (int i = 0; i < maxMensajes; i++)
            {
                var msg = mensajes[i];
                var td = msg.tira as TiraDatosDecod021;
                if (td == null)
                    continue;

                Console.WriteLine($"===== MENSAJE CAT048 #{i + 1} =====");

                // Aquí imprime propiedades reales de tu clase TiraDatosDecod048
                // Ajusta los nombres según tu clase

                // Ejemplos típicos (cámbialos por los tuyos):
                // Console.WriteLine($"Data Source ID: {td48.DataSourceID}");
                // Console.WriteLine($"Time Of Day: {td48.TimeOfDay}");
                // Console.WriteLine($"Track Number: {td48.TrackNumber}");
                // Console.WriteLine($"Aircraft Address: {td48.AircraftAddress}");
                // Console.WriteLine($"Aircraft ID: {td48.AircraftID}");
                // Console.WriteLine($"Mode 3/A: {td48.Mode3A}");
                // Console.WriteLine($"Position X: {td48.PosX}");
                // Console.WriteLine($"Position Y: {td48.PosY}");

                Console.WriteLine();
            }

            Console.WriteLine("Fin de la prueba CAT21. Pulsa una tecla para salir.");
            Console.ReadKey();
        }
    }
}
