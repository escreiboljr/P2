namespace GUI_ASTERIX
{
    partial class MenuTutorial
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
            pictureBox1 = new PictureBox();
            buttonAtras = new Button();
            buttonAvanzar = new Button();
            textBoxContador = new TextBox();
            labelContador = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.MenuSimulacion;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1404, 763);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // buttonAtras
            // 
            buttonAtras.Anchor = AnchorStyles.Bottom;
            buttonAtras.Location = new Point(439, 787);
            buttonAtras.Name = "buttonAtras";
            buttonAtras.Size = new Size(75, 23);
            buttonAtras.TabIndex = 4;
            buttonAtras.Text = "Retroceder";
            buttonAtras.UseVisualStyleBackColor = true;
            buttonAtras.Click += buttonAtras_Click;
            // 
            // buttonAvanzar
            // 
            buttonAvanzar.Anchor = AnchorStyles.Bottom;
            buttonAvanzar.Location = new Point(782, 787);
            buttonAvanzar.Name = "buttonAvanzar";
            buttonAvanzar.Size = new Size(75, 23);
            buttonAvanzar.TabIndex = 5;
            buttonAvanzar.Text = "Avanzar";
            buttonAvanzar.UseVisualStyleBackColor = true;
            buttonAvanzar.Click += buttonAvanzar_Click;
            // 
            // textBoxContador
            // 
            textBoxContador.Anchor = AnchorStyles.Bottom;
            textBoxContador.Location = new Point(595, 788);
            textBoxContador.Name = "textBoxContador";
            textBoxContador.Size = new Size(40, 23);
            textBoxContador.TabIndex = 6;
            textBoxContador.Text = "1";
            textBoxContador.TextAlign = HorizontalAlignment.Center;
            // 
            // labelContador
            // 
            labelContador.Anchor = AnchorStyles.Bottom;
            labelContador.AutoSize = true;
            labelContador.Location = new Point(664, 791);
            labelContador.Name = "labelContador";
            labelContador.Size = new Size(26, 15);
            labelContador.TabIndex = 7;
            labelContador.Text = "/XX";
            // 
            // MenuTutorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1404, 834);
            Controls.Add(labelContador);
            Controls.Add(textBoxContador);
            Controls.Add(buttonAvanzar);
            Controls.Add(buttonAtras);
            Controls.Add(pictureBox1);
            Name = "MenuTutorial";
            Text = "MenuTutorial";
            Load += MenuTutorial_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button buttonAtras;
        private Button buttonAvanzar;
        private TextBox textBoxContador;
        private Label labelContador;
    }
}