using Archivos;
using DatosDecod;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public partial class InformacionCAT48 : Form
    {
        public Mensaje mensaje;
        private ExportarCSV csv = new ExportarCSV();

        private string Valor(object valor) => csv.Valor(valor);
        private string TextoTYP(string v) => csv.TextoTYP(v);
        private string TextoSIM(int v) => csv.TextoSIM(v);
        private string TextoRDP(int v) => csv.TextoRDP(v);
        private string TextoSPI(int v) => csv.TextoSPI(v);
        private string TextoRAB48(int v) => csv.TextoRAB48(v);
        private string TextoTST48(int v) => csv.TextoTST48(v);
        private string TextoERR(int v) => csv.TextoERR(v);
        private string TextoXPP(int v) => csv.TextoXPP(v);
        private string TextoME(int v) => csv.TextoME(v);
        private string TextoMI(int v) => csv.TextoMI(v);
        private string TextoFOE_FRI(string v) => csv.TextoFOE_FRI(v);
        private string TextoADSB_EP(int v) => csv.TextoADSB_EP(v);
        private string TextoADSB_VAL(int ep, int v) => csv.TextoADSB_VAL(ep, v);
        private string TextoSCN_EP(int v) => csv.TextoSCN_EP(v);
        private string TextoSCN_VAL(int ep, int v) => csv.TextoSCN_VAL(ep, v);
        private string TextoPAI_EP(int v) => csv.TextoPAI_EP(v);
        private string TextoPAI_VAL(int ep, int v) => csv.TextoPAI_VAL(ep, v);
        private string TextoRAD(string v) => csv.TextoRAD(v);
        private string TextoCNF(int v) => csv.TextoCNF(v);
        private string TextoDOU(int v) => csv.TextoDOU(v);
        private string TextoMAH(int v) => csv.TextoMAH(v);
        private string TextoCDM(string v) => csv.TextoCDM(v);
        private string TextoTRE(int v) => csv.TextoTRE(v);
        private string TextoGHO(int v) => csv.TextoGHO(v);
        private string TextoSUP(int v) => csv.TextoSUP(v);
        private string TextoTCC(int v) => csv.TextoTCC(v);
        private string TextoCOM(int v) => csv.TextoCOM(v);
        private string TextoSTAT(int v) => csv.TextoSTAT(v);
        private string TextoSI(int v) => csv.TextoSI(v);
        private string TextoMSSC(int v) => csv.TextoMSSC(v);
        private string TextoARC(int v) => csv.TextoARC(v);
        private string TextoAIC(int v) => csv.TextoAIC(v);

        public InformacionCAT48()
        {
            InitializeComponent();
        }

        private void InformacionCAT48_Load(object sender, EventArgs e)
        {
            if (mensaje == null || mensaje.categoria != 48 ||
                !(mensaje.tira is TiraDatosDecod048))
            {
                MessageBox.Show("No hay datos CAT48 disponibles.");
                return;
            }

            TiraDatosDecod048 datos = (TiraDatosDecod048)mensaje.tira;

            // IDENTIFICACIÓN
            labelIdentificador.Text =
                "Identificador: " + Valor(datos.AircrftIddent);

            labelAircraftAddres.Text =
                "Aircraft Address: " + Valor(datos.AircrftAddrs);

            labelAircraftIddent.Text =
                "Aircraft Identifier: " + Valor(datos.AircrftIddent);


            // TARGET REPORT DESCRIPTOR
            labelTYP.Text = "TYP: " + TextoTYP(datos.TargetRep.TYP);
            labelSIM.Text = "SIM: " + TextoSIM(datos.TargetRep.SIM);
            label3.Text = "RDP: " + TextoRDP(datos.TargetRep.RDP);
            label4.Text = "SPI: " + TextoSPI(datos.TargetRep.SPI);
            labelRAB.Text = "RAB: " + TextoRAB48(datos.TargetRep.RAB);
            labelTST.Text = "TST: " + TextoTST48(datos.TargetRep.TST);
            labelERR.Text = "ERR: " + TextoERR(datos.TargetRep.ERR);
            labelXPP.Text = "XPP: " + TextoXPP(datos.TargetRep.XPP);
            labelME.Text = "ME: " + TextoME(datos.TargetRep.ME);
            labelMI.Text = "MI: " + TextoMI(datos.TargetRep.MI);

            labelFOE_FRI.Text =
                "FOE/FRI: " + TextoFOE_FRI(datos.TargetRep.FOE_FRI);

            labelADSBEP.Text =
                "ADSB_EP: " + TextoADSB_EP(datos.TargetRep.ADSB_EP);

            labelADSB_VAL.Text =
                "ADSB_VAL: " + TextoADSB_VAL(
                    datos.TargetRep.ADSB_EP,
                    datos.TargetRep.ADSB_VAL);

            labelSCN_EP.Text =
                "SCN_EP: " + TextoSCN_EP(datos.TargetRep.SCN_EP);

            labelSCN_VAL.Text =
                "SCN_VAL: " + TextoSCN_VAL(
                    datos.TargetRep.SCN_EP,
                    datos.TargetRep.SCN_VAL);

            labelPAI_EP.Text =
                "PAI_EP: " + TextoPAI_EP(datos.TargetRep.PAI_EP);

            labelPAI_VAL.Text =
                "PAI_VAL: " + TextoPAI_VAL(
                    datos.TargetRep.PAI_EP,
                    datos.TargetRep.PAI_VAL);


            // RADAR PLOT CHARACTERISTICS
            labelSRL.Text = "SRL: " + Valor(datos.RadarPlot.SRL);
            labelSRR.Text = "SRR: " + Valor(datos.RadarPlot.SRR);
            labelSAM.Text = "SAM: " + Valor(datos.RadarPlot.SAM);
            labelPRL.Text = "PRL: " + Valor(datos.RadarPlot.PRL);
            labelPAM.Text = "PAM: " + Valor(datos.RadarPlot.PAM);
            labelRPD.Text = "RPD: " + Valor(datos.RadarPlot.RPD);
            labelAPD.Text = "APD: " + Valor(datos.RadarPlot.APD);


            // TRACK STATUS
            labelCNF.Text = "CNF: " + TextoCNF(datos.TrckStatus.CNF);
            labelRAD.Text = "RAD: " + TextoRAD(datos.TrckStatus.RAD);
            labelDOU.Text = "DOU: " + TextoDOU(datos.TrckStatus.DOU);
            labelMAH.Text = "MAH: " + TextoMAH(datos.TrckStatus.MAH);
            labelCDM.Text = "CDM: " + TextoCDM(datos.TrckStatus.CDM);
            labelTRE.Text = "TRE: " + TextoTRE(datos.TrckStatus.TRE);
            labelGHO.Text = "GHO: " + TextoGHO(datos.TrckStatus.GHO);
            labelSUP.Text = "SUP: " + TextoSUP(datos.TrckStatus.SUP);
            labelTCC.Text = "TCC: " + TextoTCC(datos.TrckStatus.TCC);


            // COMMUNICATION / ACAS CAPABILITY
            labelCOM.Text = "COM: " + TextoCOM(datos.CommACAScapability.COM);
            labelSTAT.Text = "STAT: " + TextoSTAT(datos.CommACAScapability.STAT);
            labelSI.Text = "SI: " + TextoSI(datos.CommACAScapability.SI);
            labelMSSC.Text = "MSSC: " + TextoMSSC(datos.CommACAScapability.MSSC);
            labelARC.Text = "ARC: " + TextoARC(datos.CommACAScapability.ARC);
            labelAIC.Text = "AIC: " + TextoAIC(datos.CommACAScapability.AIC);
            labelB1A.Text = "B1A: " + Valor(datos.CommACAScapability.B1A);
            labelB1B.Text = "B1B: " + Valor(datos.CommACAScapability.B1B);


            // MODE S - BDS 4.0
            if (datos.ModeS?.BDS40 != null)
            {
                var bds = datos.ModeS.BDS40;

                labelMCP.Text = "MCP: " + Valor(bds.MCP);
                labelFMS.Text = "FMS: " + Valor(bds.FMS);
                labelBaro.Text = "Baro: " + Valor(bds.Baro);
                labelVNAV.Text = "VNAV: " + Valor(bds.VNAV);
                labelAltHold.Text = "AltHold: " + Valor(bds.AltHold);
                labelApp.Text = "App: " + Valor(bds.App);
            }


            // MODE S - BDS 5.0
            if (datos.ModeS?.BDS50 != null)
            {
                var bds = datos.ModeS.BDS50;

                labelRoll.Text = "Roll: " + Valor(bds.Roll);
                labelTrack.Text = "Track: " + Valor(bds.Track);
                labelGroundSpeed.Text =
                    "GroundSpeed: " + Valor(bds.GroundSpeed);
                labelTrackRate.Text =
                    "TrackRate: " + Valor(bds.TrackRate);
                labelTrueAirspeed.Text =
                    "TrueAirspeed: " + Valor(bds.TrueAirspeed);
            }


            // MODE S - BDS 6.0
            if (datos.ModeS?.BDS60 != null)
            {
                var bds = datos.ModeS.BDS60;

                labelHeading.Text = "Heading: " + Valor(bds.Heading);
                labelIAS.Text = "IAS: " + Valor(bds.IAS);
                labelMach.Text = "Mach: " + Valor(bds.Mach);
                labelBaroRate.Text =
                    "BaroRate: " + Valor(bds.BaroRate);
                labelInertialVS.Text =
                    "InertialVS: " + Valor(bds.InertialVS);
            }
        }

    }
}
