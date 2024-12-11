using BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLyPhieuThu : UserControl
    {
        public string khuvuc { get; set; }
        private QuanLyPhieuThuBLL bll = new QuanLyPhieuThuBLL();
        DataTable dt;
        public QuanLyPhieuThu()
        {
            InitializeComponent();
        }

        private void QuanLyPhieuThu_Load(object sender, EventArgs e)
        {
            LoadCBOThang();
            LoadCBONam();
            LoadALlPT();
        }
        private void LoadCBOThang()
        {
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i.ToString("00"));
            }
            int currentMonth = DateTime.Now.Month;

            cboThang.SelectedIndex = currentMonth -1;

        }
        private void LoadCBONam()
        {
            cboNam.Items.Clear();
            int startYear = 2020;
            int endYear = DateTime.Now.Year + 5;
            for (int year = startYear; year <= endYear; year++)
            {
                cboNam.Items.Add(year);
            }
            int currentYear = DateTime.Now.Year;
            int index = cboNam.Items.IndexOf(currentYear);
            if (index != -1)
            {
                cboNam.SelectedIndex = index;
            }
            else
            {
                cboNam.SelectedIndex = 0;
            }
        }
        private void LoadDGVPhieuThu(int thang, int nam)
        {
            dt = bll.GetPTTheoThangNam(thang, nam, khuvuc);
            dgvPT.DataSource = dt;
            if (dgvPT.Columns.Contains("TrangThai"))
            {
                dgvPT.Columns["TrangThai"].DefaultCellStyle.Format = "N0";
            }
            dgvPT.Columns["CSC Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSC Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền DV"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Thanh Toán"].DefaultCellStyle.Format = "N0";
            // Làm mới DataGridView
            dgvPT.Refresh();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDGVPhieuThu(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
        }
        private void LoadALlPT()
        {
            dt = bll.GetALLPT(khuvuc);
            dgvPT.DataSource = dt;
            if (dgvPT.Columns.Contains("TrangThai"))
            {
                dgvPT.Columns["TrangThai"].DefaultCellStyle.Format = "N0";
            }
            dgvPT.Columns["CSC Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSC Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền DV"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Thanh Toán"].DefaultCellStyle.Format = "N0";
            // Làm mới DataGridView
            dgvPT.Refresh();
        }
        private void buttonRefesh_Click(object sender, EventArgs e)
        {
            LoadALlPT();

        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                TaoPhieuThu f = new TaoPhieuThu();
                f.khuvuc = khuvuc;
                mainForm.ShowControl(f);
            }
        }

        private void dgvPT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPT.Columns["XemChiTiet"].Index && e.RowIndex >= 0)
            {
                // Lấy thông tin dòng hiện tại
                string mapt = dgvPT.Rows[e.RowIndex].Cells["Mã PT"].Value.ToString();
                if (this.ParentForm is MainForm mainForm)
                {
                    ThongTinPhieuThu f = new ThongTinPhieuThu();
                    f.mapt = mapt;
                    f.khuvuc = khuvuc;
                    mainForm.ShowControl(f);
                }
            }
        }

        private void dgvPT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPT.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value.ToString() == "True")
                {
                    e.Value = "Đã thanh toán";
                    e.CellStyle.ForeColor = Color.Green; // Màu xanh
                    e.CellStyle.Font = new Font(dgvPT.Font, FontStyle.Bold | FontStyle.Regular); 
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 10, FontStyle.Bold); 
                }
                else if (e.Value.ToString() == "False")
                {
                    e.Value = "Chưa thanh toán";
                    e.CellStyle.ForeColor = Color.Red; // Màu đỏ
                    e.CellStyle.Font = new Font(dgvPT.Font, FontStyle.Bold | FontStyle.Regular); 
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 10, FontStyle.Bold);
                }
            }
        }

        private void dgvPT_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dgvPT.Rows[e.RowIndex];
            int trangThai = Convert.ToInt32(row.Cells["TrangThai"].Value);
            int temp = 0;
            if (DateTime.TryParse(row.Cells["Ngày Lập"].Value?.ToString(), out DateTime ngayTao))
            {
                temp = (DateTime.Now.Date - ngayTao.Date).Days;
            }
            if (trangThai == 0)
            {

                if (temp <= 5)
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                else
                    row.DefaultCellStyle.BackColor = Color.PaleVioletRed;
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //dt = bll.GetPTTheoThangNamPhong(int.Parse(cboThang.Text), int.Parse(cboNam.Text), khuvuc, textBox1.Text);
            DataTable dtnew = dt.Copy(); 
            string filterText = textBox1.Text;

            for (int i = dtnew.Rows.Count - 1; i >= 0; i--) 
            {
                DataRow dr = dtnew.Rows[i];
                if (!dr["Phòng"].ToString().Contains(filterText)) 
                {
                    dr.Delete(); 
                }
            }

            dtnew.AcceptChanges(); 

            dgvPT.DataSource = dtnew;

            if (dgvPT.Columns.Contains("TrangThai"))
            {
                dgvPT.Columns["TrangThai"].DefaultCellStyle.Format = "N0";
            }

            dgvPT.Columns["CSC Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Điện"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSC Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["CSM Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền Nước"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tiền DV"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
            dgvPT.Columns["Thanh Toán"].DefaultCellStyle.Format = "N0";

            dgvPT.Refresh();

        }
    }
}
