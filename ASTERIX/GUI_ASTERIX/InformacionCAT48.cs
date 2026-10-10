using Archivos;
using DatosDecod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class InformacionCAT48 : Form
    {
        public Mensaje mensaje;
        public InformacionCAT48()
        {
            InitializeComponent();
        }
        private void InformacionCAT48_Load(object sender, EventArgs e)
        {
            TiraDatosDecod048 datos = (TiraDatosDecod048)mensaje.tira;
            labelIdentificador.Text = !string.IsNullOrWhiteSpace(datos.AircrftIddent)
                    ? "Identificador: " + datos.AircrftIddent
                    : "Identificador: N/A";
        }
    }
}
