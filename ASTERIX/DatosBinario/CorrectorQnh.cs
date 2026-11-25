using System;
using System.Collections.Generic;

namespace Archvios
{
    internal sealed class CorrectorQnh
    {
        private sealed class Estado
        {
            public double referencia = double.NaN;
            public double ultimoTiempo = double.NaN;
        }

        private readonly Dictionary<string, Estado> estados =
            new Dictionary<string, Estado>(
                StringComparer.OrdinalIgnoreCase);

        public void Reiniciar()
        {
            estados.Clear();
        }

        private static bool PresionValida(double presion)
        {
            return double.IsFinite(presion) &&
                   presion >= 900.0 &&
                   presion <= 1100.0;
        }

        public double Obtener(
            string? direccion,
            double bps,
            double tiempo,
            double qnhReferencia)
        {
            // Se mantiene por compatibilidad con LeerDatos.
            // En tu caso, qnhReferencia permanece como NaN.
            if (PresionValida(qnhReferencia))
            {
                return qnhReferencia;
            }

            if (string.IsNullOrWhiteSpace(direccion) ||
                !double.IsFinite(tiempo) ||
                tiempo < 0.0 ||
                tiempo >= 86400.0)
            {
                return double.NaN;
            }

            string clave = direccion.Trim();

            if (!estados.TryGetValue(clave, out Estado? estado))
            {
                estado = new Estado();
                estados.Add(clave, estado);
            }

            if (double.IsFinite(estado.ultimoTiempo))
            {
                double diferencia = tiempo - estado.ultimoTiempo;

                // Paso por medianoche.
                if (diferencia < -43200.0)
                {
                    diferencia += 86400.0;
                }

                // No aplicar una referencia futura a un mensaje atrasado.
                if (diferencia < 0.0)
                {
                    return double.NaN;
                }
            }

            estado.ultimoTiempo = tiempo;

            bool presionEstandar =
                double.IsFinite(bps) &&
                bps >= 1013.0 &&
                bps <= 1013.5;

            if (PresionValida(bps) && !presionEstandar)
            {
                // Guardar el último BP válido no estándar.
                estado.referencia = bps;
            }

            // STD o BPS ausente: conservar la referencia anterior.
            return estado.referencia;
        }
    }
}