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
            labelEstadoVuelo = new Label();
            labelMode3A = new Label();
            labelVelocidad = new Label();
            pictureBoxMasInformacion = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMasInformacion).BeginInit();
            SuspendLayout();
            // 
            // labelIdentificador
            // 
            labelIdentificador.AutoSize = true;
            labelIdentificador.BackColor = Color.Transparent;
            labelIdentificador.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelIdentificador.ForeColor = Color.Turquoise;
            labelIdentificador.Location = new Point(38, 23);
            labelIdentificador.Name = "labelIdentificador";
            labelIdentificador.Size = new Size(202, 30);
            labelIdentificador.TabIndex = 0;
            labelIdentificador.Text = "Identificador: N/A";
            // 
            // labelFL
            // 
            labelFL.AutoSize = true;
            labelFL.BackColor = Color.Transparent;
            labelFL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFL.ForeColor = Color.LightGoldenrodYellow;
            labelFL.Location = new Point(38, 176);
            labelFL.Name = "labelFL";
            labelFL.Size = new Size(120, 19);
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
            labelTituloPosicion.Location = new Point(38, 61);
            labelTituloPosicion.Margin = new Padding(2, 0, 2, 0);
            labelTituloPosicion.Name = "labelTituloPosicion";
            labelTituloPosicion.Size = new Size(86, 21);
            labelTituloPosicion.TabIndex = 2;
            labelTituloPosicion.Text = "POSICIÓN";
            // 
            // labelLatitud
            // 
            labelLatitud.AutoSize = true;
            labelLatitud.BackColor = Color.Transparent;
            labelLatitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLatitud.ForeColor = Color.Lavender;
            labelLatitud.Location = new Point(38, 80);
            labelLatitud.Margin = new Padding(2, 0, 2, 0);
            labelLatitud.Name = "labelLatitud";
            labelLatitud.Size = new Size(90, 19);
            labelLatitud.TabIndex = 3;
            labelLatitud.Text = "Latitud: N/A";
            // 
            // labelLongitud
            // 
            labelLongitud.AutoSize = true;
            labelLongitud.BackColor = Color.Transparent;
            labelLongitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelLongitud.ForeColor = Color.Lavender;
            labelLongitud.Location = new Point(38, 97);
            labelLongitud.Margin = new Padding(2, 0, 2, 0);
            labelLongitud.Name = "labelLongitud";
            labelLongitud.Size = new Size(103, 19);
            labelLongitud.TabIndex = 4;
            labelLongitud.Text = "Longitud: N/A";
            // 
            // labelAltitud
            // 
            labelAltitud.AutoSize = true;
            labelAltitud.BackColor = Color.Transparent;
            labelAltitud.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelAltitud.ForeColor = Color.Lavender;
            labelAltitud.Location = new Point(38, 113);
            labelAltitud.Margin = new Padding(2, 0, 2, 0);
            labelAltitud.Name = "labelAltitud";
            labelAltitud.Size = new Size(89, 19);
            labelAltitud.TabIndex = 5;
            labelAltitud.Text = "Altitud: N/A";
            // 
            // labelTituloVuelo
            // 
            labelTituloVuelo.AutoSize = true;
            labelTituloVuelo.BackColor = Color.Transparent;
            labelTituloVuelo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloVuelo.ForeColor = Color.LightGoldenrodYellow;
            labelTituloVuelo.Location = new Point(38, 157);
            labelTituloVuelo.Margin = new Padding(2, 0, 2, 0);
            labelTituloVuelo.Name = "labelTituloVuelo";
            labelTituloVuelo.Size = new Size(62, 21);
            labelTituloVuelo.TabIndex = 6;
            labelTituloVuelo.Text = "VUELO";
            // 
            // labelDireccion
            // 
            labelDireccion.AutoSize = true;
            labelDireccion.BackColor = Color.Transparent;
            labelDireccion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelDireccion.ForeColor = Color.LightGoldenrodYellow;
            labelDireccion.Location = new Point(38, 193);
            labelDireccion.Margin = new Padding(2, 0, 2, 0);
            labelDireccion.Name = "labelDireccion";
            labelDireccion.Size = new Size(107, 19);
            labelDireccion.TabIndex = 7;
            labelDireccion.Text = "Dirección: N/A";
            // 
            // labelRumbo
            // 
            labelRumbo.AutoSize = true;
            labelRumbo.BackColor = Color.Transparent;
            labelRumbo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRumbo.ForeColor = Color.LightGoldenrodYellow;
            labelRumbo.Location = new Point(38, 208);
            labelRumbo.Margin = new Padding(2, 0, 2, 0);
            labelRumbo.Name = "labelRumbo";
            labelRumbo.Size = new Size(92, 19);
            labelRumbo.TabIndex = 8;
            labelRumbo.Text = "Rumbo: N/A";
            // 
            // labelTrackNumber
            // 
            labelTrackNumber.AutoSize = true;
            labelTrackNumber.BackColor = Color.Transparent;
            labelTrackNumber.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTrackNumber.ForeColor = Color.LightGoldenrodYellow;
            labelTrackNumber.Location = new Point(38, 223);
            labelTrackNumber.Margin = new Padding(2, 0, 2, 0);
            labelTrackNumber.Name = "labelTrackNumber";
            labelTrackNumber.Size = new Size(140, 19);
            labelTrackNumber.TabIndex = 9;
            labelTrackNumber.Text = "Track Number: N/A";
            // 
            // labelTituloDeteccion
            // 
            labelTituloDeteccion.AutoSize = true;
            labelTituloDeteccion.BackColor = Color.Transparent;
            labelTituloDeteccion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloDeteccion.ForeColor = Color.PeachPuff;
            labelTituloDeteccion.Location = new Point(38, 268);
            labelTituloDeteccion.Margin = new Padding(2, 0, 2, 0);
            labelTituloDeteccion.Name = "labelTituloDeteccion";
            labelTituloDeteccion.Size = new Size(99, 21);
            labelTituloDeteccion.TabIndex = 10;
            labelTituloDeteccion.Text = "DETECCIÓN";
            // 
            // labelRadar
            // 
            labelRadar.AutoSize = true;
            labelRadar.BackColor = Color.Transparent;
            labelRadar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelRadar.ForeColor = Color.PeachPuff;
            labelRadar.Location = new Point(38, 287);
            labelRadar.Margin = new Padding(2, 0, 2, 0);
            labelRadar.Name = "labelRadar";
            labelRadar.Size = new Size(85, 19);
            labelRadar.TabIndex = 11;
            labelRadar.Text = "Radar: N/A";
            // 
            // labelADSB
            // 
            labelADSB.AutoSize = true;
            labelADSB.BackColor = Color.Transparent;
            labelADSB.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelADSB.ForeColor = Color.PeachPuff;
            labelADSB.Location = new Point(38, 304);
            labelADSB.Margin = new Padding(2, 0, 2, 0);
            labelADSB.Name = "labelADSB";
            labelADSB.Size = new Size(87, 19);
            labelADSB.TabIndex = 12;
            labelADSB.Text = "ADS-B: N/A";
            // 
            // labelUltimoTiempo
            // 
            labelUltimoTiempo.AutoSize = true;
            labelUltimoTiempo.BackColor = Color.Transparent;
            labelUltimoTiempo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempo.ForeColor = Color.PeachPuff;
            labelUltimoTiempo.Location = new Point(237, 283);
            labelUltimoTiempo.Margin = new Padding(2, 0, 2, 0);
            labelUltimoTiempo.Name = "labelUltimoTiempo";
            labelUltimoTiempo.Size = new Size(141, 19);
            labelUltimoTiempo.TabIndex = 13;
            labelUltimoTiempo.Text = "Último tiempo: N/A";
            // 
            // labelUltimoTiempoRadar
            // 
            labelUltimoTiempoRadar.AutoSize = true;
            labelUltimoTiempoRadar.BackColor = Color.Transparent;
            labelUltimoTiempoRadar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempoRadar.ForeColor = Color.PeachPuff;
            labelUltimoTiempoRadar.Location = new Point(237, 302);
            labelUltimoTiempoRadar.Margin = new Padding(2, 0, 2, 0);
            labelUltimoTiempoRadar.Name = "labelUltimoTiempoRadar";
            labelUltimoTiempoRadar.Size = new Size(186, 19);
            labelUltimoTiempoRadar.TabIndex = 14;
            labelUltimoTiempoRadar.Text = "Último tiempo Radar: N/A";
            // 
            // labelUltimoTiempoADSB
            // 
            labelUltimoTiempoADSB.AutoSize = true;
            labelUltimoTiempoADSB.BackColor = Color.Transparent;
            labelUltimoTiempoADSB.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelUltimoTiempoADSB.ForeColor = Color.PeachPuff;
            labelUltimoTiempoADSB.Location = new Point(237, 321);
            labelUltimoTiempoADSB.Margin = new Padding(2, 0, 2, 0);
            labelUltimoTiempoADSB.Name = "labelUltimoTiempoADSB";
            labelUltimoTiempoADSB.Size = new Size(188, 19);
            labelUltimoTiempoADSB.TabIndex = 15;
            labelUltimoTiempoADSB.Text = "Último tiempo ADS-B: N/A";
            // 
            // labelEstadoVuelo
            // 
            labelEstadoVuelo.AutoSize = true;
            labelEstadoVuelo.BackColor = Color.Transparent;
            labelEstadoVuelo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelEstadoVuelo.ForeColor = Color.LightGoldenrodYellow;
            labelEstadoVuelo.Location = new Point(237, 176);
            labelEstadoVuelo.Name = "labelEstadoVuelo";
            labelEstadoVuelo.Size = new Size(129, 19);
            labelEstadoVuelo.TabIndex = 16;
            labelEstadoVuelo.Text = "Estado vuelo: N/A";
            // 
            // labelMode3A
            // 
            labelMode3A.AutoSize = true;
            labelMode3A.BackColor = Color.Transparent;
            labelMode3A.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelMode3A.ForeColor = Color.PeachPuff;
            labelMode3A.Location = new Point(38, 320);
            labelMode3A.Margin = new Padding(2, 0, 2, 0);
            labelMode3A.Name = "labelMode3A";
            labelMode3A.Size = new Size(111, 19);
            labelMode3A.TabIndex = 17;
            labelMode3A.Text = "Mode 3/A: N/A";
            // 
            // labelVelocidad
            // 
            labelVelocidad.AutoSize = true;
            labelVelocidad.BackColor = Color.Transparent;
            labelVelocidad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelVelocidad.ForeColor = Color.LightGoldenrodYellow;
            labelVelocidad.Location = new Point(237, 193);
            labelVelocidad.Margin = new Padding(2, 0, 2, 0);
            labelVelocidad.Name = "labelVelocidad";
            labelVelocidad.Size = new Size(110, 19);
            labelVelocidad.TabIndex = 18;
            labelVelocidad.Text = "Velocidad: N/A";
            // 
            // pictureBoxMasInformacion
            // 
            pictureBoxMasInformacion.Image = Properties.Resources.Botón_azul_brillante_con_icono_de_documento;
            pictureBoxMasInformacion.InitialImage = Properties.Resources.Botón_azul_brillante_con_icono_de_documento;
            pictureBoxMasInformacion.Location = new Point(102, 356);
            pictureBoxMasInformacion.Name = "pictureBoxMasInformacion";
            pictureBoxMasInformacion.Size = new Size(239, 87);
            pictureBoxMasInformacion.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMasInformacion.TabIndex = 19;
            pictureBoxMasInformacion.TabStop = false;
            pictureBoxMasInformacion.Click += pictureBoxMasInformacion_Click;
            // 
            // InformacionAvion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(470, 468);
            Controls.Add(pictureBoxMasInformacion);
            Controls.Add(labelVelocidad);
            Controls.Add(labelMode3A);
            Controls.Add(labelEstadoVuelo);
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
            Name = "InformacionAvion";
            Text = "Información del avión";
            Load += InformacionAvion_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxMasInformacion).EndInit();
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
        private Label labelEstadoVuelo;
        private Label labelMode3A;
        private Label labelVelocidad;
        private PictureBox pictureBoxMasInformacion;
    }
}