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
            bool blancoPuro = false;
            bool TransFijo = false;
            bool Ground = false;
            bool cat21 = checkBoxCat21.Checked;
            bool cat48 = checkBoxCat48.Checked;
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
            if (Ground == false)
            {
                pictureBoxSlideButtonGround.Image = Properties.Resources.sliderActivado;
                Ground = true;
            }
            else
            {
                pictureBoxSlideButtonGround.Image = Properties.Resources.sliderApagado;
                Ground = false;
            }
        }

        private void buttonAplciarCambios_Click(object sender, EventArgs e)
        {
            cat48 = checkBoxCat48.Checked;
            cat21 = checkBoxCat21.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
