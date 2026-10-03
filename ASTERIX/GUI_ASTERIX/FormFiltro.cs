using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class FormFiltro : Form
    {
        bool blancoPuro;
        bool TransFijo;
        bool Ground;
        bool cat48;
        bool cat21;
        double TrayectoriaMin;
        double TrayectoriaMax;

        public bool BlancoPuro
        {
            get { return blancoPuro; }
        }
        public bool transFijo
        {
            get { return TransFijo; }
        }
        public bool ground
        {
            get { return Ground; }
        }
        public bool Cat48
        {
            get { return cat48; }
        }
        public bool Cat21
        {
            get { return cat21; }
        }
        public double trayectoriaMin
        { get { return TrayectoriaMin; } }
        public double trayectoriaMax
        { get { return TrayectoriaMax; } }
        bool filtrarTrayectoria { get; set; }
        public bool FiltrarTrayectoria
        { get { return  filtrarTrayectoria; }  }
        public FormFiltro()
        {
            InitializeComponent();
        }

        private void pictureBoxSlideButtonBlancoPuro_Click(object sender, EventArgs e)
        {
            if (blancoPuro == false)
            {
                pictureBoxSlideButtonBlancoPuro.Image = Properties.Resources.sliderActivado;
                blancoPuro = true;
            }
            else
            {
                pictureBoxSlideButtonBlancoPuro.Image = Properties.Resources.sliderApagado;
                blancoPuro = false;
            }
        }

        private void FormFiltro_Load(object sender, EventArgs e)
        {
            blancoPuro = false;
            TransFijo = false;
            Ground = false;
            cat21 = checkBoxCat21.Checked;
            cat48 = checkBoxCat48.Checked;
        }

        private void pictureBoxSlideButtonTransFijo_Click(object sender, EventArgs e)
        {
            if (TransFijo == false)
            {
                pictureBoxSlideButtonTransFijo.Image = Properties.Resources.sliderActivado;
                TransFijo = true;
            }
            else
            {
                pictureBoxSlideButtonTransFijo.Image = Properties.Resources.sliderApagado;
                TransFijo = false;
            }
        }

        private void pictureBoxSlideButtonGround_Click(object sender, EventArgs e)
        {
            Ground = !Ground;

            if (Ground)
            {
                pictureBoxSlideButtonGround.Image =
                    Properties.Resources.sliderActivado;
            }
            else
            {
                pictureBoxSlideButtonGround.Image =
                    Properties.Resources.sliderApagado;
            }
        }

        private void buttonAplciarCambios_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
    "Ground: " + Ground +
    "\nCAT21: " + cat21 +
    "\nCAT48: " + cat48
);
            cat48 = checkBoxCat48.Checked;
            cat21 = checkBoxCat21.Checked;

            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBoxTrayectoriaMax.Text))
            {
                if (double.TryParse(textBox1.Text, out double min) &&
                    double.TryParse(textBoxTrayectoriaMax.Text, out double max))
                {
                    if (min >= 0 && min <= 360 &&
                        max >= 0 && max <= 360)
                    {
                        TrayectoriaMin = min;
                        TrayectoriaMax = max;
                        filtrarTrayectoria = true;
                    }
                    else
                    {
                        MessageBox.Show("La trayectoria debe estar entre 0 y 360 grados.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Introduce valores válidos para la trayectoria.");
                    return;
                }
            }
            else
            {
                filtrarTrayectoria = false;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
