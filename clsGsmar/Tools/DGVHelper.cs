using System.Data;
using System.Drawing;
using System.Windows.Forms;
using clsGsmar.Models;

namespace clsGsmar.Tools
{
    public static class DGVHelper
    {
        public static CheckBox headerCheckBox = new CheckBox();

        public static void SetupDataGridViewColumns(DataGridView dgv)
        {
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AllowUserToResizeRows = false;
        dgv.AllowUserToResizeColumns = true;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.RowHeadersWidth = 25;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;



            // Clear existing
            dgv.Columns.Clear();

            // Add CheckBox Column
            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                Width = 30,
                Name = "ChkCell"
            };
            dgv.Columns.Add(chkCol);

            // Add other columns with friendly headers
            dgv.Columns.Add("BrID", "ID");
            dgv.Columns.Add("BrName", "Brand Name");
            dgv.Columns.Add("BrUrl", "Brand URL");
            dgv.Columns.Add("Pcnt", "Phones Count");
        }

        public static void AddHeaderCheckBox(DataGridView dgv, EventHandler headerClickHandler)
        {
            // Place the headerCheckBox at header cell 0
            Point headerCellLocation = dgv.GetCellDisplayRectangle(0, -1, true).Location;

            headerCheckBox.Location = new Point(headerCellLocation.X + 8, headerCellLocation.Y + 2);
            headerCheckBox.BackColor = Color.White;
            headerCheckBox.Size = new Size(18, 18);
            headerCheckBox.Click += headerClickHandler;

            dgv.Controls.Add(headerCheckBox);
        }

        public static void BindData(DataGridView dgv, List<Brand> brands)
        {
            dgv.Rows.Clear();

            foreach (var brand in brands)
            {
                dgv.Rows.Add(false, brand.Id, brand.Name, brand.Url, brand.PhoneCount);
            }
        }

        public static void HeaderCheckBox_Clicked(object sender, DataGridView dgv)
        {
            bool isChecked = headerCheckBox.Checked;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["ChkCell"];
                chk.Value = isChecked;
            }
        }

        public static void CellContentClick(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv.Columns["ChkCell"].Index && e.RowIndex >= 0)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);

                bool allChecked = true;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!(bool?)row.Cells["ChkCell"].Value ?? false == false)
                    {
                        allChecked = false;
                        break;
                    }
                }

                headerCheckBox.CheckedChanged -= (s, ev) => { };
                headerCheckBox.Checked = allChecked;
                headerCheckBox.CheckedChanged += (s, ev) => { };
            }
        }
    }
}
