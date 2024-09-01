using QuanLyPhongTro.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void ShowControl(System.Windows.Forms.Control control)
        {
            panelForm.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelForm.Controls.Add(control);
        }

        private void buttonQuanLyChungCu_Click(object sender, EventArgs e)
        {
            var chungCuControl = new QuanLiChungCuControl();
            ShowControl(chungCuControl);
        }

        private void buttonQuanLyPhong_Click(object sender, EventArgs e)
        {
            var phongControl = new QuanLiPhong();
            ShowControl(phongControl);
        }

        private void buttonQuanLyDanCu_Click(object sender, EventArgs e)
        {
            var danCuControl = new QuanLiDanCu();
            ShowControl(danCuControl);
        }

        private void buttonQuanLyDichVu_Click(object sender, EventArgs e)
        {
            var dichVuControl = new QuanLiDichVu();
            ShowControl(dichVuControl);
        }

        private void buttonQuanLyPhieuThu_Click(object sender, EventArgs e)
        {
            var phieuThuControl = new QuanLyPhieuThu();
            ShowControl(phieuThuControl);
        }

        private void buttonQuanLyThongKe_Click(object sender, EventArgs e)
        {
            var thongKeControl = new QuanLiThongKe();
            ShowControl(thongKeControl);
        }

        private void buttonQuanLyFeedBack_Click(object sender, EventArgs e)
        {
            var feedBackControl = new QuanLiFeedBack();
            ShowControl(feedBackControl);
        }

        private void buttonDangXuat_Click(object sender, EventArgs e)
        {
            DangNhap dangNhapForm = new DangNhap();
            dangNhapForm.Show();
            this.Hide(); // Ẩn MainForm
        }
    }
}
