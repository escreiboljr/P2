namespace GUI_ASTERIX
{
    partial class FormCargando
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
            progressBarCarga = new ProgressBar();
            labelTitulo = new Label();
            labelPorcentaje = new Label();
            SuspendLayout();
            // 
            // progressBarCarga
            // 
            progressBarCarga.Location = new Point(57, 112);
            progressBarCarga.Name = "progressBarCarga";
            progressBarCarga.Size = new Size(165, 23);
            progressBarCarga.Style = ProgressBarStyle.Continuous;
            progressBarCarga.TabIndex = 0;
            // 
            // labelTitulo
            // 
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelTitulo.ForeColor = SystemColors.Control;
            labelTitulo.Location = new Point(65, 20);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(159, 30);
            labelTitulo.TabIndex = 1;
            labelTitulo.Text = "Leyendo datos:";
            // 
            // labelPorcentaje
            // 
            labelPorcentaje.AutoSize = true;
            labelPorcentaje.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelPorcentaje.ForeColor = SystemColors.Control;
            labelPorcentaje.Location = new Point(106, 66);
            labelPorcentaje.Name = "labelPorcentaje";
            labelPorcentaje.Size = new Size(57, 30);
            labelPorcentaje.TabIndex = 2;
            labelPorcentaje.Text = "XX%";
            // 
            // FormCargando
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(277, 169);
            ControlBox = false;
            Controls.Add(labelPorcentaje);
            Controls.Add(labelTitulo);
            Controls.Add(progressBarCarga);
            Name = "FormCargando";
            TopMost = true;
            Load += FormCargando_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBarCarga;
        private Label labelTitulo;
        private Label labelPorcentaje;
    }
}