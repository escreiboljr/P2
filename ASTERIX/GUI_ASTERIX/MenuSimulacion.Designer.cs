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
            verToolStripMenuItem = new ToolStripMenuItem();
            mostrarDatosEnCeldasToolStripMenuItem = new ToolStripMenuItem();
            trayectoriasToolStripMenuItem = new ToolStripMenuItem();
            heatmapToolStripMenuItem = new ToolStripMenuItem();
            mostrarHeatmapCompletoToolStripMenuItem = new ToolStripMenuItem();
            mostrarHeatmapDinamicoToolStripMenuItem = new ToolStripMenuItem();
            ayudaToolStripMenuItem = new ToolStripMenuItem();
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
            labelTituloVelocidadReproduccion = new Label();
            labelTituloHora = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            labelTituloVerde = new Label();
            label2 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            labelTituloAsterix = new Label();
            timerClick = new System.Windows.Forms.Timer(components);
            textBoxHora = new TextBox();
            textBoxSegundo = new TextBox();
            textBoxMinuto = new TextBox();
            buttonSimularDesdeHora = new Button();
            labelVelocidad = new Label();
            trackBarTiempo = new TrackBar();
            labelTiempoFinal = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox6 = new PictureBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarTiempo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, filtrarToolStripMenuItem, verToolStripMenuItem, ayudaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1281, 29);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cargaDatosrToolStripMenuItem, guardarEnCSVToolStripMenuItem });
            archivoToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(75, 25);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // cargaDatosrToolStripMenuItem
            // 
            cargaDatosrToolStripMenuItem.Name = "cargaDatosrToolStripMenuItem";
            cargaDatosrToolStripMenuItem.Size = new Size(191, 26);
            cargaDatosrToolStripMenuItem.Text = "Cargar datos";
            cargaDatosrToolStripMenuItem.Click += cargaDatosrToolStripMenuItem_Click;
            // 
            // guardarEnCSVToolStripMenuItem
            // 
            guardarEnCSVToolStripMenuItem.Name = "guardarEnCSVToolStripMenuItem";
            guardarEnCSVToolStripMenuItem.Size = new Size(191, 26);
            guardarEnCSVToolStripMenuItem.Text = "Guardar en CSV";
            guardarEnCSVToolStripMenuItem.Click += guardarEnCSVToolStripMenuItem_Click;
            // 
            // filtrarToolStripMenuItem
            // 
            filtrarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aplicarToolStripMenuItem, limpiarToolStripMenuItem });
            filtrarToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            filtrarToolStripMenuItem.Name = "filtrarToolStripMenuItem";
            filtrarToolStripMenuItem.Size = new Size(63, 25);
            filtrarToolStripMenuItem.Text = "Filtrar";
            // 
            // aplicarToolStripMenuItem
            // 
            aplicarToolStripMenuItem.Name = "aplicarToolStripMenuItem";
            aplicarToolStripMenuItem.Size = new Size(172, 26);
            aplicarToolStripMenuItem.Text = "Aplicar filtros";
            aplicarToolStripMenuItem.Click += aplicarToolStripMenuItem_Click;
            // 
            // limpiarToolStripMenuItem
            // 
            limpiarToolStripMenuItem.Name = "limpiarToolStripMenuItem";
            limpiarToolStripMenuItem.Size = new Size(172, 26);
            limpiarToolStripMenuItem.Text = "Limpiar";
            limpiarToolStripMenuItem.Click += limpiarToolStripMenuItem_Click;
            // 
            // verToolStripMenuItem
            // 
            verToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostrarDatosEnCeldasToolStripMenuItem, trayectoriasToolStripMenuItem, heatmapToolStripMenuItem });
            verToolStripMenuItem.Font = new Font("Segoe UI", 12F);
            verToolStripMenuItem.Name = "verToolStripMenuItem";
            verToolStripMenuItem.Size = new Size(45, 25);
            verToolStripMenuItem.Text = "Ver";
            // 
            // mostrarDatosEnCeldasToolStripMenuItem
            // 
            mostrarDatosEnCeldasToolStripMenuItem.Name = "mostrarDatosEnCeldasToolStripMenuItem";
            mostrarDatosEnCeldasToolStripMenuItem.Size = new Size(254, 26);
            mostrarDatosEnCeldasToolStripMenuItem.Text = "Mostar aviones en celdas";
            mostrarDatosEnCeldasToolStripMenuItem.Click += mostrarDatosEnCeldasToolStripMenuItem_Click;
            // 
            // trayectoriasToolStripMenuItem
            // 
            trayectoriasToolStripMenuItem.Name = "trayectoriasToolStripMenuItem";
            trayectoriasToolStripMenuItem.Size = new Size(254, 26);
            trayectoriasToolStripMenuItem.Text = "Trayectorias";
            trayectoriasToolStripMenuItem.Click += trayectoriasToolStripMenuItem_Click;
            // 
            // heatmapToolStripMenuItem
            // 
            heatmapToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mostrarHeatmapCompletoToolStripMenuItem, mostrarHeatmapDinamicoToolStripMenuItem });
            heatmapToolStripMenuItem.Name = "heatmapToolStripMenuItem";
            heatmapToolStripMenuItem.Size = new Size(254, 26);
            heatmapToolStripMenuItem.Text = "Heatmap";
            // 
            // mostrarHeatmapCompletoToolStripMenuItem
            // 
            mostrarHeatmapCompletoToolStripMenuItem.Name = "mostrarHeatmapCompletoToolStripMenuItem";
            mostrarHeatmapCompletoToolStripMenuItem.Size = new Size(269, 26);
            mostrarHeatmapCompletoToolStripMenuItem.Text = "Mostrar heatmap completo";
            mostrarHeatmapCompletoToolStripMenuItem.Click += mostrarHeatmapCompletoToolStripMenuItem_Click;
            // 
            // mostrarHeatmapDinamicoToolStripMenuItem
            // 
            mostrarHeatmapDinamicoToolStripMenuItem.Name = "mostrarHeatmapDinamicoToolStripMenuItem";
            mostrarHeatmapDinamicoToolStripMenuItem.Size = new Size(269, 26);
            mostrarHeatmapDinamicoToolStripMenuItem.Text = "Mostrar heatmap dinamico";
            mostrarHeatmapDinamicoToolStripMenuItem.Click += mostrarHeatmapDinamicoToolStripMenuItem_Click;
            // 
            // ayudaToolStripMenuItem
            // 
            ayudaToolStripMenuItem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            ayudaToolStripMenuItem.Size = new Size(66, 25);
            ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // timerSimulacion
            // 
            timerSimulacion.Tick += timerSimulacion_Tick;
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new Point(101, 32);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(21, 23);
            buttonPlay.TabIndex = 1;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Visible = false;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(128, 32);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(14, 23);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Visible = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(371, 29);
            label1.Name = "label1";
            label1.Size = new Size(158, 47);
            label1.TabIndex = 3;
            label1.Text = "00:00:00";
            label1.Visible = false;
            // 
            // trackBarVelSimulacion
            // 
            trackBarVelSimulacion.LargeChange = 1;
            trackBarVelSimulacion.Location = new Point(25, 647);
            trackBarVelSimulacion.Maximum = 8;
            trackBarVelSimulacion.Minimum = 1;
            trackBarVelSimulacion.Name = "trackBarVelSimulacion";
            trackBarVelSimulacion.Size = new Size(240, 45);
            trackBarVelSimulacion.TabIndex = 8;
            trackBarVelSimulacion.Value = 1;
            trackBarVelSimulacion.Scroll += trackBarVelSimulacion_Scroll;
            // 
            // buttonAvanzar
            // 
            buttonAvanzar.Location = new Point(148, 32);
            buttonAvanzar.Name = "buttonAvanzar";
            buttonAvanzar.Size = new Size(21, 23);
            buttonAvanzar.TabIndex = 6;
            buttonAvanzar.Text = "Avanzar";
            buttonAvanzar.UseVisualStyleBackColor = true;
            buttonAvanzar.Visible = false;
            buttonAvanzar.Click += buttonAvanzar_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(256, 32);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(16, 23);
            buttonReset.TabIndex = 7;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Visible = false;
            buttonReset.Click += buttonReset_Click;
            // 
            // dataGridAviones
            // 
            dataGridAviones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAviones.Location = new Point(297, 49);
            dataGridAviones.Name = "dataGridAviones";
            dataGridAviones.RowHeadersWidth = 62;
            dataGridAviones.Size = new Size(962, 536);
            dataGridAviones.TabIndex = 8;
            dataGridAviones.Visible = false;
            // 
            // buttonDataGrid
            // 
            buttonDataGrid.Location = new Point(175, 32);
            buttonDataGrid.Name = "buttonDataGrid";
            buttonDataGrid.Size = new Size(75, 23);
            buttonDataGrid.TabIndex = 9;
            buttonDataGrid.Text = "Mostrar Tabla";
            buttonDataGrid.UseVisualStyleBackColor = true;
            buttonDataGrid.Visible = false;
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
            gMapControl1.Location = new Point(25, 49);
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
            gMapControl1.Size = new Size(1234, 536);
            gMapControl1.TabIndex = 10;
            gMapControl1.Zoom = 0D;
            // 
            // labelTituloVelocidadReproduccion
            // 
            labelTituloVelocidadReproduccion.AutoSize = true;
            labelTituloVelocidadReproduccion.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelTituloVelocidadReproduccion.ForeColor = SystemColors.ControlLightLight;
            labelTituloVelocidadReproduccion.Location = new Point(12, 614);
            labelTituloVelocidadReproduccion.Name = "labelTituloVelocidadReproduccion";
            labelTituloVelocidadReproduccion.Size = new Size(270, 30);
            labelTituloVelocidadReproduccion.TabIndex = 15;
            labelTituloVelocidadReproduccion.Text = "Velocidad de reproducción";
            // 
            // labelTituloHora
            // 
            labelTituloHora.AutoSize = true;
            labelTituloHora.BackColor = Color.Transparent;
            labelTituloHora.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTituloHora.ForeColor = SystemColors.ControlLightLight;
            labelTituloHora.Location = new Point(295, 646);
            labelTituloHora.Name = "labelTituloHora";
            labelTituloHora.Size = new Size(147, 32);
            labelTituloHora.TabIndex = 16;
            labelTituloHora.Text = "Hora actual: ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.botonReproducir;
            pictureBox1.InitialImage = (Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new Point(673, 662);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(44, 43);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.botonAvanzar;
            pictureBox2.InitialImage = (Image)resources.GetObject("pictureBox2.InitialImage");
            pictureBox2.Location = new Point(765, 662);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(44, 43);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.botonRetroceder;
            pictureBox3.InitialImage = (Image)resources.GetObject("pictureBox3.InitialImage");
            pictureBox3.Location = new Point(575, 662);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(44, 43);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 19;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // labelTituloVerde
            // 
            labelTituloVerde.Anchor = AnchorStyles.Bottom;
            labelTituloVerde.AutoSize = true;
            labelTituloVerde.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            labelTituloVerde.ForeColor = SystemColors.ControlLightLight;
            labelTituloVerde.Location = new Point(902, 675);
            labelTituloVerde.Name = "labelTituloVerde";
            labelTituloVerde.Size = new Size(68, 25);
            labelTituloVerde.TabIndex = 20;
            labelTituloVerde.Text = "ADS-B";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(1020, 675);
            label2.Name = "label2";
            label2.Size = new Size(62, 25);
            label2.TabIndex = 21;
            label2.Text = "Radar";
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Bottom;
            pictureBox4.Image = Properties.Resources.large_green_square_1f7e9;
            pictureBox4.InitialImage = (Image)resources.GetObject("pictureBox4.InitialImage");
            pictureBox4.Location = new Point(868, 675);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(28, 28);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 23;
            pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Anchor = AnchorStyles.Bottom;
            pictureBox5.Image = Properties.Resources.images;
            pictureBox5.InitialImage = (Image)resources.GetObject("pictureBox5.InitialImage");
            pictureBox5.Location = new Point(983, 676);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(29, 28);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 24;
            pictureBox5.TabStop = false;
            // 
            // labelTituloAsterix
            // 
            labelTituloAsterix.AutoSize = true;
            labelTituloAsterix.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTituloAsterix.ForeColor = Color.Turquoise;
            labelTituloAsterix.Location = new Point(1107, 675);
            labelTituloAsterix.Name = "labelTituloAsterix";
            labelTituloAsterix.Size = new Size(162, 47);
            labelTituloAsterix.TabIndex = 26;
            labelTituloAsterix.Text = "ASTERIX";
            // 
            // textBoxHora
            // 
            textBoxHora.BackColor = Color.FromArgb(18, 28, 45);
            textBoxHora.BorderStyle = BorderStyle.None;
            textBoxHora.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            textBoxHora.ForeColor = SystemColors.ControlLightLight;
            textBoxHora.Location = new Point(290, 600);
            textBoxHora.Name = "textBoxHora";
            textBoxHora.Size = new Size(42, 47);
            textBoxHora.TabIndex = 30;
            textBoxHora.Text = "00";
            textBoxHora.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxSegundo
            // 
            textBoxSegundo.BackColor = Color.FromArgb(18, 28, 45);
            textBoxSegundo.BorderStyle = BorderStyle.None;
            textBoxSegundo.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            textBoxSegundo.ForeColor = SystemColors.ControlLightLight;
            textBoxSegundo.Location = new Point(388, 600);
            textBoxSegundo.Name = "textBoxSegundo";
            textBoxSegundo.Size = new Size(42, 47);
            textBoxSegundo.TabIndex = 31;
            textBoxSegundo.Text = "00";
            textBoxSegundo.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxMinuto
            // 
            textBoxMinuto.BackColor = Color.FromArgb(18, 28, 45);
            textBoxMinuto.BorderStyle = BorderStyle.None;
            textBoxMinuto.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            textBoxMinuto.ForeColor = SystemColors.ControlLightLight;
            textBoxMinuto.Location = new Point(339, 600);
            textBoxMinuto.Name = "textBoxMinuto";
            textBoxMinuto.Size = new Size(42, 47);
            textBoxMinuto.TabIndex = 32;
            textBoxMinuto.Text = "00";
            textBoxMinuto.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonSimularDesdeHora
            // 
            buttonSimularDesdeHora.Location = new Point(278, 32);
            buttonSimularDesdeHora.Name = "buttonSimularDesdeHora";
            buttonSimularDesdeHora.Size = new Size(87, 23);
            buttonSimularDesdeHora.TabIndex = 33;
            buttonSimularDesdeHora.Text = "Simular";
            buttonSimularDesdeHora.UseVisualStyleBackColor = true;
            buttonSimularDesdeHora.Visible = false;
            buttonSimularDesdeHora.Click += buttonSimularDesdeHora_Click;
            // 
            // labelVelocidad
            // 
            labelVelocidad.AutoSize = true;
            labelVelocidad.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold);
            labelVelocidad.ForeColor = SystemColors.ControlLightLight;
            labelVelocidad.Location = new Point(121, 680);
            labelVelocidad.Name = "labelVelocidad";
            labelVelocidad.Size = new Size(32, 30);
            labelVelocidad.TabIndex = 35;
            labelVelocidad.Text = "x1";
            labelVelocidad.TextAlign = ContentAlignment.TopCenter;
            // 
            // trackBarTiempo
            // 
            trackBarTiempo.Location = new Point(453, 611);
            trackBarTiempo.Maximum = 100;
            trackBarTiempo.Name = "trackBarTiempo";
            trackBarTiempo.Size = new Size(456, 45);
            trackBarTiempo.TabIndex = 36;
            trackBarTiempo.MouseUp += trackBarTiempo_MouseUp;
            // 
            // labelTiempoFinal
            // 
            labelTiempoFinal.AutoSize = true;
            labelTiempoFinal.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            labelTiempoFinal.ForeColor = SystemColors.ControlLightLight;
            labelTiempoFinal.Location = new Point(915, 600);
            labelTiempoFinal.Name = "labelTiempoFinal";
            labelTiempoFinal.Size = new Size(158, 47);
            labelTiempoFinal.TabIndex = 37;
            labelTiempoFinal.Text = "00:00:00";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(323, 598);
            label3.Name = "label3";
            label3.Size = new Size(29, 47);
            label3.TabIndex = 38;
            label3.Text = ":";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(372, 599);
            label4.Name = "label4";
            label4.Size = new Size(29, 47);
            label4.TabIndex = 39;
            label4.Text = ":";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.botonRecargar;
            pictureBox6.InitialImage = (Image)resources.GetObject("pictureBox6.InitialImage");
            pictureBox6.Location = new Point(495, 662);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(44, 43);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 40;
            pictureBox6.TabStop = false;
            pictureBox6.Click += buttonReset_Click;
            // 
            // MenuSimulacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 28, 45);
            ClientSize = new Size(1281, 731);
            Controls.Add(pictureBox6);
            Controls.Add(label1);
            Controls.Add(labelTiempoFinal);
            Controls.Add(trackBarTiempo);
            Controls.Add(labelVelocidad);
            Controls.Add(buttonSimularDesdeHora);
            Controls.Add(textBoxMinuto);
            Controls.Add(textBoxSegundo);
            Controls.Add(textBoxHora);
            Controls.Add(labelTituloAsterix);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(label2);
            Controls.Add(labelTituloVerde);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(labelTituloHora);
            Controls.Add(labelTituloVelocidadReproduccion);
            Controls.Add(gMapControl1);
            Controls.Add(buttonDataGrid);
            Controls.Add(dataGridAviones);
            Controls.Add(buttonReset);
            Controls.Add(buttonAvanzar);
            Controls.Add(trackBarVelSimulacion);
            Controls.Add(buttonStop);
            Controls.Add(buttonPlay);
            Controls.Add(menuStrip1);
            Controls.Add(label3);
            Controls.Add(label4);
            MainMenuStrip = menuStrip1;
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
            ((System.ComponentModel.ISupportInitialize)trackBarTiempo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
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
        private Label labelTituloVelocidadReproduccion;
        private Label labelTituloHora;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolStripMenuItem aplicarToolStripMenuItem;
        private ToolStripMenuItem limpiarToolStripMenuItem;
        private Label labelTituloVerde;
        private Label label2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private Label labelTituloAsterix;
        private ToolStripMenuItem guardarEnCSVToolStripMenuItem;
        private System.Windows.Forms.Timer timerClick;
        private TextBox textBoxHora;
        private TextBox textBoxSegundo;
        private TextBox textBoxMinuto;
        private Button buttonSimularDesdeHora;
        private ToolStripMenuItem verToolStripMenuItem;
        private ToolStripMenuItem mostrarDatosEnCeldasToolStripMenuItem;
        private ToolStripMenuItem trayectoriasToolStripMenuItem;
        private ToolStripMenuItem heatmapToolStripMenuItem;
        private ToolStripMenuItem mostrarHeatmapCompletoToolStripMenuItem;
        private ToolStripMenuItem mostrarHeatmapDinamicoToolStripMenuItem;
        private ToolStripMenuItem ayudaToolStripMenuItem;
        private Button buttonSalirMenuInicio;
        private Label labelVelocidad;
        private TrackBar trackBarTiempo;
        private Label labelTiempoFinal;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox6;
    }
}
