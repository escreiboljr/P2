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
                bool eliminarTransponderFijo)
            {

                IEnumerable<Mensaje> listaFiltrada = listaOriginal;

                if (cat48 && cat21)
                {
                    cat48=false;
                    cat21=false;
                }


                if (cat48)
                {
                    listaFiltrada = FiltroCategoria(
                        listaFiltrada, 48);
                }
                
                if (cat21)
                {
                    listaFiltrada = FiltroCategoria(
                        listaFiltrada, 21);
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