namespace DatosDecod21
{
    public class TiraDatosDecod021
    {
         public int DataSourceID { get; set; }
         public TargetReportDescriptor TargetRep { get; set; }
         public double ReservedExpField { get; set; }
         public List<double> PosWGS84HighRes { get; set; }
         public string TargetAddress { get; set; }
         public double TimeReceptionPosition { get; set; }
         public string Mode3ACode { get; set; }
         public double FlightLevel { get; set; }
         public string TargetIdentification { get; set; }

        public TiraDatosDecod021()
         {
            this.DataSourceID = -1;
            this.TargetRep = new TargetReportDescriptor();
            this.ReservedExpField = -1;
            this.PosWGS84HighRes = new List<double> { 0, 0 };
            this.TargetAddress = "";
            this.TimeReceptionPosition = -1;
            this.Mode3ACode = "";
            this.FlightLevel = double.NaN;
            this.TargetIdentification = "";
        }


        public void DecodePosWGS84HighRes(Queue<byte> ColaBytes)
        {
            // Leer los cuatro bytes de la latitud
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();
            byte b4 = ColaBytes.Dequeue();

            // Leer los cuatro bytes de la longitud
            byte b5 = ColaBytes.Dequeue();
            byte b6 = ColaBytes.Dequeue();
            byte b7 = ColaBytes.Dequeue();
            byte b8 = ColaBytes.Dequeue();

            // Unir cada grupo en un entero de 32 bits con signo
            int latRaw = (b1 << 24) | (b2 << 16) | (b3 << 8) | b4;
            int lonRaw = (b5 << 24) | (b6 << 16) | (b7 << 8) | b8;

            // Convertir a grados con la escala indicada por EUROCONTROL
            double factor = 180.0 / 1073741824.0; // 180 / 2^30

            this.PosWGS84HighRes[0] = latRaw * factor;
            this.PosWGS84HighRes[1] = lonRaw * factor;
        }

        public void DecodeTargetAddress(Queue<byte> ColaBytes)
        {
            // Leer los tres bytes de la dirección
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();

            // Unirlos en un número de 24 bits
            int direccion = (b1 << 16) | (b2 << 8) | b3;

            // Guardarlo como texto hexadecimal de seis caracteres
            this.TargetAddress = direccion.ToString();
        }

        public void DecodeTimeReceptionPosition(Queue<byte> ColaBytes)
        {
            // Leer los tres bytes.
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();
            byte b3 = ColaBytes.Dequeue();

            // Unirlos en un número de 24 bits.
            int raw = (b1 << 16) | (b2 << 8) | b3;

            // Convertir a segundos desde medianoche UTC.
            this.TimeReceptionPosition = raw / 128.0;
        }

        public void DecodeMode3A(Queue<byte> ColaBytes)
        {
            // Leer los dos bytes
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();

            // Unirlos y conservar solo los 12 bits del código
            int codigo = ((b1 << 8) | b2);

            // Convertir a octal y completar hasta cuatro cifras
            this.Mode3ACode = Convert.ToString(codigo);
        }

        public void DecodeFlightLevel(Queue<byte> ColaBytes)
        {
           
            // Leer los dos bytes
            byte b1 = ColaBytes.Dequeue();
            byte b2 = ColaBytes.Dequeue();

            // Unirlos en un número de 16 bits
            int raw = (b1 << 8) | b2;

            // Interpretar el signo: complemento a dos de 16 bits
            if ((raw & 0x8000) != 0)
                raw -= 65536;

            // Convertir a nivel de vuelo
            this.FlightLevel = raw * 0.25;
        }
        private char DecodeCharICAO(int v)
        {
            if (v == 0) return ' ';
            if (v >= 1 && v <= 26) return (char)('A' + (v - 1));
            if (v >= 48 && v <= 57) return (char)('0' + (v - 48));
            return ' ';
        }

        public void DecodeTargetIdentification(Queue<byte> cola)
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

            this.TargetIdentification = result.Trim();
        }

        public List<int> ByteToBits(byte b)
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

        public void DecodeBPS(Queue<byte> data)
        {
            byte b1 = data.Dequeue();
            List<int> bits = ByteToBits(b1);
            if (bits[0] == 1)
            {
                byte oct1 = data.Dequeue();
                byte oct2 = data.Dequeue();
                int raw = (oct1 << 8) | oct2;
                double presion = raw * 0.1;
            }
        }

        public void DecodeTargetReportDescriptor(Queue<byte> data)
        {
            byte b1 = data.Dequeue();
            byte b2 = data.Dequeue();
            byte b3 = data.Dequeue();

            List<byte> listaB = new List<byte> { b1,b2,b3};
            List<int> listaBits = JoinBytesToBits(listaB);

            this.TargetRep.ATP = string.Join("", listaBits.GetRange(0, 3));
            this.TargetRep.ARC = string.Join("", listaBits.GetRange(3, 2));
            this.TargetRep.RC = listaBits[5];
            this.TargetRep.RAB = listaBits[6];
            if (listaBits[7]==1)
            {
                this.TargetRep.DCR = listaBits[8];
                this.TargetRep.GBS = listaBits[9];
                this.TargetRep.SIM = listaBits[10];
                this.TargetRep.TST = listaBits[11];
                this.TargetRep.SAA = listaBits[12];
                this.TargetRep.CL = string.Join("", listaBits.GetRange(13, 2 ));
            }
        }
    }   
}
