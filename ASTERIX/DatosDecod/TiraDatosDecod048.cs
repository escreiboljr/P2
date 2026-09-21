using DatosDecod48;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DatosDecod
{
    public class TiraDatosDecod048
    {
        public List<string> DataSourceID { get; set; }
        public double TimeOfDay { get; set; }
        public TargetReportDescriptor TargetRep { get; set; }
        public List<double> PosSlantPolarCoord { get; set; }
        public Mode3 mode3 { get; set; }
        public FlightLevel FL {  get; set; }
        public int RadarPlot { get; set; }
        public int AircrftAddrs { get; set; }
        public int AircrftIddent { get; set; }
        public int ModeS { get; set; }
        public int TrckVelPolRepr { get; set; }
        public int TrckStatus {  get; set; }
        public int CommACAScapability { get; set; }

        public TiraDatosDecod048()
        {
            this.DataSourceID = ["sac","sic"];
            this.TimeOfDay = -1;
            this.TargetRep = new TargetReportDescriptor();
            this.PosSlantPolarCoord = [0,0];
            this.mode3 = new Mode3();
            this.FL = new FlightLevel();
            this.RadarPlot = -1;
            this.AircrftAddrs = -1;
            this.AircrftIddent = -1;
            this.ModeS = -1;
            this.TrckVelPolRepr = -1;
            this.TrckStatus = -1;
            this.CommACAScapability = -1;
        }

        public static byte BitsToByte(List<int> bits)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                result <<= 1;               // desplaza a la izquierda
                result |= (byte)bits[i];    // añade el bit
            }
            return result;
        }


        public void DecDataSourceID(List<int> listaBits)
        {
            string SAC = string.Join("", listaBits.GetRange(0, 8));
            string SIC = string.Join("",listaBits.GetRange(8, 8));
            this.DataSourceID[0] = SAC;
            this.DataSourceID[1] = SIC;
        }

        public void DecodeTimeOfDayFromBits(List<int> listaBits)
        {
            // Extraer los 3 octetos (24 bits)
            List<int> bits1 = listaBits.GetRange(0, 8);
            List<int> bits2 = listaBits.GetRange(8, 8);
            List<int> bits3 = listaBits.GetRange(16, 8);

            byte byte1 = BitsToByte(bits1);
            byte byte2 = BitsToByte(bits2);
            byte byte3 = BitsToByte(bits3);

            // Unir los 3 bytes en un entero de 24 bits
            int raw = (byte1 << 16) | (byte2 << 8) | byte3;

            // Convertir a segundos desde medianoche
            this.TimeOfDay =  raw / 128.0;
        }

        public void DecodePosSlantPolarCoord(List<int> listaBits)
        {
            List<int> bits1 = listaBits.GetRange(0, 8);   // RHO high byte
            List<int> bits2 = listaBits.GetRange(8, 8);   // RHO low byte
            List<int> bits3 = listaBits.GetRange(16, 8);  // THETA high byte
            List<int> bits4 = listaBits.GetRange(24, 8);  // THETA low byte

            byte b1 = BitsToByte(bits1);
            byte b2 = BitsToByte(bits2);
            byte b3 = BitsToByte(bits3);
            byte b4 = BitsToByte(bits4);

            int rhoRaw = (b1 << 8) | b2;     // RHO = 16 bits
            int thetaRaw = (b3 << 8) | b4;   // THETA = 16 bits


            double rhoNM = rhoRaw / 256.0;                     // RHO en millas náuticas
            double thetaDeg = thetaRaw * (360.0 / 65536.0);    // THETA en grados

            this.PosSlantPolarCoord[0] = rhoNM;
            this.PosSlantPolarCoord[1] = thetaDeg;
        }

        public void DecodeTargetRep(List<int>listaBits)
        {
            string TYP = string.Join("", listaBits.GetRange(0, 3));
            this.TargetRep.TYP = TYP;
            this.TargetRep.SIM = listaBits[3];
            this.TargetRep.RDP = listaBits[4];
            this.TargetRep.SPI = listaBits[5];
            this.TargetRep.RAB = listaBits[6];
            
            if (listaBits[7] == 1)
            {
                this.TargetRep.TST = listaBits[8];
                this.TargetRep.ERR = listaBits[9];
                this.TargetRep.XPP = listaBits[10];
                this.TargetRep.ME = listaBits[11];
                this.TargetRep.MI = listaBits[12];
                string FOE_FRI = string.Join("", listaBits.GetRange(13, 2));
                this.TargetRep.FOE_FRI = FOE_FRI;
                
                if (listaBits[15] == 1)
                {
                    this.TargetRep.ADSB_EP = listaBits[16];
                    this.TargetRep.ADSB_VAL = listaBits[17]; 
                    this.TargetRep.SCN_EP = listaBits[18];
                    this.TargetRep.SCN_VAL = listaBits[19];
                    this.TargetRep.PAI_EP = listaBits[20];
                    this.TargetRep.PAI_VAL = listaBits[21];
                }
            }
        }

        public void DecodeMode3A (List<int> listaBits)
        {
            this.mode3.V = listaBits[0];
            this.mode3.G = listaBits[1];
            this.mode3.L = listaBits[2];
            string reply = string.Join("", listaBits.GetRange(4, 12));
            this.mode3.reply = reply;
        }

        public void DecodeFL(List<int> listaBits)
        {
            this.FL.V = listaBits[0];
            this.FL.G = listaBits[1];

            int i = 0;
            double suma = 0;
            while (i<14)
            {
                if (listaBits[i+2] == 1)
                {
                    int potencia = 13 - i;
                    int valor = 1<<(13-i);
                    suma = suma + valor;
                }
                i ++;
            }
            double FL = suma * 0.25;
            this.FL.FL = FL;
        }
    }
}
    