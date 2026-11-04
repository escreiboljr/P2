namespace GUI_ASTERIX
{
    partial class FormFiltro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFiltro));
            labelTituloCat = new Label();
            labelTitBlancoPuro = new Label();
            labelTitTransFijo = new Label();
            labelTitTrayAeronav = new Label();
            labelTitGround = new Label();
            checkBoxCat48 = new CheckBox();
            checkBoxCat21 = new CheckBox();
            textBox1 = new TextBox();
            textBoxTrayectoriaMax = new TextBox();
            pictureBoxSlideButtonBlancoPuro = new PictureBox();
            pictureBoxSlideButtonGround = new PictureBox();
            pictureBoxSlideButtonTransFijo = new PictureBox();
            textBoxAltitudMax = new TextBox();
            textBoxAltitudMin = new TextBox();
            labelTituloAltitud = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonBlancoPuro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonGround).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonTransFijo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // labelTituloCat
            // 
            labelTituloCat.AutoSize = true;
            labelTituloCat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloCat.ForeColor = SystemColors.ControlLightLight;
            labelTituloCat.Location = new Point(29, 54);
            labelTituloCat.Name = "labelTituloCat";
            labelTituloCat.Size = new Size(151, 21);
            labelTituloCat.TabIndex = 0;
            labelTituloCat.Text = "Categoria ASTERIX";
            // 
            // labelTitBlancoPuro
            // 
            labelTitBlancoPuro.AutoSize = true;
            labelTitBlancoPuro.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitBlancoPuro.ForeColor = SystemColors.ControlLightLight;
            labelTitBlancoPuro.Location = new Point(29, 102);
            labelTitBlancoPuro.Name = "labelTitBlancoPuro";
            labelTitBlancoPuro.Size = new Size(102, 21);
            labelTitBlancoPuro.TabIndex = 1;
            labelTitBlancoPuro.Text = "Blanco puro";
            // 
            // labelTitTransFijo
            // 
            labelTitTransFijo.AutoSize = true;
            labelTitTransFijo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitTransFijo.ForeColor = SystemColors.ControlLightLight;
            labelTitTransFijo.Location = new Point(29, 155);
            labelTitTransFijo.Name = "labelTitTransFijo";
            labelTitTransFijo.Size = new Size(135, 21);
            labelTitTransFijo.TabIndex = 2;
            labelTitTransFijo.Text = "Transponder fijo";
            // 
            // labelTitTrayAeronav
            // 
            labelTitTrayAeronav.AutoSize = true;
            labelTitTrayAeronav.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitTrayAeronav.ForeColor = SystemColors.ControlLightLight;
            labelTitTrayAeronav.Location = new Point(29, 211);
            labelTitTrayAeronav.Name = "labelTitTrayAeronav";
            labelTitTrayAeronav.Size = new Size(170, 21);
            labelTitTrayAeronav.TabIndex = 3;
            labelTitTrayAeronav.Text = "Trayectoria aeronave";
            // 
            // labelTitGround
            // 
            labelTitGround.AutoSize = true;
            labelTitGround.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitGround.ForeColor = SystemColors.ControlLightLight;
            labelTitGround.Location = new Point(29, 320);
            labelTitGround.Name = "labelTitGround";
            labelTitGround.Size = new Size(67, 21);
            labelTitGround.TabIndex = 4;
            labelTitGround.Text = "Ground";
            // 
            // checkBoxCat48
            // 
            checkBoxCat48.AutoSize = true;
            checkBoxCat48.Checked = true;
            checkBoxCat48.CheckState = CheckState.Checked;
            checkBoxCat48.Location = new Point(209, 54);
            checkBoxCat48.Name = "checkBoxCat48";
            checkBoxCat48.Size = new Size(60, 19);
            checkBoxCat48.TabIndex = 5;
            checkBoxCat48.Text = "CAT48";
            checkBoxCat48.UseVisualStyleBackColor = true;
            checkBoxCat48.CheckedChanged += this.checkBoxCat48_CheckedChanged;
            // 
            // checkBoxCat21
            // 
            checkBoxCat21.AutoSize = true;
            checkBoxCat21.Checked = true;
            checkBoxCat21.CheckState = CheckState.Checked;
            checkBoxCat21.Location = new Point(316, 54);
            checkBoxCat21.Name = "checkBoxCat21";
            checkBoxCat21.Size = new Size(60, 19);
            checkBoxCat21.TabIndex = 6;
            checkBoxCat21.Text = "CAT21";
            checkBoxCat21.UseVisualStyleBackColor = true;
            checkBoxCat21.CheckedChanged += checkBoxCat21_CheckedChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(208, 209);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            textBox1.Text = "0";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxTrayectoriaMax
            // 
            textBoxTrayectoriaMax.Location = new Point(356, 209);
            textBoxTrayectoriaMax.Name = "textBoxTrayectoriaMax";
            textBoxTrayectoriaMax.Size = new Size(100, 23);
            textBoxTrayectoriaMax.TabIndex = 8;
            textBoxTrayectoriaMax.Text = "360";
            textBoxTrayectoriaMax.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBoxSlideButtonBlancoPuro
            // 
            pictureBoxSlideButtonBlancoPuro.Image = Properties.Resources.sliderApagado;
            pictureBoxSlideButtonBlancoPuro.Location = new Point(208, 97);
            pictureBoxSlideButtonBlancoPuro.Name = "pictureBoxSlideButtonBlancoPuro";
            pictureBoxSlideButtonBlancoPuro.Size = new Size(81, 40);
            pictureBoxSlideButtonBlancoPuro.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSlideButtonBlancoPuro.TabIndex = 9;
            pictureBoxSlideButtonBlancoPuro.TabStop = false;
            pictureBoxSlideButtonBlancoPuro.Click += pictureBoxSlideButtonBlancoPuro_Click;
            // 
            // pictureBoxSlideButtonGround
            // 
            pictureBoxSlideButtonGround.Image = Properties.Resources.sliderApagado;
            pictureBoxSlideButtonGround.Location = new Point(208, 306);
            pictureBoxSlideButtonGround.Name = "pictureBoxSlideButtonGround";
            pictureBoxSlideButtonGround.Size = new Size(81, 40);
            pictureBoxSlideButtonGround.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSlideButtonGround.TabIndex = 12;
            pictureBoxSlideButtonGround.TabStop = false;
            pictureBoxSlideButtonGround.Click += pictureBoxSlideButtonGround_Click;
            // 
            // pictureBoxSlideButtonTransFijo
            // 
            pictureBoxSlideButtonTransFijo.Image = Properties.Resources.sliderApagado;
            pictureBoxSlideButtonTransFijo.Location = new Point(208, 143);
            pictureBoxSlideButtonTransFijo.Name = "pictureBoxSlideButtonTransFijo";
            pictureBoxSlideButtonTransFijo.Size = new Size(81, 40);
            pictureBoxSlideButtonTransFijo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSlideButtonTransFijo.TabIndex = 13;
            pictureBoxSlideButtonTransFijo.TabStop = false;
            pictureBoxSlideButtonTransFijo.Click += pictureBoxSlideButtonTransFijo_Click;
            // 
            // textBoxAltitudMax
            // 
            textBoxAltitudMax.Location = new Point(356, 264);
            textBoxAltitudMax.Name = "textBoxAltitudMax";
            textBoxAltitudMax.Size = new Size(100, 23);
            textBoxAltitudMax.TabIndex = 17;
            textBoxAltitudMax.Text = "360";
            textBoxAltitudMax.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxAltitudMin
            // 
            textBoxAltitudMin.Location = new Point(208, 264);
            textBoxAltitudMin.Name = "textBoxAltitudMin";
            textBoxAltitudMin.Size = new Size(100, 23);
            textBoxAltitudMin.TabIndex = 16;
            textBoxAltitudMin.Text = "0";
            textBoxAltitudMin.TextAlign = HorizontalAlignment.Center;
            // 
            // labelTituloAltitud
            // 
            labelTituloAltitud.AutoSize = true;
            labelTituloAltitud.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTituloAltitud.ForeColor = SystemColors.ControlLightLight;
            labelTituloAltitud.Location = new Point(29, 266);
            labelTituloAltitud.Name = "labelTituloAltitud";
            labelTituloAltitud.Size = new Size(63, 21);
            labelTituloAltitud.TabIndex = 15;
            labelTituloAltitud.Text = "Altitud";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Botón_neón_de_aplicar_filtros;
            pictureBox1.Location = new Point(134, 369);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(201, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 18;
            pictureBox1.TabStop = false;
            pictureBox1.Click += buttonAplciarCambios_Click;
            // 
            // FormFiltro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(499, 474);
            Controls.Add(pictureBox1);
            Controls.Add(textBoxAltitudMax);
            Controls.Add(textBoxAltitudMin);
            Controls.Add(labelTituloAltitud);
            Controls.Add(pictureBoxSlideButtonTransFijo);
            Controls.Add(pictureBoxSlideButtonGround);
            Controls.Add(pictureBoxSlideButtonBlancoPuro);
            Controls.Add(textBoxTrayectoriaMax);
            Controls.Add(textBox1);
            Controls.Add(checkBoxCat21);
            Controls.Add(checkBoxCat48);
            Controls.Add(labelTitGround);
            Controls.Add(labelTitTrayAeronav);
            Controls.Add(labelTitTransFijo);
            Controls.Add(labelTitBlancoPuro);
            Controls.Add(labelTituloCat);
            ForeColor = SystemColors.ControlLightLight;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormFiltro";
            Text = "Filtros";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonBlancoPuro).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonGround).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonTransFijo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTituloCat;
        private Label labelTitBlancoPuro;
        private Label labelTitTransFijo;
        private Label labelTitTrayAeronav;
        private Label labelTitGround;
        private CheckBox checkBoxCat48;
        private CheckBox checkBoxCat21;
        private TextBox textBox1;
        private TextBox textBoxTrayectoriaMax;
        private PictureBox pictureBoxSlideButtonBlancoPuro;
        private PictureBox pictureBoxSlideButtonGround;
        private PictureBox pictureBoxSlideButtonTransFijo;
        private TextBox textBoxAltitudMax;
        private TextBox textBoxAltitudMin;
        private Label labelTituloAltitud;
        private PictureBox pictureBox1;
    }
}