using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DatosDecod
{
    public class TiraDatosDecod048
    {
        public List<string> DataSourceID { get; set; }
        public double TimeOfDay { get; set; }
        public int TargetRep { get; set; }
        public List<double> PosSlantPolarCoord { get; set; }
        public int Mode3 { get; set; }
        public int FL {  get; set; }
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
            this.TargetRep = -1;
            this.PosSlantPolarCoord = [0,0];
            this.Mode3 = -1;
            this.FL = -1;
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
        //bcjcbdcbj

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

        public void DecodeMode3A (List<int> listaBits)
        {

        }
    }
}
    