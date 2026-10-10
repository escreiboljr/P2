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
using System.Security.Policy;

namespace GUI_ASTERIX
{
    public partial class MenuSimulacion : Form
    {
        private double TiempoActual = 0;
        private List<Mensaje> ListaMensajes = new List<Mensaje>();
        private List<Mensaje> ListaMensajesFiltrados = new List<Mensaje>();
        //private List<Mensaje> AvionesActuales = new List<Mensaje>();
        private List<AvionSimulacion> AvionesActuales = new List<AvionSimulacion>();
        private List<AvionSimulacion> MensajesActuales = new List<AvionSimulacion>();
        private Stack<List<AvionSimulacion>> historialAviones = new Stack<List<AvionSimulacion>>();
        private Stack<double> historialTiempo = new Stack<double>();
        private bool Reproduciendo = false;
        private GMapOverlay capaAviones;
        private GMapOverlay capaTrayectorias;
        private List<AvionSimulacion> avionesTrayectoriaSeleccionados = new List<AvionSimulacion>();
        public bool oculto = true;
        private AvionSimulacion avionClick;
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

            capaTrayectorias = new GMapOverlay("trayectorias");
            gMapControl1.Overlays.Add(capaTrayectorias);

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
            AvanzarUnSegundo();
        }

        private void AvanzarUnSegundo()
        {
            historialAviones.Push(CopiarAviones());
            historialTiempo.Push(TiempoActual);
            TiempoActual = TiempoActual + 1;
            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");
            ActualizarAviones();
            EliminarAvionesInactivos();
            ActualizarDataGrid();
            ActualizarMapa();
            MostrarTrayectorias();
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

                double velocidad = double.NaN;
                double rumbo = double.NaN;
                string mode3A = "";
                string estadoVuelo = "";

                bool esADSB = false;
                bool esRadar = false;

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 mensaje21 = (TiraDatosDecod021)mensaje.tira;

                    mode3A = mensaje21.Mode3ACode;

                    tiempoMensaje = mensaje21.TimeReceptionPosition;

                    direccion = mensaje21.TargetAddress;
                    identificador = mensaje21.TargetIdentification;

                    latitud = mensaje21.PosWGS84HighRes[0];
                    longitud = mensaje21.PosWGS84HighRes[1];

                    if (!double.IsNaN(mensaje21.FlightLevel))
                    {
                        flightLevel = mensaje21.FlightLevel;
                        altitud = mensaje21.AltitudCorregida;
                    }

                    esADSB = true;
                }

