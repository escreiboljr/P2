using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class BDS40
    {
        public int MCP { get; private set; }
        public int FMS { get; private set; }
        public int Baro { get; private set; }
        public int VNAV { get; private set; }
        public int AltHold { get; private set; }
        public int App { get; private set; }

        public void Decode(byte[] mb)
        {
            int rawMCP = (mb[1] << 4) | (mb[2] >> 4);
            int rawFMS = ((mb[2] & 0x0F) << 8) | mb[3];

            int rawBaro = mb[4];
            int vnav = (mb[5] >> 7) & 1;
            int altHold = (mb[5] >> 6) & 1;
            int app = (mb[5] >> 5) & 1;

            MCP = rawMCP * 16;
            FMS = rawFMS * 4;
            Baro = rawBaro + 800;
            VNAV = vnav;
            AltHold = altHold;
            App = app;
        }
    }
}
