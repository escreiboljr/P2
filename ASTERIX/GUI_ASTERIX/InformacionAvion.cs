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
                if (!string.IsNullOrWhiteSpace(avion.identificador))
                {
                    labelIdentificador.Text =
                        "Identificador: " + avion.identificador;
                }
                else
                {
                    labelIdentificador.Text = "Identificador: N/A";
                }

                if (!double.IsNaN(avion.flightLevel))
                {
                    labelFL.Text =
                        "Flight Level: " + avion.flightLevel.ToString("0.##");
                }
                else
                {
                    labelFL.Text = "Flight Level: N/A";
                }
            }
            else
            {
                labelIdentificador.Text = "Identificador: N/A";
                labelFL.Text = "Flight Level: N/A";
            }
        }
    }
}
