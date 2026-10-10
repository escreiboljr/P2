using DatosDecod;
using DatosDecod21;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;

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

                        TextoTYP(m.TargetRep.TYP) + ";" +
                        TextoSIM(m.TargetRep.SIM) + ";" +
                        TextoRDP(m.TargetRep.RDP) + ";" +
                        TextoSPI(m.TargetRep.SPI) + ";" +
                        TextoRAB48(m.TargetRep.RAB) + ";" +
                        TextoTST48(m.TargetRep.TST) + ";" +
                        TextoERR(m.TargetRep.ERR) + ";" +
                        TextoXPP(m.TargetRep.XPP) + ";" +
                        TextoME(m.TargetRep.ME) + ";" +
                        TextoMI(m.TargetRep.MI) + ";" +
                        TextoFOE_FRI(m.TargetRep.FOE_FRI) + ";" +
                        TextoADSB_EP(m.TargetRep.ADSB_EP) + ";" +
                        TextoADSB_VAL(m.TargetRep.ADSB_EP, m.TargetRep.ADSB_VAL) + ";" +
                        TextoSCN_EP(m.TargetRep.SCN_EP) + ";" +
                        TextoSCN_VAL(m.TargetRep.SCN_EP, m.TargetRep.SCN_VAL) + ";" +
                        TextoPAI_EP(m.TargetRep.PAI_EP) + ";" +
                        TextoPAI_VAL(m.TargetRep.PAI_EP, m.TargetRep.PAI_VAL) + ";" +

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

                        (m.tieneTrackVelocity ? Valor(m.TrckVelPolRepr[0]) : "-") + ";" +

                        (m.tieneTrackVelocity ? Valor(m.TrckVelPolRepr[1]) : "-") + ";" +

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

                        TextoATP21(m.TargetRep.ATP) + ";" +
                        TextoARC21(m.TargetRep.ARC) + ";" +
                        TextoRC21(m.TargetRep.RC) + ";" +
                        TextoRAB21(m.TargetRep.RAB) + ";" +

                        TextoDCR21(m.TargetRep.DCR) + ";" +
                        TextoGBS21(m.TargetRep.GBS) + ";" +
                        TextoSIM(m.TargetRep.SIM) + ";" +
                        TextoTST21(m.TargetRep.TST) + ";" +
                        TextoSAA21(m.TargetRep.SAA) + ";" +
                        TextoCL21(m.TargetRep.CL) + ";" +

                        TextoIPC21(m.TargetRep.IPC) + ";" +
                        TextoNOGO21(m.TargetRep.NOGO) + ";" +
                        TextoCPR21(m.TargetRep.CPR) + ";" +
                        TextoLDPJ21(m.TargetRep.LDPJ) + ";" +
                        TextoRCF21(m.TargetRep.RCF)
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

                            TextoSIM(m.TargetRep.SIM) + ";" +
                            TextoRAB21(m.TargetRep.RAB) + ";" +
                            TextoTST21(m.TargetRep.TST) + ";" +


                            // TRD CAT21

                            TextoATP21(m.TargetRep.ATP) + ";" +
                            TextoARC21(m.TargetRep.ARC) + ";" +
                            TextoRC21(m.TargetRep.RC) + ";" +
                            TextoDCR21(m.TargetRep.DCR) + ";" +
                            TextoGBS21(m.TargetRep.GBS) + ";" +
                            TextoSAA21(m.TargetRep.SAA) + ";" +
                            TextoCL21(m.TargetRep.CL) + ";" +
                            TextoIPC21(m.TargetRep.IPC) + ";" +
                            TextoNOGO21(m.TargetRep.NOGO) + ";" +
                            TextoCPR21(m.TargetRep.CPR) + ";" +
                            TextoLDPJ21(m.TargetRep.LDPJ) + ";" +
                            TextoRCF21(m.TargetRep.RCF) + ";" +


                            // TRD CAT48 -> VACÍOS
                            // 14 campos

                            Vacio(14) +


                            // CAT21

                            Valor(m.QNH) + ";" +
                            Valor(m.ReservedExpField) + ";" +


                            // RESTO CAT48 -> VACÍO

                            string.Join(";", Enumerable.Repeat("-", 50))
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
                            TextoSIM(m.TargetRep.SIM) + ";" +
                            TextoRAB48(m.TargetRep.RAB) + ";" +
                            TextoTST48(m.TargetRep.TST) + ";" +

                            // TRD CAT21 -> VACÍOS
                            // 12 campos
                            Vacio(12) +

                            // TRD CAT48
                            TextoTYP(m.TargetRep.TYP) + ";" +
                            TextoRDP(m.TargetRep.RDP) + ";" +
                            TextoSPI(m.TargetRep.SPI) + ";" +
                            TextoERR(m.TargetRep.ERR) + ";" +
                            TextoXPP(m.TargetRep.XPP) + ";" +
                            TextoME(m.TargetRep.ME) + ";" +
                            TextoMI(m.TargetRep.MI) + ";" +
                            TextoFOE_FRI(m.TargetRep.FOE_FRI) + ";" +
                            TextoADSB_EP(m.TargetRep.ADSB_EP) + ";" +
                            TextoADSB_VAL(m.TargetRep.ADSB_EP, m.TargetRep.ADSB_VAL) + ";" +
                            TextoSCN_EP(m.TargetRep.SCN_EP) + ";" +
                            TextoSCN_VAL(m.TargetRep.SCN_EP, m.TargetRep.SCN_VAL) + ";" +
                            TextoPAI_EP(m.TargetRep.PAI_EP) + ";" +
                            TextoPAI_VAL(m.TargetRep.PAI_EP, m.TargetRep.PAI_VAL) + ";" +

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
                                : "-") + ";" +

                            (m.tieneTrackVelocity
                                ? Valor(m.TrckVelPolRepr[1])
                                : "-") + ";" +


                            // TRACK STATUS

                            Valor(m.TrckStatus.CNF) + ";" +
                            TextoRAD(m.TrckStatus.RAD) + ";" +
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

                            ValorBDS40(m) + ";" +


                            // BDS 5,0

                            ValorBDS50(m) + ";" +


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
                default: return typ ?? "-";
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
                default: return rad ?? "-";
            }
        }

        private string TextoCL21(string cl)
        {
            switch (cl)
            {
                case "00": return "Report valid";
                case "01": return "Report suspect";
                case "10": return "No information";
                case "11": return "Reserved for future use";
                default: return cl ?? "";
            }
        }

        private string TextoIPC21(int ipc)
        {
            switch (ipc)
            {
                case 0: return "Default";
                case 1: return "Independent Position Check failed";
                default: return "";
            }
        }

        private string TextoNOGO21(int nogo)
        {
            switch (nogo)
            {
                case 0: return "NOGO-bit not set";
                case 1: return "NOGO-bit set";
                default: return "";
            }
        }

        private string TextoCPR21(int cpr)
        {
            switch (cpr)
            {
                case 0: return "CPR Validation correct";
                case 1: return "CPR Validation failed";
                default: return "";
            }
        }

        private string TextoLDPJ21(int ldpj)
        {
            switch (ldpj)
            {
                case 0: return "LDPJ not detected";
                case 1: return "LDPJ detected";
                default: return "";
            }
        }

        private string TextoRCF21(int rcf)
        {
            switch (rcf)
            {
                case 0: return "Default";
                case 1: return "Range Check failed";
                default: return "";
            }
        }
        private string TextoTST21(int tst)
        {
            switch (tst)
            {
                case 0: return "Default";
                case 1: return "Test Target";
                default: return "";
            }
        }

        private string TextoSAA21(int saa)
        {
            switch (saa)
            {
                case 0: return "Equipment capable to provide Selected Altitude";
                case 1: return "Equipment not capable to provide Selected Altitude";
                default: return "";
            }
        }

        private string TextoGBS21(int gbs)
        {
            switch (gbs)
            {
                case 0: return "Ground Bit not set";
                case 1: return "Ground Bit set";
                default: return "";
            }
        }
        private string TextoDCR21(int dcr)
        {
            switch (dcr)
            {
                case 0: return "No differential correction (ADS-B)";
                case 1: return "Differential correction (ADS-B)";
                default: return "";
            }
        }

        private string TextoRAB21(int rab)
        {
            switch (rab)
            {
                case 0: return "Report from target transponder";
                case 1: return "Report from field monitor (fixed transponder)";
                default: return "";
            }
        }
        private string TextoRC21(int rc)
        {
            switch (rc)
            {
                case 0: return "Default";
                case 1: return "Range Check passed, CPR Validation pending";
                default: return "-";
            }
        }
        private string TextoARC21(string arc)
        {
            switch (arc)
            {
                case "00": return "25 ft";
                case "01": return "100 ft";
                case "10": return "Unknown";
                case "11": return "Invalid";
                default: return arc ?? "-";
            }
        }

        private string TextoATP21(string atp)
        {
            switch (atp)
            {
                case "000": return "24-Bit ICAO address";
                case "001": return "Duplicate address";
                case "010": return "Surface vehicle address";
                case "011": return "Anonymous address";
                case "100":
                case "101":
                case "110":
                case "111":
                    return "Reserved for future use";
                default: return atp ?? "-";
            }
        }

        private string TextoPAI_VAL(int ep, int valor)
        {
            if (ep != 1)
                return "-";

            switch (valor)
            {
                case 0: return "Not available";
                case 1: return "Available";
                default: return "-";
            }
        }

        private string TextoPAI_EP(int ep)
        {
            switch (ep)
            {
                case 0: return "PAI not populated";
                case 1: return "PAI populated";
                default: return "-";
            }
        }

        private string TextoSCN_VAL(int ep, int valor)
        {
            if (ep != 1)
                return "-";

            switch (valor)
            {
                case 0: return "Not available";
                case 1: return "Available";
                default: return "-";
            }
        }

        private string TextoSCN_EP(int ep)
        {
            switch (ep)
            {
                case 0: return "SCN not populated";
                case 1: return "SCN populated";
                default: return "-";
            }
        }

        private string TextoADSB_VAL(int ep, int valor)
        {
            if (ep != 1)
                return "-";

            switch (valor)
            {
                case 0: return "Not available";
                case 1: return "Available";
                default: return "-";
            }
        }

        private string TextoADSB_EP(int ep)
        {
            switch (ep)
            {
                case 0: return "ADSB not populated";
                case 1: return "ADSB populated";
                default: return "-";
            }
        }

        private string TextoFOE_FRI(string foeFri)
        {
            switch (foeFri)
            {
                case "00": return "No Mode 4 interrogation";
                case "01": return "Friendly target";
                case "10": return "Unknown target";
                case "11": return "No reply";
                default: return foeFri ?? "-";
            }
        }

        private string TextoMI(int mi)
        {
            switch (mi)
            {
                case 0: return "No military identification";
                case 1: return "Military identification";
                default: return "-";
            }
        }

        private string TextoME(int me)
        {
            switch (me)
            {
                case 0: return "No military emergency";
                case 1: return "Military emergency";
                default: return "-";
            }
        }

        private string TextoXPP(int xpp)
        {
            switch (xpp)
            {
                case 0: return "No X-Pulse present";
                case 1: return "X-Pulse present";
                default: return "-";
            }
        }

        private string TextoERR(int err)
        {
            switch (err)
            {
                case 0: return "No Extended Range";
                case 1: return "Extended Range present";
                default: return "-";
            }
        }

        private string TextoTST48(int tst)
        {
            switch (tst)
            {
                case 0: return "Real target report";
                case 1: return "Test target report";
                default: return "-";
            }
        }
        private string TextoRAB48(int rab)
        {
            switch (rab)
            {
                case 0: return "Report from aircraft transponder";
                case 1: return "Report from field monitor (fixed transponder)";
                default: return "-";
            }
        }

        private string TextoSPI(int spi)
        {
            switch (spi)
            {
                case 0: return "Absence of SPI";
                case 1: return "Special Position Identification";
                default: return "-";
            }
        }
        private string TextoRDP(int rdp)
        {
            switch (rdp)
            {
                case 0: return "Report from RDP Chain 1";
                case 1: return "Report from RDP Chain 2";
                default: return "-";
            }
        }

        private string TextoSIM(int sim)
        {
            switch (sim)
            {
                case 0: return "Actual target report";
                case 1: return "Simulated target report";
                default: return "-";
            }
        }

        private string Valor(object valor)
        {
            if (valor == null)
                return "-";

            if (valor is double)
            {
                double numero = (double)valor;

                if (double.IsNaN(numero))
                    return "-";

                return numero.ToString(
                    CultureInfo.GetCultureInfo("es-ES")
                );
            }

            if (valor is int)
            {
                int numero = (int)valor;

                if (numero == -1 || numero == int.MinValue)
                    return "-";

                return numero.ToString();
            }

            if (valor is string)
            {
                string texto = (string)valor;

                if (string.IsNullOrEmpty(texto))
                    return "-";

                return texto;
            }

            return valor.ToString();
        }

        private string Vacio(int cantidad)
        {
            return string.Join(";", Enumerable.Repeat("-", cantidad)) + ";";
        }

        private string ValorBDS40(TiraDatosDecod048 m)
        {
            if (m.ModeS?.BDS40 == null)
                return string.Join(";", Enumerable.Repeat("-", 6));

            var bds = m.ModeS.BDS40;

            return string.Join(";", new string[]
            {
                Valor(bds.MCP),
                Valor(bds.FMS),
                Valor(bds.Baro),
                Valor(bds.VNAV),
                Valor(bds.AltHold),
                Valor(bds.App)
            });
        }

        private string ValorBDS50(TiraDatosDecod048 m) // Prepara los cinco datos de BDS50 para exportarlos al CSV                                        
        {
            if (m.ModeS?.BDS50 == null)
                return string.Join(";", Enumerable.Repeat("-", 5)); // Si no hay datos escribe cinco guiones uno por columna

            var bds = m.ModeS.BDS50;

            return string.Join(";", new string[]   // Separa los valores con ; sin añadir un separador al final
            {
                Valor(bds.Roll),
                Valor(bds.Track),
                Valor(bds.GroundSpeed),
                Valor(bds.TrackRate),
                Valor(bds.TrueAirspeed)
            });
        }

        private string ValorBDS60(TiraDatosDecod048 m)  // Prepara los cinco datos de BDS60 para exportarlos al CSV
        {
            if (m.ModeS?.BDS60 == null)
                return string.Join(";", Enumerable.Repeat("-", 5)); // Si no hay datos escribe cinco guiones uno por columna.

            var bds = m.ModeS.BDS60;

            return string.Join(";", new string[]   // Separa los valores con ; sin añadir un separador al final
            {
                Valor(bds.Heading),
                Valor(bds.IAS),
                Valor(bds.Mach),
                Valor(bds.BaroRate),
                Valor(bds.InertialVS)
            });
        }

    }
}