namespace GUI_ASTERIX
{
    public partial class MenuSimulacion : Form
    {
        private double TiempoActual = 0;
        private bool Reproduciendo = false;
        public MenuSimulacion()
        {
            InitializeComponent();
            timerSimulacion.Interval = 1000;
        }

        private void MenuSimulacion_Load(object sender, EventArgs e)
        {

        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Reproduciendo = true;
            timerSimulacion.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Reproduciendo = false;
            timerSimulacion.Stop();
        }

        private void timerSimulacion_Tick(object sender, EventArgs e)
        {
            TiempoActual= TiempoActual+1;
            label1.Text = TiempoActual.ToString();
        }
    }
}
