using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class ACAScap
    {
        public string COM {  get; set; }
        public string STAT { get; set; }
        public int SI { get; set; }
        public int MSSC { get; set; }
        public int ARC { get; set; }
        public int AIC { get; set; }
        public int B1A { get; set; }
        public string B1B { get; set; }

        public ACAScap()
        {
            this.COM = "";
            this.STAT = "";
            this.SI = -1;
            this.MSSC = -1;
            this.ARC = -1;
            this.AIC = -1;
            this.B1A = -1;
            this.B1B = "";
        }
    }
}
