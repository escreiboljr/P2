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
            buttonAplciarCambios = new Button();
            textBoxAltitudMax = new TextBox();
            textBoxAltitudMin = new TextBox();
            labelTituloAltitud = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonBlancoPuro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonGround).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonTransFijo).BeginInit();
            SuspendLayout();
            // 
            // labelTituloCat
            // 
            labelTituloCat.AutoSize = true;
            labelTituloCat.Location = new Point(29, 63);
            labelTituloCat.Name = "labelTituloCat";
            labelTituloCat.Size = new Size(105, 15);
            labelTituloCat.TabIndex = 0;
            labelTituloCat.Text = "Categoria ASTERIX";
            // 
            // labelTitBlancoPuro
            // 
            labelTitBlancoPuro.AutoSize = true;
            labelTitBlancoPuro.Location = new Point(29, 108);
            labelTitBlancoPuro.Name = "labelTitBlancoPuro";
            labelTitBlancoPuro.Size = new Size(71, 15);
            labelTitBlancoPuro.TabIndex = 1;
            labelTitBlancoPuro.Text = "Blanco puro";
            // 
            // labelTitTransFijo
            // 
            labelTitTransFijo.AutoSize = true;
            labelTitTransFijo.Location = new Point(29, 160);
            labelTitTransFijo.Name = "labelTitTransFijo";
            labelTitTransFijo.Size = new Size(93, 15);
            labelTitTransFijo.TabIndex = 2;
            labelTitTransFijo.Text = "Transponder fijo";
            // 
            // labelTitTrayAeronav
            // 
            labelTitTrayAeronav.AutoSize = true;
            labelTitTrayAeronav.Location = new Point(29, 209);
            labelTitTrayAeronav.Name = "labelTitTrayAeronav";
            labelTitTrayAeronav.Size = new Size(116, 15);
            labelTitTrayAeronav.TabIndex = 3;
            labelTitTrayAeronav.Text = "Trayectoria aeronave";
            // 
            // labelTitGround
            // 
            labelTitGround.AutoSize = true;
            labelTitGround.Location = new Point(29, 291);
            labelTitGround.Name = "labelTitGround";
            labelTitGround.Size = new Size(47, 15);
            labelTitGround.TabIndex = 4;
            labelTitGround.Text = "Ground";
            // 
            // checkBoxCat48
            // 
            checkBoxCat48.AutoSize = true;
            checkBoxCat48.Checked = true;
            checkBoxCat48.CheckState = CheckState.Checked;
            checkBoxCat48.Location = new Point(179, 61);
            checkBoxCat48.Name = "checkBoxCat48";
            checkBoxCat48.Size = new Size(60, 19);
            checkBoxCat48.TabIndex = 5;
            checkBoxCat48.Text = "CAT48";
            checkBoxCat48.UseVisualStyleBackColor = true;
            // 
            // checkBoxCat21
            // 
            checkBoxCat21.AutoSize = true;
            checkBoxCat21.Checked = true;
            checkBoxCat21.CheckState = CheckState.Checked;
            checkBoxCat21.Location = new Point(286, 61);
            checkBoxCat21.Name = "checkBoxCat21";
            checkBoxCat21.Size = new Size(60, 19);
            checkBoxCat21.TabIndex = 6;
            checkBoxCat21.Text = "CAT21";
            checkBoxCat21.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(164, 200);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            textBox1.Text = "0";
            // 
            // textBoxTrayectoriaMax
            // 
            textBoxTrayectoriaMax.Location = new Point(312, 200);
            textBoxTrayectoriaMax.Name = "textBoxTrayectoriaMax";
            textBoxTrayectoriaMax.Size = new Size(100, 23);
            textBoxTrayectoriaMax.TabIndex = 8;
            textBoxTrayectoriaMax.Text = "360";
            // 
            // pictureBoxSlideButtonBlancoPuro
            // 
            pictureBoxSlideButtonBlancoPuro.Image = Properties.Resources.sliderApagado;
            pictureBoxSlideButtonBlancoPuro.Location = new Point(166, 97);
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
            pictureBoxSlideButtonGround.Location = new Point(166, 275);
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
            pictureBoxSlideButtonTransFijo.Location = new Point(166, 143);
            pictureBoxSlideButtonTransFijo.Name = "pictureBoxSlideButtonTransFijo";
            pictureBoxSlideButtonTransFijo.Size = new Size(81, 40);
            pictureBoxSlideButtonTransFijo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSlideButtonTransFijo.TabIndex = 13;
            pictureBoxSlideButtonTransFijo.TabStop = false;
            pictureBoxSlideButtonTransFijo.Click += pictureBoxSlideButtonTransFijo_Click;
            // 
            // buttonAplciarCambios
            // 
            buttonAplciarCambios.Location = new Point(73, 352);
            buttonAplciarCambios.Name = "buttonAplciarCambios";
            buttonAplciarCambios.Size = new Size(121, 23);
            buttonAplciarCambios.TabIndex = 14;
            buttonAplciarCambios.Text = "Aplicar filtros";
            buttonAplciarCambios.UseVisualStyleBackColor = true;
            buttonAplciarCambios.Click += buttonAplciarCambios_Click;
            // 
            // textBoxAltitudMax
            // 
            textBoxAltitudMax.Location = new Point(312, 241);
            textBoxAltitudMax.Name = "textBoxAltitudMax";
            textBoxAltitudMax.Size = new Size(100, 23);
            textBoxAltitudMax.TabIndex = 17;
            textBoxAltitudMax.Text = "360";
            // 
            // textBoxAltitudMin
            // 
            textBoxAltitudMin.Location = new Point(164, 241);
            textBoxAltitudMin.Name = "textBoxAltitudMin";
            textBoxAltitudMin.Size = new Size(100, 23);
            textBoxAltitudMin.TabIndex = 16;
            textBoxAltitudMin.Text = "0";
            // 
            // labelTituloAltitud
            // 
            labelTituloAltitud.AutoSize = true;
            labelTituloAltitud.Location = new Point(29, 250);
            labelTituloAltitud.Name = "labelTituloAltitud";
            labelTituloAltitud.Size = new Size(43, 15);
            labelTituloAltitud.TabIndex = 15;
            labelTituloAltitud.Text = "Altitud";
            // 
            // FormFiltro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 450);
            Controls.Add(textBoxAltitudMax);
            Controls.Add(textBoxAltitudMin);
            Controls.Add(labelTituloAltitud);
            Controls.Add(buttonAplciarCambios);
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
            Name = "FormFiltro";
            Text = "FormFiltro";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonBlancoPuro).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonGround).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSlideButtonTransFijo).EndInit();
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
        private Button buttonAplciarCambios;
        private TextBox textBoxAltitudMax;
        private TextBox textBoxAltitudMin;
        private Label labelTituloAltitud;
    }
}