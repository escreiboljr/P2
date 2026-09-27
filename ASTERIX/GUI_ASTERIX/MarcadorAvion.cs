using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace GUI_ASTERIX
{
    internal class MarcadorAvion : GMapMarker
    {
        public double rumbo;
        public Color color;

        public MarcadorAvion(PointLatLng posicion, double rumboAvion, Color colorAvion)
            : base(posicion)
        {
            rumbo = rumboAvion;
            color = colorAvion;

            Size = new Size(24, 24);
            Offset = new Point(-12, -12);
        }

        public override void OnRender(Graphics g)
        {
            GraphicsState estado = g.Save();

            float centroX = LocalPosition.X + 12;
            float centroY = LocalPosition.Y + 12;

            g.TranslateTransform(centroX, centroY);
            g.RotateTransform((float)rumbo);

            PointF[] flecha =
            {
                new PointF(0, -10),
                new PointF(7, 8),
                new PointF(0, 4),
                new PointF(-7, 8)
            };

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, flecha);
            }

            using (Pen pen = new Pen(Color.Black, 1))
            {
                g.DrawPolygon(pen, flecha);
            }

            g.Restore(estado);
        }
    }
}