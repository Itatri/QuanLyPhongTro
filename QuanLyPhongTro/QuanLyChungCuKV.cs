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
    public partial class QuanLyChungCuKV : Form
    {
        int flag = 0;
        // Lấy chuỗi kết nối từ tệp App.config
        private string chuoiketnoi = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        public QuanLyChungCuKV()
        {
            InitializeComponent();
            CenterToScreen();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cài đặt chế độ tự động điều chỉnh độ rộng cột

            // Thêm lựa chọn vào ComboBox trang thái
            comboBoxTrangThai.Items.Add("Đang hoạt động");
            comboBoxTrangThai.Items.Add("Không hoạt động");

            comboBoxTrangThai.SelectedIndex = 1; // Đặt "Không hoạt động" làm trạng thái mặc định
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            // Ẩn hiển textbox
            textBoxMaChungCu.Enabled = true;
            textBoxTenChungCu.Enabled = true;
            comboBoxTrangThai.Enabled = true;

            //Ẩn hiển button
            buttonLuu.Enabled = true;
            //buttonXoa.Enabled = false;
            buttonCapNhat.Enabled = false;
            buttonThem.Enabled = false;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBoxMaChungCu.Text = selectedRow.Cells["MaKhuVuc"].Value.ToString();
                textBoxTenChungCu.Text = selectedRow.Cells["TenKhuVuc"].Value.ToString();

                // Kiểm tra và gán giá trị của ComboBox TrangThai
                bool trangThai = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);
                comboBoxTrangThai.SelectedItem = trangThai ? "Đang hoạt động" : "Không hoạt động";
            }
        }

        private void QuanLyChungCuKV_Load(object sender, EventArgs e)
        {

            LoadKhuVucData(); // Gọi phương thức để tải dữ liệu khi form tải

            // Ẩn hiển textbox
            textBoxMaChungCu.Enabled = false;
            textBoxTenChungCu.Enabled = false;
            comboBoxTrangThai.Enabled = false;

            // Ẩn hiện button 
            buttonThem.Enabled = true;
            //buttonXoa.Enabled = true;
            buttonCapNhat.Enabled = true;
            buttonTaiKhoan.Enabled = true;
            buttonLuu.Enabled = false;

            // Hiển thị form toàn màn hình
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
        }

        private void LoadKhuVucData()
        {
            //using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            //{
            //    connection.Open();

            //    // Truy vấn để lấy thông tin khu vực, số lượng phòng và số người trong khu vực
            //    string query = @"
            //SELECT kv.MaKhuVuc, 
            //       kv.TenKhuVuc, 
            //       COUNT(DISTINCT p.MaPhong) AS SoLuongPhong,  -- Đếm số phòng trong khu vực
            //       COUNT(DISTINCT ttk.MaKhachTro) AS SoNguoiO,   -- Đếm số người ở trong khu vực
            //       kv.TrangThai
            //FROM KhuVuc kv
            //LEFT JOIN Phong p ON kv.MaKhuVuc = p.MaKhuVuc
            //LEFT JOIN ThongTinKhach ttk ON p.MaPhong = ttk.MaPhong
            //GROUP BY kv.MaKhuVuc, kv.TenKhuVuc, kv.TrangThai";

            //    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            //    DataTable dataTable = new DataTable();
            //    adapter.Fill(dataTable);

            //    // Gán dữ liệu vào DataGridView
            //    dataGridView1.DataSource = dataTable;

            //    // Đặt tên hiển thị cho các cột mới
            //    dataGridView1.Columns["MaKhuVuc"].HeaderText = "Mã khu vực";
            //    dataGridView1.Columns["TenKhuVuc"].HeaderText = "Tên khu vực";
            //    dataGridView1.Columns["SoLuongPhong"].HeaderText = "Số phòng";
            //    dataGridView1.Columns["SoNguoiO"].HeaderText = "Số người";
            //    dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";

            //    // Sắp xếp lại thứ tự cột
            //    dataGridView1.Columns["MaKhuVuc"].DisplayIndex = 0; // Mã khu vực
            //    dataGridView1.Columns["TenKhuVuc"].DisplayIndex = 1; // Tên khu vực
            //    dataGridView1.Columns["SoLuongPhong"].DisplayIndex = 2; // Số phòng
            //    dataGridView1.Columns["SoNguoiO"].DisplayIndex = 3; // Số người
            //    dataGridView1.Columns["TrangThai"].DisplayIndex = 4; // Trạng thái
            //}

            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            {
                connection.Open();

                // Truy vấn để lấy thông tin khu vực, số lượng phòng và số người trong khu vực
                string query = @"
    SELECT kv.MaKhuVuc, 
           kv.TenKhuVuc, 
           COUNT(DISTINCT p.MaPhong) AS SoLuongPhong,  -- Đếm số phòng trong khu vực
           COUNT(DISTINCT ttk.MaKhachTro) AS SoNguoiO,   -- Đếm số người ở trong khu vực
           kv.TrangThai
    FROM KhuVuc kv
    LEFT JOIN Phong p ON kv.MaKhuVuc = p.MaKhuVuc
    LEFT JOIN ThongTinKhach ttk ON p.MaPhong = ttk.MaPhong
    GROUP BY kv.MaKhuVuc, kv.TenKhuVuc, kv.TrangThai";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Gán dữ liệu vào DataGridView
                dataGridView1.DataSource = dataTable;

                // Đặt tên hiển thị cho các cột mới
                dataGridView1.Columns["MaKhuVuc"].HeaderText = "Mã chung cư";
                dataGridView1.Columns["TenKhuVuc"].HeaderText = "Tên chung cư";
                dataGridView1.Columns["SoLuongPhong"].HeaderText = "Số phòng";
                dataGridView1.Columns["SoNguoiO"].HeaderText = "Số người";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";

                // Sắp xếp lại thứ tự cột
                dataGridView1.Columns["MaKhuVuc"].DisplayIndex = 0; // Mã chung cư
                dataGridView1.Columns["TenKhuVuc"].DisplayIndex = 1; // Tên chung cư
                dataGridView1.Columns["SoLuongPhong"].DisplayIndex = 2; // Số phòng
                dataGridView1.Columns["SoNguoiO"].DisplayIndex = 3; // Số người
                dataGridView1.Columns["TrangThai"].DisplayIndex = 4; // Trạng thái
            }
        }

        private void textBoxMaChungCu_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTenChungCu_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            flag = 3;

            //Ẩn hiển button
            buttonLuu.Enabled = true;

        }

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa có dòng nào được chọn trong DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khu vực để cập nhật thông tin.");
                return;
            }

            flag = 2;
            // Ẩn hiển textbox
            textBoxMaChungCu.Enabled = false;
            textBoxTenChungCu.Enabled = true;
            comboBoxTrangThai.Enabled = true;


            //Ẩn hiển button
            buttonLuu.Enabled = true;
            buttonThem.Enabled = false;
            //buttonXoa.Enabled = false;
            buttonCapNhat.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Khởi tạo form TaiKhoanKhuVuc
            TaiKhoanDangNhap taiKhoanForm = new TaiKhoanDangNhap();

            // Hiển thị form dưới dạng không modal (người dùng có thể tương tác với các form khác)
            taiKhoanForm.Show();
            this.Hide(); // Ẩn MainForm
            // Nếu muốn hiển thị form dưới dạng modal (người dùng không thể tương tác với form khác)
            // taiKhoanForm.ShowDialog();
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                // Kiểm tra xem các trường dữ liệu có bị trống không
                if (string.IsNullOrEmpty(textBoxMaChungCu.Text) || string.IsNullOrEmpty(textBoxTenChungCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                // Lấy giá trị từ các điều khiển trên form
                string maKhuVuc = textBoxMaChungCu.Text;
                string tenKhuVuc = textBoxTenChungCu.Text;
                bool trangThai = comboBoxTrangThai.SelectedItem.ToString() == "Đang hoạt động";

                // Kết nối cơ sở dữ liệu và thực hiện truy vấn chèn
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    try
                    {
                        connection.Open();
                        string query = "INSERT INTO KhuVuc (MaKhuVuc, TenKhuVuc, TrangThai) VALUES (@MaKhuVuc, @TenKhuVuc, @TrangThai)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Thêm tham số vào câu lệnh SQL để tránh SQL Injection
                            command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                            command.Parameters.AddWithValue("@TenKhuVuc", tenKhuVuc);
                            command.Parameters.AddWithValue("@TrangThai", trangThai);

                            // Thực thi câu lệnh
                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Thêm khu vực thành công.");
                                LoadKhuVucData(); // Tải lại dữ liệu để cập nhật DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm khu vực.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

            if (flag == 2)
            {
                // Kiểm tra nếu mã khu vực hoặc tên khu vực trống
                if (string.IsNullOrEmpty(textBoxMaChungCu.Text) || string.IsNullOrEmpty(textBoxTenChungCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mã khu vực và tên khu vực.");
                    return;
                }

                string maKhuVuc = textBoxMaChungCu.Text;
                string tenKhuVuc = textBoxTenChungCu.Text;
                bool trangThai = comboBoxTrangThai.SelectedItem.ToString() == "Đang hoạt động";

                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE KhuVuc SET TenKhuVuc = @TenKhuVuc, TrangThai = @TrangThai WHERE MaKhuVuc = @MaKhuVuc";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                            command.Parameters.AddWithValue("@TenKhuVuc", tenKhuVuc);
                            command.Parameters.AddWithValue("@TrangThai", trangThai);

                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Cập nhật khu vực thành công.");
                                LoadKhuVucData(); // Tải lại dữ liệu để cập nhật DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khu vực để cập nhật.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

            if (flag == 3)
            {

                // Kiểm tra nếu mã khu vực trống
                if (string.IsNullOrEmpty(textBoxMaChungCu.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã khu vực cần xóa.");
                    return;
                }

                string maKhuVuc = textBoxMaChungCu.Text;

                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);

                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Xóa khu vực thành công.");
                                LoadKhuVucData(); // Tải lại dữ liệu để cập nhật DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy khu vực để xóa.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

            textBoxMaChungCu.Clear();
            textBoxTenChungCu.Clear();


            // Ẩn hiện button 
            buttonThem.Enabled = true;
            //buttonXoa.Enabled = true;
            buttonCapNhat.Enabled = true;
            buttonTaiKhoan.Enabled = true;
            buttonLuu.Enabled = false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadKhuVucData(); // Gọi phương thức để tải dữ liệu khi form tải
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoadKhuVucData(); // Gọi phương thức để tải dữ liệu khi form tải
        }
    }
}
