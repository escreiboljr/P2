using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class BDS60
    {
        public double Heading { get; private set; }
        public int IAS { get; private set; }
        public double Mach { get; private set; }
        public int BaroRate { get; private set; }
        public int InertialVS { get; private set; }

        public void Decode(byte[] mb)
        {
            int rawHeading = ((mb[1] << 3) | (mb[2] >> 5)) & 0x1FF;
            int rawIAS = ((mb[2] & 0x1F) << 6) | (mb[3] >> 2);
            int rawMach = ((mb[3] & 0x03) << 8) | mb[4];

            int rawBaro = (mb[5] << 3) | (mb[6] >> 5);
            rawBaro = BDS.TwosComplement(rawBaro, 11);

            int rawIVS = ((mb[6] & 0x1F) << 6) | (mb[7] >> 2);
            rawIVS = BDS.TwosComplement(rawIVS, 11);

            Heading = rawHeading * 0.703125;
            IAS = rawIAS;
            Mach = rawMach * 0.001;
            BaroRate = rawBaro * 32;
            InertialVS = rawIVS * 32;
        }
    }
}
