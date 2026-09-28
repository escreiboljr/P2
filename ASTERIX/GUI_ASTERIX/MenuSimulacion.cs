using Archivos;
using Archvios;
using DatosDecod;
using DatosDecod21;
using DatosDecod48;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Microsoft.VisualBasic;
using Simulacion;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;

namespace GUI_ASTERIX
{
    public partial class MenuSimulacion : Form
    {
        private double TiempoActual = 0;
        private List<Mensaje> ListaMensajes = new List<Mensaje>();
        private List<Mensaje> ListaMensajesFiltrados = new List<Mensaje>();
        //private List<Mensaje> AvionesActuales = new List<Mensaje>();
        private List<AvionSimulacion> AvionesActuales = new List<AvionSimulacion>();
        private Stack<List<AvionSimulacion>> historialAviones = new Stack<List<AvionSimulacion>>();
        private Stack<double> historialTiempo = new Stack<double>();
        private bool Reproduciendo = false;
        private GMapOverlay capaAviones;
        public MenuSimulacion()
        {
            InitializeComponent();
            timerSimulacion.Interval = 1000;
            gMapControl1.OnMarkerClick += GMapControl1_OnMarkerClick;
        }

        private void MenuSimulacion_Load(object sender, EventArgs e)
        {
            //-----------parte del datagrid.-------------------------------
            dataGridAviones.Columns.Clear();

            dataGridAviones.Columns.Add("direccion", "Dirección");
            dataGridAviones.Columns.Add("identificador", "Identificador");
            dataGridAviones.Columns.Add("latitud", "Latitud");
            dataGridAviones.Columns.Add("longitud", "Longitud");
            dataGridAviones.Columns.Add("fl", "FL");
            dataGridAviones.Columns.Add("altitud", "Altitud ft");
            dataGridAviones.Columns.Add("sistema", "Sistema");
            dataGridAviones.Columns.Add("ultimoTiempo", "Último mensaje");

            dataGridAviones.AllowUserToAddRows = false;
            dataGridAviones.ReadOnly = true;
            dataGridAviones.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            //---------------parte del mapañ.........................
            GMapProvider.UserAgent = "PGTA-ASTERIX-Simulator/1.0";

            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMapControl1.MapProvider = OpenStreetMapProvider.Instance;

            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.ShowCenter = false;

            // Zona de interés con un poco de margen
            RectLatLng zona = RectLatLng.FromLTRB(
                1.4,    // Oeste
                41.8,   // Norte
                2.7,    // Este
                40.8    // Sur
            );

            gMapControl1.SetZoomToFitRect(zona);

            capaAviones = new GMapOverlay("aviones");
            gMapControl1.Overlays.Add(capaAviones);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Reproduciendo = true;
            timerSimulacion.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Reproduciendo = false;
            timerSimulacion.Stop();
        }

        private void timerSimulacion_Tick(object sender, EventArgs e)
        {
            historialAviones.Push(CopiarAviones());
            historialTiempo.Push(TiempoActual);
            AvanzarUnSegundo();
        }

        private void AvanzarUnSegundo()
        {
            historialAviones.Push(CopiarAviones());
            historialTiempo.Push(TiempoActual);
            TiempoActual = TiempoActual + 1;
            label1.Text = TiempoActual.ToString();
            ActualizarAviones();
            ActualizarDataGrid();
            ActualizarMapa();
        }

        private void trackBarVelSimulacion_Scroll(object sender, EventArgs e)
        {
            if (trackBarVelSimulacion.Value == 1)
            {
                timerSimulacion.Interval = 1000;
                //labelVelocidad.Text = "x1";
            }

            if (trackBarVelSimulacion.Value == 2)
            {
                timerSimulacion.Interval = 500;
                //labelVelocidad.Text = "x2";
            }

            if (trackBarVelSimulacion.Value == 3)
            {
                timerSimulacion.Interval = 250;
                //labelVelocidad.Text = "x4";
            }

            if (trackBarVelSimulacion.Value == 4)
            {
                timerSimulacion.Interval = 125;
                //labelVelocidad.Text = "x8";
            }
        }

