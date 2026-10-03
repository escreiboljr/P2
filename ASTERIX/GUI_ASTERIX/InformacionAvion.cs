using Simulacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class InformacionAvion : Form
    {
        public AvionSimulacion avion;
        public InformacionAvion()
        {
            InitializeComponent();
            timerActualizar.Interval = 500;
            timerActualizar.Tick += timerActualizar_Tick;
            timerActualizar.Start();
        }

        private void InformacionAvion_Load(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void timerActualizar_Tick(object sender, EventArgs e)
        {
            ActualizarDatos();
        }
        private void ActualizarDatos()
        {
            if (avion != null)
            {
                // IDENTIFICADOR
                labelIdentificador.Text = !string.IsNullOrWhiteSpace(avion.identificador)
                    ? "Identificador: " + avion.identificador
                    : "Identificador: N/A";

                // POSICIÓN
                labelLatitud.Text = !double.IsNaN(avion.latitud)
                    ? "Latitud: " + avion.latitud.ToString("0.0000")
                    : "Latitud: N/A";

                labelLongitud.Text = !double.IsNaN(avion.longitud)
                    ? "Longitud: " + avion.longitud.ToString("0.0000")
                    : "Longitud: N/A";

                labelAltitud.Text = !double.IsNaN(avion.altitud)
                    ? "Altitud: " + avion.altitud.ToString("0.##")
                    : "Altitud: N/A";

                // VUELO
                labelFL.Text = !double.IsNaN(avion.flightLevel)
                    ? "Flight Level: " + avion.flightLevel.ToString("0.##")
                    : "Flight Level: N/A";

                labelDireccion.Text = !string.IsNullOrWhiteSpace(avion.direccion)
                    ? "Dirección: " + avion.direccion
                    : "Dirección: N/A";

                labelRumbo.Text =
                    "Rumbo: " + avion.rumbo.ToString("0.##");

                labelTrackNumber.Text = avion.trackNumber >= 0
                    ? "Track Number: " + avion.trackNumber
                    : "Track Number: N/A";

                // DETECCIÓN
                labelRadar.Text =
                    "Radar: " + (avion.detectadoRadar ? "Sí" : "No");

                labelADSB.Text =
                    "ADS-B: " + (avion.detectadoADSB ? "Sí" : "No");

                labelUltimoTiempo.Text =
                "Última detección: " +
                TimeSpan.FromSeconds(avion.ultimoTiempo).ToString(@"hh\:mm\:ss");

                labelUltimoTiempoRadar.Text = avion.ultimoTiempoRadar >= 0
                    ? "Última detección radar: " +
                      TimeSpan.FromSeconds(avion.ultimoTiempoRadar).ToString(@"hh\:mm\:ss")
                    : "Última detección radar: N/A";

                labelUltimoTiempoADSB.Text = avion.ultimoTiempoADSB >= 0
                    ? "Última detección ADS-B: " +
                      TimeSpan.FromSeconds(avion.ultimoTiempoADSB).ToString(@"hh\:mm\:ss")
                    : "Última detección ADS-B: N/A";
                labelVelocidad.Text = !double.IsNaN(avion.velocidad)
                    ? "Velocidad: " + avion.velocidad.ToString("0.##") + " kt"
                    : "Velocidad: N/A";

                labelEstadoVuelo.Text = !string.IsNullOrWhiteSpace(avion.estadoVuelo)
                    ? "Estado: " + avion.estadoVuelo
                    : "Estado: N/A";

                labelMode3A.Text = !string.IsNullOrWhiteSpace(avion.mode3A)
                    ? "Mode 3/A: " + avion.mode3A
                    : "Mode 3/A: N/A";
            }
            else
            {
                labelIdentificador.Text = "Identificador: N/A";

                labelLatitud.Text = "Latitud: N/A";
                labelLongitud.Text = "Longitud: N/A";
                labelAltitud.Text = "Altitud: N/A";

                labelFL.Text = "Flight Level: N/A";
                labelDireccion.Text = "Dirección: N/A";
                labelRumbo.Text = "Rumbo: N/A";
                labelTrackNumber.Text = "Track Number: N/A";

                labelRadar.Text = "Radar: N/A";
                labelADSB.Text = "ADS-B: N/A";
                labelUltimoTiempo.Text = "Última detección: N/A";
                labelUltimoTiempoRadar.Text = "Última detección Radar: N/A";
                labelUltimoTiempoADSB.Text = "Última detección ADS-B: N/A";
            }
        }
    }
}
