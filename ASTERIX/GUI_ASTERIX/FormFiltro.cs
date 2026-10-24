
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
        // variables que almacenan el estado de los filtros
        bool blancoPuro;
        bool TransFijo;
        bool Ground;
        bool cat48;
        bool cat21;
        double TrayectoriaMin;
        double TrayectoriaMax;
        bool filtrarAltura;
        double alturaMin;
        double alturaMax;

        // propiedades para consultar los filtros seleccionados desde otros formularios
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
        {
            get { return TrayectoriaMin; }
        }
        public double trayectoriaMax
        {
            get { return TrayectoriaMax; }
        }

        bool filtrarTrayectoria { get; set; }

        public bool FiltrarTrayectoria
        {
            get { return filtrarTrayectoria; }
        }

        public bool FiltrarAltura
        {
            get { return filtrarAltura; }
        }

        public double AlturaMin
        {
            get { return alturaMin; }
        }

        public double AlturaMax
        {
            get { return alturaMax; }
        }

        // inicializar el formulario con los filtros establecidos anteriormente
        public FormFiltro(bool blancoPuro, bool transFijo, bool ground, bool cat21, bool cat48, bool filtrarTrayectoria, double trayectoriaMin, double trayectoriaMax, bool filtrarAltura, double alturaMin, double alturaMax)
        {
            InitializeComponent();

            this.blancoPuro = blancoPuro;
            this.TransFijo = transFijo;
            this.Ground = ground;
            this.cat21 = cat21;
            this.cat48 = cat48;
            this.filtrarTrayectoria = filtrarTrayectoria;
            this.TrayectoriaMin = trayectoriaMin;
            this.TrayectoriaMax = trayectoriaMax;
            this.filtrarAltura = filtrarAltura;
            this.alturaMin = alturaMin;
            this.alturaMax = alturaMax;

            ActualizarControles();
        }

        // activar o desactivar el filtro de blanco puro
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

        // actualizar los controles al cargar el formulario
        private void FormFiltro_Load(object sender, EventArgs e)
        {
            ActualizarControles();
        }

        // activar o desactivar el filtro de transpondedor fijo
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

        // activar o desactivar el filtro de aviones en tierra
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

        // comprobar los valores introducidos y aplicar los filtros seleccionados
        private void buttonAplciarCambios_Click(object sender, EventArgs e)
        {
            cat48 = checkBoxCat48.Checked;
            cat21 = checkBoxCat21.Checked;

            // validar el intervalo de trayectoria entre 0 y 360 grados
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
            // comprobar y aplicar el filtro de nivel de vuelo
            if (!string.IsNullOrWhiteSpace(textBoxAltitudMin.Text) &&
                !string.IsNullOrWhiteSpace(textBoxAltitudMax.Text))
            {
                if (double.TryParse(textBoxAltitudMin.Text, out double min) &&
                    double.TryParse(textBoxAltitudMax.Text, out double max))
                {
                    if (min >= 0 && max <= 600 && min <= max)
                    {
                        alturaMin = min;
                        alturaMax = max;
                        filtrarAltura = true;
                    }
                    else
                    {
                        MessageBox.Show("Introduce un rango de flight levels válido entre 0 y 600.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Introduce valores válidos para los flight levels.");
                    return;
                }
            }
            else
            {
                filtrarAltura = false;
            }

            // confirmar los cambios y cerrar el formulario
            DialogResult = DialogResult.OK;
            Close();
        }

        // actualizar los controles visuales según el estado de los filtros
        private void ActualizarControles()
        {
            // actualizar las categorías seleccionadas
            checkBoxCat21.Checked = cat21;
            checkBoxCat48.Checked = cat48;

            // actualizar el filtro de blanco puro
            pictureBoxSlideButtonBlancoPuro.Image = blancoPuro
                ? Properties.Resources.sliderActivado
                : Properties.Resources.sliderApagado;

            // actualizar el filtro de transpondedor fijo
            pictureBoxSlideButtonTransFijo.Image = TransFijo
                ? Properties.Resources.sliderActivado
                : Properties.Resources.sliderApagado;

            // actualizar el filtro de aviones en tierra
            pictureBoxSlideButtonGround.Image = Ground
                ? Properties.Resources.sliderActivado
                : Properties.Resources.sliderApagado;

            // mostrar el intervalo de trayectoria si el filtro está activado
            if (filtrarTrayectoria)
            {
                textBox1.Text = TrayectoriaMin.ToString();
                textBoxTrayectoriaMax.Text = TrayectoriaMax.ToString();
            }
            else
            {
                textBox1.Text = "";
                textBoxTrayectoriaMax.Text = "";
            }
            // mostrar los límites del filtro de nivel de vuelo
            if (filtrarAltura)
            {
                textBoxAltitudMin.Text = alturaMin.ToString();
                textBoxAltitudMax.Text = alturaMax.ToString();
            }
            else
            {
                textBoxAltitudMin.Text = "";
                textBoxAltitudMax.Text = "";
            }
        }
    }
}
