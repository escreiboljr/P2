using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VisioForge.Libs.DirectShowLib;

namespace GUI_ASTERIX
{
    public partial class MenuTutorial : Form
    {
        private int contador = 1;
        public MenuTutorial()
        {
            InitializeComponent();
        }

        private void MenuTutorial_Load(object sender, EventArgs e)
        {

        }

        private void buttonAvanzar_Click(object sender, EventArgs e)
        {
            contador = contador + 1;
            ActualizarTutorial();
        }

        private void buttonAtras_Click(object sender, EventArgs e)
        {
            contador = contador - 1;
            if (contador == 0)
            {
                contador = 1;
            }
            ActualizarTutorial();
        }
        public void ActualizarTutorial()
        {
            textBoxContador.Text = contador.ToString();
            if (contador == 1)
            {
                labelTitulo1.Visible = true;
                labelTitulo2.Visible = true;
                labelTitulo3.Visible = true;
                pictureBox1.Image = Properties.Resources.MenuSimulacion;
            }
            if (contador == 2)
            {
                labelTitulo1.Visible = false;
                labelTitulo2.Visible = false;
                labelTitulo3.Visible = false;
                pictureBox1.Image = Properties.Resources.RecuadroHoraControlesSim;
            }
            if (contador == 3)
            {
                pictureBox1.Image = Properties.Resources.RecuadroVelocidadSim;
            }
            if (contador == 4)
            {
                pictureBox1.Image = Properties.Resources.RecuadroSimdesdeHora;
            }
            if (contador == 5)
            {
                pictureBox1.Image = Properties.Resources.RecuadroReset;
            }
            if (contador == 6)
            {
                pictureBox1.Image = Properties.Resources.RecuadroMapa;
            }
            if (contador == 7)
            {
                pictureBox1.Image = Properties.Resources.MapaConControles;
            }
            if (contador == 8)
            {
                pictureBox1.Image = Properties.Resources.RecuadroAdsbRadar;
            }
            if (contador == 9)
            {
                pictureBox1.Image = Properties.Resources.MenuSimArchivos;
            }
            if (contador == 10)
            {
                pictureBox1.Image = Properties.Resources.MenuSimFiltro;
            }
            if (contador == 11)
            {
                pictureBox1.Image = Properties.Resources.MenuFiltro;
            }
            if (contador == 12)
            {
                pictureBox1.Image = Properties.Resources.MenuSimVer;
            }
            if (contador == 13)
            {
                pictureBox1.Image = Properties.Resources.MenuSimHeatmap;
            }
        }
    }
}
