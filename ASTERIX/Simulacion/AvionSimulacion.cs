namespace Simulacion
{
    public class AvionSimulacion
    {
        public string direccion {  get; set; }
        public string identificador {  get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public double altitud { get; set; }
        public double ultimoTiempo { get; set; }
        public double ultimoTiempoRadar { get; set; }
        public double ultimoTiempoADSB { get; set; }
        public bool detectadoRadar { get; set; }
        public bool detectadoADSB { get; set; }
        public double flightLevel { get; set; }
        public int trackNumber { get; set; }

        public AvionSimulacion()
        {
            identificador = "";
            latitud = 0;
            longitud = 0;
            altitud = 0;

            ultimoTiempo = 0;
            ultimoTiempoRadar = -1;
            ultimoTiempoADSB = -1;

            detectadoRadar = false;
            detectadoADSB = false;
            flightLevel = double.NaN;
            trackNumber = -1;
        }
    }
}
