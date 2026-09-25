using System;
using System.Collections.Generic;
using System.Text;

namespace Archivos
{
    internal class Filtros
    {
        public class GestorFiltrosRadar
        {
            public List<Mensaje> AplicarFiltros(
                List<Mensaje> listaOriginal,
                bool usarFiltroAltitud,
                bool usarFiltroTrack,
                bool usarFiltroIdentificacion,
                double altitudMax,
                int trackBuscado,
                string idBuscado)
            {
                var listaFiltrada = listaOriginal.AsEnumerable();
                /*
                if (usarFiltroAltitud)
                    listaFiltrada = FiltroAltitud(listaFiltrada, altitudMax);

                if (usarFiltroTrack)
                    listaFiltrada = FiltroTrack(listaFiltrada, trackBuscado);

                if (usarFiltroIdentificacion)
                    listaFiltrada = FiltroIdentificacion(listaFiltrada, idBuscado);
                */
                return listaFiltrada.ToList();
            }

            private IEnumerable<Mensaje> FiltroCategoria(IEnumerable<Mensaje> listaFiltrada, int cat)  //filtro Categoria ASTERIX
            {
                return listaFiltrada.Where(x=> x.categoria == cat);
            }
            //private IEnumerable<Mensaje> FIltroBlancoPuro()
            private IEnumerable<Mensaje> FiltrarTransponderFijo(IEnumerable<Mensaje> listaFiltro)
            {
                return listaFiltro.Where(x => x.TransFijo == false);
            }
            private IEnumerable<Mensaje> FiltrarTrayectoria(IEnumerable<Mensaje> listaFiltro, double trayectoria)
            {
                
            }
        }
    }
}