                else if (mensaje.categoria == 48)
                {

                    TiraDatosDecod048 mensaje48 = (TiraDatosDecod048)mensaje.tira;
                    bool trackSinIdentificar = string.IsNullOrEmpty(mensaje48.AircrftAddrs) && string.IsNullOrEmpty(mensaje48.AircrftIddent) && mensaje48.TargetRep.TYP == "001";

                    if (mensaje48.TrckStatus.CNF == 1 || trackSinIdentificar)
                    {
                        continue;
                    }

                    if (mensaje48.tieneTrackVelocity)
                    {
                        velocidad = mensaje48.TrckVelPolRepr[0];
                        rumbo = mensaje48.TrckVelPolRepr[1];
                    }
                    mode3A = mensaje48.mode3.reply;

                    tiempoMensaje = mensaje48.TimeOfDay;

                    direccion = mensaje48.AircrftAddrs ?? ""; //comentar esta linea 
                    identificador = mensaje48.AircrftIddent;
                    trackNumber = mensaje48.TrackNum;

                    latitud = mensaje48.PositionCorrectedLatLonAlt[0];
                    longitud = mensaje48.PositionCorrectedLatLonAlt[1];

                    if (!double.IsNaN(mensaje48.FL.FL))
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
                    if (!double.IsNaN(latitud) && !double.IsNaN(longitud))
                    {
                        AvionSimulacion mensajeVisual =
                            new AvionSimulacion();

                        mensajeVisual.direccion = direccion;
                        mensajeVisual.identificador = identificador;
                        mensajeVisual.trackNumber = trackNumber;

                        mensajeVisual.latitud = latitud;
                        mensajeVisual.longitud = longitud;

                        mensajeVisual.altitud = altitud;
                        mensajeVisual.flightLevel = flightLevel;

                        mensajeVisual.velocidad = velocidad;
                        mensajeVisual.rumbo = rumbo;
                        mensajeVisual.mode3A = mode3A;

                        mensajeVisual.ultimoTiempo = tiempoMensaje;

                        mensajeVisual.mensajeOriginal = mensaje;

                        if (esADSB)
                        {
                            mensajeVisual.detectadoADSB = true;
                            mensajeVisual.ultimoTiempoADSB = tiempoMensaje;
                        }

                        if (esRadar)
                        {
                            mensajeVisual.detectadoRadar = true;
                            mensajeVisual.ultimoTiempoRadar = tiempoMensaje;
                        }

                        ActualizarMensajeVisual(mensajeVisual);
                    }
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
                        nuevoAvion.rumbo = rumbo;

                        nuevoAvion.latitud = latitud;
                        nuevoAvion.longitud = longitud;
                        if (!double.IsNaN(latitud) && !double.IsNaN(longitud))
                        {
                            nuevoAvion.trayectoria.Add(
                            new double[] { latitud, longitud }
                            );
                        }
                        nuevoAvion.altitud = altitud;
                        nuevoAvion.flightLevel = flightLevel;

                        nuevoAvion.ultimoTiempo = tiempoMensaje;

                        nuevoAvion.velocidad = velocidad;
                        nuevoAvion.mode3A = mode3A;
                        nuevoAvion.estadoVuelo = estadoVuelo;

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
                                avionEncontrado.trayectoria.Add(
                                new double[] { latitud, longitud }
                                );
                            }

                            if (!double.IsNaN(altitud))
                            {
                                avionEncontrado.altitud = altitud;
                            }

                            if (!double.IsNaN(flightLevel))
                            {
                                avionEncontrado.flightLevel = flightLevel;
                            }
                            if (!double.IsNaN(velocidad))
                            {
                                avionEncontrado.velocidad = velocidad;
                            }

                            if (!string.IsNullOrWhiteSpace(mode3A))
                            {
                                avionEncontrado.mode3A = mode3A;
                            }

                            if (!string.IsNullOrWhiteSpace(estadoVuelo))
                            {
                                avionEncontrado.estadoVuelo = estadoVuelo;
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

                    //if (TiempoActual - avion.ultimoTiempo >= 10)
                    //{
                    //    AvionesActuales.RemoveAt(i);
                    //}
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
            MensajesActuales.Clear();

            dataGridAviones.Rows.Clear();

            historialAviones.Clear();
            historialTiempo.Clear();

            capaTrayectorias.Routes.Clear();
            avionesTrayectoriaSeleccionados.Clear();

            BuscarTiempoInicial();

            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");

            ActualizarAviones();
            ActualizarDataGrid();
            ActualizarMapa();

            pictureBox1.Image = Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_52_2;

            capaTrayectorias.Routes.Clear();
        }

