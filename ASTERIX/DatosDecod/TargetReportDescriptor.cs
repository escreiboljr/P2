using System;
using System.Collections.Generic;
using System.Text;

namespace DatosDecod
{
    public class TargetReportDescriptor
    {
        // Primer byte
        public string TYP { get;  set; }
        public int SIM { get;  set; }
        public int RDP { get;  set; }
        public int SPI { get;  set; }
        public int RAB { get;  set; }

        // Primera extensión.
        // null indica que esa extensión no se ha recibido.
        public int TST { get;  set; }
        public int ERR { get;  set; }
        public int XPP { get;  set; }
        public int ME { get;  set; }
        public int MI { get;  set; }
        public string FOE_FRI { get;  set; }

        // Segunda extensión
        public int ADSB_EP { get;  set; }
        public int ADSB_VAL { get;  set; }
        public int SCN_EP { get;  set; }
        public int SCN_VAL { get;  set; }
        public int PAI_EP { get;  set; }
        public int PAI_VAL { get;  set; }

        // Número de bytes que ocupa este ítem
        public int Longitud { get;  set; }

        public TargetReportDescriptor()
        {
            this.TYP = "";
            this.SIM = -1;
            this.RDP = -1;
            this.SPI = -1;
            this.RAB = -1;
            this.TST = -1;
            this.ERR = -1;
            this.XPP = -1;
            this.ME = -1;
            this.MI = -1;
            this.FOE_FRI = "";
            this.ADSB_EP = -1;
            this.ADSB_VAL = -1;
            this.SCN_EP = -1;
            this.SCN_VAL = -1;
            this.PAI_EP = -1;
            this.PAI_VAL = -1;
        }
    }
}
