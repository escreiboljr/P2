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
                    "Último tiempo: " + avion.ultimoTiempo.ToString("0.##");

                labelUltimoTiempoRadar.Text = avion.ultimoTiempoRadar >= 0
                    ? "Último tiempo Radar: " + avion.ultimoTiempoRadar.ToString("0.##")
                    : "Último tiempo Radar: N/A";

                labelUltimoTiempoADSB.Text = avion.ultimoTiempoADSB >= 0
                    ? "Último tiempo ADS-B: " + avion.ultimoTiempoADSB.ToString("0.##")
                    : "Último tiempo ADS-B: N/A";
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
                labelUltimoTiempo.Text = "Último tiempo: N/A";
                labelUltimoTiempoRadar.Text = "Último tiempo Radar: N/A";
                labelUltimoTiempoADSB.Text = "Último tiempo ADS-B: N/A";
            }
        }
    }
}
