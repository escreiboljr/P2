using DatosDecod;
using DatosDecod21;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archivos
{
    public class Filtros
    {
        public List<Mensaje> AplicarFiltros(
            List<Mensaje> listaOriginal,
            bool cat48,
            bool cat21,
            bool eliminarBlancoPuro,
            bool eliminarTransponderFijo,
            bool filtrarTrayectoria,
            double trayectoriaMin,
            double trayectoriaMax)
        {
            IEnumerable<Mensaje> listaFiltrada = listaOriginal;

            if (cat48 && cat21)
            {
                cat48 = false;
                cat21 = false;
            }

            if (cat48)
            {
                listaFiltrada = FiltroCategoria(listaFiltrada, 48);
            }

            if (cat21)
            {
                listaFiltrada = FiltroCategoria(listaFiltrada, 21);
            }

            if (eliminarBlancoPuro)
            {
                listaFiltrada = FiltrarBlancoPuro(listaFiltrada);
            }

            if (eliminarTransponderFijo)
            {
                listaFiltrada = FiltrarTransponderFijo(listaFiltrada);
            }

            if (filtrarTrayectoria)
            {
                listaFiltrada = FiltrarTrayectoria(
                    listaFiltrada,
                    trayectoriaMin,
                    trayectoriaMax);
            }

            return listaFiltrada.ToList();
        }

        private IEnumerable<Mensaje> FiltroCategoria(IEnumerable<Mensaje> listaFiltrada, int cat)  //filtro Categoria ASTERIX
            {
                return listaFiltrada.Where(x=> x.categoria == cat);
            }

            //private IEnumerable<Mensaje> FIltroBlancoPuro()
            private IEnumerable<Mensaje> FiltrarBlancoPuro(IEnumerable<Mensaje> listaFiltro)
            {
                return listaFiltro;
            }
            private IEnumerable<Mensaje> FiltrarTransponderFijo(IEnumerable<Mensaje> listaFiltro)
            {
                return listaFiltro.Where(x => x.TransFijo == false);
            }
        /*private IEnumerable<Mensaje> FiltrarTrayectoria(IEnumerable<Mensaje> listaFiltro, double trayectoria)
        {

        }*/
        private IEnumerable<Mensaje> FiltrarTrayectoria(
IEnumerable<Mensaje> listaFiltro,
double trayectoriaMin,
double trayectoriaMax)
        {
            List<Mensaje> resultado = new List<Mensaje>();

            Dictionary<string, double[]> posicionesAnteriores =
                new Dictionary<string, double[]>();

            foreach (Mensaje mensaje in listaFiltro)
            {
                string identificador = "";
                double latitud = double.NaN;
                double longitud = double.NaN;


                // CAT21
                if (mensaje.categoria == 21)
                {
                    TiraDatosDecod021 mensaje21 =
                        (TiraDatosDecod021)mensaje.tira;

                    identificador = mensaje21.TargetAddress;

                    latitud = mensaje21.PosWGS84HighRes[0];
                    longitud = mensaje21.PosWGS84HighRes[1];
                }


                // CAT48
                else if (mensaje.categoria == 48)
                {
                    TiraDatosDecod048 mensaje48 =
                        (TiraDatosDecod048)mensaje.tira;

                    identificador = mensaje48.AircrftAddrs.ToString();

                    latitud = mensaje48.PositionCorrectedLatLonAlt[0];
                    longitud = mensaje48.PositionCorrectedLatLonAlt[1];
                }


                // Si no tenemos una posición válida, no podemos calcular rumbo
                if (double.IsNaN(latitud) ||
                    double.IsNaN(longitud))
                {
                    continue;
                }


                // Si ya conocemos una posición anterior
                if (posicionesAnteriores.ContainsKey(identificador))
                {
                    double latAnterior =
                        posicionesAnteriores[identificador][0];

                    double lonAnterior =
                        posicionesAnteriores[identificador][1];


                    double rumbo = CalcularRumbo(
                        latAnterior,
                        lonAnterior,
                        latitud,
                        longitud);


                    if (RumboDentroDelRango(
                        rumbo,
                        trayectoriaMin,
                        trayectoriaMax))
                    {
                        resultado.Add(mensaje);
                    }
                }


                // Actualizamos la última posición conocida
                posicionesAnteriores[identificador] =
                    new double[] { latitud, longitud };
            }

            return resultado;
        }
        private double CalcularRumbo(
        double lat1,
        double lon1,
        double lat2,
        double lon2)
        {
            double lat1Rad = lat1 * Math.PI / 180.0;
            double lat2Rad = lat2 * Math.PI / 180.0;

            double diferenciaLon =
                (lon2 - lon1) * Math.PI / 180.0;

            double y =
                Math.Sin(diferenciaLon) *
                Math.Cos(lat2Rad);

            double x =
                Math.Cos(lat1Rad) * Math.Sin(lat2Rad) -
                Math.Sin(lat1Rad) *
                Math.Cos(lat2Rad) *
                Math.Cos(diferenciaLon);

            double rumbo =
                Math.Atan2(y, x) * 180.0 / Math.PI;

            if (rumbo < 0)
            {
                rumbo += 360;
            }

            return rumbo;
        }
        private bool RumboDentroDelRango(
        double rumbo,
        double minimo,
        double maximo)
        {
            if (minimo <= maximo)
            {
                return rumbo >= minimo &&
                       rumbo <= maximo;
            }
            else
            {
                // El intervalo pasa por 0°
                return rumbo >= minimo ||
                       rumbo <= maximo;
            }
        }
    }
}