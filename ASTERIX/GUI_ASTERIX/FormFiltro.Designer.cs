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
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // labelTituloCat
            // 
            labelTituloCat.AutoSize = true;
            labelTituloCat.Location = new Point(31, 167);
            labelTituloCat.Name = "labelTituloCat";
            labelTituloCat.Size = new Size(105, 15);
            labelTituloCat.TabIndex = 0;
            labelTituloCat.Text = "Categoria ASTERIX";
            // 
            // labelTitBlancoPuro
            // 
            labelTitBlancoPuro.AutoSize = true;
            labelTitBlancoPuro.Location = new Point(31, 212);
            labelTitBlancoPuro.Name = "labelTitBlancoPuro";
            labelTitBlancoPuro.Size = new Size(71, 15);
            labelTitBlancoPuro.TabIndex = 1;
            labelTitBlancoPuro.Text = "Blanco puro";
            // 
            // labelTitTransFijo
            // 
            labelTitTransFijo.AutoSize = true;
            labelTitTransFijo.Location = new Point(31, 264);
            labelTitTransFijo.Name = "labelTitTransFijo";
            labelTitTransFijo.Size = new Size(93, 15);
            labelTitTransFijo.TabIndex = 2;
            labelTitTransFijo.Text = "Transponder fijo";
            // 
            // labelTitTrayAeronav
            // 
            labelTitTrayAeronav.AutoSize = true;
            labelTitTrayAeronav.Location = new Point(31, 313);
            labelTitTrayAeronav.Name = "labelTitTrayAeronav";
            labelTitTrayAeronav.Size = new Size(116, 15);
            labelTitTrayAeronav.TabIndex = 3;
            labelTitTrayAeronav.Text = "Trayectoria aeronave";
            // 
            // labelTitGround
            // 
            labelTitGround.AutoSize = true;
            labelTitGround.Location = new Point(31, 360);
            labelTitGround.Name = "labelTitGround";
            labelTitGround.Size = new Size(47, 15);
            labelTitGround.TabIndex = 4;
            labelTitGround.Text = "Ground";
            // 
            // checkBoxCat48
            // 
            checkBoxCat48.AutoSize = true;
            checkBoxCat48.Location = new Point(231, 166);
            checkBoxCat48.Name = "checkBoxCat48";
            checkBoxCat48.Size = new Size(60, 19);
            checkBoxCat48.TabIndex = 5;
            checkBoxCat48.Text = "CAT48";
            checkBoxCat48.UseVisualStyleBackColor = true;
            // 
            // checkBoxCat21
            // 
            checkBoxCat21.AutoSize = true;
            checkBoxCat21.Location = new Point(338, 166);
            checkBoxCat21.Name = "checkBoxCat21";
            checkBoxCat21.Size = new Size(60, 19);
            checkBoxCat21.TabIndex = 6;
            checkBoxCat21.Text = "CAT21";
            checkBoxCat21.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(199, 309);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(348, 309);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 8;
            // 
            // FormFiltro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox2);
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
        private TextBox textBox2;
    }
}