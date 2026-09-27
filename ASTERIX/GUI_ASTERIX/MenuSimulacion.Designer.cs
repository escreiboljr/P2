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
            filtrarToolStripMenuItem = new ToolStripMenuItem();
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
            aplicarToolStripMenuItem = new ToolStripMenuItem();
            limpiarToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, filtrarToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1391, 29);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargaDatosrToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(75, 25);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cargaDatosrToolStripMenuItem
            // 
            cargaDatosrToolStripMenuItem.Name = "cargaDatosrToolStripMenuItem";
            cargaDatosrToolStripMenuItem.Size = new Size(163, 26);
            cargaDatosrToolStripMenuItem.Text = "Carga datos";
            cargaDatosrToolStripMenuItem.Click += cargaDatosrToolStripMenuItem_Click;
            // 
            // filtrarToolStripMenuItem
            // 
            filtrarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aplicarToolStripMenuItem, limpiarToolStripMenuItem });
            filtrarToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            filtrarToolStripMenuItem.Name = "filtrarToolStripMenuItem";
            filtrarToolStripMenuItem.Size = new Size(63, 25);
            filtrarToolStripMenuItem.Text = "Filtrar";
            // 
            // timerSimulacion
            // 
            timerSimulacion.Tick += timerSimulacion_Tick;
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new Point(19, 67);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(75, 23);
            buttonPlay.TabIndex = 1;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(133, 67);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(75, 23);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(151, 608);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 3;
            label1.Text = "00:00:00";
            // 
            // trackBarVelSimulacion
            // 
            trackBarVelSimulacion.LargeChange = 4;
            trackBarVelSimulacion.Location = new Point(50, 498);
            trackBarVelSimulacion.Maximum = 4;
            trackBarVelSimulacion.Minimum = 1;
            trackBarVelSimulacion.Name = "trackBarVelSimulacion";
            trackBarVelSimulacion.Size = new Size(163, 45);
            trackBarVelSimulacion.TabIndex = 4;
            trackBarVelSimulacion.Value = 1;
            trackBarVelSimulacion.Scroll += trackBarVelSimulacion_Scroll;
            // 
            // buttonAvanzar
            // 
            buttonAvanzar.Location = new Point(19, 106);
            buttonAvanzar.Name = "buttonAvanzar";
            buttonAvanzar.Size = new Size(75, 23);
            buttonAvanzar.TabIndex = 6;
            buttonAvanzar.Text = "Avanzar";
            buttonAvanzar.UseVisualStyleBackColor = true;
            buttonAvanzar.Click += buttonAvanzar_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(98, 648);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(75, 23);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // dataGridAviones
            // 
            dataGridAviones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAviones.Location = new Point(293, 49);
            dataGridAviones.Name = "dataGridAviones";
            dataGridAviones.Size = new Size(1069, 623);
            dataGridAviones.TabIndex = 8;
            dataGridAviones.Visible = false;
            // 
            // buttonDataGrid
            // 
            buttonDataGrid.Location = new Point(91, 421);
            buttonDataGrid.Name = "buttonDataGrid";
            buttonDataGrid.Size = new Size(75, 23);
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
            gMapControl1.Location = new Point(310, 49);
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
            gMapControl1.Size = new Size(1069, 623);
            gMapControl1.TabIndex = 10;
            gMapControl1.Zoom = 0D;
            // 
            // labelX1
            // 
            labelX1.AutoSize = true;
            labelX1.Location = new Point(56, 528);
            labelX1.Name = "labelX1";
            labelX1.Size = new Size(18, 15);
            labelX1.TabIndex = 11;
            labelX1.Text = "x1";
            // 
            // labelx2
            // 
            labelx2.AutoSize = true;
            labelx2.Location = new Point(98, 528);
            labelx2.Name = "labelx2";
            labelx2.Size = new Size(18, 15);
            labelx2.TabIndex = 12;
            labelx2.Text = "x2";
            // 
            // labelx8
            // 
            labelx8.AutoSize = true;
            labelx8.Location = new Point(190, 528);
            labelx8.Name = "labelx8";
            labelx8.Size = new Size(18, 15);
            labelx8.TabIndex = 14;
            labelx8.Text = "x8";
            // 
            // labelx4
            // 
            labelx4.AutoSize = true;
            labelx4.Location = new Point(148, 528);
            labelx4.Name = "labelx4";
            labelx4.Size = new Size(18, 15);
            labelx4.TabIndex = 13;
            labelx4.Text = "x4";
            // 
            // labelTituloVelocidadReproduccion
            // 
            labelTituloVelocidadReproduccion.AutoSize = true;
            labelTituloVelocidadReproduccion.Location = new Point(59, 469);
            labelTituloVelocidadReproduccion.Name = "labelTituloVelocidadReproduccion";
            labelTituloVelocidadReproduccion.Size = new Size(148, 15);
            labelTituloVelocidadReproduccion.TabIndex = 15;
            labelTituloVelocidadReproduccion.Text = "Velocidad de reproducción";
            // 
            // labelTituloHora
            // 
            labelTituloHora.AutoSize = true;
            labelTituloHora.Location = new Point(50, 608);
            labelTituloHora.Name = "labelTituloHora";
            labelTituloHora.Size = new Size(74, 15);
            labelTituloHora.TabIndex = 16;
            labelTituloHora.Text = "Hora actual: ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.playbutton_113628;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(110, 327);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(47, 52);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources._25650;
            pictureBox2.InitialImage = (Image)resources.GetObject("pictureBox2.InitialImage");
            pictureBox2.Location = new Point(190, 327);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(47, 52);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.retroceder;
            pictureBox3.InitialImage = (Image)resources.GetObject("pictureBox3.InitialImage");
            pictureBox3.Location = new Point(27, 327);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(47, 52);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;
            // 
            // aplicarToolStripMenuItem
            // 
            aplicarToolStripMenuItem.Name = "aplicarToolStripMenuItem";
            aplicarToolStripMenuItem.Size = new Size(180, 26);
            aplicarToolStripMenuItem.Text = "Aplicar filtros";
            // 
            // limpiarToolStripMenuItem
            // 
            limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            limpiarToolStripMenuItem.Size = new Size(180, 26);
            limpiarToolStripMenuItem.Text = "Limpiar";
            // 
            // MenuSimulacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1391, 700);
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
            Name = "MenuSimulacion";
            Text = "ASTERIX";
            Load += MenuSimulacion_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
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
    }
}
