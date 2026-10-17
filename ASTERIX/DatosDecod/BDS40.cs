
using System;

namespace DatosDecod48
{
    public class BDS40
    {
        public int MCP { get; private set; }
        public int FMS { get; private set; }
        public double Baro { get; private set; } = double.NaN;
        public int VNAV { get; private set; }
        public int AltHold { get; private set; }
        public int App { get; private set; }
        private int LeerBits(byte[] mb, int inicio, int cantidad)
        {
            int valor = 0;

            for (int i = 0; i < cantidad; i++)
            {
                int posicion = inicio - 1 + i;
                int indiceByte = posicion / 8;
                int indiceBit = 7 - (posicion % 8);

                int bit = (mb[indiceByte] >> indiceBit) & 1;
                valor = (valor << 1) | bit;
            }

            return valor;
        }

        public void Decode(byte[] mb)
        {
            if (mb == null || mb.Length < 7)
                throw new ArgumentException("BDS40 requiere 7 bytes MB.");

            // MCP: estado bit 1, valor bits 2-13
            bool mcpValido = LeerBits(mb, 1, 1) == 1;
            MCP = mcpValido ? LeerBits(mb, 2, 12) * 16 : 0;

            // FMS: estado bit 14, valor bits 15-26
            bool fmsValido = LeerBits(mb, 14, 1) == 1;
            FMS = fmsValido ? LeerBits(mb, 15, 12) * 16 : 0;

            // BPS: estado bit 27, valor bits 28-39
            bool baroValido = LeerBits(mb, 27, 1) == 1;

            if (baroValido)
            {
                int rawBaro = LeerBits(mb, 28, 12);
                Baro = 800.0 + rawBaro * 0.1;
            }
            else
            {
                Baro = double.NaN;
            }

            // Modos: estado bit 40, valores bits 41-43
            bool modosValidos = LeerBits(mb, 40, 1) == 1;

            VNAV = modosValidos ? LeerBits(mb, 41, 1) : 0;
            AltHold = modosValidos ? LeerBits(mb, 42, 1) : 0;
            App = modosValidos ? LeerBits(mb, 43, 1) : 0;
        }
    }
}
