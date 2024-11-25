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
    public partial class QuanLiThongKe : UserControl
    {
        public string khuvuc {  get; set; }
        private ThongKeBLL bll = new ThongKeBLL();
        public QuanLiThongKe()
        {
            InitializeComponent();
        }

        private void QuanLiThongKe_Load(object sender, EventArgs e)
        {
            LoadCBOThang();
            LoadCBONam();
            cboTK.SelectedIndex = 0;
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

        private void btnTK_Click(object sender, EventArgs e)
        {
            int thang = int.Parse(cboThang.Text);
            int nam = int.Parse(cboNam.Text);
            if(cboTK.SelectedIndex == 0)
            {
                ThongKeDoangThuThang(nam, thang, khuvuc);
            }
             else if (cboTK.SelectedIndex == 1)
            {
                ThongKeDoangThu(nam, khuvuc);
            }
            else if(cboTK.SelectedIndex == 2)
            {
                ThongKeDichVuThang(thang,nam,khuvuc);
            }
            else if(cboTK.SelectedIndex == 3)
            {
                ThongKeDichVuNam(nam,khuvuc);
            }
        }

        private void ThongKeDoangThu(int nam, string khuvuc)
        {
            DataTable dt = bll.ThongKeDoanhThuTheoNam(nam,khuvuc);
            dgvDT.DataSource = null;
            dgvDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDT.DataSource = dt;
            dgvDT.Columns["Tháng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Phòng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Số Kí Điện Sử Dụng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Điện"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Số Khối Nước Sử Dụng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Nước"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Dịch Vụ"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";

        }
        private void ThongKeDoangThuThang(int nam, int thang, string khuvuc)
        {
            DataTable dt = bll.ThongKeDoanhThuTheoThang(nam, thang, khuvuc);
            dgvDT.DataSource = null;
            dgvDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDT.DataSource = dt;
            dgvDT.Columns["Tên Phòng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Phòng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Số Ký Điện Sử Dụng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Điện"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Số Khối Nước Sử Dụng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Nước"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Dịch Vụ"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Doanh Thu"].DefaultCellStyle.Format = "N0";

        }
        private void ThongKeDichVuThang(int thang, int nam, string khuvuc)
        {
            DataTable dt = bll.ThongKeDichVuTheoThangVaKhuVuc(thang,nam,khuvuc);
            dgvDT.DataSource = null;
            dgvDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDT.DataSource = dt;
            dgvDT.Columns["Số Lần Sử Dụng"].DefaultCellStyle.Format = "N0";
            dgvDT.Columns["Tổng Tiền Dịch Vụ"].DefaultCellStyle.Format = "N0";
            
        }
        private void ThongKeDichVuNam(int nam, string khuvuc)
        {
            DataTable dt = bll.ThongKeDichVuTheoNamVaKhuVuc(nam,khuvuc);
            dgvDT.DataSource = null;
            dgvDT.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDT.DataSource = dt;
            dgvDT.Columns["Tổng Tiền Dịch Vụ"].DefaultCellStyle.Format = "N0";
        }

    }
}