        private void cargaDatosrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog explorador = new OpenFileDialog())
            {
                explorador.Title = "Seleccionar archivo ASTERIX";

                if (explorador.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    // Primero leemos el archivo nuevo
                    LeerDatos lector = new LeerDatos();

                    List<Mensaje> nuevosMensajes =
                        lector.DatosProcesados(explorador.FileName);

                    if (nuevosMensajes == null || nuevosMensajes.Count == 0)
                    {
                        MessageBox.Show("El archivo no contiene mensajes válidos.");
                        return;
                    }

                    // Detener la simulación anterior
                    timerSimulacion.Stop();
                    Reproduciendo = false;

                    pictureBox1.Image =
                        Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_52_2;

                    // Borrar todos los datos anteriores
                    AvionesActuales.Clear();
                    MensajesActuales.Clear();

                    historialAviones.Clear();
                    historialTiempo.Clear();

                    avionesTrayectoriaSeleccionados.Clear();

                    dataGridAviones.Rows.Clear();

                    capaAviones.Markers.Clear();
                    capaTrayectorias.Routes.Clear();

                    // Asignar exclusivamente los mensajes del nuevo archivo
                    ListaMensajes = nuevosMensajes;
                    ListaMensajesFiltrados = new List<Mensaje>(nuevosMensajes);

                    int cat21 = ListaMensajesFiltrados.Count(m => m.categoria == 21);
                    int cat48 = ListaMensajesFiltrados.Count(m => m.categoria == 48);

                    MessageBox.Show(
                        "Archivo: " + explorador.FileName +
                        "\nCAT21: " + cat21 +
                        "\nCAT48: " + cat48
                    );

                    // Reiniciar el tiempo al comienzo del nuevo archivo
                    TiempoActual = 0;
                    BuscarTiempoInicial();

                    TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
                    label1.Text = hora.ToString(@"hh\:mm\:ss");

                    // Actualizar todo con los datos nuevos
                    ActualizarAviones();
                    ActualizarDataGrid();
                    ActualizarMapa();
                    MostrarTrayectorias();

                    MessageBox.Show(
                        "Archivo cargado correctamente.\n" +
                        "Mensajes: " + ListaMensajes.Count
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al cargar el archivo:\n" + ex.Message
                    );
                }
            }
        }
        private void ActualizarDataGrid()
        {
            dataGridAviones.Rows.Clear();

            foreach (AvionSimulacion avion in AvionesActuales)
            {
                string sistema = "";

                if (avion.detectadoRadar)
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
            dataGridAviones.BringToFront();
            dataGridAviones.Visible = !dataGridAviones.Visible;

        }
        private void ActualizarMapa()
        {
            capaAviones.Markers.Clear();

            foreach (AvionSimulacion mensaje in MensajesActuales)
            {
                if (!double.IsNaN(mensaje.latitud) &&
                    !double.IsNaN(mensaje.longitud))
                {
                    PointLatLng posicion =
                        new PointLatLng(
                            mensaje.latitud,
                            mensaje.longitud
                        );
                    AvionSimulacion avionReal = null;

                    foreach (AvionSimulacion avion in AvionesActuales)
                    {
                        if (!string.IsNullOrEmpty(mensaje.direccion) &&
                            mensaje.direccion != "-1")
                        {
                            if (avion.direccion == mensaje.direccion)
                            {
                                avionReal = avion;
                                break;
                            }
                        }
                        else if (mensaje.trackNumber != -1)
                        {
                            if ((avion.direccion == "" ||
                                 avion.direccion == "-1") &&
                                avion.trackNumber == mensaje.trackNumber)
                            {
                                avionReal = avion;
                                break;
                            }
                        }
                    }
                    Color colorAvion;

                    if (mensaje.detectadoRadar)
                    {
                        colorAvion = Color.Red;
                    }
                    else
                    {
                        colorAvion = Color.Green;
                    }


                    double rumbo = 0;

                    if (avionReal != null)
                    {
                        rumbo = avionReal.rumbo;
                    }


                    MarcadorAvion marcador =
                        new MarcadorAvion(
                            posicion,
                            rumbo,
                            colorAvion
                        );
                    marcador.Tag = mensaje;
                    string fl = "N/A";

                    if (!double.IsNaN(mensaje.flightLevel))
                    {
                        fl = mensaje.flightLevel.ToString();
                    }
                    string sistema = "";

                    if (mensaje.detectadoRadar)
                    {
                        sistema = "Radar";
                    }
                    else if (mensaje.detectadoADSB)
                    {
                        sistema = "ADS-B";
                    }
                    marcador.ToolTipText =
                        "\n" +
                        mensaje.identificador +
                        "\nFL " +
                        fl +
                        "\n" +
                        sistema;

                    marcador.ToolTipMode =
                        MarkerTooltipMode.OnMouseOver;

                    capaAviones.Markers.Add(marcador);
                }
            }
            gMapControl1.Refresh();
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

        private void GMapControl1_OnMarkerClick(
    GMapMarker item,
    MouseEventArgs e)
        {
            AvionSimulacion mensaje =
                item.Tag as AvionSimulacion;

            if (mensaje == null)
            {
                return;
            }


            // CLICK IZQUIERDO -> mostrar trayectoria
            if (e.Button == MouseButtons.Left)
            {
                // Buscamos el avión real, que contiene
                // toda la trayectoria acumulada
                AvionSimulacion avionTrayectoria =
                    BuscarAvionSeleccionado(mensaje);

                if (avionTrayectoria == null)
                {
                    return;
                }

                if (avionesTrayectoriaSeleccionados.Contains(avionTrayectoria))
                {
                    avionesTrayectoriaSeleccionados.Remove(avionTrayectoria);
                }
                else
                {
                    avionesTrayectoriaSeleccionados.Add(avionTrayectoria);
                }

                MostrarTrayectorias();
            }


            // CLICK DERECHO -> información del mensaje concreto
            else if (e.Button == MouseButtons.Right)
            {
                InformacionAvion ventana =
                    new InformacionAvion();

                ventana.avion = mensaje;

                ventana.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Reproduciendo == false)
            {
                Reproduciendo = true;
                pictureBox1.Image = Properties.Resources.Icono_de_pausa_blanco_sobre_transparente1;
                timerSimulacion.Start();
            }
            else if (Reproduciendo == true)
            {
                Reproduciendo = false;
                timerSimulacion.Stop();
                pictureBox1.Image = Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_52_2;
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
                // Guardamos qué aviones tenían la trayectoria seleccionada
                List<string> clavesSeleccionadas = new List<string>();

                foreach (AvionSimulacion avion in avionesTrayectoriaSeleccionados)
                {
                    clavesSeleccionadas.Add(ObtenerClaveAvion(avion));
                }

                // Recuperamos estado anterior
                AvionesActuales = historialAviones.Pop();
                TiempoActual = historialTiempo.Pop();
                ActualizarMensajesActuales();

                // Reconstruimos la lista de seleccionados
                avionesTrayectoriaSeleccionados.Clear();

                foreach (AvionSimulacion avion in AvionesActuales)
                {
                    if (clavesSeleccionadas.Contains(ObtenerClaveAvion(avion)))
                    {
                        avionesTrayectoriaSeleccionados.Add(avion);
                    }
                }

                // Actualizamos hora
                TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
                label1.Text = hora.ToString(@"hh\:mm\:ss");

                ActualizarDataGrid();
                ActualizarMapa();
                MostrarTrayectorias();
            }
        }
        private void ActualizarMensajesActuales()
        {
            MensajesActuales.Clear();
            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double tiempoMensaje = -1;

                string direccion = "";
                string identificador = "";

                double latitud = double.NaN;
                double longitud = double.NaN;
                double altitud = double.NaN;
                double flightLevel = double.NaN;
                double rumbo = double.NaN;

                int trackNumber = -1;

                bool esADSB = false;
                bool esRadar = false;

                double velocidad = double.NaN;
                string mode3A = "";

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 mensaje21 =
                        (TiraDatosDecod021)mensaje.tira;

                    tiempoMensaje =
                        mensaje21.TimeReceptionPosition;

                    direccion =
                        mensaje21.TargetAddress;

                    identificador =
                        mensaje21.TargetIdentification;

                    latitud =
                        mensaje21.PosWGS84HighRes[0];

                    longitud =
                        mensaje21.PosWGS84HighRes[1];

                    flightLevel =
                        mensaje21.FlightLevel;

                    altitud =
                        mensaje21.AltitudCorregida;

                    esADSB = true;

                    mode3A = mensaje21.Mode3ACode;
                }

                else if (mensaje.categoria == 48)
                {
                    TiraDatosDecod048 mensaje48 =
                        (TiraDatosDecod048)mensaje.tira;

                    bool trackSinIdentificar =
                        string.IsNullOrEmpty(mensaje48.AircrftAddrs) &&
                        string.IsNullOrEmpty(mensaje48.AircrftIddent) &&
                        mensaje48.TargetRep.TYP == "001";

                    if (mensaje48.TrckStatus.CNF == 1 ||
                        trackSinIdentificar)
                    {
                        continue;
                    }

                    tiempoMensaje =
                        mensaje48.TimeOfDay;

                    direccion = mensaje48.AircrftAddrs ?? "";

                    identificador =
                        mensaje48.AircrftIddent;

                    trackNumber =
                        mensaje48.TrackNum;

                    latitud =
                        mensaje48.PositionCorrectedLatLonAlt[0];

                    longitud =
                        mensaje48.PositionCorrectedLatLonAlt[1];

                    if (!double.IsNaN(mensaje48.FL.FL))
                    {
                        flightLevel = mensaje48.FL.FL;
                    }

                    if (!double.IsNaN(
                        mensaje48.PositionCorrectedLatLonAlt[2]))
                    {
                        altitud =
                            mensaje48.PositionCorrectedLatLonAlt[2];
                    }

                    esRadar = true;

                    if (mensaje48.tieneTrackVelocity)
                    {
                        velocidad = mensaje48.TrckVelPolRepr[0];
                        rumbo = mensaje48.TrckVelPolRepr[1];
                    }

                    mode3A = mensaje48.mode3.reply;
                }


                if (tiempoMensaje <= TiempoActual &&
                    tiempoMensaje > TiempoActual - 10)
                {
                    if (double.IsNaN(latitud) ||
                        double.IsNaN(longitud))
                    {
                        continue;
                    }

                    AvionSimulacion mensajeVisual =
                        new AvionSimulacion();

                    mensajeVisual.mensajeOriginal = mensaje;

                    mensajeVisual.direccion = direccion;
                    mensajeVisual.identificador = identificador;

                    mensajeVisual.trackNumber = trackNumber;

                    mensajeVisual.latitud = latitud;
                    mensajeVisual.longitud = longitud;

                    mensajeVisual.altitud = altitud;
                    mensajeVisual.flightLevel = flightLevel;

                    mensajeVisual.ultimoTiempo = tiempoMensaje;

                    mensajeVisual.detectadoADSB = esADSB;
                    mensajeVisual.detectadoRadar = esRadar;
                    mensajeVisual.rumbo = rumbo;

                    if (esADSB)
                    {
                        mensajeVisual.ultimoTiempoADSB =
                            tiempoMensaje;
                    }

                    if (esRadar)
                    {
                        mensajeVisual.ultimoTiempoRadar =
                            tiempoMensaje;
                    }

                    mensajeVisual.velocidad = velocidad;
                    mensajeVisual.mode3A = mode3A;

                    ActualizarMensajeVisual(mensajeVisual);
                }
            }
        }
        private void ActualizarMensajeVisual(AvionSimulacion nuevoMensaje)
        {
            AvionSimulacion mensajeAnterior = null;

            foreach (AvionSimulacion mensaje in MensajesActuales)
            {
                bool mismoAvion = false;

                // Si tenemos dirección válida, identificamos por dirección
                if (!string.IsNullOrEmpty(nuevoMensaje.direccion) &&
                    nuevoMensaje.direccion != "-1")
                {
                    mismoAvion =
                        mensaje.direccion == nuevoMensaje.direccion;
                }

                // Si no hay dirección, usamos Track Number
                else if (nuevoMensaje.trackNumber != -1)
                {
                    mismoAvion =
                        (mensaje.direccion == "" ||
                         mensaje.direccion == "-1") &&
                        mensaje.trackNumber == nuevoMensaje.trackNumber;
                }


                if (!mismoAvion)
                {
                    continue;
                }


                // Además de ser el mismo avión,
                // tiene que ser el mismo SISTEMA

                bool mismoSistema =
                    (mensaje.detectadoADSB &&
                     nuevoMensaje.detectadoADSB)
                    ||
                    (mensaje.detectadoRadar &&
                     nuevoMensaje.detectadoRadar);


                if (mismoSistema)
                {
                    mensajeAnterior = mensaje;
                    break;
                }
            }


            // Si ya existía un mensaje de ese avión y sistema,
            // sustituimos el antiguo
            if (mensajeAnterior != null)
            {
                mensajeAnterior.direccion = nuevoMensaje.direccion;
                mensajeAnterior.identificador = nuevoMensaje.identificador;
                mensajeAnterior.trackNumber = nuevoMensaje.trackNumber;

                mensajeAnterior.latitud = nuevoMensaje.latitud;
                mensajeAnterior.longitud = nuevoMensaje.longitud;

                mensajeAnterior.mensajeOriginal = nuevoMensaje.mensajeOriginal;

                if (!double.IsNaN(nuevoMensaje.altitud))
                {
                    mensajeAnterior.altitud = nuevoMensaje.altitud;
                }

                if (!double.IsNaN(nuevoMensaje.flightLevel))
                {
                    mensajeAnterior.flightLevel = nuevoMensaje.flightLevel;
                }

                if (!double.IsNaN(nuevoMensaje.velocidad))
                {
                    mensajeAnterior.velocidad = nuevoMensaje.velocidad;
                }
                if (!double.IsNaN(nuevoMensaje.rumbo))
                {
                    mensajeAnterior.rumbo = nuevoMensaje.rumbo;
                }

                if (!string.IsNullOrWhiteSpace(nuevoMensaje.mode3A))
                {
                    mensajeAnterior.mode3A = nuevoMensaje.mode3A;
                }

                mensajeAnterior.ultimoTiempo = nuevoMensaje.ultimoTiempo;

                mensajeAnterior.detectadoADSB = nuevoMensaje.detectadoADSB;
                mensajeAnterior.detectadoRadar = nuevoMensaje.detectadoRadar;

                if (nuevoMensaje.detectadoADSB)
                {
                    mensajeAnterior.ultimoTiempoADSB =
                        nuevoMensaje.ultimoTiempoADSB;
                }

                if (nuevoMensaje.detectadoRadar)
                {
                    mensajeAnterior.ultimoTiempoRadar =
                        nuevoMensaje.ultimoTiempoRadar;
                }
            }
            else
            {
                MensajesActuales.Add(nuevoMensaje);
            }
        }
        private string ObtenerClaveAvion(AvionSimulacion avion)
        {
            if (!string.IsNullOrEmpty(avion.direccion) &&
                avion.direccion != "-1")
            {
                return avion.direccion;
            }

            return "TRACK_" + avion.trackNumber;
        }

        private void aplicarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFiltro ventana = new FormFiltro();

            if (ventana.ShowDialog() == DialogResult.OK)
            {
                Filtros gestorFiltros = new Filtros();

                ListaMensajesFiltrados = gestorFiltros.AplicarFiltros(
                    ListaMensajes,
                    ventana.Cat48,
                    ventana.Cat21,
                    ventana.BlancoPuro,
                    ventana.transFijo,
                    ventana.FiltrarTrayectoria,
                    ventana.trayectoriaMin,
                    ventana.trayectoriaMax,
                    ventana.ground
                );
                ReconstruirAvionesFiltrados();
            }
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaMensajesFiltrados = new List<Mensaje>(ListaMensajes);

            AvionesActuales.Clear();
            MensajesActuales.Clear();

            historialAviones.Clear();
            historialTiempo.Clear();

            capaTrayectorias.Routes.Clear();
            avionesTrayectoriaSeleccionados.Clear();

            ActualizarAviones();
            ActualizarDataGrid();
            ActualizarMapa();

            gMapControl1.Refresh();
        }

        private void guardarEnCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();

            guardar.Filter = "Archivo CSV (*.csv)|*.csv";
            guardar.DefaultExt = "csv";
            guardar.FileName = "ASTERIX.csv";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                int cat21 = ListaMensajesFiltrados.Count(m => m.categoria == 21);
                int cat48 = ListaMensajesFiltrados.Count(m => m.categoria == 48);
                /*
                MessageBox.Show(
                    "Mensajes que se van a exportar:\n" +
                    "CAT21: " + cat21 + "\n" +
                    "CAT48: " + cat48
                ); */

                ExportarCSV exportador = new ExportarCSV();

                exportador.Exportar(
                    ListaMensajesFiltrados,
                    guardar.FileName
                );

                MessageBox.Show("CSV guardado correctamente");
            }
        }
        private AvionSimulacion BuscarAvionSeleccionado(AvionSimulacion avionAnterior)
        {
            if (avionAnterior == null)
            {
                return null;
            }

            foreach (AvionSimulacion avion in AvionesActuales)
            {
                if (!string.IsNullOrEmpty(avionAnterior.direccion) &&
                    avionAnterior.direccion != "-1")
                {
                    if (avion.direccion == avionAnterior.direccion)
                    {
                        return avion;
                    }
                }

                else
                {
                    if (avion.trackNumber == avionAnterior.trackNumber)
                    {
                        return avion;
                    }
                }
            }

            return null;
        }
        private void MostrarTrayectorias()
        {
            capaTrayectorias.Routes.Clear();

            foreach (AvionSimulacion avion in avionesTrayectoriaSeleccionados)
            {
                if (avion.trayectoria.Count < 2)
                {
                    continue;
                }

                List<PointLatLng> puntos = new List<PointLatLng>();

                foreach (double[] punto in avion.trayectoria)
                {
                    puntos.Add(
                        new PointLatLng(punto[0], punto[1])
                    );
                }

                GMapRoute ruta = new GMapRoute(
                    puntos,
                    "Trayectoria " + avion.identificador
                );

                ruta.Stroke = new Pen(Color.Blue, 2);

                capaTrayectorias.Routes.Add(ruta);
            }
            gMapControl1.Refresh();
        }
        private void ReconstruirAvionesFiltrados()
        {
            double tiempoOriginal = TiempoActual;

            AvionesActuales.Clear();
            MensajesActuales.Clear();

            historialAviones.Clear();
            historialTiempo.Clear();

            capaAviones.Markers.Clear();
            capaTrayectorias.Routes.Clear();
            avionesTrayectoriaSeleccionados.Clear();

            // Buscar el tiempo inicial del archivo filtrado
            BuscarTiempoInicial();

            double tiempoInicial = TiempoActual;

            // Reconstruir desde el inicio hasta el instante actual
            for (double tiempo = tiempoInicial; tiempo <= tiempoOriginal; tiempo++)
            {
                TiempoActual = tiempo;
                ActualizarAviones();
                EliminarAvionesInactivos();
            }

            // Recuperar el tiempo en el que estaba la simulación
            TiempoActual = tiempoOriginal;

            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");

            ActualizarDataGrid();
            ActualizarMapa();
            MostrarTrayectorias();
        }

        private void EliminarAvionesInactivos()
        {
            // Eliminar mensajes antiguos del mapa
            MensajesActuales.RemoveAll(mensaje =>
                TiempoActual - mensaje.ultimoTiempo >= 10
            );

            // Eliminar aviones que llevan 10 segundos sin enviar mensajes
            AvionesActuales.RemoveAll(avion =>
                TiempoActual - avion.ultimoTiempo >= 10
            );
        }

        private void buttonSimularDesdeHora_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxHora.Text, out int horas) ||
        !int.TryParse(textBoxMinuto.Text, out int minutos) ||
        !int.TryParse(textBoxSegundo.Text, out int segundos))
            {
                MessageBox.Show("Introduce valores numéricos válidos.");
                return;
            }

            // Comprobar que la hora sea válida
            if (horas < 0 || horas > 23 ||
                minutos < 0 || minutos > 59 ||
                segundos < 0 || segundos > 59)
            {
                MessageBox.Show("La hora introducida no es válida.");
                return;
            }

            if (ListaMensajesFiltrados.Count == 0)
            {
                MessageBox.Show("Primero debes cargar un archivo ASTERIX.");
                return;
            }

            // Detener la simulación
            timerSimulacion.Stop();
            Reproduciendo = false;

            // Limpiar el estado anterior
            AvionesActuales.Clear();
            MensajesActuales.Clear();

            historialAviones.Clear();
            historialTiempo.Clear();

            avionesTrayectoriaSeleccionados.Clear();

            capaAviones.Markers.Clear();
            capaTrayectorias.Routes.Clear();

            dataGridAviones.Rows.Clear();

            // Convertir la hora a segundos
            TiempoActual = horas * 3600 + minutos * 60 + segundos;

            // Actualizar la hora mostrada
            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");

            // Mostrar los aviones de ese instante
            ActualizarAviones();
            EliminarAvionesInactivos();

            ActualizarDataGrid();
            ActualizarMapa();
            MostrarTrayectorias();

            // Dejar la simulación pausada
            pictureBox1.Image =
                Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_52_2;
        }
    }
}
