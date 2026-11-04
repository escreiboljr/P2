using System.Drawing;
using System.Windows.Forms;

namespace GUI_ASTERIX
{
    public static class EstiloDataGrid
    {
        public static void PersonalizarDataGrid(DataGridView dataGridAviones)
        {
            // colores del programa asterix
            Color fondo = Color.FromArgb(17, 27, 44);
            Color fondoAlterno = Color.FromArgb(23, 38, 58);
            Color encabezado = Color.FromArgb(20, 43, 63);
            Color cian = Color.FromArgb(54, 220, 235);
            Color seleccion = Color.FromArgb(24, 85, 110);

            // aspecto general
            dataGridAviones.BackgroundColor = fondo;
            dataGridAviones.BorderStyle = BorderStyle.None;
            dataGridAviones.GridColor = Color.FromArgb(35, 65, 85);

            // encabezados de las columnas
            dataGridAviones.EnableHeadersVisualStyles = false;
            dataGridAviones.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;

            dataGridAviones.ColumnHeadersDefaultCellStyle.BackColor = encabezado;
            dataGridAviones.ColumnHeadersDefaultCellStyle.ForeColor = cian;
            dataGridAviones.ColumnHeadersDefaultCellStyle.SelectionBackColor = encabezado;
            dataGridAviones.ColumnHeadersDefaultCellStyle.SelectionForeColor = cian;
            dataGridAviones.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridAviones.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridAviones.ColumnHeadersHeight = 42;

            // aspecto de las filas
            dataGridAviones.DefaultCellStyle.BackColor = fondo;
            dataGridAviones.DefaultCellStyle.ForeColor = Color.White;
            dataGridAviones.DefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Regular);

            dataGridAviones.DefaultCellStyle.SelectionBackColor = seleccion;
            dataGridAviones.DefaultCellStyle.SelectionForeColor = Color.White;

            // alternar ligeramente el color de las filas
            dataGridAviones.AlternatingRowsDefaultCellStyle.BackColor = fondoAlterno;
            dataGridAviones.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dataGridAviones.AlternatingRowsDefaultCellStyle.SelectionBackColor = seleccion;
            dataGridAviones.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // bordes discretos entre filas
            dataGridAviones.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            // eliminar la columna lateral de selección
            dataGridAviones.RowHeadersVisible = false;

            // dimensiones
            dataGridAviones.RowTemplate.Height = 34;
            dataGridAviones.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // comportamiento de selección
            dataGridAviones.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridAviones.MultiSelect = false;

            // evitar modificaciones manuales
            dataGridAviones.AllowUserToAddRows = false;
            dataGridAviones.AllowUserToDeleteRows = false;
            dataGridAviones.ReadOnly = true;

            // eliminar bordes de enfoque innecesarios
            dataGridAviones.DefaultCellStyle.Padding =
                new Padding(5, 0, 5, 0);
        }

    }
}
