namespace GUI_ASTERIX
{
    partial class MenuSimulacion
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuSimulacion));
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            cargaDatosrToolStripMenuItem = new ToolStripMenuItem();
            guardarEnCSVToolStripMenuItem = new ToolStripMenuItem();
            filtrarToolStripMenuItem = new ToolStripMenuItem();
            aplicarToolStripMenuItem = new ToolStripMenuItem();
            limpiarToolStripMenuItem = new ToolStripMenuItem();
            timerSimulacion = new System.Windows.Forms.Timer(components);
            buttonPlay = new Button();
            buttonStop = new Button();
            label1 = new Label();
            trackBarVelSimulacion = new TrackBar();
            buttonAvanzar = new Button();
            buttonReset = new Button();
            dataGridAviones = new DataGridView();
            buttonDataGrid = new Button();
            gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            labelX1 = new Label();
            labelx2 = new Label();
            labelx8 = new Label();
            labelx4 = new Label();
            labelTituloVelocidadReproduccion = new Label();
            labelTituloHora = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            labelTituloVerde = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            labelTituloAsterix = new Label();
            timerClick = new System.Windows.Forms.Timer(components);
            pictureBoxPausa = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPausa).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, filtrarToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(9, 3, 0, 3);
            menuStrip1.Size = new Size(1924, 42);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargaDatosrToolStripMenuItem, guardarEnCSVToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(110, 36);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cargaDatosrToolStripMenuItem
            // 
            cargaDatosrToolStripMenuItem.Name = "cargaDatosrToolStripMenuItem";
            cargaDatosrToolStripMenuItem.Size = new Size(286, 40);
            cargaDatosrToolStripMenuItem.Text = "Cargar datos";
            cargaDatosrToolStripMenuItem.Click += cargaDatosrToolStripMenuItem_Click;
            // 
            // guardarEnCSVToolStripMenuItem
            // 
            guardarEnCSVToolStripMenuItem.Name = "guardarEnCSVToolStripMenuItem";
            guardarEnCSVToolStripMenuItem.Size = new Size(286, 40);
            guardarEnCSVToolStripMenuItem.Text = "Guardar en CSV";
            guardarEnCSVToolStripMenuItem.Click += guardarEnCSVToolStripMenuItem_Click;
            // 
            // filtrarToolStripMenuItem
            // 
            filtrarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aplicarToolStripMenuItem, limpiarToolStripMenuItem });
            filtrarToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            filtrarToolStripMenuItem.Name = "filtrarToolStripMenuItem";
            filtrarToolStripMenuItem.Size = new Size(90, 36);
            filtrarToolStripMenuItem.Text = "Filtrar";
            // 
            // aplicarToolStripMenuItem
            // 
            aplicarToolStripMenuItem.Name = "aplicarToolStripMenuItem";
            aplicarToolStripMenuItem.Size = new Size(257, 40);
            aplicarToolStripMenuItem.Text = "Aplicar filtros";
            aplicarToolStripMenuItem.Click += aplicarToolStripMenuItem_Click;
            // 
            // limpiarToolStripMenuItem
            // 
            limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            limpiarToolStripMenuItem.Size = new Size(257, 40);
            limpiarToolStripMenuItem.Text = "Limpiar";
            limpiarToolStripMenuItem.Click += limpiarToolStripMenuItem_Click;
            // 
            // timerSimulacion
            // 
            timerSimulacion.Tick += timerSimulacion_Tick;
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new Point(63, 662);
            buttonPlay.Margin = new Padding(4, 5, 4, 5);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(107, 38);
            buttonPlay.TabIndex = 1;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Visible = false;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(211, 662);
            buttonStop.Margin = new Padding(4, 5, 4, 5);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(107, 38);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Visible = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(216, 1013);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 25);
            label1.TabIndex = 3;
            label1.Text = "00:00:00";
            // 
            // trackBarVelSimulacion
            // 
            trackBarVelSimulacion.LargeChange = 4;
            trackBarVelSimulacion.Location = new Point(71, 830);
            trackBarVelSimulacion.Margin = new Padding(4, 5, 4, 5);
            trackBarVelSimulacion.Maximum = 4;
            trackBarVelSimulacion.Minimum = 1;
            trackBarVelSimulacion.Name = "trackBarVelSimulacion";
            trackBarVelSimulacion.Size = new Size(233, 69);
            trackBarVelSimulacion.TabIndex = 4;
            trackBarVelSimulacion.Value = 1;
            trackBarVelSimulacion.Scroll += trackBarVelSimulacion_Scroll;
            // 
            // buttonAvanzar
            // 
            buttonAvanzar.Location = new Point(63, 710);
            buttonAvanzar.Margin = new Padding(4, 5, 4, 5);
            buttonAvanzar.Name = "buttonAvanzar";
            buttonAvanzar.Size = new Size(107, 38);
            buttonAvanzar.TabIndex = 6;
            buttonAvanzar.Text = "Avanzar";
            buttonAvanzar.UseVisualStyleBackColor = true;
            buttonAvanzar.Visible = false;
            buttonAvanzar.Click += buttonAvanzar_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(140, 1080);
            buttonReset.Margin = new Padding(4, 5, 4, 5);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(107, 38);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // dataGridAviones
            // 
            dataGridAviones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAviones.Location = new Point(443, 82);
            dataGridAviones.Margin = new Padding(4, 5, 4, 5);
            dataGridAviones.Name = "dataGridAviones";
            dataGridAviones.RowHeadersWidth = 62;
            dataGridAviones.Size = new Size(1527, 1038);
            dataGridAviones.TabIndex = 8;
            dataGridAviones.Visible = false;
            // 
            // buttonDataGrid
            // 
            buttonDataGrid.Location = new Point(211, 710);
            buttonDataGrid.Margin = new Padding(4, 5, 4, 5);
            buttonDataGrid.Name = "buttonDataGrid";
            buttonDataGrid.Size = new Size(107, 38);
            buttonDataGrid.TabIndex = 9;
            buttonDataGrid.Text = "Mostrar Tabla";
            buttonDataGrid.UseVisualStyleBackColor = true;
            buttonDataGrid.Click += buttonDataGrid_Click;
            // 
            // gMapControl1
            // 
            gMapControl1.Bearing = 0F;
            gMapControl1.CanDragMap = true;
            gMapControl1.EmptyTileColor = Color.Navy;
            gMapControl1.GrayScaleMode = false;
            gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            gMapControl1.LevelsKeepInMemory = 5;
            gMapControl1.Location = new Point(443, 82);
            gMapControl1.Margin = new Padding(4, 5, 4, 5);
            gMapControl1.MarkersEnabled = true;
            gMapControl1.MaxZoom = 2;
            gMapControl1.MinZoom = 2;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.Name = "gMapControl1";
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.RetryLoadTile = 0;
            gMapControl1.RoutesEnabled = true;
            gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            gMapControl1.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Size = new Size(1527, 1038);
            gMapControl1.TabIndex = 10;
            gMapControl1.Zoom = 0D;
            // 
            // labelX1
            // 
            labelX1.AutoSize = true;
            labelX1.ForeColor = SystemColors.ControlLightLight;
            labelX1.Location = new Point(80, 880);
            labelX1.Margin = new Padding(4, 0, 4, 0);
            labelX1.Name = "labelX1";
            labelX1.Size = new Size(30, 25);
            labelX1.TabIndex = 11;
            labelX1.Text = "x1";
            // 
            // labelx2
            // 
            labelx2.AutoSize = true;
            labelx2.ForeColor = SystemColors.ControlLightLight;
            labelx2.Location = new Point(140, 880);
            labelx2.Margin = new Padding(4, 0, 4, 0);
            labelx2.Name = "labelx2";
            labelx2.Size = new Size(30, 25);
            labelx2.TabIndex = 12;
            labelx2.Text = "x2";
            // 
            // labelx8
            // 
            labelx8.AutoSize = true;
            labelx8.ForeColor = SystemColors.ControlLightLight;
            labelx8.Location = new Point(271, 880);
            labelx8.Margin = new Padding(4, 0, 4, 0);
            labelx8.Name = "labelx8";
            labelx8.Size = new Size(30, 25);
            labelx8.TabIndex = 14;
            labelx8.Text = "x8";
            // 
            // labelx4
            // 
            labelx4.AutoSize = true;
            labelx4.ForeColor = SystemColors.ControlLightLight;
            labelx4.Location = new Point(211, 880);
            labelx4.Margin = new Padding(4, 0, 4, 0);
            labelx4.Name = "labelx4";
            labelx4.Size = new Size(30, 25);
            labelx4.TabIndex = 13;
            labelx4.Text = "x4";
            // 
            // labelTituloVelocidadReproduccion
            // 
            labelTituloVelocidadReproduccion.AutoSize = true;
            labelTituloVelocidadReproduccion.ForeColor = SystemColors.ControlLightLight;
            labelTituloVelocidadReproduccion.Location = new Point(84, 782);
            labelTituloVelocidadReproduccion.Margin = new Padding(4, 0, 4, 0);
            labelTituloVelocidadReproduccion.Name = "labelTituloVelocidadReproduccion";
            labelTituloVelocidadReproduccion.Size = new Size(224, 25);
            labelTituloVelocidadReproduccion.TabIndex = 15;
            labelTituloVelocidadReproduccion.Text = "Velocidad de reproducción";
            // 
            // labelTituloHora
            // 
            labelTituloHora.AutoSize = true;
            labelTituloHora.ForeColor = SystemColors.ControlLightLight;
            labelTituloHora.Location = new Point(71, 1013);
            labelTituloHora.Margin = new Padding(4, 0, 4, 0);
            labelTituloHora.Name = "labelTituloHora";
            labelTituloHora.Size = new Size(111, 25);
            labelTituloHora.TabIndex = 16;
            labelTituloHora.Text = "Hora actual: ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_52_2;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(140, 545);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(67, 87);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_56_53_3;
            pictureBox2.InitialImage = (Image)resources.GetObject("pictureBox2.InitialImage");
            pictureBox2.Location = new Point(323, 545);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(67, 87);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Imagen_de_ChatGPT_1_oct_2026__17_54_09;
            pictureBox3.InitialImage = (Image)resources.GetObject("pictureBox3.InitialImage");
            pictureBox3.Location = new Point(65, 545);
            pictureBox3.Margin = new Padding(4, 5, 4, 5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(67, 87);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // labelTituloVerde
            // 
            labelTituloVerde.AutoSize = true;
            labelTituloVerde.ForeColor = SystemColors.ControlLightLight;
            labelTituloVerde.Location = new Point(167, 312);
            labelTituloVerde.Margin = new Padding(4, 0, 4, 0);
            labelTituloVerde.Name = "labelTituloVerde";
            labelTituloVerde.Size = new Size(64, 25);
            labelTituloVerde.TabIndex = 20;
            labelTituloVerde.Text = "ADS-B";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(167, 357);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(58, 25);
            label2.TabIndex = 21;
            label2.Text = "Radar";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(167, 402);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(132, 25);
            label3.TabIndex = 22;
            label3.Text = "Radar + ADS-B";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.large_green_square_1f7e9;
            pictureBox4.InitialImage = (Image)resources.GetObject("pictureBox4.InitialImage");
            pictureBox4.Location = new Point(99, 312);
            pictureBox4.Margin = new Padding(4, 5, 4, 5);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(23, 25);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 23;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.images;
            pictureBox5.InitialImage = (Image)resources.GetObject("pictureBox5.InitialImage");
            pictureBox5.Location = new Point(99, 357);
            pictureBox5.Margin = new Padding(4, 5, 4, 5);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(23, 25);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 24;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.large_blue_square_1f7e6;
            pictureBox6.InitialImage = (Image)resources.GetObject("pictureBox6.InitialImage");
            pictureBox6.Location = new Point(99, 402);
            pictureBox6.Margin = new Padding(4, 5, 4, 5);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(23, 25);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 25;
            pictureBox6.TabStop = false;
            // 
            // labelTituloAsterix
            // 
            labelTituloAsterix.AutoSize = true;
            labelTituloAsterix.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTituloAsterix.ForeColor = Color.Turquoise;
            labelTituloAsterix.Location = new Point(84, 178);
            labelTituloAsterix.Margin = new Padding(4, 0, 4, 0);
            labelTituloAsterix.Name = "labelTituloAsterix";
            labelTituloAsterix.Size = new Size(243, 71);
            labelTituloAsterix.TabIndex = 26;
            labelTituloAsterix.Text = "ASTERIX";
            // 
            // pictureBoxPausa
            // 
            pictureBoxPausa.BackColor = Color.Transparent;
            pictureBoxPausa.Image = Properties.Resources.Icono_de_pausa_blanco_sobre_transparente2;
            pictureBoxPausa.Location = new Point(214, 545);
            pictureBoxPausa.Name = "pictureBoxPausa";
            pictureBoxPausa.Size = new Size(102, 87);
            pictureBoxPausa.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPausa.TabIndex = 27;
            pictureBoxPausa.TabStop = false;
            // 
            // MenuSimulacion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(1924, 1050);
            Controls.Add(pictureBoxPausa);
            Controls.Add(labelTituloAsterix);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(labelTituloVerde);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(labelTituloHora);
            Controls.Add(labelTituloVelocidadReproduccion);
            Controls.Add(labelx8);
            Controls.Add(labelx4);
            Controls.Add(labelx2);
            Controls.Add(labelX1);
            Controls.Add(gMapControl1);
            Controls.Add(buttonDataGrid);
            Controls.Add(dataGridAviones);
            Controls.Add(buttonReset);
            Controls.Add(buttonAvanzar);
            Controls.Add(trackBarVelSimulacion);
            Controls.Add(label1);
            Controls.Add(buttonStop);
            Controls.Add(buttonPlay);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            Name = "MenuSimulacion";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "ASTERIX";
            Load += MenuSimulacion_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPausa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem cargaDatosrToolStripMenuItem;
        private ToolStripMenuItem filtrarToolStripMenuItem;
        private System.Windows.Forms.Timer timerSimulacion;
        private Button buttonPlay;
        private Button buttonStop;
        private Label label1;
        private TrackBar trackBarVelSimulacion;
        private Button buttonAvanzar;
        private Button buttonReset;
        private DataGridView dataGridAviones;
        private Button buttonDataGrid;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private Label labelX1;
        private Label labelx2;
        private Label labelx8;
        private Label labelx4;
        private Label labelTituloVelocidadReproduccion;
        private Label labelTituloHora;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolStripMenuItem aplicarToolStripMenuItem;
        private ToolStripMenuItem limpiarToolStripMenuItem;
        private Label labelTituloVerde;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private Label labelTituloAsterix;
        private ToolStripMenuItem guardarEnCSVToolStripMenuItem;
        private System.Windows.Forms.Timer timerClick;
        private PictureBox pictureBoxPausa;
    }
}
