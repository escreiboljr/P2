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
            this.Shown += (s, e) => this.ActiveControl = null;
        }

        private void MenuTutorial_Load(object sender, EventArgs e)
        {
            ActualizarTutorial();
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
                pictureBox1.Image = Properties.Resources.Tutorial_de_la_interfaz_principal_ASTERIX;
            }
            if (contador == 2)
            {
                pictureBox1.Image = Properties.Resources.Tutorial_del_menú_Archivo_en_ASTERIX;
            }
            if (contador == 3)
            {
                pictureBox1.Image = Properties.Resources.Tutorial_del_menú_Filtrar_ASTERIX;
            }
            if (contador == 4)
            {
                pictureBox1.Image = Properties.Resources.Tutorial_del_Menú_Ver_de_ASTERIX;
            }
            if (contador == 5)
            {
                pictureBox1.Image = Properties.Resources.Guía_Heatmap_de_ASTERIX;
            }
            if (contador == 6)
            {
                pictureBox1.Image = Properties.Resources.Tutorial_del_menú_Ayuda_en_ASTERIX;
            }
            if (contador == 7)
            {
                pictureBox1.Image = Properties.Resources.Guía_visual_de_controles_del_mapa_Asterix;
            }
            if (contador == 8)
            {
                pictureBox1.Image = Properties.Resources.Tutorial__Ventana_de_filtros_neon;
            }
            if (contador == 9)
            {
                pictureBox1.Image = Properties.Resources.Tutorial__Ventana_de_información_del_avión;
            }
            if (contador > 9)
            {
                contador = 9;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
