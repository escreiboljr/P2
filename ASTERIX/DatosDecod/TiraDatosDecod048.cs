using DatosDecod48;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DatosDecod
{
    public class TiraDatosDecod048
    {
        public List<byte> DataSourceID { get; set; }
        public double TimeOfDay { get; set; }
        public TargetReportDescriptor TargetRep { get; set; }
        public List<double> PosSlantPolarCoord { get; set; }
        public List<double> PositionCorrectedLatLonAlt { get; set; }
        public Mode3 mode3 { get; set; }
        public FlightLevel FL {  get; set; }
        public RadarPlotCharacteristics RadarPlot { get; set; }
        public int AircrftAddrs { get; set; }
        public string AircrftIddent { get; set; }
        public class ModeSData
        {
            public BDS40? BDS40 { get; set; }
            public BDS50? BDS50 { get; set; }
            public BDS60? BDS60 { get; set; }
        }
        public ModeSData ModeS { get; set; }
        public int TrackNum { get; set; }
        public List<double> TrckVelPolRepr { get; set; }
        public TrackStatus TrckStatus {  get; set; }
        public ACAScap CommACAScapability { get; set; }

        public TiraDatosDecod048()
        {
            this.DataSourceID = new List<byte> { 0, 0 };
            this.TimeOfDay = -1;
            this.TargetRep = new TargetReportDescriptor();
            this.PosSlantPolarCoord = new List<double> { 0, 0 };
            this.PositionCorrectedLatLonAlt = new List<double> { 0, 0, 0 };
            this.mode3 = new Mode3();
            this.FL = new FlightLevel();
            this.RadarPlot = new RadarPlotCharacteristics();
            this.AircrftAddrs = -1;
            this.AircrftIddent = "";
            this.ModeS = new ModeSData();
            this.TrackNum = -1;
            this.TrckVelPolRepr = new List<double> { 0, 0 };
            this.TrckStatus = new TrackStatus();
            this.CommACAScapability = new ACAScap();
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

        public static List<int> ByteToBits(byte b)
        {
            List<int> bits = new List<int>(8);

            for (int i = 7; i >= 0; i--)
            {
                int bit = (b >> i) & 1;
                bits.Add(bit);
            }
            return bits;
        }

        public List<int> JoinBytesToBits(List<byte> listaBytes)
        {
            List<int> resultado = new List<int>();

            foreach (byte b in listaBytes)
            {
                List<int> bits = ByteToBits(b);   // tu función existente
                resultado.AddRange(bits);         // concatenar los 8 bits
            }

            return resultado;  // devuelve todos los bits juntos
        }

        public void DecDataSourceID(Queue<byte> ColaBytes)
        {
            byte SAC = ColaBytes.Dequeue();
            byte SIC = ColaBytes.Dequeue();

            this.DataSourceID[0] = SAC;
            this.DataSourceID[1] = SIC;
        }

        public void DecodeTimeOfDayFromBits(Queue<byte> ColaBytes)
        {
            byte byte1 = ColaBytes.Dequeue();
            byte byte2 = ColaBytes.Dequeue();
            byte byte3 = ColaBytes.Dequeue();

            // Unir los 3 bytes en un entero de 24 bits
            int raw = (byte1 << 16) | (byte2 << 8) | byte3;

            // Convertir a segundos desde medianoche
            this.TimeOfDay =  raw / 128.0;
        }

        public void DecodePosSlantPolarCoord(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();
            byte b4 = ColaBytes.Dequeue();

            int rhoRaw = (b1 << 8) | b2;     // RHO = 16 bits
            int thetaRaw = (b3 << 8) | b4;   // THETA = 16 bits

            double rhoNM = rhoRaw / 256.0;                     // RHO en millas náuticas
            double thetaDeg = thetaRaw * (360.0 / 65536.0);    // THETA en grados

            this.PosSlantPolarCoord[0] = rhoNM;
            this.PosSlantPolarCoord[1] = thetaDeg;
        }

        public void ConvertToWGS84(double radarLat, double radarLon)
        {
            double rhoNM = this.PosSlantPolarCoord[0];
            double thetaDeg = this.PosSlantPolarCoord[1];

            double rho_m = rhoNM * 1852.0;
            double theta_rad = thetaDeg * Math.PI / 180.0;

            double X = rho_m * Math.Sin(theta_rad);
            double Y = rho_m * Math.Cos(theta_rad);

            double R = 6378137.0;
            double lat = radarLat + (Y / R) * (180.0 / Math.PI);
            double lon = radarLon + (X / (R * Math.Cos(radarLat * Math.PI / 180.0))) * (180.0 / Math.PI);

            this.PositionCorrectedLatLonAlt[0] = lat;
            this.PositionCorrectedLatLonAlt[1] = lon;
        }

        public void DecodeTargetRep(Queue<byte> ColaBytes)    //preguntar lo del third extent
        {
            byte b = ColaBytes.Dequeue();
            List<int> listaBits = ByteToBits(b);
            this.TargetRep.TYP = string.Join("", listaBits.GetRange(0, 3));
            this.TargetRep.SIM = listaBits[3];
            this.TargetRep.RDP = listaBits[4];
            this.TargetRep.SPI = listaBits[5];
            this.TargetRep.RAB = listaBits[6];
            if (listaBits[7] == 1)
            {
                b = ColaBytes.Dequeue();
                listaBits = ByteToBits(b);
                this.TargetRep.TST = listaBits[0];
                this.TargetRep.ERR = listaBits[1];
                this.TargetRep.XPP = listaBits[2];
                this.TargetRep.ME = listaBits[3];
                this.TargetRep.MI = listaBits[4];
                this.TargetRep.FOE_FRI = string.Join("", listaBits.GetRange(5, 2));
                if (listaBits[7] == 1)
                {
                    b = ColaBytes.Dequeue();
                    listaBits = ByteToBits(b);
                    this.TargetRep.ADSB_EP = listaBits[0];
                    this.TargetRep.ADSB_VAL = listaBits[1];
                    this.TargetRep.SCN_EP = listaBits[2];
                    this.TargetRep.SCN_VAL = listaBits[3];
                    this.TargetRep.PAI_EP = listaBits[4];
                    this.TargetRep.PAI_VAL = listaBits[5];
                }
            }
        }

        public void DecodeMode3A (Queue<byte> ColaBytes)
        {
            byte byte1 = ColaBytes.Dequeue();               // leer los 2 bytes de la cola
            byte byte2 = ColaBytes.Dequeue();

            List<byte> listaByte = new List<byte> { byte1, byte2};     // Juntar los bytes en una lista de bits
            List<int> listaBits = JoinBytesToBits(listaByte);

            this.mode3.V = listaBits[0];
            this.mode3.G = listaBits[1];
            this.mode3.L = listaBits[2];
            string reply = string.Join("", listaBits.GetRange(4, 12));
            this.mode3.reply = reply;
        }

        public void DecodeFL(Queue<byte> ColaBytes)
        {
            byte byte1 = ColaBytes.Dequeue();               // leer los 2 bytes de la cola
            byte byte2 = ColaBytes.Dequeue();

            List<byte> listaByte = new List<byte> { byte1, byte2 };     // Juntar los bytes en una lista de bits
            List<int> listaBits = JoinBytesToBits(listaByte);

            this.FL.V = listaBits[0];
            this.FL.G = listaBits[1];

            int i = 0;
            double suma = 0;
            while (i<14)
            {
                if (listaBits[i+2] == 1)
                {
                    int potencia = 13 - i;
                    int valor = 1<<(potencia);
                    suma = suma + valor;
                }
                i ++;
            }
            double FL = suma * 0.25;
            this.FL.FL = FL;

            //a partir de aqui es la correccion, hay que revisar q este bien

            double altitudFt = FL * 100.0;
            double qnhActual = 1013.25;
            double ultimoQNH = 1013.25;

            if (altitudFt >= 6000)
            {
                this.PositionCorrectedLatLonAlt[2] = altitudFt;
                return;
            }
            if (qnhActual >= 1013.0 && qnhActual <= 1013.5)
            {
                this.PositionCorrectedLatLonAlt[2] = altitudFt;
                return;
            }
            bool pareceSTD = (altitudFt % 100 == 0);
            if (pareceSTD)
            {
                double corr = (ultimoQNH - 1013.25) * 30.0;
                this.PositionCorrectedLatLonAlt[2] = altitudFt + corr;
                return;
            }
            double correccion = (qnhActual - 1013.25) * 30.0;
            this.PositionCorrectedLatLonAlt[2] = altitudFt + correccion;

        }
        private int TwosComplementToInt(byte b)    //esta funcion es para el decodeRadarPlot
        {
            // Si el MSB está a 1 → número negativo
            if ((b & 0x80) != 0)
                return b - 256;   // ejemplo: 0xF0 → -16

            return b;             // positivo normal
        }

        public void DecodeRadarPlot(Queue<byte> ColaBytes)
        {
            byte PrimerOcteto = ColaBytes.Dequeue();
            List<int> listaBitsPrimerOcteto = ByteToBits(PrimerOcteto);

            if (listaBitsPrimerOcteto[0] == 1)
            {
                this.RadarPlot.SRL = ColaBytes.Dequeue() * 0.044;
            }
            if (listaBitsPrimerOcteto[1] == 1)
            {
                this.RadarPlot.SRR = ColaBytes.Dequeue() * 1;
            }
            if (listaBitsPrimerOcteto[2] == 1)
            {
                byte raw = ColaBytes.Dequeue();
                int signed = TwosComplementToInt(raw);
                this.RadarPlot.SAM = signed * 1;
            }
            if (listaBitsPrimerOcteto[3] == 1)
            {
                this.RadarPlot.PRL = ColaBytes.Dequeue() * 0.044;
            }
            if (listaBitsPrimerOcteto[4] == 1)
            {
                byte raw = ColaBytes.Dequeue();
                int signed = TwosComplementToInt(raw);
                this.RadarPlot.PAM = signed * 1;
            }
            if (listaBitsPrimerOcteto[5] == 1)
            {
                byte raw = ColaBytes.Dequeue();
                int signed = TwosComplementToInt(raw);
                this.RadarPlot.RPD = signed * (1.0 / 256.0);
            }
            if (listaBitsPrimerOcteto[6] == 1)
            {
                byte raw = ColaBytes.Dequeue();
                int signed = TwosComplementToInt(raw);
                this.RadarPlot.APD = signed * (360.0 / Math.Pow(2, 14));
            }
        }

        public void DecodeAircraftAddress(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();

            int raw = (b1 << 16) | (b2 << 8) | b3;
            this.AircrftAddrs = raw;
        }

        private char DecodeCharICAO(int v)
        {
            if (v == 0) return ' ';
            if (v >= 1 && v <= 26) return (char)('A' + (v - 1));
            if (v >= 48 && v <= 57) return (char)('0' + (v - 48));
            return ' ';
        }

        public void DecodeAircraftID(Queue<byte> cola)
        {
            List<byte> bytes = new List<byte>();      //lee los 6 bytes
            for (int i = 0; i < 6; i++)
            {
                bytes.Add(cola.Dequeue());
            }

            List<int> bits = JoinBytesToBits(bytes);     //los une en la lista en forma de bits

            string result = "";
            for (int i = 0; i < 8; i++)
            {
                int start = i * 6;
                int value = 0;

                for (int b = 0; b < 6; b++)
                {
                    value = (value << 1) | bits[start + b];
                }

                result += DecodeCharICAO(value);
            }

            this.AircrftIddent = result.Trim();
        }

        public void DecodeI048_250(Queue<byte> cola)
        {
            byte[] mb = new byte[9];
            for (int i = 0; i < 9; i++)
                mb[i] = cola.Dequeue();

            byte bds = mb[8];
            int major = (bds >> 4) & 0x0F;
            int minor = bds & 0x0F;

            if (major == 4 && minor == 0)
            {
                this.ModeS.BDS40 = new BDS40();
                this.ModeS.BDS40.Decode(mb);
            }
            else if (major == 5 && minor == 0)
            {
                this.ModeS.BDS50 = new BDS50();
                this.ModeS.BDS50.Decode(mb);
            }
            else if (major == 6 && minor == 0)
            {
                this.ModeS.BDS60 = new BDS60();
                this.ModeS.BDS60.Decode(mb);
            }
        }

        public void DecodeTrack(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();

            int trackNumber = (b1 << 8) | b2;

            this.TrackNum = trackNumber;   
        }

        public void DecodeTrackVelocityPolar(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();
            byte b4 = ColaBytes.Dequeue();

            // Ground Speed (signed 16 bits)
            int gsRaw = (b1 << 8) | b2;
            if ((gsRaw & 0x8000) != 0)
                gsRaw -= 65536;

            double groundSpeed = gsRaw / 128.0;

            // Heading Rate (signed 16 bits)
            int hrRaw = (b3 << 8) | b4;
            if ((hrRaw & 0x8000) != 0)
                hrRaw -= 65536;

            double headingRate = hrRaw * (360.0 / 65536.0);

            this.TrckVelPolRepr[0] = groundSpeed;
            this.TrckVelPolRepr[1] = headingRate;
        }

        public void DecodeTrackStatus(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            List<int> listaBits = ByteToBits(b1);

            this.TrckStatus.CNF = listaBits[0];         // Primer octeto
            this.TrckStatus.RAD = string.Join("", listaBits.GetRange(1, 2)); 
            this.TrckStatus.DOU = listaBits[3];
            this.TrckStatus.MAH = listaBits[4];
            this.TrckStatus.CDM = string.Join("", listaBits.GetRange(5, 2));

            if (listaBits[7] == 1)          // FX del primer octeto
            {
                b1 = ColaBytes.Dequeue();
                listaBits = ByteToBits(b1);
                this.TrckStatus.TRE = listaBits[0];          // Segundo octeto
                this.TrckStatus.GHO = listaBits[1];
                this.TrckStatus.SUP = listaBits[2];
                this.TrckStatus.TCC = listaBits[3];
            }
        }

        public void DecodeCommACAScapability(Queue<byte> ColaBytes)
        {
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();

            List<byte> list = new List<byte> { b1,b2};
            List<int> listaBits = JoinBytesToBits(list);

            this.CommACAScapability.COM = string.Join("", listaBits.GetRange(0, 3));
            this.CommACAScapability.STAT = string.Join("", listaBits.GetRange(3, 3));
            this.CommACAScapability.SI = listaBits[6];
            this.CommACAScapability.MSSC = listaBits[8];
            this.CommACAScapability.ARC = listaBits[9];
            this.CommACAScapability.AIC = listaBits[10];
            this.CommACAScapability.B1A = listaBits[11];
            this.CommACAScapability.B1B = string.Join("", listaBits.GetRange(12, 4));
        }
    }
}
    