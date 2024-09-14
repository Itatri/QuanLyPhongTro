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
                string query = "SELECT COUNT(*) FROM DangNhap WHERE ID = @ID AND PassWord = @Password AND MaKhuVuc = @MaKhuVuc AND TrangThai = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@MaKhuVuc", region);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //string id = txtTaiKhoan.Text;
            //string password = txtMatKhau.Text;
            //string region = cbbChungCu.SelectedItem?.ToString();

            //if (string.IsNullOrEmpty(region))
            //{
            //    MessageBox.Show("Vui lòng chọn khu vực!");
            //    return;
            //}

            //if (ValidateLogin(id, password, region))
            //{
            //    // Đăng nhập thành công, mở form chính
            //    MessageBox.Show("Đăng nhập thành công!");

            //    // Gọi OpenUserControl1 với biến region
            //    OpenUserControl1(region);
            //}
            //else
            //{
            //    MessageBox.Show("Sai tên đăng nhập, mật khẩu hoặc khu vực!");
            //}

            string id = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            string region = cbbChungCu.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(region))
            {
                MessageBox.Show("Vui lòng chọn khu vực!");
                return;
            }

            if (ValidateLogin(id, password, region))
            {
                // Đăng nhập thành công, mở form chính
                MessageBox.Show("Đăng nhập thành công!");

                // Tạo và mở instance của MainForm
                MainForm mainForm = new MainForm();
                mainForm.SetKhuVuc(region);
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
            TaoQuanLyPhong userControl = new TaoQuanLyPhong();
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

        }

        private void cbbChungCu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
