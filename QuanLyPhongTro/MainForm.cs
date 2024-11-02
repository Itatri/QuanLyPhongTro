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
        private string id;
        private string region;
        private string makhuvuc;

        public MainForm(string id, string region, string makhuvuc)
        {
            //InitializeComponent();
            //this.id = id;
            //this.region = region;

            //// Hiển thị trang chủ và truyền id, region
            //var trangchuControl = new MainForm_TrangChu();
            //trangchuControl.SetUserInfo(id, region); // Truyền id và region vào trang chủ
            //var thongtinAdmin = new ThongTinAdmin();
            //thongtinAdmin.SetUserInfo(id, region); // Truyền id và region

            //ShowControl(trangchuControl); // Hiển thị trang chủ

            InitializeComponent();
            this.id = id;
            this.region = region;
            this.makhuvuc = makhuvuc;

            // Hiển thị trang chủ và truyền id, region, makhuvuc
            //var trangchuControl = new MainForm_TrangChu();
            var trangchuControl = new MainForm2();

            trangchuControl.SetUserInfo(id, region, makhuvuc); // Truyền id, region và makhuvuc vào trang chủ
            var thongtinAdmin = new ThongTinAdmin();
            thongtinAdmin.SetUserInfo(id, region); // Truyền id và region

            ShowControl(trangchuControl); // Hiển thị trang chủ


        }


        public void ShowControl(System.Windows.Forms.Control control)
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

        private void pictureBoxLogout_Click(object sender, EventArgs e)
        {

        }

        private void buttonTrangChu_Click(object sender, EventArgs e)
        {
            // Khi nhấn nút Trang Chủ, truyền id và region vào trang chủ
            //var trangchuControl = new MainForm_TrangChu();
            var trangchuControl = new MainForm2();

            trangchuControl.SetUserInfo(id, region, makhuvuc); // Truyền id và region vào trang chủ
            ShowControl(trangchuControl);


        }

        private void buttonTaiKhoan_Click(object sender, EventArgs e)
        {
            var thongtinAdmin = new ThongTinAdmin();
            thongtinAdmin.SetUserInfo(id, region);
            ShowControl(thongtinAdmin);
        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
