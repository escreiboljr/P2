using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archivos
{
    internal class Filtros
    {
        public class GestorFiltrosRadar
        {
            public List<Mensaje> AplicarFiltros(
                List<Mensaje> listaOriginal,
                bool usarFiltroCategoria,
                bool eliminarBlancoPuro,
                bool eliminarTransponderFijo,
                int categoria)
            {

                IEnumerable<Mensaje> listaFiltrada = listaOriginal;

                if (usarFiltroCategoria)
                {
                    listaFiltrada = FiltroCategoria(
                        listaFiltrada,
                        categoria);
                }

                if (eliminarBlancoPuro)
                {
                    listaFiltrada = FiltrarBlancoPuro(
                        listaFiltrada);
                }

                if (eliminarTransponderFijo)
                {
                    listaFiltrada = FiltrarTransponderFijo(
                        listaFiltrada);
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
        }
    }
}