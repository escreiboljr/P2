using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod48
{
    public class RadarPlotCharacteristics
    {
        public double SRL { get; set; }
        public int SRR { get; set; }
        public int SAM { get; set; }
        public double PRL { get; set; }
        public int PAM {  get; set; }
        public double RPD { get; set; }
        public double APD { get; set; }

        public RadarPlotCharacteristics()
        {
            this.SRL = -1;
            this.SRR = -1;
            this.SAM = -1;
            this.PRL = -1;
            this.PAM = -1;
            this.RPD = -1;
            this.APD = -1;
        }
    }
}
