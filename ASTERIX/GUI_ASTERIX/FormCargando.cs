using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class FormCargando : Form
    {
        public FormCargando()
        {
            InitializeComponent();
        }

        private void FormCargando_Load(object sender, EventArgs e)
        {

        }
        public void ActualizarProgreso(int porcentaje)
        {
            porcentaje = Math.Max(0, Math.Min(100, porcentaje));

            progressBarCarga.Value = porcentaje;
            labelPorcentaje.Text = porcentaje + "%";
        }
    }
}
