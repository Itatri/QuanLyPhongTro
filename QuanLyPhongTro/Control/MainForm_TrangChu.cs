using BLL;
using DTO;
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
    public partial class MainForm_TrangChu : UserControl
    {
        private QuanLiDanCu controlQuanLyDanCu;
        private QuanLiFeedBack controlQuanLyPhanHoi;
        private KhuVucBLL khuVucBLL;
        private ThongTinAdminBLL thongTinAdminBLL;
        public MainForm_TrangChu()
        {
            InitializeComponent();
            controlQuanLyDanCu = new QuanLiDanCu();
            controlQuanLyDanCu.Dock = DockStyle.Fill; // Đặt control hiển thị chiếm toàn bộ 

            controlQuanLyPhanHoi = new QuanLiFeedBack();
            controlQuanLyPhanHoi.Dock = DockStyle.Fill; // Đặt control hiển thị chiếm toàn bộ panel

            khuVucBLL = new KhuVucBLL(); // Khởi tạo BLL để gọi DAL
            thongTinAdminBLL = new ThongTinAdminBLL();
        }
        // Phương thức để nhận id và region và cập nhật label
        ////public void SetUserInfo(string id, string region)
        ////{
        ////    lbTaiKhoan.Text = id; 
        ////    // Lấy thông tin khu vực từ maKhuVuc và hiển thị lên labelKhuVuc
        ////    KhuVucDTO khuVuc = khuVucBLL.GetKhuVucByMaKhuVuc(region);
        ////    if (khuVuc != null)
        ////    {
        ////        labelKhuVuc.Text = khuVuc.TenKhuVuc; // Hiển thị tên khu vực
        ////    }
        ////    else
        ////    {
        ////        labelKhuVuc.Text = "Khu vực không tồn tại"; // Xử lý trường hợp không tìm thấy
        ////    }

        ////    ThongTinAdminDTO thongTinAdmin = thongTinAdminBLL.LayThongTinAdminTheoIdUser(id);
        ////    if (thongTinAdmin != null)
        ////    {
        ////        labelTaiKhoanXC.Text = thongTinAdmin.HoTen;
        ////    }
        ////    else
        ////    {
        ////        labelTaiKhoanXC.Text = "Tài khoản không tồn tại";
        ////    }
        ////}
        // Phương thức để nhận id và region và cập nhật label
        // Phương thức để nhận id và region và cập nhật label
        public void SetUserInfo(string id, string region)
        {
            lbTaiKhoan.Text = id;

            // Lấy thông tin khu vực từ maKhuVuc và hiển thị lên labelKhuVuc
            KhuVucDTO khuVuc = khuVucBLL.GetKhuVucByMaKhuVuc(region);
            if (khuVuc != null)
            {
                labelKhuVuc.Text = khuVuc.TenKhuVuc; // Hiển thị tên khu vực
            }
            else
            {
                labelKhuVuc.Text = "Khu vực không tồn tại"; // Xử lý trường hợp không tìm thấy
            }

            // Lấy thông tin admin từ bảng ThongTinAdmin theo IdUser
            ThongTinAdminDTO thongTinAdmin = thongTinAdminBLL.LayThongTinAdminTheoIdUser(id);
            if (thongTinAdmin != null)
            {
                labelTaiKhoanXC.Text = thongTinAdmin.HoTen; // Nếu có thông tin thì hiển thị họ tên
            }
            else
            {
                labelTaiKhoanXC.Text = "Chưa cập nhật"; // Nếu không có thông tin thì hiển thị "Chưa cập nhật"
            }
        }


        private void btnQLDanCu_Click(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            this.Controls.Add(controlQuanLyDanCu); // Thêm control QuanLyDanCu

        }

        private void btnQLPhanHoi_Click(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            this.Controls.Add(controlQuanLyPhanHoi); // Thêm control QuanLyDanCu
        }

        private void MainForm_TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void btnLuuTru_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
