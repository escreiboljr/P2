using Archivos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatosDecod21;

namespace GUI_ASTERIX
{
    public partial class InformacionCAT21 : Form
    {
        public Mensaje mensaje;
        private ExportarCSV csv = new ExportarCSV();
        private string Valor(object valor) => csv.Valor(valor);

        public InformacionCAT21()
        {
            InitializeComponent();
        }

        private void InformacionCAT21_Load(object sender, EventArgs e)
        {
            if (mensaje == null || mensaje.categoria != 21 ||
                !(mensaje.tira is TiraDatosDecod021))
            {
                MessageBox.Show("No hay datos CAT21 disponibles.");
                return;
            }

            TiraDatosDecod021 datos = (TiraDatosDecod021)mensaje.tira;

            // IDENTIFICACIÓN
            labelIdentificador.Text =
                "Identificador: " + Valor(datos.TargetIdentification);


            // TARGET REPORT DESCRIPTOR
            labelATP.Text =
                "ATP: " + csv.TextoATP21(datos.TargetRep.ATP);

            labelARC.Text =
                "ARC: " + csv.TextoARC21(datos.TargetRep.ARC);

            labelRC.Text =
                "RC: " + csv.TextoRC21(datos.TargetRep.RC);

            labelRAB.Text =
                "RAB: " + csv.TextoRAB21(datos.TargetRep.RAB);

            labelDCR.Text =
                "DCR: " + csv.TextoDCR21(datos.TargetRep.DCR);

            labelGBS.Text =
                "GBS: " + csv.TextoGBS21(datos.TargetRep.GBS);

            labelSIM.Text =
                "SIM: " + csv.TextoSIM(datos.TargetRep.SIM);

            labelTST.Text =
                "TST: " + csv.TextoTST21(datos.TargetRep.TST);

            labelSAA.Text =
                "SAA: " + csv.TextoSAA21(datos.TargetRep.SAA);

            labelCL.Text =
                "CL: " + csv.TextoCL21(datos.TargetRep.CL);

            labelIPC.Text =
                "IPC: " + csv.TextoIPC21(datos.TargetRep.IPC);

            labelNOGO.Text =
                "NOGO: " + csv.TextoNOGO21(datos.TargetRep.NOGO);

            labelCPR.Text =
                "CPR: " + csv.TextoCPR21(datos.TargetRep.CPR);

            labelLDPJ.Text =
                "LDPJ: " + csv.TextoLDPJ21(datos.TargetRep.LDPJ);

            labelRCF.Text =
                "RCF: " + csv.TextoRCF21(datos.TargetRep.RCF);


            // OTHER
            labelReservedExpFiedl.Text =
                "BPS: " + Valor(datos.ReservedExpField);

        }

    }
}
