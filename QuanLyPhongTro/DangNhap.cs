using QuanLyPhongTro.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;




namespace QuanLyPhongTro
{
    public partial class DangNhap : Form
    {
        SqlConnection ketnoi;
        SqlCommand thuchien;
        SqlDataReader DocDuLieu;

        // Lấy chuỗi kết nối từ tệp App.config
        private string chuoiketnoi = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        string lenh;


        public DangNhap()
        {
            InitializeComponent();
            cbbChungCu.DropDownStyle = ComboBoxStyle.DropDownList;
            //cbbChungCu.BackColor = Color.LightBlue; // Đặt màu nền
            //cbbChungCu.ForeColor = Color.Aqua; // Đặt màu chữ
            txtTaiKhoan.MaxLength = 15;
            txtMatKhau.MaxLength = 15;

            LoadComboBoxKhuVuc();

        }

        private void LoadComboBoxKhuVuc()
        {
            ketnoi = new SqlConnection(chuoiketnoi);
            lenh = @"SELECT MaKhuVuc FROM DangNhap";
            ketnoi.Open();
            thuchien = new SqlCommand(lenh, ketnoi);
            DocDuLieu = thuchien.ExecuteReader();
            while (DocDuLieu.Read())
            {
                cbbChungCu.Items.Add(DocDuLieu[0].ToString());
            }
            ketnoi.Close();

        }

        private bool ValidateLogin(string id, string password, string region)
        {
            bool isValid = false;

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();
                string query = "SELECT ID, PassWord FROM DangNhap WHERE MaKhuVuc = @MaKhuVuc AND TrangThai = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaKhuVuc", region);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string dbID = reader["ID"].ToString();
                            string dbPassword = reader["PassWord"].ToString();

                            // So sánh ID và Password với sự phân biệt chữ hoa chữ thường
                            if (string.Equals(dbID, id, StringComparison.Ordinal) && string.Equals(dbPassword, password, StringComparison.Ordinal))
                            {
                                isValid = true;
                                break; // Thoát khỏi vòng lặp khi tìm thấy một tài khoản hợp lệ
                            }
                        }
                    }
                }
            }
            return isValid;
        }
        private bool ValidateSpecialLogin(string id, string password)
        {
            

            // Giả sử cbbChungCu là tên của ComboBox
            if (string.Equals(id, "DN01", StringComparison.Ordinal) &&
                string.Equals(password, "pass123", StringComparison.Ordinal) &&
                string.IsNullOrEmpty(cbbChungCu.Text))
            {
                return true;
            }

            return false;


        }


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string makhuvuc = cbbChungCu.Text;
            string id = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            string region = cbbChungCu.SelectedItem?.ToString();

            if (ValidateSpecialLogin(id, password))
            {
                //// Đăng nhập đặc biệt thành công, mở QuanLiChungCuControl
                //QuanLiChungCuControl quanLiChungCuControl = new QuanLiChungCuControl();
                //Form quanLiChungCuForm = new Form
                //{
                //    Text = "Quản lý chung cư",
                //    Size = new Size(1200, 1200)
                //};
                //quanLiChungCuForm.Controls.Add(quanLiChungCuControl);
                //quanLiChungCuControl.Dock = DockStyle.Fill;
                //quanLiChungCuForm.Show();

                //// Ẩn form đăng nhập
                //this.Hide();
                // Đăng nhập đặc biệt thành công, mở form QuanLyChungCuKV
                QuanLyChungCuKV quanLyChungCuKV = new QuanLyChungCuKV();
                quanLyChungCuKV.Show();

                // Ẩn form đăng nhập
                this.Hide();

            }
            else if (!string.IsNullOrEmpty(region) && ValidateLogin(id, password, region))
            {
                // Đăng nhập thành công, mở MainForm và truyền id, region, makhuvuc
                MainForm mainForm = new MainForm(id, region, makhuvuc);
                mainForm.Show();

                // Ẩn form đăng nhập
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập, mật khẩu hoặc khu vực!");
            }
        }

        private void OpenUserControl1(string khuVuc)
        {
            TaoQuanLyPhong1 userControl = new TaoQuanLyPhong1();
            //userControl.SetKhuVuc(khuVuc); // Sử dụng biến khuVuc
            Form userControlForm = new Form
            {
                Text = "Quản lý phòng",
                Size = new Size(800, 600)
            };
            userControlForm.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
            userControlForm.Show();

        }

        private void cbbKhuVucChungCu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegion = cbbChungCu.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedRegion))
            {
                //taoQuanLyPhongForm.UpdateMaKhuVuc(selectedRegion);
            }

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.MaxLength = 20; // Giới hạn 20 ký tự
            txtTaiKhoan.KeyPress += new KeyPressEventHandler(txtTaiKhoan_KeyPress); // Đăng ký sự kiện KeyPress


            txtMatKhau.MaxLength = 20; // Giới hạn 20 ký tự
            txtMatKhau.KeyPress += new KeyPressEventHandler(txtMatKhau_KeyPress); // Đăng ký sự kiện KeyPress

        }

        private void cbbChungCu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDangNhap_Enter(object sender, EventArgs e)
        {
            //// Đổi màu khi button được chọn
            //btnDangNhap.BackColor = Color.BlueViolet; // Màu bạn muốn khi tab tới
        }

        private void btnDangNhap_Leave(object sender, EventArgs e)
        {
            //btnDangNhap.BackColor = Color.Red;
        }

        private void DangNhap_Load_1(object sender, EventArgs e)
        {
            //btnDangNhap.BackColor = Color.Red;
        }

        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Kiểm tra nếu phím Enter được nhấn
            {
                e.Handled = true; // Ngăn chặn Enter xuống dòng
            }

        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // Kiểm tra nếu phím Enter được nhấn
            {
                e.Handled = true; // Ngăn chặn Enter xuống dòng
            }

        }
    }
}
