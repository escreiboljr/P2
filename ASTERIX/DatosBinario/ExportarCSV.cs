using DatosDecod;
using DatosDecod21;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Archivos
{
    public class ExportarCSV
    {
        public void Exportar(List<Mensaje> mensajes, string ruta)
        {
            bool hayCAT21 = false;
            bool hayCAT48 = false;

            foreach (Mensaje mensaje in mensajes)
            {
                if (mensaje.categoria == 21)
                    hayCAT21 = true;

                if (mensaje.categoria == 48)
                    hayCAT48 = true;
            }

            if (hayCAT21 && !hayCAT48)
            {
                ExportarCAT21(mensajes, ruta);
            }
            else if (hayCAT48 && !hayCAT21)
            {
                ExportarCAT48(mensajes, ruta);
            }
            else
            {
                ExportarCombinado(mensajes, ruta);
            }
        }

        private void ExportarCAT48(List<Mensaje> mensajes, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(
                    "Categoria;" +
                    "Hora;" +
                    "Target Address;" +
                    "Target Identification;" +
                    "Track Number;" +
                    "Flight Level;" +
                    "Latitude;" +
                    "Longitude;" +
                    "Altitude;" +
                    "TYP;" +
                    "CNF;" +
                    "RAD"
                );

                foreach (Mensaje mensaje in mensajes)
                {
                    if (mensaje.categoria != 48)
                        continue;

                    TiraDatosDecod048 m = (TiraDatosDecod048)mensaje.tira;

                    sw.WriteLine(
                        "48;" +
                        Valor(m.TimeOfDay) + ";" +
                        Valor(m.AircrftAddrs) + ";" +
                        Valor(m.AircrftIddent) + ";" +
                        Valor(m.TrackNum) + ";" +
                        Valor(m.FL.FL) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[0]) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[1]) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[2]) + ";" +
                        Valor(m.TargetRep.TYP) + ";" +
                        Valor(m.TrckStatus.CNF) + ";" +
                        Valor(m.TrckStatus.RAD)
                    );
                }
            }
        }

        private void ExportarCAT21(List<Mensaje> mensajes, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(
                    "Categoria;" +
                    "SAC;" +
                    "SIC;" +
                    "TimeReceptionPosition;" +
                    "TargetAddress;" +
                    "TargetIdentification;" +
                    "Mode3A;" +
                    "FlightLevel;" +
                    "Latitude;" +
                    "Longitude"
                );

                foreach (Mensaje mensaje in mensajes)
                {
                    if (mensaje.categoria != 21)
                        continue;

                    TiraDatosDecod021 m =
                        (TiraDatosDecod021)mensaje.tira;

                    sw.WriteLine(
                        "21;" +
                        Valor(m.DataSourceID[0]) + ";" +
                        Valor(m.DataSourceID[1]) + ";" +
                        Valor(m.TimeReceptionPosition) + ";" +
                        Valor(m.TargetAddress) + ";" +
                        Valor(m.TargetIdentification) + ";" +
                        Valor(m.Mode3ACode) + ";" +
                        Valor(m.FlightLevel) + ";" +
                        Valor(m.PosWGS84HighRes[0]) + ";" +
                        Valor(m.PosWGS84HighRes[1])
                    );
                }
            }
        }

        private void ExportarCombinado(List<Mensaje> mensajes, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(
                    "Categoria;" +
                    "SAC;" +
                    "SIC;" +
                    "Tiempo;" +
                    "TargetAddress;" +
                    "TargetIdentification;" +
                    "Mode3A;" +
                    "FlightLevel;" +
                    "Latitude;" +
                    "Longitude;" +
                    "Altitude;" +
                    "TrackNumber;" +
                    "TYP;" +
                    "CNF;" +
                    "RAD"
                );

                foreach (Mensaje mensaje in mensajes)
                {
                    if (mensaje.categoria == 21)
                    {
                        TiraDatosDecod021 m =
                            (TiraDatosDecod021)mensaje.tira;

                        sw.WriteLine(
                            "21;" +
                            Valor(m.DataSourceID[0]) + ";" +
                            Valor(m.DataSourceID[1]) + ";" +
                            Valor(m.TimeReceptionPosition) + ";" +
                            Valor(m.TargetAddress) + ";" +
                            Valor(m.TargetIdentification) + ";" +
                            Valor(m.Mode3ACode) + ";" +
                            Valor(m.FlightLevel) + ";" +
                            Valor(m.PosWGS84HighRes[0]) + ";" +
                            Valor(m.PosWGS84HighRes[1]) + ";" +

                            // Altitude
                            ";" +

                            // TrackNumber
                            ";" +

                            // TYP
                            ";" +

                            // CNF
                            ";" +

                            // RAD
                            ""
                        );
                    }

                    else if (mensaje.categoria == 48)
                    {
                        TiraDatosDecod048 m =
                            (TiraDatosDecod048)mensaje.tira;

                        sw.WriteLine(
                            "48;" +

                            // SAC
                            Valor(m.DataSourceID[0]) + ";" +

                            // SIC
                            Valor(m.DataSourceID[1]) + ";" +

                            Valor(m.TimeOfDay) + ";" +
                            Valor(m.AircrftAddrs) + ";" +
                            Valor(m.AircrftIddent) + ";" +

                            // Mode3A
                            Valor(m.mode3) + ";" +

                            Valor(m.FL.FL) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[0]) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[1]) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[2]) + ";" +
                            Valor(m.TrackNum) + ";" +
                            Valor(m.TargetRep.TYP) + ";" +
                            Valor(m.TrckStatus.CNF) + ";" +
                            Valor(m.TrckStatus.RAD)
                        );
                    }
                }
            }
        }
        private string Valor(object valor)
        {
            if (valor == null)
                return "";

            if (valor is double)
            {
                double numero = (double)valor;

                if (double.IsNaN(numero))
                    return "";

                return numero.ToString(
                    CultureInfo.GetCultureInfo("es-ES")
                );
            }

            return valor.ToString();
        }
    }
}
