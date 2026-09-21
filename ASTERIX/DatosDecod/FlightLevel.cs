using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class FlightLevel
    {
        public int V {  get; set; }
        public int G { get; set; }
        public double FL { get; set; }

        public FlightLevel()
        {
            this.V = -1;
            this.G = -1;
            this.FL = -1;
        }
    }
}
