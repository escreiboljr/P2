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
            SuspendLayout();
            // 
            // labelIdentificador
            // 
            labelIdentificador.AutoSize = true;
            labelIdentificador.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelIdentificador.ForeColor = Color.Turquoise;
            labelIdentificador.Location = new Point(55, 46);
            labelIdentificador.Margin = new Padding(4, 0, 4, 0);
            labelIdentificador.Name = "labelIdentificador";
            labelIdentificador.Size = new Size(224, 32);
            labelIdentificador.TabIndex = 0;
            labelIdentificador.Text = "Identificador: N/A";
            // 
            // labelFL
            // 
            labelFL.AutoSize = true;
            labelFL.BackColor = Color.Transparent;
            labelFL.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelFL.ForeColor = Color.WhiteSmoke;
            labelFL.Location = new Point(55, 78);
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
            // InformacionAvion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(468, 214);
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
    }
}