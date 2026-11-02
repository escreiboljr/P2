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
            textBoxContador = new TextBox();
            labelContador = new Label();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.Guía_visual_de_controles_del_mapa_Asterix1;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(981, 763);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBoxContador
            // 
            textBoxContador.Anchor = AnchorStyles.Bottom;
            textBoxContador.BackColor = Color.FromArgb(18, 28, 45);
            textBoxContador.BorderStyle = BorderStyle.None;
            textBoxContador.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxContador.ForeColor = Color.White;
            textBoxContador.Location = new Point(403, 778);
            textBoxContador.Name = "textBoxContador";
            textBoxContador.Size = new Size(40, 28);
            textBoxContador.TabIndex = 6;
            textBoxContador.Text = "1";
            textBoxContador.TextAlign = HorizontalAlignment.Center;
            // 
            // labelContador
            // 
            labelContador.Anchor = AnchorStyles.Bottom;
            labelContador.AutoSize = true;
            labelContador.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelContador.ForeColor = Color.White;
            labelContador.Location = new Point(433, 780);
            labelContador.Name = "labelContador";
            labelContador.Size = new Size(44, 30);
            labelContador.TabIndex = 7;
            labelContador.Text = "/  9";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.botonAntes;
            pictureBox3.Location = new Point(239, 766);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(136, 63);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            pictureBox3.Click += buttonAtras_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.botonSiguiente;
            pictureBox2.Location = new Point(500, 766);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(136, 63);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            pictureBox2.Click += buttonAvanzar_Click;
            // 
            // MenuTutorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(981, 834);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox3);
            Controls.Add(labelContador);
            Controls.Add(textBoxContador);
            Controls.Add(pictureBox1);
            Name = "MenuTutorial";
            Text = "MenuTutorial";
            Load += MenuTutorial_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox textBoxContador;
        private Label labelContador;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
    }
}