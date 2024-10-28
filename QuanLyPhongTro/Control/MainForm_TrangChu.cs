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
        private TaoQuanLyPhong1 controltaoQuanLyPhong1;
        private QuanLiPhong controlquanLiPhong;
        private QuanLiDichVu controlquanLiDichVu;
        private QuanLyPhieuThu controlquanlyphieuthu;
        private string makhuvuc; // Khai báo biến lưu thông tin makhuvuc
        private string id;
        private KhuVucBLL khuVucBLL;
        private ThongTinAdminBLL thongTinAdminBLL;
        public MainForm_TrangChu()
        {
            InitializeComponent();
            controlQuanLyDanCu = new QuanLiDanCu();
            controlQuanLyDanCu.Dock = DockStyle.Fill; // Đặt control hiển thị chiếm toàn bộ 

            controlQuanLyPhanHoi = new QuanLiFeedBack();
            controlQuanLyPhanHoi.Dock = DockStyle.Fill; // Đặt control hiển thị chiếm toàn bộ panel

            controltaoQuanLyPhong1 = new TaoQuanLyPhong1();
            controltaoQuanLyPhong1.Dock = DockStyle.Fill;

            controlquanLiPhong = new QuanLiPhong();
            controlquanLiPhong.Dock = DockStyle.Fill;

            controlquanLiDichVu = new QuanLiDichVu();
            controlquanLiDichVu.Dock = DockStyle.Fill;

            controlquanlyphieuthu = new QuanLyPhieuThu();
            controlquanlyphieuthu.Dock = DockStyle.Fill;

            khuVucBLL = new KhuVucBLL(); // Khởi tạo BLL để gọi DAL
            thongTinAdminBLL = new ThongTinAdminBLL();

          
        }


        public void SetUserInfo(string id, string region, string makhuvuc)
        {
            lbTaiKhoan.Text = id;
            this.id = id;
            this.makhuvuc = makhuvuc; // Gán giá trị makhuvuc vào biến lưu
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

        private void btnQuanLyPhong_Click(object sender, EventArgs e)
        {
            //this.Controls.Clear(); // Xóa các control hiện có
            //controlquanLiPhong.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            //controlquanLiPhong.id = id;
            //this.Controls.Add(controlquanLiPhong); // Thêm control QuanLyPhong

            this.Controls.Clear(); // Xóa các control hiện có
            controlquanLiPhong.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            this.Controls.Add(controlquanLiPhong); // Thêm control QuanLyPhong
        }

        private void btnQLHopDong_Click(object sender, EventArgs e)
        {

        }

        private void btnQLDichVu_Click(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            //controlquanLiDichVu.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            this.Controls.Add(controlquanLiDichVu); // Thêm control QuanLyPhong
        }

        private void btnQLGiaoDich_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            controlquanlyphieuthu.khuvuc = makhuvuc;
            this.Controls.Add(controlquanlyphieuthu); 
        }
    }
}
