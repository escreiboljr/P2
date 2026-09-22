using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    internal class BDS
    {
            public static int TwosComplement(int value, int bits)
        {
            int signBit = 1 << (bits - 1);
            if ((value & signBit) != 0)
                return value - (1 << bits);

            return value;
        }
    }
}
