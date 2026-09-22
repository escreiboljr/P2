using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DatosDecod48
{
    public class TrackStatus
    {
        public int CNF {  get; set; }
        public string RAD { get; set; }
        public int DOU { get; set; }
        public int MAH { get; set; }
        public string CDM { get; set; }
        public int TRE { get; set; }
        public int GHO { get; set; }
        public int SUP { get; set; }
        public int TCC { get; set; }

        public TrackStatus()
        {
            this.CNF = -1;
            this.RAD = "";
            this.DOU = -1;
            this.MAH = -1;
            this.CDM = "";
            this.TRE = -1;
            this.GHO = -1;
            this.SUP = -1;
            this.TCC = -1;
        }
    }
}
