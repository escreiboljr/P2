namespace GUI_ASTERIX
{
    partial class InformacionAvion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labelIdentificador = new Label();
            labelFL = new Label();
            timerActualizar = new System.Windows.Forms.Timer(components);
            labelTituloPosicion = new Label();
            labelLatitud = new Label();
            labelLongitud = new Label();
            labelAltitud = new Label();
            labelTituloVuelo = new Label();
            labelDireccion = new Label();
            labelRumbo = new Label();
            labelTrackNumber = new Label();
            labelTituloDeteccion = new Label();
            labelRadar = new Label();
            labelADSB = new Label();
            labelUltimoTiempo = new Label();
            labelUltimoTiempoRadar = new Label();
            labelUltimoTiempoADSB = new Label();
            SuspendLayout();
            // 
            // labelIdentificador
            // 
            labelIdentificador.AutoSize = true;
            labelIdentificador.BackColor = Color.Transparent;
            labelIdentificador.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelIdentificador.ForeColor = Color.Turquoise;
            labelIdentificador.Location = new Point(55, 39);
            labelIdentificador.Margin = new Padding(4, 0, 4, 0);
            labelIdentificador.Name = "labelIdentificador";
            labelIdentificador.Size = new Size(294, 45);
            labelIdentificador.TabIndex = 0;
            labelIdentificador.Text = "Identificador: N/A";
            // 
            // labelFL
            // 
            labelFL.AutoSize = true;
            labelFL.BackColor = Color.Transparent;
            labelFL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFL.ForeColor = Color.LightGoldenrodYellow;
            labelFL.Location = new Point(55, 293);
            labelFL.Margin = new Padding(4, 0, 4, 0);
            labelFL.Name = "labelFL";
            labelFL.Size = new Size(171, 28);
            labelFL.TabIndex = 1;
            labelFL.Text = "Flight Level: N/A";
            // 
            // timerActualizar
            // 
            timerActualizar.Tick += timerActualizar_Tick;
            // 
            // labelTituloPosicion
            // 
            labelTituloPosicion.AutoSize = true;
            labelTituloPosicion.BackColor = Color.Transparent;
            labelTituloPosicion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloPosicion.ForeColor = Color.Lavender;
            labelTituloPosicion.Location = new Point(55, 101);
            labelTituloPosicion.Name = "labelTituloPosicion";
            labelTituloPosicion.Size = new Size(128, 32);
            labelTituloPosicion.TabIndex = 2;
            labelTituloPosicion.Text = "POSICIÓN";
            // 
            // labelLatitud
            // 
            labelLatitud.AutoSize = true;
            labelLatitud.BackColor = Color.Transparent;
            labelLatitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLatitud.ForeColor = Color.Lavender;
            labelLatitud.Location = new Point(54, 133);
            labelLatitud.Name = "labelLatitud";
            labelLatitud.Size = new Size(129, 28);
            labelLatitud.TabIndex = 3;
            labelLatitud.Text = "Latitud: N/A";
            // 
            // labelLongitud
            // 
            labelLongitud.AutoSize = true;
            labelLongitud.BackColor = Color.Transparent;
            labelLongitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLongitud.ForeColor = Color.Lavender;
            labelLongitud.Location = new Point(55, 161);
            labelLongitud.Name = "labelLongitud";
            labelLongitud.Size = new Size(146, 28);
            labelLongitud.TabIndex = 4;
            labelLongitud.Text = "Longitud: N/A";
            // 
            // labelAltitud
            // 
            labelAltitud.AutoSize = true;
            labelAltitud.BackColor = Color.Transparent;
            labelAltitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAltitud.ForeColor = Color.Lavender;
            labelAltitud.Location = new Point(54, 189);
            labelAltitud.Name = "labelAltitud";
            labelAltitud.Size = new Size(128, 28);
            labelAltitud.TabIndex = 5;
            labelAltitud.Text = "Altitud: N/A";
            // 
            // labelTituloVuelo
            // 
            labelTituloVuelo.AutoSize = true;
            labelTituloVuelo.BackColor = Color.Transparent;
            labelTituloVuelo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloVuelo.ForeColor = Color.LightGoldenrodYellow;
            labelTituloVuelo.Location = new Point(55, 261);
            labelTituloVuelo.Name = "labelTituloVuelo";
            labelTituloVuelo.Size = new Size(89, 32);
            labelTituloVuelo.TabIndex = 6;
            labelTituloVuelo.Text = "VUELO";
            // 
            // labelDireccion
            // 
            labelDireccion.AutoSize = true;
            labelDireccion.BackColor = Color.Transparent;
            labelDireccion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDireccion.ForeColor = Color.LightGoldenrodYellow;
            labelDireccion.Location = new Point(55, 321);
            labelDireccion.Name = "labelDireccion";
            labelDireccion.Size = new Size(152, 28);
            labelDireccion.TabIndex = 7;
            labelDireccion.Text = "Dirección: N/A";
            // 
            // labelRumbo
            // 
            labelRumbo.AutoSize = true;
            labelRumbo.BackColor = Color.Transparent;
            labelRumbo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRumbo.ForeColor = Color.LightGoldenrodYellow;
            labelRumbo.Location = new Point(55, 346);
            labelRumbo.Name = "labelRumbo";
            labelRumbo.Size = new Size(129, 28);
            labelRumbo.TabIndex = 8;
            labelRumbo.Text = "Rumbo: N/A";
            // 
            // labelTrackNumber
            // 
            labelTrackNumber.AutoSize = true;
            labelTrackNumber.BackColor = Color.Transparent;
            labelTrackNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTrackNumber.ForeColor = Color.LightGoldenrodYellow;
            labelTrackNumber.Location = new Point(54, 371);
            labelTrackNumber.Name = "labelTrackNumber";
            labelTrackNumber.Size = new Size(196, 28);
            labelTrackNumber.TabIndex = 9;
            labelTrackNumber.Text = "Track Number: N/A";
            // 
            // labelTituloDeteccion
            // 
            labelTituloDeteccion.AutoSize = true;
            labelTituloDeteccion.BackColor = Color.Transparent;
            labelTituloDeteccion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloDeteccion.ForeColor = Color.PeachPuff;
            labelTituloDeteccion.Location = new Point(55, 446);
            labelTituloDeteccion.Name = "labelTituloDeteccion";
            labelTituloDeteccion.Size = new Size(146, 32);
            labelTituloDeteccion.TabIndex = 10;
            labelTituloDeteccion.Text = "DETECCIÓN";
            // 
            // labelRadar
            // 
            labelRadar.AutoSize = true;
            labelRadar.BackColor = Color.Transparent;
            labelRadar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRadar.ForeColor = Color.PeachPuff;
            labelRadar.Location = new Point(55, 478);
            labelRadar.Name = "labelRadar";
            labelRadar.Size = new Size(118, 28);
            labelRadar.TabIndex = 11;
            labelRadar.Text = "Radar: N/A";
            // 
            // labelADSB
            // 
            labelADSB.AutoSize = true;
            labelADSB.BackColor = Color.Transparent;
            labelADSB.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelADSB.ForeColor = Color.PeachPuff;
            labelADSB.Location = new Point(55, 503);
            labelADSB.Name = "labelADSB";
            labelADSB.Size = new Size(123, 28);
            labelADSB.TabIndex = 12;
            labelADSB.Text = "ADS-B: N/A";
            // 
            // labelUltimoTiempo
            // 
            labelUltimoTiempo.AutoSize = true;
            labelUltimoTiempo.BackColor = Color.Transparent;
            labelUltimoTiempo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempo.ForeColor = Color.PeachPuff;
            labelUltimoTiempo.Location = new Point(54, 528);
            labelUltimoTiempo.Name = "labelUltimoTiempo";
            labelUltimoTiempo.Size = new Size(199, 28);
            labelUltimoTiempo.TabIndex = 13;
            labelUltimoTiempo.Text = "Último tiempo: N/A";
            // 
            // labelUltimoTiempoRadar
            // 
            labelUltimoTiempoRadar.AutoSize = true;
            labelUltimoTiempoRadar.BackColor = Color.Transparent;
            labelUltimoTiempoRadar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempoRadar.ForeColor = Color.PeachPuff;
            labelUltimoTiempoRadar.Location = new Point(310, 478);
            labelUltimoTiempoRadar.Name = "labelUltimoTiempoRadar";
            labelUltimoTiempoRadar.Size = new Size(261, 28);
            labelUltimoTiempoRadar.TabIndex = 14;
            labelUltimoTiempoRadar.Text = "Último tiempo Radar: N/A";
            // 
            // labelUltimoTiempoADSB
            // 
            labelUltimoTiempoADSB.AutoSize = true;
            labelUltimoTiempoADSB.BackColor = Color.Transparent;
            labelUltimoTiempoADSB.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempoADSB.ForeColor = Color.PeachPuff;
            labelUltimoTiempoADSB.Location = new Point(310, 506);
            labelUltimoTiempoADSB.Name = "labelUltimoTiempoADSB";
            labelUltimoTiempoADSB.Size = new Size(266, 28);
            labelUltimoTiempoADSB.TabIndex = 15;
            labelUltimoTiempoADSB.Text = "Último tiempo ADS-B: N/A";
            // 
            // InformacionAvion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(762, 610);
            Controls.Add(labelUltimoTiempoADSB);
            Controls.Add(labelUltimoTiempoRadar);
            Controls.Add(labelUltimoTiempo);
            Controls.Add(labelADSB);
            Controls.Add(labelRadar);
            Controls.Add(labelTituloDeteccion);
            Controls.Add(labelTrackNumber);
            Controls.Add(labelRumbo);
            Controls.Add(labelDireccion);
            Controls.Add(labelTituloVuelo);
            Controls.Add(labelAltitud);
            Controls.Add(labelLongitud);
            Controls.Add(labelLatitud);
            Controls.Add(labelTituloPosicion);
            Controls.Add(labelFL);
            Controls.Add(labelIdentificador);
            ForeColor = Color.WhiteSmoke;
            Margin = new Padding(4, 5, 4, 5);
            Name = "InformacionAvion";
            Text = "Información del avión";
            Load += InformacionAvion_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelIdentificador;
        private Label labelFL;
        private System.Windows.Forms.Timer timerActualizar;
        private Label labelTituloPosicion;
        private Label labelLatitud;
        private Label labelLongitud;
        private Label labelAltitud;
        private Label labelTituloVuelo;
        private Label labelDireccion;
        private Label labelRumbo;
        private Label labelTrackNumber;
        private Label labelTituloDeteccion;
        private Label labelRadar;
        private Label labelADSB;
        private Label labelUltimoTiempo;
        private Label labelUltimoTiempoRadar;
        private Label labelUltimoTiempoADSB;
    }
}