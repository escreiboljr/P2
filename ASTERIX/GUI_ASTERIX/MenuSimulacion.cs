using Archivos;
using Archvios;
using DatosDecod;
using DatosDecod21;
using DatosDecod48;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Gst;
using Microsoft.VisualBasic;
using Simulacion;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace GUI_ASTERIX
{
    public partial class MenuSimulacion : Form
    {
        private BarraTiempoPersonalizada barraTiempoNueva;
        private double TiempoActual = 0;
        private double tiempoInicioSimulacion = 0;
        private double tiempoFinSimulacion = 0;
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
        private GMapOverlay capaHeatmap;
        private bool mostrarHeatmapDinamico = false;
        private double ultimoTiempoHeatmap = -1;
        private const double intervaloHeatmap = 10;
        private bool mostrarHeatmap = false;
        private List<AvionSimulacion> avionesTrayectoriaSeleccionados = new List<AvionSimulacion>();
        public bool oculto = true;
        private AvionSimulacion avionClick;
        private bool mostrarTodasTrayectorias = false;
        public MenuSimulacion()
        {
            InitializeComponent();
            timerSimulacion.Interval = 1000;
            gMapControl1.OnMarkerClick += GMapControl1_OnMarkerClick;
            LeerDatos prueba = new LeerDatos();

            //AjustarMapa();
            //this.Resize += (sender, e) => AjustarMapa();

            barraTiempoNueva = new BarraTiempoPersonalizada();

            barraTiempoNueva.Location = trackBarTiempo.Location;
            barraTiempoNueva.Size = new Size(trackBarTiempo.Width, 36);

            barraTiempoNueva.Anchor = trackBarTiempo.Anchor;
            barraTiempoNueva.BackColor = Color.FromArgb(21, 29, 41);

            barraTiempoNueva.TiempoMinimo = trackBarTiempo.Minimum;
            barraTiempoNueva.TiempoMaximo = trackBarTiempo.Maximum;
            barraTiempoNueva.TiempoValor = trackBarTiempo.Value;

            // Añadirla al mismo contenedor que la barra original
            trackBarTiempo.Parent.Controls.Add(barraTiempoNueva);

            // Ocultar la barra antigua
            trackBarTiempo.Visible = false;

            barraTiempoNueva.BringToFront();
            barraTiempoNueva.ValorConfirmado += (sender, e) =>
            {
                bool estabaReproduciendo = Reproduciendo;

                trackBarTiempo.Value = barraTiempoNueva.TiempoValor;

                trackBarTiempo_Scroll(sender, EventArgs.Empty);

                if (estabaReproduciendo)
                {
                    Reproduciendo = true;
                    pictureBox1.Image = Properties.Resources.botonPausa;
                    timerSimulacion.Start();
                }
            };
            textBoxHora.KeyDown += HoraTextBox_KeyDown;
            textBoxMinuto.KeyDown += HoraTextBox_KeyDown;
            textBoxSegundo.KeyDown += HoraTextBox_KeyDown;
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

            capaHeatmap = new GMapOverlay("heatmap");
            gMapControl1.Overlays.Add(capaHeatmap);

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
            if (TiempoActual <= tiempoFinSimulacion)
            {
                trackBarTiempo.Value = (int)(
                    TiempoActual - tiempoInicioSimulacion
                );
            }
            barraTiempoNueva.TiempoValor = trackBarTiempo.Value;
            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");
            ActualizarHoraTextBox();
            ActualizarAviones();
            EliminarAvionesInactivos();
            ActualizarDataGrid();
            ActualizarMapa();
            MostrarTrayectorias();
            ActualizarHeatmapDinamico();
        }

        private void trackBarVelSimulacion_Scroll(object sender, EventArgs e)
        {
            if (trackBarVelSimulacion.Value == 1)
            {
                timerSimulacion.Interval = 1000;
                labelVelocidad.Text = "x1";
            }

            if (trackBarVelSimulacion.Value == 2)
            {
                timerSimulacion.Interval = 500;
                labelVelocidad.Text = "x2";
            }

            if (trackBarVelSimulacion.Value == 3)
            {
                timerSimulacion.Interval = 250;
                labelVelocidad.Text = "x4";
            }

            if (trackBarVelSimulacion.Value == 4)
            {
                timerSimulacion.Interval = 125;
                labelVelocidad.Text = "x8";
            }
            if (trackBarVelSimulacion.Value == 5)
            {
                timerSimulacion.Interval = 62;
                labelVelocidad.Text = "x16";
            }

            if (trackBarVelSimulacion.Value == 6)
            {
                timerSimulacion.Interval = 31;
                labelVelocidad.Text = "x32";
            }

            if (trackBarVelSimulacion.Value == 7)
            {
                timerSimulacion.Interval = 16;
                labelVelocidad.Text = "x64";
            }

            if (trackBarVelSimulacion.Value == 8)
            {
                timerSimulacion.Interval = 8;
                labelVelocidad.Text = "x128";
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

                double bps = double.NaN;
                double altitudPresionFt = double.NaN;
                double altitudQNHFt = double.NaN;
                double qnhUtilizado = double.NaN;

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

                        bps = mensaje21.BPS;
                        altitudPresionFt = mensaje21.altitudPresionFt;
                        altitudQNHFt = mensaje21.altitudQNHFt;
                        qnhUtilizado = mensaje21.qnhUtilizado;

                        // Utilizar la altitud corregida cuando esté disponible
                        if (!double.IsNaN(altitudQNHFt))
                        {
                            altitud = altitudQNHFt;
                        }
                        else
                        {
                            altitud = altitudPresionFt;
                        }
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

                        bps = mensaje48.BPS;
                        altitudPresionFt = mensaje48.altitudPresionFt;
                        altitudQNHFt = mensaje48.altitudQNHFt;
                        qnhUtilizado = mensaje48.qnhUtilizado;

                        // Usar la altitud corregida cuando esté disponible
                        altitud = !double.IsNaN(altitudQNHFt)
                            ? altitudQNHFt
                            : altitudPresionFt;
                    }

                    esRadar = true;
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
                        mensajeVisual.bps = bps;
                        mensajeVisual.altitudPresionFt = altitudPresionFt;
                        mensajeVisual.altitudQNHFt = altitudQNHFt;
                        mensajeVisual.qnhUtilizado = qnhUtilizado;

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
                        nuevoAvion.bps = bps;
                        nuevoAvion.altitudPresionFt = altitudPresionFt;
                        nuevoAvion.altitudQNHFt = altitudQNHFt;
                        nuevoAvion.qnhUtilizado = qnhUtilizado;

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

                        nuevoAvion.mensajeOriginal = mensaje;
                        if (mensaje.categoria == 21)
                            nuevoAvion.ultimoMensajeCAT21 = mensaje;
                        else if (mensaje.categoria == 48)
                            nuevoAvion.ultimoMensajeCAT48 = mensaje;
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
                            if (esADSB || esRadar)
                            {
                                avionEncontrado.bps = bps;
                                avionEncontrado.altitudPresionFt = altitudPresionFt;
                                avionEncontrado.altitudQNHFt = altitudQNHFt;
                                avionEncontrado.qnhUtilizado = qnhUtilizado;
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
                            avionEncontrado.mensajeOriginal = mensaje;
                            if (mensaje.categoria == 21)
                                avionEncontrado.ultimoMensajeCAT21 = mensaje;
                            else if (mensaje.categoria == 48)
                                avionEncontrado.ultimoMensajeCAT48 = mensaje;

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
            double ultimoTiempo = double.MinValue;

            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double tiempo = double.NaN;

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

                if (double.IsNaN(tiempo) ||
                    double.IsInfinity(tiempo) ||
                    tiempo < 0)
                {
                    continue;
                }

                if (tiempo < primerTiempo)
                    primerTiempo = tiempo;

                if (tiempo > ultimoTiempo)
                    ultimoTiempo = tiempo;
            }

            if (primerTiempo != double.MaxValue)
            {
                tiempoInicioSimulacion = Math.Floor(primerTiempo);
                tiempoFinSimulacion = Math.Ceiling(ultimoTiempo);
                trackBarTiempo.Minimum = 0;

                trackBarTiempo.Maximum = (int)(
                    tiempoFinSimulacion - tiempoInicioSimulacion
                );

                trackBarTiempo.Value = 0;
                barraTiempoNueva.TiempoMinimo = trackBarTiempo.Minimum;
                barraTiempoNueva.TiempoMaximo = trackBarTiempo.Maximum;
                barraTiempoNueva.TiempoValor = trackBarTiempo.Value;

                TiempoActual = tiempoInicioSimulacion;

                TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
                label1.Text = hora.ToString(@"hh\:mm\:ss");
                ActualizarHoraTextBox();

                TimeSpan duracion = TimeSpan.FromSeconds(
                    tiempoFinSimulacion - tiempoInicioSimulacion
                );
                TimeSpan horaFinal = TimeSpan.FromSeconds(tiempoFinSimulacion);

                labelTiempoFinal.Text = horaFinal.ToString(@"hh\:mm\:ss");
            }
        }
        private void trackBarTiempo_Scroll(object sender, EventArgs e)
{
    if (ListaMensajesFiltrados.Count == 0)
        return;

    // Pausar la simulación
    timerSimulacion.Stop();
    Reproduciendo = false;
    pictureBox1.Image = Properties.Resources.botonReproducir;

    // Limpiar los datos anteriores
    AvionesActuales.Clear();
    MensajesActuales.Clear();

    historialAviones.Clear();
    historialTiempo.Clear();

    avionesTrayectoriaSeleccionados.Clear();

    capaAviones.Markers.Clear();
    capaTrayectorias.Routes.Clear();

    // Obtener el tiempo seleccionado
    TiempoActual = tiempoInicioSimulacion + trackBarTiempo.Value;

    // Actualizar la hora
    TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
    label1.Text = hora.ToString(@"hh\:mm\:ss");


    // Actualizar los mensajes visibles de los últimos 10 segundos
    ActualizarMensajesActuales();

    // Actualizar la interfaz
    ActualizarDataGrid();
    ActualizarMapa();
    MostrarTrayectorias();

    ActualizarHeatmapDinamico(true);
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
            ActualizarHoraTextBox();

            ActualizarAviones();
            ActualizarDataGrid();
            ActualizarMapa();

            pictureBox1.Image = Properties.Resources.botonReproducir;

            capaTrayectorias.Routes.Clear();

            densidadesDinamicas.Clear();
            siguientePuntoHeatmap = 0;
            ultimoTiempoHeatmap = -1;

            ActualizarHeatmapDinamico(true);
        }

        private async void cargaDatosrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog explorador = new OpenFileDialog())
            {
                explorador.Title = "Seleccionar archivo ASTERIX";

                if (explorador.ShowDialog() != DialogResult.OK)
                    return;

                try
                {

                    List<Mensaje> nuevosMensajes;

                    using (FormCargando cargando = new FormCargando())
                    {
                        cargando.Show(this);
                        cargando.ActualizarProgreso(0);

                        IProgress<int> progreso = new Progress<int>(porcentaje =>
                        {
                            if (!cargando.IsDisposed)
                                cargando.ActualizarProgreso(porcentaje);
                        });

                        try
                        {
                            nuevosMensajes = await Task.Run(() =>
                            {
                                LeerDatos lector = new LeerDatos();

                                return lector.DatosProcesados(
                                    explorador.FileName,
                                    progreso
                                );
                            });
                        }
                        finally
                        {
                            cargando.Close();
                        }
                    }

                    if (nuevosMensajes == null || nuevosMensajes.Count == 0)
                    {
                        MessageBox.Show("El archivo no contiene mensajes válidos.");
                        return;
                    }

                    // Detener la simulación anterior
                    timerSimulacion.Stop();
                    Reproduciendo = false;

                    pictureBox1.Image =
                        Properties.Resources.botonReproducir;

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
                    InvalidarHeatmap();

                    PrepararPuntosHeatmap();

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
                    ActualizarHoraTextBox();

                    // Actualizar todo con los datos nuevos
                    ActualizarAviones();
                    ActualizarDataGrid();
                    ActualizarMapa();
                    MostrarTrayectorias();

                    ActualizarHeatmapDinamico(true);

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


                    double rumbo = mensaje.rumbo;

                    if (avionReal != null && !double.IsNaN(avionReal.rumbo))
                    {
                        rumbo = avionReal.rumbo;
                    }

                    if (double.IsNaN(rumbo))
                    {
                        rumbo = 0;
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

        private void GMapControl1_OnMarkerClick(GMapMarker item,MouseEventArgs e)
        {
            AvionSimulacion mensaje = item.Tag as AvionSimulacion;

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
                AvionSimulacion avionReal = BuscarAvionSeleccionado(mensaje);

                if (avionReal == null)
                    return;

                InformacionAvion ventana = new InformacionAvion();

                ventana.avion = avionReal;

                ventana.categoriaSeleccionada =
                    mensaje.detectadoRadar ? 48 : 21;

                ventana.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Reproduciendo == false)
            {
                Reproduciendo = true;
                pictureBox1.Image = Properties.Resources.botonPausa;
                timerSimulacion.Start();
            }
            else if (Reproduciendo == true)
            {
                Reproduciendo = false;
                timerSimulacion.Stop();
                pictureBox1.Image = Properties.Resources.botonReproducir;
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
                ActualizarHoraTextBox();

                ActualizarDataGrid();
                ActualizarMapa();
                MostrarTrayectorias();

                ActualizarHeatmapDinamico(true);
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

                double bps = double.NaN;
                double altitudPresionFt = double.NaN;
                double altitudQNHFt = double.NaN;
                double qnhUtilizado = double.NaN;

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

                    altitud = !double.IsNaN(mensaje21.altitudQNHFt)
                                 ? mensaje21.altitudQNHFt
                                 : mensaje21.altitudPresionFt;
                    bps = mensaje21.BPS;
                    altitudPresionFt = mensaje21.altitudPresionFt;
                    altitudQNHFt = mensaje21.altitudQNHFt;
                    qnhUtilizado = mensaje21.qnhUtilizado;

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

                        bps = mensaje48.BPS;
                        altitudPresionFt = mensaje48.altitudPresionFt;
                        altitudQNHFt = mensaje48.altitudQNHFt;
                        qnhUtilizado = mensaje48.qnhUtilizado;

                        altitud = !double.IsNaN(altitudQNHFt)
                            ? altitudQNHFt
                            : altitudPresionFt;
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
                    mensajeVisual.bps = bps;
                    mensajeVisual.altitudPresionFt = altitudPresionFt;
                    mensajeVisual.altitudQNHFt = altitudQNHFt;
                    mensajeVisual.qnhUtilizado = qnhUtilizado;

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
            foreach (AvionSimulacion avion in MensajesActuales)
            {
                if (!double.IsNaN(avion.rumbo))
                    continue;

                double mejorTiempo = double.MinValue;
                double latitudAnterior = double.NaN;
                double longitudAnterior = double.NaN;

                foreach (Mensaje mensaje in ListaMensajesFiltrados)
                {
                    double tiempo;
                    string direccion;
                    double latitud;
                    double longitud;

                    if (mensaje.categoria == 21)
                    {
                        TiraDatosDecod021 datos = (TiraDatosDecod021)mensaje.tira;

                        tiempo = datos.TimeReceptionPosition;
                        direccion = datos.TargetAddress;
                        latitud = datos.PosWGS84HighRes[0];
                        longitud = datos.PosWGS84HighRes[1];
                    }
                    else if (mensaje.categoria == 48)
                    {
                        TiraDatosDecod048 datos = (TiraDatosDecod048)mensaje.tira;

                        tiempo = datos.TimeOfDay;
                        direccion = datos.AircrftAddrs;
                        latitud = datos.PositionCorrectedLatLonAlt[0];
                        longitud = datos.PositionCorrectedLatLonAlt[1];
                    }
                    else
                    {
                        continue;
                    }

                    if (direccion != avion.direccion ||
                        tiempo >= avion.ultimoTiempo ||
                        tiempo <= mejorTiempo)
                    {
                        continue;
                    }

                    if (double.IsNaN(latitud) || double.IsNaN(longitud))
                        continue;

                    mejorTiempo = tiempo;
                    latitudAnterior = latitud;
                    longitudAnterior = longitud;
                }

                if (!double.IsNaN(latitudAnterior))
                {
                    avion.rumbo = CalcularRumbo(
                        latitudAnterior,
                        longitudAnterior,
                        avion.latitud,
                        avion.longitud
                    );
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
                if (nuevoMensaje.detectadoADSB || nuevoMensaje.detectadoRadar)
                {
                    mensajeAnterior.bps = nuevoMensaje.bps;
                    mensajeAnterior.altitudPresionFt = nuevoMensaje.altitudPresionFt;
                    mensajeAnterior.altitudQNHFt = nuevoMensaje.altitudQNHFt;
                    mensajeAnterior.qnhUtilizado = nuevoMensaje.qnhUtilizado;
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

        private bool filtroBlancoPuro = false;
        private bool filtroTransFijo = false;
        private bool filtroGround = false;

        private bool filtroCat21 = true;
        private bool filtroCat48 = true;

        private bool filtroTrayectoria = false;
        private double filtroTrayectoriaMin = 0;
        private double filtroTrayectoriaMax = 360;

        private bool filtroAltura = false;
        private double filtroAlturaMin = 0;
        private double filtroAlturaMax = 999;
        private void aplicarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFiltro ventana = new FormFiltro(
                filtroBlancoPuro,
                filtroTransFijo,
                filtroGround,
                filtroCat21,
                filtroCat48,
                filtroTrayectoria,
                filtroTrayectoriaMin,
                filtroTrayectoriaMax
            );

            if (ventana.ShowDialog() == DialogResult.OK)
            {
                // Guardar los valores seleccionados
                filtroBlancoPuro = ventana.BlancoPuro;
                filtroTransFijo = ventana.transFijo;
                filtroGround = ventana.ground;

                filtroCat21 = ventana.Cat21;
                filtroCat48 = ventana.Cat48;

                filtroTrayectoria = ventana.FiltrarTrayectoria;
                filtroTrayectoriaMin = ventana.trayectoriaMin;
                filtroTrayectoriaMax = ventana.trayectoriaMax;

                // Aplicar filtros
                Filtros gestorFiltros = new Filtros();

                ListaMensajesFiltrados = gestorFiltros.AplicarFiltros(
                    ListaMensajes,
                    filtroCat48,
                    filtroCat21,
                    filtroBlancoPuro,
                    filtroTransFijo,
                    filtroTrayectoria,
                    filtroTrayectoriaMin,
                    filtroTrayectoriaMax,
                    filtroGround
                );

                ReconstruirAvionesFiltrados();
                InvalidarHeatmap();

                PrepararPuntosHeatmap();
                ActualizarHeatmapDinamico(true);
            }
        }

        private void limpiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaMensajesFiltrados = new List<Mensaje>(ListaMensajes);
            InvalidarHeatmap();

            PrepararPuntosHeatmap();
            ActualizarHeatmapDinamico(true);

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

            List<AvionSimulacion> avionesMostrar = mostrarTodasTrayectorias
                ? AvionesActuales
                : avionesTrayectoriaSeleccionados;

            foreach (AvionSimulacion avion in avionesMostrar)
            {
                if (avion.trayectoria == null || avion.trayectoria.Count < 2)
                {
                    continue;
                }

                List<PointLatLng> puntos = new List<PointLatLng>();

                foreach (double[] punto in avion.trayectoria)
                {
                    puntos.Add(new PointLatLng(punto[0], punto[1]));
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

            double tiempoInicial = tiempoInicioSimulacion;

            // Reconstruir desde el inicio hasta el instante actual
            for (double tiempo = tiempoInicial; tiempo <= tiempoOriginal; tiempo++)
            {
                TiempoActual = tiempo;
                ActualizarAviones();
                EliminarAvionesInactivos();
            }

            // Recuperar el tiempo en el que estaba la simulación
            // Recuperar el tiempo seleccionado
            TiempoActual = tiempoOriginal;

            // Actualizar la posición de la barra
            trackBarTiempo.Value = (int)(
                TiempoActual - tiempoInicioSimulacion
            );

            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);
            label1.Text = hora.ToString(@"hh\:mm\:ss");
            ActualizarHoraTextBox();

            ActualizarMensajesActuales();

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

            double nuevoTiempo = horas * 3600 + minutos * 60 + segundos;

            if (nuevoTiempo < tiempoInicioSimulacion ||
                nuevoTiempo > tiempoFinSimulacion)
            {
                MessageBox.Show("La hora está fuera del intervalo de la simulación.");
                return;
            }

            bool estabaReproduciendo = Reproduciendo;

            // Actualizar la barra de reproducción
            trackBarTiempo.Value = Math.Max(
                trackBarTiempo.Minimum,
                Math.Min(
                    trackBarTiempo.Maximum,
                    (int)(nuevoTiempo - tiempoInicioSimulacion)
                )
            );

            barraTiempoNueva.TiempoValor = trackBarTiempo.Value;

            // Aprovechar el método existente para actualizar mapa y datos
            trackBarTiempo_Scroll(sender, EventArgs.Empty);

            // Mostrar la hora introducida
            textBoxHora.Text = horas.ToString("00");
            textBoxMinuto.Text = minutos.ToString("00");
            textBoxSegundo.Text = segundos.ToString("00");

            // Mantener la reproducción si estaba activa
            if (estabaReproduciendo)
            {
                Reproduciendo = true;
                pictureBox1.Image = Properties.Resources.botonPausa;
                timerSimulacion.Start();
            }
        }

        private void mostrarDatosEnCeldasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridAviones.BringToFront();
            dataGridAviones.Visible = !dataGridAviones.Visible;
            mostrarDatosEnCeldasToolStripMenuItem.Checked = dataGridAviones.Visible;
        }

        private void trayectoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mostrarTodasTrayectorias = !mostrarTodasTrayectorias;

            trayectoriasToolStripMenuItem.Checked =
                mostrarTodasTrayectorias;

            if (mostrarTodasTrayectorias)
            {
                // Mostrar todas las trayectorias
                MostrarTrayectorias();
            }
            else
            {
                // Quitar todas las trayectorias
                avionesTrayectoriaSeleccionados.Clear();
                capaTrayectorias.Routes.Clear();
                gMapControl1.Refresh();
            }
        }

        private void mostrarHeatmapCompletoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mostrarHeatmapDinamico = false;
            mostrarHeatmapDinamicoToolStripMenuItem.Checked = false;

            mostrarHeatmap = !mostrarHeatmap;
            mostrarHeatmapCompletoToolStripMenuItem.Checked = mostrarHeatmap;
            if (mostrarHeatmap)
            {
                CalcularHeatmapCompleto();
            }
            capaHeatmap.IsVisibile = mostrarHeatmap;
            gMapControl1.Refresh();
        }


        private void CalcularHeatmapCompleto()
        {
            capaHeatmap.Markers.Clear();

            // Tamaño aproximado de las celdas:
            // 0,005 grados de latitud equivale a unos 555 metros.
            const double tamanoCelda = 0.0005;

            // Cada celda guarda los identificadores de las aeronaves
            // que han pasado por ella.
            Dictionary<(int, int), HashSet<string>> densidades =
                new Dictionary<(int, int), HashSet<string>>();

            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double latitud;
                double longitud;
                string identificadorAvion;

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 datos =
                        (TiraDatosDecod021)mensaje.tira;

                    if (datos.PosWGS84HighRes == null ||
                        datos.PosWGS84HighRes.Count < 2)
                        continue;

                    latitud = datos.PosWGS84HighRes[0];
                    longitud = datos.PosWGS84HighRes[1];

                    identificadorAvion = datos.TargetAddress;

                    if (string.IsNullOrWhiteSpace(identificadorAvion))
                        identificadorAvion = datos.TargetIdentification;
                }
                else if (mensaje.categoria == 48)
                {
                    TiraDatosDecod048 datos =
                        (TiraDatosDecod048)mensaje.tira;

                    bool trackSinIdentificar =
                        string.IsNullOrEmpty(datos.AircrftAddrs) &&
                        string.IsNullOrEmpty(datos.AircrftIddent) &&
                        datos.TargetRep.TYP == "001";

                    if (datos.TrckStatus.CNF == 1 || trackSinIdentificar)
                        continue;

                    if (datos.PositionCorrectedLatLonAlt == null ||
                        datos.PositionCorrectedLatLonAlt.Count < 2)
                        continue;

                    latitud = datos.PositionCorrectedLatLonAlt[0];
                    longitud = datos.PositionCorrectedLatLonAlt[1];

                    identificadorAvion = datos.AircrftAddrs;

                    if (string.IsNullOrWhiteSpace(identificadorAvion))
                        identificadorAvion = datos.AircrftIddent;
                }
                else
                {
                    continue;
                }

                if (double.IsNaN(latitud) ||
                    double.IsNaN(longitud) ||
                    double.IsInfinity(latitud) ||
                    double.IsInfinity(longitud))
                    continue;

                // Ignorar posiciones sin identificación fiable.
                if (string.IsNullOrWhiteSpace(identificadorAvion))
                    continue;

                int fila = (int)Math.Floor(latitud / tamanoCelda);
                int columna = (int)Math.Floor(longitud / tamanoCelda);

                var clave = (fila, columna);

                if (!densidades.TryGetValue(clave, out HashSet<string> aviones))
                {
                    aviones = new HashSet<string>();
                    densidades.Add(clave, aviones);
                }

                aviones.Add(identificadorAvion);
            }
            DibujarHeatmap(densidades);
        }

        private void DibujarHeatmap(
            Dictionary<(int, int), HashSet<string>> densidades)
        {
            capaHeatmap.Markers.Clear();

            if (densidades.Count == 0)
            {
                gMapControl1.Refresh();
                return;
            }

            Dictionary<(int, int), double> densidadesSuavizadas =
                SuavizarHeatmap(densidades);

            double maximo = densidadesSuavizadas.Values.Max();

            foreach (var celda in densidadesSuavizadas)
            {
                double cantidad = celda.Value;

                double intensidad = Math.Log(1.0 + cantidad) /
                                   Math.Log(1.0 + maximo);

                Color color;

                if (intensidad < 0.33)
                {
                    double t = intensidad / 0.33;

                    color = Color.FromArgb(
                        110, (int)(255 * t), 220, 0);
                }
                else if (intensidad < 0.66)
                {
                    double t = (intensidad - 0.33) / 0.33;

                    color = Color.FromArgb(
                        130, 255, (int)(220 - 100 * t), 0);
                }
                else
                {
                    double t = (intensidad - 0.66) / 0.34;

                    color = Color.FromArgb(
                        160, 255, (int)(120 * (1 - t)), 0);
                }

                double latMin = celda.Key.Item1 * tamanoCeldaHeatmap;
                double lonMin = celda.Key.Item2 * tamanoCeldaHeatmap;

                CeldaHeatmap marcador = new CeldaHeatmap(
                    latMin,
                    latMin + tamanoCeldaHeatmap,
                    lonMin,
                    lonMin + tamanoCeldaHeatmap,
                    color
                );

                capaHeatmap.Markers.Add(marcador);
            }

            gMapControl1.Refresh();
        }

        private void InvalidarHeatmap()
        {
            if (capaHeatmap == null)
                return;

            capaHeatmap.Markers.Clear();

            if (mostrarHeatmap)
                CalcularHeatmapCompleto();
        }

        private Dictionary<(int, int), double> SuavizarHeatmap(
            Dictionary<(int, int), HashSet<string>> densidades)
        {
            Dictionary<(int, int), double> suavizadas =
                new Dictionary<(int, int), double>();

            int radio = 4;
            double sigma = 2.5;

            foreach (var celda in densidades)
            {
                int fila = celda.Key.Item1;
                int columna = celda.Key.Item2;

                double suma = 0;
                double sumaPesos = 0;

                for (int i = -radio; i <= radio; i++)
                {
                    for (int j = -radio; j <= radio; j++)
                    {
                        double distancia2 = i * i + j * j;

                        double peso = Math.Exp(
                            -distancia2 / (2 * sigma * sigma)
                        );

                        var vecino = (fila + i, columna + j);

                        if (densidades.TryGetValue(
                            vecino, out HashSet<string> aviones))
                        {
                            suma += aviones.Count * peso;
                        }

                        sumaPesos += peso;
                    }
                }

                suavizadas[celda.Key] = suma / sumaPesos;
            }

            return suavizadas;
        }

        private void ActualizarHeatmapDinamico(bool forzar = false)
        {
            if (!mostrarHeatmapDinamico)
                return;

            if (!forzar &&
                ultimoTiempoHeatmap >= 0 &&
                TiempoActual >= ultimoTiempoHeatmap &&
                TiempoActual - ultimoTiempoHeatmap < intervaloHeatmap)
            {
                return;
            }
            CalcularDensidadesDinamicas();
            DibujarHeatmap(densidadesDinamicas);

            ultimoTiempoHeatmap = TiempoActual;
        }


        private class PuntoHeatmap
        {
            public double tiempo;
            public double latitud;
            public double longitud;
            public string identificador;
        }
        private List<PuntoHeatmap> puntosHeatmap = new List<PuntoHeatmap>();
        private Dictionary<(int, int), HashSet<string>> densidadesDinamicas = new Dictionary<(int, int), HashSet<string>>();
        private int siguientePuntoHeatmap = 0;
        private const double tamanoCeldaHeatmap = 0.0005;


        private void PrepararPuntosHeatmap()
        {
            puntosHeatmap.Clear();

            foreach (Mensaje mensaje in ListaMensajesFiltrados)
            {
                double tiempo;
                double latitud;
                double longitud;
                string identificador;

                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 datos =
                        (TiraDatosDecod021)mensaje.tira;

                    if (datos.PosWGS84HighRes == null ||
                        datos.PosWGS84HighRes.Count < 2)
                        continue;

                    tiempo = datos.TimeReceptionPosition;
                    latitud = datos.PosWGS84HighRes[0];
                    longitud = datos.PosWGS84HighRes[1];

                    identificador = datos.TargetAddress;

                    if (string.IsNullOrWhiteSpace(identificador))
                        identificador = datos.TargetIdentification;
                }
                else if (mensaje.categoria == 48)
                {
                    TiraDatosDecod048 datos =
                        (TiraDatosDecod048)mensaje.tira;

                    bool trackSinIdentificar =
                        string.IsNullOrEmpty(datos.AircrftAddrs) &&
                        string.IsNullOrEmpty(datos.AircrftIddent) &&
                        datos.TargetRep.TYP == "001";

                    if (datos.TrckStatus.CNF == 1 || trackSinIdentificar)
                        continue;

                    if (datos.PositionCorrectedLatLonAlt == null ||
                        datos.PositionCorrectedLatLonAlt.Count < 2)
                        continue;

                    tiempo = datos.TimeOfDay;
                    latitud = datos.PositionCorrectedLatLonAlt[0];
                    longitud = datos.PositionCorrectedLatLonAlt[1];

                    identificador = datos.AircrftAddrs;

                    if (string.IsNullOrWhiteSpace(identificador))
                        identificador = datos.AircrftIddent;
                }
                else
                {
                    continue;
                }

                if (double.IsNaN(tiempo) ||
                    double.IsInfinity(tiempo) ||
                    tiempo < 0 ||
                    double.IsNaN(latitud) ||
                    double.IsInfinity(latitud) ||
                    double.IsNaN(longitud) ||
                    double.IsInfinity(longitud) ||
                    string.IsNullOrWhiteSpace(identificador))
                {
                    continue;
                }

                puntosHeatmap.Add(new PuntoHeatmap
                {
                    tiempo = tiempo,
                    latitud = latitud,
                    longitud = longitud,
                    identificador = identificador
                });
            }

            puntosHeatmap.Sort((a, b) =>
                a.tiempo.CompareTo(b.tiempo));

            densidadesDinamicas.Clear();
            siguientePuntoHeatmap = 0;
            ultimoTiempoHeatmap = -1;
        }

        private void CalcularDensidadesDinamicas()
        {
            // Si hemos retrocedido, reconstruir desde el principio
            if (ultimoTiempoHeatmap >= 0 &&
                TiempoActual < ultimoTiempoHeatmap)
            {
                densidadesDinamicas.Clear();
                siguientePuntoHeatmap = 0;
            }

            // Incorporar solo las posiciones hasta TiempoActual
            while (siguientePuntoHeatmap < puntosHeatmap.Count)
            {
                PuntoHeatmap punto =
                    puntosHeatmap[siguientePuntoHeatmap];

                if (punto.tiempo > TiempoActual)
                    break;

                int fila = (int)Math.Floor(
                    punto.latitud / tamanoCeldaHeatmap);

                int columna = (int)Math.Floor(
                    punto.longitud / tamanoCeldaHeatmap);

                var clave = (fila, columna);

                if (!densidadesDinamicas.TryGetValue(
                    clave, out HashSet<string> aviones))
                {
                    aviones = new HashSet<string>();
                    densidadesDinamicas.Add(clave, aviones);
                }

                aviones.Add(punto.identificador);

                siguientePuntoHeatmap++;
            }
        }


        private void mostrarHeatmapDinamicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            mostrarHeatmapDinamico = !mostrarHeatmapDinamico;

            if (mostrarHeatmapDinamico)
            {
                // Desactivar el modo completo
                mostrarHeatmap = false;
                mostrarHeatmapCompletoToolStripMenuItem.Checked = false;

                mostrarHeatmapDinamicoToolStripMenuItem.Checked = true;

                capaHeatmap.IsVisibile = true;

                PrepararPuntosHeatmap();
                ActualizarHeatmapDinamico(true);
            }
            else
            {
                mostrarHeatmapDinamicoToolStripMenuItem.Checked = false;

                capaHeatmap.Markers.Clear();
                capaHeatmap.IsVisibile = false;

                ultimoTiempoHeatmap = -1;
            }

            gMapControl1.Refresh();
        }
        private void AjustarMapa()
        {
            int margenDerecho = 25;
            int margenInferior = 70;

            gMapControl1.Width =
                this.ClientSize.Width - gMapControl1.Left - margenDerecho;

            gMapControl1.Height =
                this.ClientSize.Height - gMapControl1.Top - margenInferior;
        }

        private void trackBarTiempo_MouseUp(object sender, MouseEventArgs e)
        {
            // Guardar si la simulación estaba reproduciéndose
            bool estabaReproduciendo = Reproduciendo;

            // Actualizar la simulación al tiempo seleccionado
            trackBarTiempo_Scroll(sender, EventArgs.Empty);

            // Recuperar el estado de reproducción
            if (estabaReproduciendo)
            {
                Reproduciendo = true;
                pictureBox1.Image = Properties.Resources.botonPausa;
                timerSimulacion.Start();
            }
        }
        private void ActualizarHoraTextBox()
        {
            // No sobrescribir la hora mientras el usuario la está editando
            if (textBoxHora.Focused ||
                textBoxMinuto.Focused ||
                textBoxSegundo.Focused)
            {
                return;
            }

            TimeSpan hora = TimeSpan.FromSeconds(TiempoActual);

            textBoxHora.Text = ((int)hora.TotalHours).ToString("00");
            textBoxMinuto.Text = hora.Minutes.ToString("00");
            textBoxSegundo.Text = hora.Seconds.ToString("00");
        }
        private void HoraTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonSimularDesdeHora_Click(sender, EventArgs.Empty);
                this.ActiveControl = null;
            }
        }
    }
}
