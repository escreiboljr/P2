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
                    "SAC;SIC;Hora;" +

                    // TARGET REPORT DESCRIPTOR
                    "TYP;SIM;RDP;SPI;RAB;TST;ERR;XPP;ME;MI;FOE_FRI;" +
                    "ADSB_EP;ADSB_VAL;SCN_EP;SCN_VAL;PAI_EP;PAI_VAL;" +

                    // POSICION
                    "RHO_NM;THETA_deg;Latitude;Longitude;Altitude_ft;" +

                    // MODE 3/A
                    "Mode3A;Mode3A_V;Mode3A_G;Mode3A_L;" +

                    // FL
                    "FlightLevel;FL_V;FL_G;" +

                    // RADAR PLOT
                    "SRL;SRR;SAM;PRL;PAM;RPD;APD;" +

                    // IDENTIFICACION
                    "TargetAddress;TargetIdentification;TrackNumber;" +

                    // VELOCIDAD
                    "GroundSpeed_kt;Heading_deg;" +

                    // TRACK STATUS
                    "CNF;RAD;DOU;MAH;CDM;TRE;GHO;SUP;TCC;" +

                    // COMM / ACAS
                    "COM;STAT;SI;MSSC;ARC;AIC;B1A;B1B"
                );

                foreach (Mensaje mensaje in mensajes)
                {
                    if (mensaje.categoria != 48)
                        continue;

                    TiraDatosDecod048 m =
                        (TiraDatosDecod048)mensaje.tira;

                    sw.WriteLine(
                        "48;" +

                        Valor(m.DataSourceID[0]) + ";" +
                        Valor(m.DataSourceID[1]) + ";" +
                        Valor(m.TimeOfDay) + ";" +

                        Valor(m.TargetRep.TYP) + ";" +
                        Valor(m.TargetRep.SIM) + ";" +
                        Valor(m.TargetRep.RDP) + ";" +
                        Valor(m.TargetRep.SPI) + ";" +
                        Valor(m.TargetRep.RAB) + ";" +
                        Valor(m.TargetRep.TST) + ";" +
                        Valor(m.TargetRep.ERR) + ";" +
                        Valor(m.TargetRep.XPP) + ";" +
                        Valor(m.TargetRep.ME) + ";" +
                        Valor(m.TargetRep.MI) + ";" +
                        Valor(m.TargetRep.FOE_FRI) + ";" +
                        Valor(m.TargetRep.ADSB_EP) + ";" +
                        Valor(m.TargetRep.ADSB_VAL) + ";" +
                        Valor(m.TargetRep.SCN_EP) + ";" +
                        Valor(m.TargetRep.SCN_VAL) + ";" +
                        Valor(m.TargetRep.PAI_EP) + ";" +
                        Valor(m.TargetRep.PAI_VAL) + ";" +

                        Valor(m.PosSlantPolarCoord[0]) + ";" +
                        Valor(m.PosSlantPolarCoord[1]) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[0]) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[1]) + ";" +
                        Valor(m.PositionCorrectedLatLonAlt[2]) + ";" +

                        Valor(m.mode3.reply) + ";" +
                        Valor(m.mode3.V) + ";" +
                        Valor(m.mode3.G) + ";" +
                        Valor(m.mode3.L) + ";" +

                        Valor(m.FL.FL) + ";" +
                        Valor(m.FL.V) + ";" +
                        Valor(m.FL.G) + ";" +

                        Valor(m.RadarPlot.SRL) + ";" +
                        Valor(m.RadarPlot.SRR) + ";" +
                        Valor(m.RadarPlot.SAM) + ";" +
                        Valor(m.RadarPlot.PRL) + ";" +
                        Valor(m.RadarPlot.PAM) + ";" +
                        Valor(m.RadarPlot.RPD) + ";" +
                        Valor(m.RadarPlot.APD) + ";" +

                        Valor(m.AircrftAddrs) + ";" +
                        Valor(m.AircrftIddent) + ";" +
                        Valor(m.TrackNum) + ";" +

                        (m.tieneTrackVelocity ? Valor(m.TrckVelPolRepr[0]) : "") + ";" +

                        (m.tieneTrackVelocity ? Valor(m.TrckVelPolRepr[1]) : "") + ";" +

                        Valor(m.TrckStatus.CNF) + ";" +
                        TextoRAD(m.TrckStatus.RAD) + ";" +
                        Valor(m.TrckStatus.DOU) + ";" +
                        Valor(m.TrckStatus.MAH) + ";" +
                        Valor(m.TrckStatus.CDM) + ";" +
                        Valor(m.TrckStatus.TRE) + ";" +
                        Valor(m.TrckStatus.GHO) + ";" +
                        Valor(m.TrckStatus.SUP) + ";" +
                        Valor(m.TrckStatus.TCC) + ";" +

                        Valor(m.CommACAScapability.COM) + ";" +
                        Valor(m.CommACAScapability.STAT) + ";" +
                        Valor(m.CommACAScapability.SI) + ";" +
                        Valor(m.CommACAScapability.MSSC) + ";" +
                        Valor(m.CommACAScapability.ARC) + ";" +
                        Valor(m.CommACAScapability.AIC) + ";" +
                        Valor(m.CommACAScapability.B1A) + ";" +
                        Valor(m.CommACAScapability.B1B)
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
                    "SAC;SIC;" +
                    "TimeReceptionPosition;" +
                    "TargetAddress;" +
                    "TargetIdentification;" +
                    "Mode3A;" +
                    "FlightLevel;" +
                    "Latitude;Longitude;" +
                    "QNH;AltitudeCorrected;" +
                    "ReservedExpField;" +

                    // TARGET REPORT DESCRIPTOR
                    "ATP;ARC;RC;RAB;" +
                    "DCR;GBS;SIM;TST;SAA;CL;" +
                    "IPC;NOGO;CPR;LDPJ;RCF"
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
                        Valor(m.PosWGS84HighRes[1]) + ";" +

                        Valor(m.QNH) + ";" +
                        Valor(m.AltitudCorregida) + ";" +
                        Valor(m.ReservedExpField) + ";" +

                        Valor(m.TargetRep.ATP) + ";" +
                        Valor(m.TargetRep.ARC) + ";" +
                        Valor(m.TargetRep.RC) + ";" +
                        Valor(m.TargetRep.RAB) + ";" +

                        Valor(m.TargetRep.DCR) + ";" +
                        Valor(m.TargetRep.GBS) + ";" +
                        Valor(m.TargetRep.SIM) + ";" +
                        Valor(m.TargetRep.TST) + ";" +
                        Valor(m.TargetRep.SAA) + ";" +
                        Valor(m.TargetRep.CL) + ";" +

                        Valor(m.TargetRep.IPC) + ";" +
                        Valor(m.TargetRep.NOGO) + ";" +
                        Valor(m.TargetRep.CPR) + ";" +
                        Valor(m.TargetRep.LDPJ) + ";" +
                        Valor(m.TargetRep.RCF)
                    );
                }
            }
        }

        private void ExportarCombinado(List<Mensaje> mensajes, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                sw.WriteLine(
                    
                    // DATOS COMUNES
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

                    
                    // TARGET REPORT DESCRIPTOR
                    // Algunos campos existen en ambas categorías
                    "TRD_SIM;" +
                    "TRD_RAB;" +
                    "TRD_TST;" +

                    // Solo CAT21
                    "TRD_ATP;" +
                    "TRD_ARC;" +
                    "TRD_RC;" +
                    "TRD_DCR;" +
                    "TRD_GBS;" +
                    "TRD_SAA;" +
                    "TRD_CL;" +
                    "TRD_IPC;" +
                    "TRD_NOGO;" +
                    "TRD_CPR;" +
                    "TRD_LDPJ;" +
                    "TRD_RCF;" +

                    // Solo CAT48
                    "TRD_TYP;" +
                    "TRD_RDP;" +
                    "TRD_SPI;" +
                    "TRD_ERR;" +
                    "TRD_XPP;" +
                    "TRD_ME;" +
                    "TRD_MI;" +
                    "TRD_FOE_FRI;" +
                    "TRD_ADSB_EP;" +
                    "TRD_ADSB_VAL;" +
                    "TRD_SCN_EP;" +
                    "TRD_SCN_VAL;" +
                    "TRD_PAI_EP;" +
                    "TRD_PAI_VAL;" +

                    
                    // SOLO CAT21
                    "QNH;" +
                    "ReservedExpField;" +
                    
                    // SOLO CAT48 - POLAR
                    "RHO_NM;" +
                    "THETA_deg;" +

                    // CAT48 - MODE 3/A
                    
                    "Mode3A_V;" +
                    "Mode3A_G;" +
                    "Mode3A_L;" +

                    
                    // CAT48 - FL
                    
                    "FL_V;" +
                    "FL_G;" +

                    
                    // CAT48 - RADAR PLOT
                    
                    "RadarPlot_SRL;" +
                    "RadarPlot_SRR;" +
                    "RadarPlot_SAM;" +
                    "RadarPlot_PRL;" +
                    "RadarPlot_PAM;" +
                    "RadarPlot_RPD;" +
                    "RadarPlot_APD;" +

                    
                    // CAT48 - TRACK
                    
                    "TrackNumber;" +
                    "GroundSpeed_kt;" +
                    "Heading_deg;" +

                    
                    // CAT48 - TRACK STATUS
                    
                    "TrackStatus_CNF;" +
                    "TrackStatus_RAD;" +
                    "TrackStatus_DOU;" +
                    "TrackStatus_MAH;" +
                    "TrackStatus_CDM;" +
                    "TrackStatus_TRE;" +
                    "TrackStatus_GHO;" +
                    "TrackStatus_SUP;" +
                    "TrackStatus_TCC;" +

                    
                    // CAT48 - COMM / ACAS
                    
                    "COM;" +
                    "STAT;" +
                    "SI;" +
                    "MSSC;" +
                    "ARC;" +
                    "AIC;" +
                    "B1A;" +
                    "B1B;" +

                    
                    // CAT48 - BDS 4,0
                    
                    "BDS40_MCP;" +
                    "BDS40_FMS;" +
                    "BDS40_Baro;" +
                    "BDS40_VNAV;" +
                    "BDS40_AltHold;" +
                    "BDS40_App;" +

                    
                    // CAT48 - BDS 5,0
                    
                    "BDS50_Roll;" +
                    "BDS50_Track;" +
                    "BDS50_GroundSpeed;" +
                    "BDS50_TrackRate;" +
                    "BDS50_TrueAirspeed;" +

                    
                    // CAT48 - BDS 6,0
                    
                    "BDS60_Heading;" +
                    "BDS60_IAS;" +
                    "BDS60_Mach;" +
                    "BDS60_BaroRate;" +
                    "BDS60_InertialVS"
                );


                foreach (Mensaje mensaje in mensajes)
                {
                    // CAT21
                    if (mensaje.categoria == 21)
                    {
                        TiraDatosDecod021 m =
                            (TiraDatosDecod021)mensaje.tira;

                        sw.WriteLine(
                            
                            // DATOS COMUNES
                            
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
                            Valor(m.AltitudCorregida) + ";" +

                            
                            // TRD COMUNES
                            
                            Valor(m.TargetRep.SIM) + ";" +
                            Valor(m.TargetRep.RAB) + ";" +
                            Valor(m.TargetRep.TST) + ";" +

                            
                            // TRD CAT21
                            
                            Valor(m.TargetRep.ATP) + ";" +
                            Valor(m.TargetRep.ARC) + ";" +
                            Valor(m.TargetRep.RC) + ";" +
                            Valor(m.TargetRep.DCR) + ";" +
                            Valor(m.TargetRep.GBS) + ";" +
                            Valor(m.TargetRep.SAA) + ";" +
                            Valor(m.TargetRep.CL) + ";" +
                            Valor(m.TargetRep.IPC) + ";" +
                            Valor(m.TargetRep.NOGO) + ";" +
                            Valor(m.TargetRep.CPR) + ";" +
                            Valor(m.TargetRep.LDPJ) + ";" +
                            Valor(m.TargetRep.RCF) + ";" +

                            
                            // TRD CAT48 -> VACÍOS
                            // 14 campos
                            
                            Vacio(14) +

                            
                            // CAT21
                            
                            Valor(m.QNH) + ";" +
                            Valor(m.ReservedExpField) + ";" +

                            
                            // RESTO CAT48 -> VACÍO
                            
                            Vacio(53)
                        );
                    }


                    
                    // CAT48
                    
                    else if (mensaje.categoria == 48)
                    {
                        TiraDatosDecod048 m =
                            (TiraDatosDecod048)mensaje.tira;

                        sw.WriteLine(
                            // DATOS COMUNES
                            "48;" +
                            Valor(m.DataSourceID[0]) + ";" +
                            Valor(m.DataSourceID[1]) + ";" +
                            Valor(m.TimeOfDay) + ";" +
                            Valor(m.AircrftAddrs) + ";" +
                            Valor(m.AircrftIddent) + ";" +
                            Valor(m.mode3.reply) + ";" +
                            Valor(m.FL.FL) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[0]) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[1]) + ";" +
                            Valor(m.PositionCorrectedLatLonAlt[2]) + ";" +

                            // TRD 
                            Valor(m.TargetRep.SIM) + ";" +
                            Valor(m.TargetRep.RAB) + ";" +
                            Valor(m.TargetRep.TST) + ";" +

                            // TRD CAT21 -> VACÍOS
                            // 12 campos
                            Vacio(12) +

                            // TRD CAT48
                            Valor(m.TargetRep.TYP) + ";" +
                            Valor(m.TargetRep.RDP) + ";" +
                            Valor(m.TargetRep.SPI) + ";" +
                            Valor(m.TargetRep.ERR) + ";" +
                            Valor(m.TargetRep.XPP) + ";" +
                            Valor(m.TargetRep.ME) + ";" +
                            Valor(m.TargetRep.MI) + ";" +
                            Valor(m.TargetRep.FOE_FRI) + ";" +
                            Valor(m.TargetRep.ADSB_EP) + ";" +
                            Valor(m.TargetRep.ADSB_VAL) + ";" +
                            Valor(m.TargetRep.SCN_EP) + ";" +
                            Valor(m.TargetRep.SCN_VAL) + ";" +
                            Valor(m.TargetRep.PAI_EP) + ";" +
                            Valor(m.TargetRep.PAI_VAL) + ";" +

                            // CAT21 -> VACÍOS
                            // QNH + ReservedExpField
                            Vacio(2) +

                            // POSICIÓN POLAR
                            Valor(m.PosSlantPolarCoord[0]) + ";" +
                            Valor(m.PosSlantPolarCoord[1]) + ";" +

                            // MODE 3/A
                            Valor(m.mode3.V) + ";" +
                            Valor(m.mode3.G) + ";" +
                            Valor(m.mode3.L) + ";" +

                            // FL
                            Valor(m.FL.V) + ";" +
                            Valor(m.FL.G) + ";" +

                            // RADAR PLOT
                            Valor(m.RadarPlot.SRL) + ";" +
                            Valor(m.RadarPlot.SRR) + ";" +
                            Valor(m.RadarPlot.SAM) + ";" +
                            Valor(m.RadarPlot.PRL) + ";" +
                            Valor(m.RadarPlot.PAM) + ";" +
                            Valor(m.RadarPlot.RPD) + ";" +
                            Valor(m.RadarPlot.APD) + ";" +

                            // TRACK
                            Valor(m.TrackNum) + ";" +

                            (m.tieneTrackVelocity
                                ? Valor(m.TrckVelPolRepr[0])
                                : "") + ";" +

                            (m.tieneTrackVelocity
                                ? Valor(m.TrckVelPolRepr[1])
                                : "") + ";" +

                            
                            // TRACK STATUS
                            
                            Valor(m.TrckStatus.CNF) + ";" +
                            Valor(m.TrckStatus.RAD) + ";" +
                            Valor(m.TrckStatus.DOU) + ";" +
                            Valor(m.TrckStatus.MAH) + ";" +
                            Valor(m.TrckStatus.CDM) + ";" +
                            Valor(m.TrckStatus.TRE) + ";" +
                            Valor(m.TrckStatus.GHO) + ";" +
                            Valor(m.TrckStatus.SUP) + ";" +
                            Valor(m.TrckStatus.TCC) + ";" +

                            
                            // COMM / ACAS
                            
                            Valor(m.CommACAScapability.COM) + ";" +
                            Valor(m.CommACAScapability.STAT) + ";" +
                            Valor(m.CommACAScapability.SI) + ";" +
                            Valor(m.CommACAScapability.MSSC) + ";" +
                            Valor(m.CommACAScapability.ARC) + ";" +
                            Valor(m.CommACAScapability.AIC) + ";" +
                            Valor(m.CommACAScapability.B1A) + ";" +
                            Valor(m.CommACAScapability.B1B) + ";" +

                            
                            // BDS 4,0
                            
                            ValorBDS40(m) +

                            
                            // BDS 5,0
                            
                            ValorBDS50(m) +

                            
                            // BDS 6,0
                            
                            ValorBDS60(m)
                        );
                    }
                }
            }
        }
        private string TextoTYP(string typ)
        {
            switch (typ)
            {
                case "000": return "No detection";
                case "001": return "Single PSR detection";
                case "010": return "Single SSR detection";
                case "011": return "SSR + PSR detection";
                case "100": return "Single ModeS All-Call";
                case "101": return "Single ModeS Roll-Call";
                case "110": return "ModeS All-Call + PSR";
                case "111": return "ModeS Roll-Call + PSR";
                default: return typ ?? "";
            }
        }

        private string TextoRAD(string rad)
        {
            switch (rad)
            {
                case "00": return "Combined Track";
                case "01": return "PSR Track";
                case "10": return "SSR/Mode S Track";
                case "11": return "Invalid";
                default: return rad ?? "";
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

        private string Vacio(int cantidad)
        {
            return new string(';', cantidad);
        }

        private string ValorBDS40(TiraDatosDecod048 m)
        {
            if (m.ModeS.BDS40 == null)
            {
                return Vacio(6);
            }

            return
                Valor(m.ModeS.BDS40.MCP) + ";" +
                Valor(m.ModeS.BDS40.FMS) + ";" +
                Valor(m.ModeS.BDS40.Baro) + ";" +
                Valor(m.ModeS.BDS40.VNAV) + ";" +
                Valor(m.ModeS.BDS40.AltHold) + ";" +
                Valor(m.ModeS.BDS40.App) + ";";
        }

        private string ValorBDS50(TiraDatosDecod048 m)
        {
            if (m.ModeS.BDS50 == null)
            {
                return Vacio(5);
            }

            return
                Valor(m.ModeS.BDS50.Roll) + ";" +
                Valor(m.ModeS.BDS50.Track) + ";" +
                Valor(m.ModeS.BDS50.GroundSpeed) + ";" +
                Valor(m.ModeS.BDS50.TrackRate) + ";" +
                Valor(m.ModeS.BDS50.TrueAirspeed) + ";";
        }

        private string ValorBDS60(TiraDatosDecod048 m)
        {
            if (m.ModeS.BDS60 == null)
            {
                return Vacio(5);
            }

            return
                Valor(m.ModeS.BDS60.Heading) + ";" +
                Valor(m.ModeS.BDS60.IAS) + ";" +
                Valor(m.ModeS.BDS60.Mach) + ";" +
                Valor(m.ModeS.BDS60.BaroRate) + ";" +
                Valor(m.ModeS.BDS60.InertialVS);
        }
    }
}
