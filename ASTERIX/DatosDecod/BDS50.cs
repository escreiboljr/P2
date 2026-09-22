using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class BDS50
    {
        public double Roll { get; private set; }
        public double Track { get; private set; }
        public int GroundSpeed { get; private set; }
        public double TrackRate { get; private set; }
        public int TrueAirspeed { get; private set; }

        public void Decode(byte[] mb)
        {
            int rawRoll = (mb[1] << 2) | (mb[2] >> 6);
            rawRoll = BDS.TwosComplement(rawRoll, 10);

            int rawTrack = ((mb[2] & 0x3F) << 4) | (mb[3] >> 4);
            int rawGS = ((mb[3] & 0x0F) << 8) | mb[4];

            int rawTAR = (mb[5] << 2) | (mb[6] >> 6);
            rawTAR = BDS.TwosComplement(rawTAR, 10);

            int rawTAS = ((mb[6] & 0x3F) << 4) | (mb[7] >> 4);

            Roll = rawRoll * 0.01;
            Track = rawTrack * 0.01;
            GroundSpeed = rawGS;
            TrackRate = rawTAR * 0.01;
            TrueAirspeed = rawTAS;
        }
    }
}
