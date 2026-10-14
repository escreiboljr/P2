using System;
using System.Collections.Generic;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace GUI_ASTERIX
{
    internal class CeldaHeatmap : GMapMarker
    {
        private readonly double latMin;
        private readonly double latMax;
        private readonly double lonMin;
        private readonly double lonMax;
        private readonly Color color;

        public CeldaHeatmap(
            double latMin,
            double latMax,
            double lonMin,
            double lonMax,
            Color color)
            : base(new PointLatLng(latMax, lonMin))
        {
            this.latMin = latMin;
            this.latMax = latMax;
            this.lonMin = lonMin;
            this.lonMax = lonMax;
            this.color = color;

            IsHitTestVisible = false;
        }

        public override void OnRender(Graphics g)
        {
            if (Overlay == null || Overlay.Control == null)
                return;

            var mapa = Overlay.Control;

            // Esquinas geográficas
            PointLatLng esquinaNO = new PointLatLng(latMax, lonMin);
            PointLatLng esquinaSE = new PointLatLng(latMin, lonMax);

            // Coordenadas en píxeles del mapa
            GPoint puntoNO = mapa.FromLatLngToLocal(esquinaNO);
            GPoint puntoSE = mapa.FromLatLngToLocal(esquinaSE);

            float ancho = (float)(puntoSE.X - puntoNO.X);
            float alto = (float)(puntoSE.Y - puntoNO.Y);

            if (ancho <= 0 || alto <= 0)
                return;

            // Dibujar usando la posición local del marcador
            float x = (float)LocalPosition.X;
            float y = (float)LocalPosition.Y;

            using (SolidBrush pincel = new SolidBrush(color))
            {
                g.FillRectangle(
                    pincel,
                    x,
                    y,
                    ancho,
                    alto
                );
            }
        }

    }
}