        private void ActualizarAviones()
        {
            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double tiempoMensaje = -1;

                string direccion = "";
                string identificador = "";

                double latitud = double.NaN;
                double longitud = double.NaN;
                double altitud = double.NaN;
                double flightLevel = double.NaN;
                int trackNumber = -1;

                bool esADSB = false;
                bool esRadar = false;

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 mensaje21 = (TiraDatosDecod021)mensaje.tira;

                    tiempoMensaje = mensaje21.TimeReceptionPosition;

                    direccion = mensaje21.TargetAddress;
                    identificador = mensaje21.TargetIdentification;

                    latitud = mensaje21.PosWGS84HighRes[0];
                    longitud = mensaje21.PosWGS84HighRes[1];

                    if (!double.IsNaN(mensaje21.FlightLevel))
                    {
                        flightLevel = mensaje21.FlightLevel;
                        altitud = mensaje21.FlightLevel * 100;
                    }

                    esADSB = true;
                }

                else if (mensaje.categoria == 48)
                {

                    TiraDatosDecod048 mensaje48 = (TiraDatosDecod048)mensaje.tira;
                    bool trackSinIdentificar = mensaje48.AircrftAddrs == -1 && string.IsNullOrEmpty(mensaje48.AircrftIddent) && mensaje48.TargetRep.TYP == "001";

                    if (mensaje48.TrckStatus.CNF == 1 || trackSinIdentificar)
                    {
                        continue;
                    }

                    tiempoMensaje = mensaje48.TimeOfDay;

                    direccion = mensaje48.AircrftAddrs.ToString();
                    identificador = mensaje48.AircrftIddent;
                    trackNumber = mensaje48.TrackNum;

                    latitud = mensaje48.PositionCorrectedLatLonAlt[0];
                    longitud = mensaje48.PositionCorrectedLatLonAlt[1];

                    if (mensaje48.FL.FL != -1)
                    {
                        flightLevel = mensaje48.FL.FL;
                    }

                    if (!double.IsNaN(mensaje48.PositionCorrectedLatLonAlt[2]))
                    {
                        altitud = mensaje48.PositionCorrectedLatLonAlt[2];
                    }

                    esRadar = true;
                    /*Debug.WriteLine(
    "ADDR: " + mensaje48.AircrftAddrs +
    " TRACK: " + mensaje48.TrackNum +
    " ID: " + mensaje48.AircrftIddent +
    " FL: " + mensaje48.FL.FL +
    " TYP: " + mensaje48.TargetRep.TYP +
    " CNF: " + mensaje48.TrckStatus.CNF +
    " RAD: " + mensaje48.TrckStatus.RAD +
    " TRE: " + mensaje48.TrckStatus.TRE +
    " GHO: " + mensaje48.TrckStatus.GHO +
    " SUP: " + mensaje48.TrckStatus.SUP +
    " LAT: " + mensaje48.PositionCorrectedLatLonAlt[0] +
    " LON: " + mensaje48.PositionCorrectedLatLonAlt[1]
);*/
                }


                if (tiempoMensaje >= TiempoActual &&
                    tiempoMensaje < TiempoActual + 1)
                {
                    AvionSimulacion avionEncontrado = null;

                    foreach (AvionSimulacion avion in AvionesActuales)
                    {
                        if (direccion != "" && direccion != "-1")
                        {
                            if (avion.direccion == direccion)
                            {
                                avionEncontrado = avion;
                                break;
                            }
                        }
                        else if (esRadar && trackNumber != -1)
                        {
                            if ((avion.direccion == "" || avion.direccion == "-1") &&
                                avion.trackNumber == trackNumber)
                            {
                                avionEncontrado = avion;
                                break;
                            }
                        }
                    }


                    if (avionEncontrado == null)
                    {
                        AvionSimulacion nuevoAvion = new AvionSimulacion();

                        nuevoAvion.direccion = direccion;
                        nuevoAvion.identificador = identificador;
                        nuevoAvion.trackNumber = trackNumber;

                        nuevoAvion.latitud = latitud;
                        nuevoAvion.longitud = longitud;
                        nuevoAvion.altitud = altitud;
                        nuevoAvion.flightLevel = flightLevel;

                        nuevoAvion.ultimoTiempo = tiempoMensaje;

                        if (esADSB)
                        {
                            nuevoAvion.ultimoTiempoADSB = tiempoMensaje;
                            nuevoAvion.detectadoADSB = true;
                        }

                        if (esRadar)
                        {
                            nuevoAvion.ultimoTiempoRadar = tiempoMensaje;
                            nuevoAvion.detectadoRadar = true;
                        }

                        AvionesActuales.Add(nuevoAvion);
                    }

                    else
                    {
                        if (!double.IsNaN(latitud) && !double.IsNaN(longitud))
                        {
                            bool actualizarPosicion = true;

                            if (esRadar && avionEncontrado.detectadoADSB)
                            {
                                actualizarPosicion = false;
                            }

                            if (actualizarPosicion &&
                                !double.IsNaN(latitud) &&
                                !double.IsNaN(longitud))
                            {
                                if (!double.IsNaN(avionEncontrado.latitud) &&
                                    !double.IsNaN(avionEncontrado.longitud))
                                {
                                    avionEncontrado.rumbo = CalcularRumbo(
                                        avionEncontrado.latitud,
                                        avionEncontrado.longitud,
                                        latitud,
                                        longitud
                                    );
                                }

                                avionEncontrado.latitud = latitud;
                                avionEncontrado.longitud = longitud;
                            }

                            if (!double.IsNaN(altitud))
                            {
                                avionEncontrado.altitud = altitud;
                            }

                            if (!double.IsNaN(flightLevel))
                            {
                                avionEncontrado.flightLevel = flightLevel;
                            }

                            avionEncontrado.ultimoTiempo = tiempoMensaje;

                            if (identificador != "")
                            {
                                avionEncontrado.identificador = identificador;
                            }

                            if (esADSB)
                            {
                                avionEncontrado.ultimoTiempoADSB = tiempoMensaje;
                                avionEncontrado.detectadoADSB = true;
                            }

                            if (esRadar)
                            {
                                avionEncontrado.ultimoTiempoRadar = tiempoMensaje;
                                avionEncontrado.detectadoRadar = true;
                            }
                            if (esRadar && trackNumber != -1)
                            {
                                avionEncontrado.trackNumber = trackNumber;
                            }
                        }
                    }
                }


