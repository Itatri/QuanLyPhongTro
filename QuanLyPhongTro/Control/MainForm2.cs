using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace QuanLyPhongTro.Control
{
    public partial class MainForm2 : UserControl
    {
        private QuanLiDanCu controlQuanLyDanCu;
        private QuanLiFeedBack controlQuanLyPhanHoi;
        private TaoQuanLyPhong1 controltaoQuanLyPhong1;
        private QuanLiPhong controlquanLiPhong;
        private QuanLiDichVu controlquanLiDichVu;
        private QuanLyPhieuThu controlquanlyphieuthu;
        private QuanLiThongKe controlthongke;
        private string makhuvuc; // Khai báo biến lưu thông tin makhuvuc
        private string id;
        private KhuVucBLL khuVucBLL;
        private ThongTinAdminBLL thongTinAdminBLL;
        public MainForm2()
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

            controlthongke = new QuanLiThongKe();
            controlthongke.Dock = DockStyle.Fill;

            khuVucBLL = new KhuVucBLL(); // Khởi tạo BLL để gọi DAL
            thongTinAdminBLL = new ThongTinAdminBLL();


            //this.Load += MainForm2_Load;

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

        private void btnQuanLyPhong_Click(object sender, EventArgs e)
        {

            this.Controls.Clear(); // Xóa các control hiện có
            controlquanLiPhong.id = id;
            controlquanLiPhong.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            this.Controls.Add(controlquanLiPhong); // Thêm control QuanLyPhong
        }


        private void btnQLGiaoDich_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            controlquanlyphieuthu.khuvuc = makhuvuc;
            this.Controls.Add(controlquanlyphieuthu);
        }

        private void btnLuuTru1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuanLyPhong_Click_1(object sender, EventArgs e)
        {

            this.Controls.Clear(); // Xóa các control hiện có
            controlquanLiPhong.id = id;
            controlquanLiPhong.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            this.Controls.Add(controlquanLiPhong); // Thêm control QuanLyPhong

        }

        private void btnQLDanCu_Click_1(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            controlQuanLyDanCu.khuvuc = makhuvuc;
            this.Controls.Add(controlQuanLyDanCu); // Thêm control QuanLyDanCu


        }

        

        private void btnQLGiaoDich_Click_1(object sender, EventArgs e)
        {
            this.Controls.Clear();
            controlquanlyphieuthu.khuvuc = makhuvuc;
            this.Controls.Add(controlquanlyphieuthu);
        }

        private void btnQLPhanHoi_Click_1(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            this.Controls.Add(controlQuanLyPhanHoi); // Thêm control QuanLyDanCu
        }

        private void btnQLHopDong1_Click(object sender, EventArgs e)
        {

        }

        private void btnQLHopDong_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuTru_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuTru_Paint(object sender, PaintEventArgs e)
        {
            //Button btn = sender as Button;
            //if (btn != null)
            //{
            //    int radius = 20; // Bán kính của góc bo tròn
            //    Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
            //    GraphicsPath path = new GraphicsPath();
            //    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            //    path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            //    path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            //    path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            //    path.CloseFigure();

            //    btn.Region = new Region(path);

            //    // Vẽ nền cho button
            //    using (SolidBrush brush = new SolidBrush(btn.BackColor))
            //    {
            //        e.Graphics.FillPath(brush, path);
            //    }

            //    // Vẽ viền cho button (nếu muốn)
            //    using (Pen pen = new Pen(Color.Black, 1))
            //    {
            //        e.Graphics.DrawPath(pen, path);
            //    }

            //    // Vẽ text của button
            //    TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, rect, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            //}
        }

        private void MainForm2_Load(object sender, EventArgs e)
        {
            //btnLuuTru.Paint += btnLuuTru_Paint;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnQLDichVu_Click_1(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            //controlquanLiDichVu.khuvuc = makhuvuc; // Truyền thông tin khu vực từ biến makhuvuc
            this.Controls.Add(controlquanLiDichVu); // Thêm control QuanLyPhong
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            controlthongke.khuvuc = makhuvuc;
            this.Controls.Add(controlthongke); // Thêm control QuanLyPhong
        }
    }
}
