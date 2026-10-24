namespace GUI_ASTERIX
{
    partial class MenuInicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuInicio));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBoxFondo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFondo).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.botonMenuSimulacion;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources.botonMenuSimulacion;
            pictureBox1.Location = new Point(234, 132);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(277, 97);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.None;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resources.botonTourPrograma;
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.Image = Properties.Resources.botonTourPrograma;
            pictureBox2.Location = new Point(234, 235);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(277, 97);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBoxFondo
            // 
            pictureBoxFondo.BackColor = Color.Transparent;
            pictureBoxFondo.BackgroundImage = Properties.Resources.botonMenuSimulacion;
            pictureBoxFondo.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxFondo.Dock = DockStyle.Fill;
            pictureBoxFondo.Image = Properties.Resources.asterix_radar_fondo;
            pictureBoxFondo.Location = new Point(0, 0);
            pictureBoxFondo.Name = "pictureBoxFondo";
            pictureBoxFondo.Size = new Size(796, 447);
            pictureBoxFondo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxFondo.TabIndex = 2;
            pictureBoxFondo.TabStop = false;
            // 
            // MenuInicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 447);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBoxFondo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MenuInicio";
            Text = "Inicio";
            Load += MenuInicio_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFondo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBoxFondo;
    }
}