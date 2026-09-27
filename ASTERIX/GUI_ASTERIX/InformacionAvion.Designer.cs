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
            labelIdentificador.Location = new Point(167, 50);
            labelIdentificador.Name = "labelIdentificador";
            labelIdentificador.Size = new Size(38, 15);
            labelIdentificador.TabIndex = 0;
            labelIdentificador.Text = "label1";
            // 
            // labelFL
            // 
            labelFL.AutoSize = true;
            labelFL.Location = new Point(206, 199);
            labelFL.Name = "labelFL";
            labelFL.Size = new Size(38, 15);
            labelFL.TabIndex = 1;
            labelFL.Text = "label2";
            // 
            // timerActualizar
            // 
            timerActualizar.Tick += timerActualizar_Tick;
            // 
            // InformacionAvion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelFL);
            Controls.Add(labelIdentificador);
            Name = "InformacionAvion";
            Text = "InformacionAvion";
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