using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLyPhieuThu : UserControl
    {
        public string khuvuc {  get; set; }
        private QuanLyPhieuThuBLL bll = new QuanLyPhieuThuBLL();
        public QuanLyPhieuThu()
        {
            InitializeComponent();
        }
        
        private void QuanLyPhieuThu_Load(object sender, EventArgs e)
        {
            LoadCBOThang();
            LoadCBONam();
            LoadDGVPhieuThu(int.Parse(cboThang.Text), int.Parse(cboNam.Text));
        }
        private void LoadCBOThang()
        {
            cboThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cboThang.Items.Add(i.ToString("00"));
            }
            int currentMonth = DateTime.Now.Month;
            cboThang.SelectedIndex = currentMonth - 1;
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
            DataTable dt = bll.GetPTTheoThangNam(thang, nam, khuvuc);
            dgvPT.DataSource = dt;
            if (dgvPT.Columns.Contains("TrangThai"))
            {
                dgvPT.Columns["TrangThai"].DisplayIndex = 1;
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

        private void buttonRefesh_Click(object sender, EventArgs e)
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            LoadDGVPhieuThu(currentMonth, currentYear);
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
    }
}
