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
            labelTitulo2 = new Label();
            labelTitulo1 = new Label();
            labelTitulo3 = new Label();
            buttonAtras = new Button();
            buttonAvanzar = new Button();
            textBoxContador = new TextBox();
            labelContador = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.MenuSimulacion;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1404, 763);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // labelTitulo2
            // 
            labelTitulo2.AutoSize = true;
            labelTitulo2.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelTitulo2.Location = new Point(495, 377);
            labelTitulo2.Name = "labelTitulo2";
            labelTitulo2.Size = new Size(542, 30);
            labelTitulo2.TabIndex = 1;
            labelTitulo2.Text = "se puede cargar mensajes, ver datos de la simulacion y ";
            // 
            // labelTitulo1
            // 
            labelTitulo1.AutoSize = true;
            labelTitulo1.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelTitulo1.Location = new Point(495, 347);
            labelTitulo1.Name = "labelTitulo1";
            labelTitulo1.Size = new Size(545, 30);
            labelTitulo1.TabIndex = 2;
            labelTitulo1.Text = "Este es el menu principal de simulacion ASTERIX, donde";
            // 
            // labelTitulo3
            // 
            labelTitulo3.AutoSize = true;
            labelTitulo3.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelTitulo3.Location = new Point(495, 407);
            labelTitulo3.Name = "labelTitulo3";
            labelTitulo3.Size = new Size(362, 30);
            labelTitulo3.TabIndex = 3;
            labelTitulo3.Text = "la posicion de los diferentes aviones.";
            // 
            // buttonAtras
            // 
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
            textBoxContador.Location = new Point(595, 788);
            textBoxContador.Name = "textBoxContador";
            textBoxContador.Size = new Size(40, 23);
            textBoxContador.TabIndex = 6;
            textBoxContador.Text = "1";
            textBoxContador.TextAlign = HorizontalAlignment.Center;
            // 
            // labelContador
            // 
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
            Controls.Add(labelTitulo3);
            Controls.Add(labelTitulo1);
            Controls.Add(labelTitulo2);
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
        private Label labelTitulo2;
        private Label labelTitulo1;
        private Label labelTitulo3;
        private Button buttonAtras;
        private Button buttonAvanzar;
        private TextBox textBoxContador;
        private Label labelContador;
    }
}