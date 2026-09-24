using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DatosDecod21
{
    public class TargetReportDescriptor
    {
        public string ATP {  get; set; }
        public string ARC { get; set; }
        public int RC { get; set; }
        public int RAB { get; set; }
        public int DCR { get; set; }
        public int GBS { get; set; }
        public int SIM { get; set; }
        public int TST { get; set; }
        public int SAA { get; set; }
        public string CL { get; set; }

        public int IPC { get; set; }
        public int NOGO { get; set; }
        public int CPR { get; set; }
        public int LDPJ { get; set; }
        public int RCF { get; set; }
    

        public TargetReportDescriptor()
        {
            this.ATP = "";
            this.ARC = "";
            this.RC = -1;
            this.RAB = -1;
            this.DCR = -1;
            this.GBS = -1;
            this.SIM = -1;
            this.TST = -1;
            this.SAA = -1;
            this.CL = "";
            this.IPC = -1;
            this.NOGO = -1;
            this.CPR = -1;
            this.LDPJ = -1;
            this.RCF = -1;
        }
    }
}
