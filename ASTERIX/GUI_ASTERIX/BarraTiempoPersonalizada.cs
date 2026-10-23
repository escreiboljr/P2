using System.ComponentModel;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public class BarraTiempoPersonalizada : Control
    {
        private int minimo = 0;
        private int maximo = 100;
        private int valor = 0;

        private bool arrastrando = false;

        public event EventHandler ValorConfirmado;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TiempoMinimo
        {
            get { return minimo; }
            set
            {
                minimo = value;
                if (maximo < minimo) maximo = minimo;
                TiempoValor = valor;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TiempoMaximo
        {
            get { return maximo; }
            set
            {
                maximo = Math.Max(minimo, value);
                TiempoValor = valor;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TiempoValor
        {
            get { return valor; }
            set
            {
                valor = Math.Max(minimo, Math.Min(maximo, value));
                Invalidate();
            }
        }

        public BarraTiempoPersonalizada()
        {
            DoubleBuffered = true;
            Height = 30;
            Cursor = Cursors.Hand;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int margen = 10;
            int ancho = Math.Max(1, Width - margen * 2);
            int centroY = Height / 2;

            float progreso = maximo > minimo
                ? (float)(valor - minimo) / (maximo - minimo)
                : 0;

            float posicionX = margen + ancho * progreso;

            using (Pen fondo = new Pen(Color.FromArgb(65, 78, 96), 5))
            using (Pen avance = new Pen(Color.FromArgb(0, 200, 235), 5))
            using (SolidBrush cursor = new SolidBrush(Color.FromArgb(0, 200, 235)))
            using (SolidBrush centro = new SolidBrush(Color.White))
            {
                fondo.StartCap = LineCap.Round;
                fondo.EndCap = LineCap.Round;

                avance.StartCap = LineCap.Round;
                avance.EndCap = LineCap.Round;

                g.DrawLine(fondo, margen, centroY,
                    margen + ancho, centroY);

                g.DrawLine(avance, margen, centroY,
                    posicionX, centroY);

                g.FillEllipse(cursor,
                    posicionX - 8, centroY - 8, 16, 16);

                g.FillEllipse(centro,
                    posicionX - 3, centroY - 3, 6, 6);
            }
        }

        private void ActualizarValorDesdeRaton(int x)
        {
            int margen = 10;
            int ancho = Math.Max(1, Width - margen * 2);

            double porcentaje =
                Math.Max(0, Math.Min(1,
                    (double)(x - margen) / ancho));

            TiempoValor = minimo + (int)Math.Round(
    porcentaje * (maximo - minimo));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            arrastrando = true;
            Capture = true;
            ActualizarValorDesdeRaton(e.X);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (arrastrando)
                ActualizarValorDesdeRaton(e.X);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!arrastrando || e.Button != MouseButtons.Left)
                return;

            ActualizarValorDesdeRaton(e.X);
            arrastrando = false;
            Capture = false;

            ValorConfirmado?.Invoke(this, EventArgs.Empty);
        }
    }
}
