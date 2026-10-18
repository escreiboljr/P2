using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class MenuInicio : Form
    {
        public MenuInicio()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form menuSim = new MenuSimulacion();
            this.Hide();
            menuSim.ShowDialog();
            this.Show();
        }

        private void MenuInicio_Load(object sender, EventArgs e)
        {
            Point posicionSimulacion = pictureBox1.PointToScreen(Point.Empty);
            Point posicionTour = pictureBox2.PointToScreen(Point.Empty);

            pictureBox1.Parent = pictureBoxFondo;
            pictureBox2.Parent = pictureBoxFondo;

            pictureBox1.Location = pictureBoxFondo.PointToClient(posicionSimulacion);
            pictureBox2.Location = pictureBoxFondo.PointToClient(posicionTour);

            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox1.BringToFront();
            pictureBox2.BringToFront();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form tutorial = new MenuTutorial();
            tutorial.Show();
        }
    }
}
