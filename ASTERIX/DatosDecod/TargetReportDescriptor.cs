using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod
{
    internal class TargetReportDescriptor
    {
        // Primer byte
        public int TYP { get; private set; }
        public bool SIM { get; private set; }
        public bool RDP { get; private set; }
        public bool SPI { get; private set; }
        public bool RAB { get; private set; }

        // Primera extensión.
        // null indica que esa extensión no se ha recibido.
        public bool? TST { get; private set; }
        public bool? ERR { get; private set; }
        public bool? XPP { get; private set; }
        public bool? ME { get; private set; }
        public bool? MI { get; private set; }
        public int? FOE_FRI { get; private set; }

        // Segunda extensión
        public bool? ADSB_EP { get; private set; }
        public bool? ADSB_VAL { get; private set; }
        public bool? SCN_EP { get; private set; }
        public bool? SCN_VAL { get; private set; }
        public bool? PAI_EP { get; private set; }
        public bool? PAI_VAL { get; private set; }

        // Número de bytes que ocupa este ítem
        public int Longitud { get; private set; }

        // Constructor: recibe los datos y la posición inicial del ítem.
        // limiteExclusivo permite limitar la lectura al bloque ASTERIX actual.
        public TargetReportDescriptor(
            byte[] datos,
            ref int posicion,
            int? limiteExclusivo = null)
        {
            if (datos == null)
                throw new System.ArgumentNullException(nameof(datos));

            int limite = limiteExclusivo ?? datos.Length;

            if (limite < 0 || limite > datos.Length)
                throw new System.ArgumentOutOfRangeException(nameof(limiteExclusivo));

            if (posicion < 0 || posicion > limite)
                throw new System.ArgumentOutOfRangeException(nameof(posicion));

            int cursor = posicion;
            int numeroByte = 0;
            byte octeto;

            do
            {
                if (cursor >= limite)
                    throw new System.FormatException(
                        "I048/020 incompleto: falta un byte.");

                octeto = datos[cursor];
                cursor++;
                numeroByte++;

                if (numeroByte == 1)
                {
                    // Bits 8, 7 y 6: tipo de detección
                    TYP = (octeto >> 5) & 0x07;

                    SIM = (octeto & 0x10) != 0; // Bit 5: simulado
                    RDP = (octeto & 0x08) != 0; // Bit 4: cadena 2 si true
                    SPI = (octeto & 0x04) != 0; // Bit 3: SPI presente
                    RAB = (octeto & 0x02) != 0; // Bit 2: monitor fijo
                }
                else if (numeroByte == 2)
                {
                    TST = (octeto & 0x80) != 0; // Bit 8: informe de prueba
                    ERR = (octeto & 0x40) != 0; // Bit 7: alcance extendido
                    XPP = (octeto & 0x20) != 0; // Bit 6: X-Pulse
                    ME = (octeto & 0x10) != 0; // Bit 5: emergencia militar
                    MI = (octeto & 0x08) != 0; // Bit 4: identificación militar

                    FOE_FRI = (octeto >> 1) & 0x03; // Bits 3 y 2
                }
                else if (numeroByte == 3)
                {
                    ADSB_EP = (octeto & 0x80) != 0;
                    ADSB_VAL = (octeto & 0x40) != 0;
                    SCN_EP = (octeto & 0x20) != 0;
                    SCN_VAL = (octeto & 0x10) != 0;
                    PAI_EP = (octeto & 0x08) != 0;
                    PAI_VAL = (octeto & 0x04) != 0;

                    // Bit 2: reservado
                }

                // Las extensiones posteriores no están definidas en la
                // edición 1.31. Se saltan siguiendo FX.
            }
            while ((octeto & 0x01) != 0); // Bit 1: FX

            Longitud = cursor - posicion;
            posicion = cursor;
        }
    }
}
