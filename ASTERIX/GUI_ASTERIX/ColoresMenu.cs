
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public class ColoresMenu : ToolStripProfessionalRenderer
    {
        private readonly Color fondo = Color.FromArgb(20, 38, 58);
        private readonly Color seleccionado = Color.FromArgb(32, 82, 108);
        private readonly Color cian = Color.FromArgb(54, 220, 235);
        private readonly Color texto = Color.White;

        public ColoresMenu() : base(new TablaColoresMenu())
        {
            RoundedEdges = false;
        }

        // dibujar el fondo de los menús desplegables
        protected override void OnRenderToolStripBackground(
            ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown)
            {
                using (SolidBrush brush = new SolidBrush(fondo))
                {
                    e.Graphics.FillRectangle(
                        brush,
                        e.AffectedBounds
                    );
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // cambiar el color de las flechas de los submenús
            e.ArrowColor = Color.FromArgb(54, 220, 235);

            base.OnRenderArrow(e);
        }


        // dibujar el fondo de cada opción del menú
        protected override void OnRenderMenuItemBackground(
            ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(
                2, 1,
                e.Item.Width - 4,
                e.Item.Height - 2
            );

            if (e.Item.Selected)
            {
                using (SolidBrush brush = new SolidBrush(seleccionado))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }

                // línea cian en el lateral de la opción seleccionada
                using (SolidBrush brush = new SolidBrush(cian))
                {
                    e.Graphics.FillRectangle(
                        brush,
                        2, 1, 3, e.Item.Height - 2
                    );
                }
            }
            else if (e.Item.Pressed)
            {
                using (SolidBrush brush = new SolidBrush(seleccionado))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
        }

        // dibujar el texto de las opciones
        protected override void OnRenderItemText(
            ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Selected ? cian : texto;
            base.OnRenderItemText(e);
        }

        // dibujar el borde del menú desplegable
        protected override void OnRenderToolStripBorder(
            ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDown)
            {
                Rectangle rect = new Rectangle(
                    0, 0,
                    e.ToolStrip.Width - 1,
                    e.ToolStrip.Height - 1
                );

                using (Pen pen = new Pen(cian))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }
    }

    public class TablaColoresMenu : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground =>
            Color.FromArgb(20, 38, 58);

        public override Color ImageMarginGradientBegin =>
            Color.FromArgb(20, 38, 58);

        public override Color ImageMarginGradientMiddle =>
            Color.FromArgb(20, 38, 58);

        public override Color ImageMarginGradientEnd =>
            Color.FromArgb(20, 38, 58);
    }

}
