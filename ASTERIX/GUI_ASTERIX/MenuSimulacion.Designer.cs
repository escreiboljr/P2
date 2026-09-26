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
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            cargaDatosrToolStripMenuItem = new ToolStripMenuItem();
            filtrarToolStripMenuItem = new ToolStripMenuItem();
            timerSimulacion = new System.Windows.Forms.Timer(components);
            buttonPlay = new Button();
            buttonStop = new Button();
            label1 = new Label();
            trackBarVelSimulacion = new TrackBar();
            labelVelocidad = new Label();
            buttonAvanzar = new Button();
            buttonReset = new Button();
            dataGridAviones = new DataGridView();
            buttonDataGrid = new Button();
            gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).BeginInit();
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
            buttonPlay.Location = new Point(47, 200);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(75, 23);
            buttonPlay.TabIndex = 1;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(151, 189);
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
            label1.Location = new Point(60, 439);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // trackBarVelSimulacion
            // 
            trackBarVelSimulacion.LargeChange = 4;
            trackBarVelSimulacion.Location = new Point(47, 475);
            trackBarVelSimulacion.Maximum = 4;
            trackBarVelSimulacion.Minimum = 1;
            trackBarVelSimulacion.Name = "trackBarVelSimulacion";
            trackBarVelSimulacion.Size = new Size(104, 45);
            trackBarVelSimulacion.TabIndex = 4;
            trackBarVelSimulacion.Value = 1;
            trackBarVelSimulacion.Scroll += trackBarVelSimulacion_Scroll;
            // 
            // labelVelocidad
            // 
            labelVelocidad.AutoSize = true;
            labelVelocidad.Location = new Point(60, 551);
            labelVelocidad.Name = "labelVelocidad";
            labelVelocidad.Size = new Size(38, 15);
            labelVelocidad.TabIndex = 5;
            labelVelocidad.Text = "label2";
            // 
            // buttonAvanzar
            // 
            buttonAvanzar.Location = new Point(47, 271);
            buttonAvanzar.Name = "buttonAvanzar";
            buttonAvanzar.Size = new Size(75, 23);
            buttonAvanzar.TabIndex = 6;
            buttonAvanzar.Text = "Avanzar";
            buttonAvanzar.UseVisualStyleBackColor = true;
            buttonAvanzar.Click += buttonAvanzar_Click;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(170, 271);
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
            buttonDataGrid.Location = new Point(95, 342);
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
            // MenuSimulacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1391, 700);
            Controls.Add(gMapControl1);
            Controls.Add(buttonDataGrid);
            Controls.Add(dataGridAviones);
            Controls.Add(buttonReset);
            Controls.Add(buttonAvanzar);
            Controls.Add(labelVelocidad);
            Controls.Add(trackBarVelSimulacion);
            Controls.Add(label1);
            Controls.Add(buttonStop);
            Controls.Add(buttonPlay);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MenuSimulacion";
            Text = "Form1";
            Load += MenuSimulacion_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarVelSimulacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAviones).EndInit();
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
        private Label labelVelocidad;
        private Button buttonAvanzar;
        private Button buttonReset;
        private DataGridView dataGridAviones;
        private Button buttonDataGrid;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}
