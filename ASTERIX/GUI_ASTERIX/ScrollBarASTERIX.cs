
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public class ScrollBarASTERIX : Control
    {
        private DataGridView tabla;
        private bool arrastrando = false;
        private int desplazamientoRaton = 0;

        private int margen = 3;
        private int altoMinimo = 30;

        private Color fondo = Color.FromArgb(20, 43, 63);
        private Color cian = Color.FromArgb(54, 220, 235);
        private Color cianHover = Color.FromArgb(100, 240, 250);

        private bool ratonEncima = false;

        public ScrollBarASTERIX()
        {
            Width = 16;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true
            );

            Cursor = Cursors.Hand;
        }

        public void Vincular(DataGridView dataGrid)
        {
            if (tabla != null)
            {
                tabla.Scroll -= Tabla_Scroll;
                tabla.RowsAdded -= Tabla_CambioFilas;
                tabla.RowsRemoved -= Tabla_CambioFilas;
                tabla.Resize -= Tabla_Resize;
                tabla.DataBindingComplete -= Tabla_DataBindingComplete;
                tabla.MouseWheel -= Tabla_MouseWheel;
            }

            tabla = dataGrid;

            if (tabla == null)
                return;

            // ocultar la barra vertical original
            tabla.ScrollBars = ScrollBars.None;

            tabla.MouseWheel += Tabla_MouseWheel;
            tabla.Scroll += Tabla_Scroll;
            tabla.RowsAdded += Tabla_CambioFilas;
            tabla.RowsRemoved += Tabla_CambioFilas;
            tabla.Resize += Tabla_Resize;
            tabla.DataBindingComplete += Tabla_DataBindingComplete;

            Actualizar();
        }

        private void Tabla_Scroll(object sender, ScrollEventArgs e)
        {
            Actualizar();
        }

        private void Tabla_CambioFilas(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Actualizar();
        }

        private void Tabla_CambioFilas(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Actualizar();
        }

        private void Tabla_Resize(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void Tabla_DataBindingComplete(
            object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            Actualizar();
        }

        public void Actualizar()
        {
            if (tabla == null || tabla.IsDisposed)
                return;

            // comprobar si hay filas que no caben en pantalla
            bool necesitaScroll = tabla.Rows.Count > 0 &&
                tabla.DisplayedRowCount(false) <
                tabla.Rows.GetRowCount(DataGridViewElementStates.Visible);

            // mostrar solo cuando la tabla esté visible
            Visible = tabla.Visible && necesitaScroll;

            if (Visible)
                BringToFront();

            Invalidate();
        }

        private int ObtenerFilasVisibles()
        {
            if (tabla == null)
                return 0;

            int altoDisponible = tabla.DisplayRectangle.Height;

            if (tabla.ColumnHeadersVisible)
                altoDisponible -= tabla.ColumnHeadersHeight;

            int altoFila = tabla.RowTemplate.Height;

            if (altoFila <= 0)
                altoFila = 30;

            return Math.Max(1, altoDisponible / altoFila);
        }

        private int ObtenerTotalFilas()
        {
            return tabla.Rows.GetRowCount(
                DataGridViewElementStates.Visible);
        }

        private Rectangle ObtenerRectanguloBarra()
        {
            int total = ObtenerTotalFilas();
            int visibles = ObtenerFilasVisibles();

            int altoDisponible = Math.Max(1, Height - margen * 2);
            int anchoBarra = Math.Max(1, Width - margen * 2);

            if (total <= visibles)
            {
                return new Rectangle(
                    margen, margen,
                    anchoBarra, altoDisponible);
            }

            int altoBarra = Math.Max(
                altoMinimo,
                (int)((double)visibles / total * altoDisponible)
            );

            altoBarra = Math.Min(altoBarra, altoDisponible);

            int maxIndice = Math.Max(1, total - visibles);

            int indiceActual = Math.Max(
                0, tabla.FirstDisplayedScrollingRowIndex);

            int recorrido = altoDisponible - altoBarra;

            int posicion = margen +
                (int)((double)indiceActual / maxIndice * recorrido);

            return new Rectangle(
                margen,
                posicion,
                anchoBarra,
                altoBarra
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(fondo);

            if (tabla == null || !Visible)
                return;

            Rectangle rect = ObtenerRectanguloBarra();

            using (SolidBrush brush = new SolidBrush(
                ratonEncima ? cianHover : cian))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ratonEncima = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ratonEncima = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left || tabla == null)
                return;

            Rectangle rect = ObtenerRectanguloBarra();

            if (rect.Contains(e.Location))
            {
                arrastrando = true;
                desplazamientoRaton = e.Y - rect.Top;
                Capture = true;
            }
            else
            {
                MoverA(e.Y - rect.Height / 2);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (arrastrando)
                MoverA(e.Y - desplazamientoRaton);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            arrastrando = false;
            Capture = false;
        }

        private void MoverA(int posicionY)
        {
            if (tabla == null || tabla.RowCount == 0)
                return;

            int total = ObtenerTotalFilas();
            int visibles = ObtenerFilasVisibles();

            int maxIndice = total - visibles;

            if (maxIndice <= 0)
                return;

            Rectangle rect = ObtenerRectanguloBarra();

            int recorrido = Height - margen * 2 - rect.Height;

            if (recorrido <= 0)
                return;

            double porcentaje =
                (double)(posicionY - margen) / recorrido;

            porcentaje = Math.Max(0, Math.Min(1, porcentaje));

            int indice = (int)Math.Round(porcentaje * maxIndice);

            // buscar la fila visible correspondiente
            int contador = 0;

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                if (!tabla.Rows[i].Visible)
                    continue;

                if (contador == indice)
                {
                    try
                    {
                        tabla.FirstDisplayedScrollingRowIndex = i;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignorar índices no desplazables
                    }

                    break;
                }

                contador++;
            }

            Invalidate();
        }


        private void Tabla_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tabla == null || tabla.Rows.Count == 0)
                return;

            int indiceActual = tabla.FirstDisplayedScrollingRowIndex;

            if (indiceActual < 0)
                return;

            // calcular el desplazamiento de la rueda
            int pasos = e.Delta / SystemInformation.MouseWheelScrollDelta;

            if (pasos == 0)
                return;

            int desplazamiento = -pasos * 3;

            int nuevoIndice = indiceActual + desplazamiento;

            // limitar el desplazamiento a las filas existentes
            nuevoIndice = Math.Max(0, nuevoIndice);
            nuevoIndice = Math.Min(tabla.Rows.Count - 1, nuevoIndice);

            try
            {
                tabla.FirstDisplayedScrollingRowIndex = nuevoIndice;
            }
            catch (InvalidOperationException)
            {
                // ignorar posiciones no válidas
            }

            Actualizar();
        }

    }
}
