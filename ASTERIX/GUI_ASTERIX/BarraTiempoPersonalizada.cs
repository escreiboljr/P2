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
        public event EventHandler ValorCambiado;

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
                int nuevoValor = Math.Max(minimo, Math.Min(maximo, value));

                if (valor != nuevoValor)
                {
                    valor = nuevoValor;
                    Invalidate();

                    ValorCambiado?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EstaArrastrando
        {
            get { return arrastrando; }
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

            int margen = 12;
            int ancho = Math.Max(1, Width - margen * 2);
            int centroY = Height / 2;

            float progreso = maximo > minimo
                ? (float)(valor - minimo) / (maximo - minimo)
                : 0;

            float posicionX = margen + ancho * progreso;

            Color colorFondo = Color.FromArgb(57, 71, 91);
            Color colorProgreso = Color.FromArgb(34, 211, 238);
            Color colorCursor = Color.White;

            // Barra completa
            using (Pen fondo = new Pen(colorFondo, 6))
            {
                fondo.StartCap = LineCap.Round;
                fondo.EndCap = LineCap.Round;

                g.DrawLine(fondo,
                    margen, centroY,
                    margen + ancho, centroY);
            }

            // Progreso de la simulación
            if (posicionX > margen)
            {
                using (Pen avance = new Pen(colorProgreso, 6))
                {
                    avance.StartCap = LineCap.Round;
                    avance.EndCap = LineCap.Round;

                    g.DrawLine(avance,
                        margen, centroY,
                        posicionX, centroY);
                }
            }

            // Cursor circular
            int radio = arrastrando ? 9 : 7;

            using (SolidBrush cursor = new SolidBrush(colorCursor))
            using (Pen borde = new Pen(colorProgreso, 2))
            {
                g.FillEllipse(cursor,
                    posicionX - radio,
                    centroY - radio,
                    radio * 2,
                    radio * 2);

                g.DrawEllipse(borde,
                    posicionX - radio,
                    centroY - radio,
                    radio * 2,
                    radio * 2);
            }
        }


        private void ActualizarValorDesdeRaton(int x)
        {
            int margen = 12;
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
