using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class Mode3
    {
        public int V { get; set; }
        public int G { get; set; }
        public int L { get; set; }
        public string reply { get; set; }

        public Mode3()  //todo es positivo
        {
            this.V = -1;
            this.G = -1;
            this.L = -1;
            this.reply = "";
        }
    }
}
