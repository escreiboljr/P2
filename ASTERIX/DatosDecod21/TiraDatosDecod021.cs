namespace DatosDecod21
{
    public class TiraDatosDecod021
    {
        public List<byte> DataSourceID { get; set; }
        public TargetReportDescriptor TargetRep { get; set; }
        public double ReservedExpField { get; set; }
        public List<double> PosWGS84HighRes { get; set; }
        public string TargetAddress { get; set; }
        public double TimeReceptionPosition { get; set; }
        public string Mode3ACode { get; set; }
        public double FlightLevel { get; set; }
        public string TargetIdentification { get; set; }
        public double QNH { get; set; }
        public double AltitudCorregida { get; set; }

        public static Dictionary<string, double> UltimosQNH =
            new Dictionary<string, double>();

        public TiraDatosDecod021()
         {
            this.DataSourceID = new List<byte> { 0, 0 };
            this.TargetRep = new TargetReportDescriptor();
            this.ReservedExpField = -1;
            this.PosWGS84HighRes = new List<double> { 0, 0 };   //no hace falta tener en cuenta las negativas asique dejamos 0,0 
            this.TargetAddress = null;
            this.TimeReceptionPosition = -1;
            this.Mode3ACode = null;
            this.FlightLevel = double.NaN;
            this.TargetIdentification = null;
            this.QNH = double.NaN;
            this.AltitudCorregida = double.NaN;
        }

        public void DecDataSourceID(Queue<byte> ColaBytes)
        {
            byte SAC = ColaBytes.Dequeue();
            byte SIC = ColaBytes.Dequeue();

            this.DataSourceID[0] = SAC;
            this.DataSourceID[1] = SIC;
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
            this.TargetAddress = direccion.ToString("X6");
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

            codigo = codigo & 0x0FFF;

            // Convertir a octal y completar hasta cuatro cifras
            this.Mode3ACode = Convert.ToString(codigo, 8).PadLeft(4, '0');
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

        public void DecodeTargetReportDescriptor(Queue<byte> data)
        {
            byte b1 = data.Dequeue();
            List<int> listaBits = ByteToBits(b1);

            this.TargetRep.ATP = string.Join("", listaBits.GetRange(0, 3));
            this.TargetRep.ARC = string.Join("", listaBits.GetRange(3, 2));
            this.TargetRep.RC = listaBits[5];
            this.TargetRep.RAB = listaBits[6];
            if (listaBits[7] == 1)
            {
                b1 = data.Dequeue();
                listaBits = ByteToBits(b1);
                this.TargetRep.DCR = listaBits[0];
                this.TargetRep.GBS = listaBits[1];
                this.TargetRep.SIM = listaBits[2];
                this.TargetRep.TST = listaBits[3];
                this.TargetRep.SAA = listaBits[4];
                this.TargetRep.CL = string.Join("", listaBits.GetRange(5, 2));
                if (listaBits[7] == 1)
                {
                    b1 = data.Dequeue();
                    listaBits = ByteToBits(b1);
                    this.TargetRep.IPC = listaBits[2];
                    this.TargetRep.NOGO = listaBits[3];
                    this.TargetRep.CPR = listaBits[4];
                    this.TargetRep.LDPJ = listaBits[5];
                    this.TargetRep.RCF = listaBits[6];
                }

            }
        }
        public void DecodeReservedExp(Queue<byte> cola)
        {
            // Primer byte: longitud total del Reserved Expansion
            byte longitud = cola.Dequeue();
          
            if (longitud < 2 || cola.Count < longitud - 1)
            {
                throw new InvalidOperationException(
                    "Longitud incorrecta del Reserved Expansion");
            }
           
            // Segundo byte: indica los campos presentes
            byte b = cola.Dequeue();
            List<int> listaBits = ByteToBits(b);

            // Primer bit = 1 significa que existe BPS
            if (listaBits[0] == 1)
            {
                byte b1 = cola.Dequeue();
                byte b2 = cola.Dequeue();

                // Los 4 primeros bits son 0
                // Nos quedamos con los 12 bits restantes
                int raw = ((b1 & 0x0F) << 8) | b2;

                this.QNH = (raw * 0.1) + 800.0;

                this.ReservedExpField = this.QNH;
            }

            // Saltar los campos restantes del Reserved Expansion
            int bytesLeidos = listaBits[0] == 1 ? 4 : 2;

            for (int i = bytesLeidos; i < longitud; i++)
            {
                cola.Dequeue();
            }
        }

        public void CorregirAltitudQNH()
        {
            if (double.IsNaN(this.FlightLevel))
            {
                this.AltitudCorregida = double.NaN;
                return;
            }

            double altitudIndicada = this.FlightLevel * 100.0;

            this.AltitudCorregida = altitudIndicada;

            // Comprobar si tenemos una aeronave identificada
            if (string.IsNullOrEmpty(this.TargetAddress))
            {
                return;
            }

            // Comprobar si el QNH recibido es diferente del estándar
            if (!double.IsNaN(this.QNH) &&
                (this.QNH < 1013.0 || this.QNH > 1013.5))
            {
                // Guardar el último QNH válido de esta aeronave
                UltimosQNH[this.TargetAddress] = this.QNH;
            }

            // Solo corregimos por debajo de 6000 ft
            if (altitudIndicada >= 6000)
            {
                return;
            }

            double qnhUtilizado;

            // Recuperar el último QNH no estándar de la aeronave
            if (UltimosQNH.TryGetValue(this.TargetAddress, out qnhUtilizado))
            {
                const double qnhEstandar = 1013.25;

                this.AltitudCorregida =
                    altitudIndicada +
                    (qnhUtilizado - qnhEstandar) * 30.0;
            }
        }
    }   
}