                for (int i = AvionesActuales.Count - 1; i >= 0; i--)
                {
                    AvionSimulacion avion = AvionesActuales[i];

                    if (avion.ultimoTiempoADSB >= 0 &&
                        TiempoActual - avion.ultimoTiempoADSB >= 10)
                    {
                        avion.detectadoADSB = false;
                    }

                    if (avion.ultimoTiempoRadar >= 0 &&
                        TiempoActual - avion.ultimoTiempoRadar >= 10)
                    {
                        avion.detectadoRadar = false;
                    }

                    if (TiempoActual - avion.ultimoTiempo >= 10)
                    {
                        AvionesActuales.RemoveAt(i);
                    }
                }
            }
        }
        private double CalcularRumbo(double lat1, double lon1, double lat2, double lon2)
        {
            double lat1Rad = lat1 * Math.PI / 180.0;
            double lat2Rad = lat2 * Math.PI / 180.0;
            double diferenciaLon = (lon2 - lon1) * Math.PI / 180.0;

            double y = Math.Sin(diferenciaLon) * Math.Cos(lat2Rad);

            double x =
                Math.Cos(lat1Rad) * Math.Sin(lat2Rad) -
                Math.Sin(lat1Rad) * Math.Cos(lat2Rad) *
                Math.Cos(diferenciaLon);

            double rumbo = Math.Atan2(y, x) * 180.0 / Math.PI;

            if (rumbo < 0)
            {
                rumbo += 360;
            }

            return rumbo;
        }
        private void BuscarTiempoInicial()
        {
            double primerTiempo = double.MaxValue;

            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double tiempo = -1;

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 mensaje21 =
                        (TiraDatosDecod021)mensaje.tira;

                    tiempo = mensaje21.TimeReceptionPosition;
                }

                else if (mensaje.categoria == 48)
                {
                    TiraDatosDecod048 mensaje48 =
                        (TiraDatosDecod048)mensaje.tira;

                    tiempo = mensaje48.TimeOfDay;
                }

                if (tiempo >= 0 && tiempo < primerTiempo)
                {
                    primerTiempo = tiempo;
                }
            }

            if (primerTiempo != double.MaxValue)
            {
                TiempoActual = Math.Floor(primerTiempo);
                label1.Text = TiempoActual.ToString();
            }
        }

        private void buttonAvanzar_Click(object sender, EventArgs e)
        {
            AvanzarUnSegundo();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            timerSimulacion.Stop();

            AvionesActuales.Clear();
            dataGridAviones.Rows.Clear();

            BuscarTiempoInicial();

            label1.Text = TiempoActual.ToString();

            ActualizarAviones();
            ActualizarDataGrid();
            ActualizarMapa();
        }

        private void cargaDatosrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog explorador = new OpenFileDialog();

            explorador.Title = "Seleccionar archivo ASTERIX";
            //explorador.Filter 

            if (explorador.ShowDialog() == DialogResult.OK)
            {
                timerSimulacion.Stop();
                Reproduciendo = false;

                AvionesActuales.Clear();
                dataGridAviones.Rows.Clear();

                string rutaArchivo = explorador.FileName;
                LeerDatos lector = new LeerDatos();

                ListaMensajes = lector.DatosProcesados(rutaArchivo);
                ListaMensajesFiltrados = ListaMensajes;

                BuscarTiempoInicial();
                ActualizarAviones();
                ActualizarDataGrid();
                ActualizarMapa();

                MessageBox.Show("Archivo cargado bien" + ListaMensajes.Count);
            }
        }
        private void ActualizarDataGrid()
        {
            dataGridAviones.Rows.Clear();

            foreach (AvionSimulacion avion in AvionesActuales)
            {
                string sistema = "";

                if (avion.detectadoRadar && avion.detectadoADSB)
                {
                    sistema = "Radar + ADS-B";
                }
                else if (avion.detectadoRadar)
                {
                    sistema = "Radar";
                }
                else if (avion.detectadoADSB)
                {
                    sistema = "ADS-B";
                }

                dataGridAviones.Rows.Add(
                    avion.direccion,
                    avion.identificador,
                    avion.latitud,
                    avion.longitud,
                    avion.flightLevel,
                    avion.altitud,
                    sistema,
                    avion.ultimoTiempo
                );
            }
        }

        private void buttonDataGrid_Click(object sender, EventArgs e)
        {
            dataGridAviones.Visible = !dataGridAviones.Visible;
        }
        private void ActualizarMapa()
        {
            capaAviones.Markers.Clear();

            foreach (AvionSimulacion avion in AvionesActuales)
            {
                if (!double.IsNaN(avion.latitud) &&
                    !double.IsNaN(avion.longitud))
                {
                    PointLatLng posicion = new PointLatLng(avion.latitud, avion.longitud);

                    Color colorAvion;

                    if (avion.detectadoRadar && avion.detectadoADSB)
                    {
                        colorAvion = Color.Blue;
                    }
                    else if (avion.detectadoRadar)
                    {
                        colorAvion = Color.Red;
                    }
                    else
                    {
                        colorAvion = Color.Green;
                    }

                    MarcadorAvion marcador = new MarcadorAvion(posicion, avion.rumbo, colorAvion);
                    marcador.Tag = avion;

                    string fl = "N/A";
                    if (!double.IsNaN(avion.flightLevel))
                    {
                        fl = avion.flightLevel.ToString();
                    }
                    marcador.ToolTipText = "\n" + avion.identificador + "\n" + "FL " + fl + "\n" + ObtenerSistema(avion);
                    marcador.ToolTipMode = MarkerTooltipMode.OnMouseOver;

                    capaAviones.Markers.Add(marcador);
                }
            }

            gMapControl1.Refresh();
        }
        private string ObtenerSistema(AvionSimulacion avion)
        {
            if (avion.detectadoRadar && avion.detectadoADSB)
                return "Radar + ADS-B";

            if (avion.detectadoRadar)
                return "Radar";

            if (avion.detectadoADSB)
                return "ADS-B";

            return "";
        }

        private void GMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            AvionSimulacion avion = item.Tag as AvionSimulacion;

            if (avion != null)
            {
                InformacionAvion ventana = new InformacionAvion();
                ventana.avion = avion;
                ventana.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Reproduciendo == false)
            {
                Reproduciendo = true;
                pictureBox1.Image = Properties.Resources.pausebutton_113576;
                timerSimulacion.Start();
            }
            else if (Reproduciendo == true)
            {
                Reproduciendo = false;
                timerSimulacion.Stop();
                pictureBox1.Image = Properties.Resources.playbutton_113628;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AvanzarUnSegundo();
        }
        private List<AvionSimulacion> CopiarAviones()
        {
            List<AvionSimulacion> copia = new List<AvionSimulacion>();

            foreach (AvionSimulacion avion in AvionesActuales)
            {
                copia.Add(avion.Copiar());
            }

            return copia;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (historialAviones.Count > 0)
            {
                AvionesActuales = historialAviones.Pop();
                TiempoActual = historialTiempo.Pop();

                ActualizarMapa();

                label1.Text = TiempoActual.ToString();
            }
        }
    }
}
