using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
namespace QuanLyPhongTro
{
    public partial class TaiKhoanDangNhap : Form
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string chuoiketnoi = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        public TaiKhoanDangNhap()
        {
            InitializeComponent();
            CenterToScreen();
            dataGridViewTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Cài đặt chế độ tự động điều chỉnh độ rộng cột

        }

        int flag = 0;
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    // Thực hiện JOIN để lấy TenKhuVuc thay vì MaKhuVuc
                    string query = @"
                SELECT DangNhap.ID, DangNhap.PassWord, KhuVuc.TenKhuVuc, DangNhap.TrangThai
                FROM DangNhap
                INNER JOIN KhuVuc ON DangNhap.MaKhuVuc = KhuVuc.MaKhuVuc";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewTaiKhoan.DataSource = dt;

                        // Đổi tên tiêu đề cột
                        dataGridViewTaiKhoan.Columns["ID"].HeaderText = "Tài Khoản";
                        dataGridViewTaiKhoan.Columns["PassWord"].HeaderText = "Mật khẩu";
                        dataGridViewTaiKhoan.Columns["TenKhuVuc"].HeaderText = "Tên khu vực";
                        dataGridViewTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng thái";

                        // Căn giữa tiêu đề cột
                        dataGridViewTaiKhoan.Columns["ID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewTaiKhoan.Columns["PassWord"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewTaiKhoan.Columns["TenKhuVuc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridViewTaiKhoan.Columns["TrangThai"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        // Ẩn cột MaKhuVuc nếu vẫn tồn tại
                        if (dataGridViewTaiKhoan.Columns["MaKhuVuc"] != null)
                        {
                            dataGridViewTaiKhoan.Columns["MaKhuVuc"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadKhuVucComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                {
                    string query = "SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc WHERE TrangThai = 1"; // Chỉ lấy các khu vực có trạng thái hoạt động
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    comboBoxKhuVuc.DataSource = dt;
                    comboBoxKhuVuc.DisplayMember = "TenKhuVuc"; // Hiển thị tên khu vực trong ComboBox
                    comboBoxKhuVuc.ValueMember = "MaKhuVuc"; // Giá trị thực sự là mã khu vực
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khu vực: " + ex.Message);
            }
        }

        private void TaiKhoanDangNhap_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadKhuVucComboBox();


            // Ânr hiện textbox
            TextBoxTaiKhoan.Enabled = false;
            textBoxMatKhau.Enabled = false;
            comboBoxKhuVuc.Enabled = false;

            // ẩn hiên button
            buttonThem.Enabled = true;
            //buttonXoa.Enabled = true;
            buttonSua.Enabled = true;
            buttonLuu.Enabled = false;

            // Hiển thị form toàn màn hình
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
        }

        private void comboBoxKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xử lý sự kiện khi người dùng chọn tên khu vực
            string maKhuVuc = comboBoxKhuVuc.SelectedValue.ToString(); // Lấy mã khu vực khi chọn

        }


        private void TextBoxTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            flag = 1;

            // Ânr hiện textbox
            TextBoxTaiKhoan.Enabled = true;
            textBoxMatKhau.Enabled = true;
            comboBoxKhuVuc.Enabled = true;


            // ẩn hiên button
            buttonLuu.Enabled = true;
            buttonSua.Enabled = false;
            buttonThem.Enabled = false;
            //buttonXoa.Enabled = false;

            textBoxMatKhau.Clear();
            TextBoxTaiKhoan.Clear();

            //string taiKhoan = TextBoxTaiKhoan.Text.Trim();
            //string matKhau = textBoxMatKhau.Text.Trim();
            //string maKhuVuc = comboBoxKhuVuc.SelectedValue?.ToString();

            //if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(maKhuVuc))
            //{
            //    MessageBox.Show("Vui lòng nhập đủ thông tin tài khoản, mật khẩu và khu vực!");
            //    return;
            //}

            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(chuoiketnoi))
            //    {
            //        connection.Open();

            //        // Kiểm tra xem khu vực đã có tài khoản hay chưa
            //        string checkQuery = "SELECT COUNT(*) FROM DangNhap WHERE MaKhuVuc = @MaKhuVuc";
            //        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            //        {
            //            checkCommand.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
            //            int count = (int)checkCommand.ExecuteScalar();

            //            if (count > 0)
            //            {
            //                MessageBox.Show("Khu vực này đã có tài khoản. Vui lòng chọn khu vực khác!");
            //                return;
            //            }
            //        }

            //        // Thực hiện thêm tài khoản mới
            //        string query = "INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai) VALUES (@ID, @PassWord, @MaKhuVuc, @TrangThai)";
            //        using (SqlCommand command = new SqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("@ID", taiKhoan);
            //            command.Parameters.AddWithValue("@PassWord", matKhau);
            //            command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
            //            command.Parameters.AddWithValue("@TrangThai", 1);

            //            int rowsAffected = command.ExecuteNonQuery();
            //            if (rowsAffected > 0)
            //            {
            //                // Cập nhật trạng thái của khu vực thành 1
            //                string updateQuery = "UPDATE KhuVuc SET TrangThai = 1 WHERE MaKhuVuc = @MaKhuVuc";
            //                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
            //                {
            //                    updateCommand.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
            //                    updateCommand.ExecuteNonQuery();
            //                }

            //                MessageBox.Show("Thêm tài khoản thành công!");
            //                LoadData(); // Tải lại dữ liệu nếu cần thiết
            //                LoadKhuVucComboBox(); // Tải lại ComboBox khu vực để loại bỏ khu vực đã sử dụng
            //            }
            //            else
            //            {
            //                MessageBox.Show("Thêm tài khoản thất bại.");
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message);
            //}
        }


        //-----28/10/2024
        private void buttonXoa_Click(object sender, EventArgs e)
        {
            flag = 3;

            buttonLuu.Enabled = true;
            //buttonXoa.Enabled = false;
            buttonThem.Enabled = false;
            buttonSua.Enabled = false;


        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            flag = 2;
            // Ẩn hiển textbox
            textBoxMatKhau.Enabled = true;
            TextBoxTaiKhoan.Enabled = false;
            comboBoxKhuVuc.Enabled = false;

            //ẩn hiên button
            buttonLuu.Enabled = true;
            buttonThem.Enabled = false;
            buttonSua.Enabled = false;
            //buttonXoa.Enabled = false;

        }

        private void dataGridViewTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có chọn một dòng có dữ liệu không
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu của dòng đã chọn
                DataGridViewRow selectedRow = dataGridViewTaiKhoan.Rows[e.RowIndex];

                // Hiển thị ID tài khoản và mật khẩu lên các TextBox
                TextBoxTaiKhoan.Text = selectedRow.Cells["ID"].Value.ToString();
                textBoxMatKhau.Text = selectedRow.Cells["PassWord"].Value.ToString();

                // Hiển thị tên khu vực trong ComboBox dựa trên giá trị TenKhuVuc
                comboBoxKhuVuc.Text = selectedRow.Cells["TenKhuVuc"].Value.ToString();
            }
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {

                string taiKhoan = TextBoxTaiKhoan.Text.Trim();
                string matKhau = textBoxMatKhau.Text.Trim();
                string maKhuVuc = comboBoxKhuVuc.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(maKhuVuc))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin tài khoản, mật khẩu và khu vực!");
                    return;
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                    {
                        connection.Open();

                        // Kiểm tra xem khu vực đã có tài khoản hay chưa
                        string checkQuery = "SELECT COUNT(*) FROM DangNhap WHERE MaKhuVuc = @MaKhuVuc";
                        using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                            int count = (int)checkCommand.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Khu vực này đã có tài khoản. Vui lòng chọn khu vực khác!");
                                return;
                            }
                        }

                        // Thực hiện thêm tài khoản mới
                        string query = "INSERT INTO DangNhap (ID, PassWord, MaKhuVuc, TrangThai) VALUES (@ID, @PassWord, @MaKhuVuc, @TrangThai)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", taiKhoan);
                            command.Parameters.AddWithValue("@PassWord", matKhau);
                            command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                            command.Parameters.AddWithValue("@TrangThai", 1);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // Cập nhật trạng thái của khu vực thành 1
                                string updateQuery = "UPDATE KhuVuc SET TrangThai = 1 WHERE MaKhuVuc = @MaKhuVuc";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                                    updateCommand.ExecuteNonQuery();
                                }
                                string insertTTAdmin = "INSERT INTO ThongTinAdmin (MaAdmin, IdUser, TrangThai) VALUES (@MaAdmin, @IdUser, @TrangThai)";
                                using (SqlCommand insertCommand = new SqlCommand(insertTTAdmin, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@MaAdmin", taiKhoan);
                                    insertCommand.Parameters.AddWithValue("@IdUser", taiKhoan);
                                    insertCommand.Parameters.AddWithValue("@TrangThai", 1);
                                    insertCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Thêm tài khoản thành công!");
                                LoadData(); // Tải lại dữ liệu nếu cần thiết
                                LoadKhuVucComboBox(); // Tải lại ComboBox khu vực để loại bỏ khu vực đã sử dụng
                            }
                            else
                            {
                                MessageBox.Show("Thêm tài khoản thất bại.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message);
                }
            }

            if (flag == 2)
            {
                // Kiểm tra xem người dùng đã chọn một dòng trong DataGridView chưa
                if (dataGridViewTaiKhoan.SelectedRows.Count > 0)
                {
                    // Lấy ID của tài khoản được chọn và mật khẩu mới từ TextBox
                    string selectedID = dataGridViewTaiKhoan.SelectedRows[0].Cells["ID"].Value.ToString();
                    string matKhauMoi = textBoxMatKhau.Text.Trim();

                    // Kiểm tra mật khẩu mới có được nhập vào chưa
                    if (string.IsNullOrEmpty(matKhauMoi))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                        return;
                    }

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                        {
                            connection.Open();

                            // Cập nhật mật khẩu trong bảng DangNhap
                            string updateQuery = "UPDATE DangNhap SET PassWord = @PassWord WHERE ID = @ID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@PassWord", matKhauMoi);
                                updateCommand.Parameters.AddWithValue("@ID", selectedID);

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cập nhật mật khẩu thành công!");
                                    LoadData(); // Tải lại dữ liệu DataGridView sau khi cập nhật
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy tài khoản để cập nhật.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản để cập nhật mật khẩu.");
                }
            }

            if (flag == 3)
            {

                if (dataGridViewTaiKhoan.SelectedRows.Count > 0)
                {
                    // Lấy ID và TenKhuVuc của tài khoản đang chọn trong DataGridView
                    string selectedID = dataGridViewTaiKhoan.SelectedRows[0].Cells["ID"].Value.ToString();
                    string tenKhuVuc = dataGridViewTaiKhoan.SelectedRows[0].Cells["TenKhuVuc"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show(
                        "Bạn có chắc chắn muốn xóa tài khoản này?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(chuoiketnoi))
                            {
                                connection.Open();

                                // Xóa tài khoản từ bảng DangNhap
                                string deleteQuery = "DELETE FROM DangNhap WHERE ID = @ID";
                                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                                {
                                    deleteCommand.Parameters.AddWithValue("@ID", selectedID);
                                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        // Lấy MaKhuVuc từ TenKhuVuc
                                        string getMaKhuVucQuery = "SELECT MaKhuVuc FROM KhuVuc WHERE TenKhuVuc = @TenKhuVuc";
                                        using (SqlCommand getMaKhuVucCommand = new SqlCommand(getMaKhuVucQuery, connection))
                                        {
                                            getMaKhuVucCommand.Parameters.AddWithValue("@TenKhuVuc", tenKhuVuc);
                                            var maKhuVuc = getMaKhuVucCommand.ExecuteScalar()?.ToString();

                                            if (!string.IsNullOrEmpty(maKhuVuc))
                                            {
                                                // Cập nhật trạng thái khu vực thành 0
                                                string updateQuery = "UPDATE KhuVuc SET TrangThai = 0 WHERE MaKhuVuc = @MaKhuVuc";
                                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                                {
                                                    updateCommand.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                                                    updateCommand.ExecuteNonQuery();
                                                }

                                                MessageBox.Show("Xóa tài khoản và cập nhật trạng thái khu vực thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Không tìm thấy mã khu vực tương ứng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }

                                        LoadData(); // Tải lại dữ liệu để cập nhật DataGridView
                                    }
                                    else
                                    {
                                        MessageBox.Show("Không tìm thấy tài khoản để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // ẩn hiên button
            buttonThem.Enabled = true;
            //buttonXoa.Enabled = true;
            buttonSua.Enabled = true;
            buttonLuu.Enabled = false;

            // Ânr hiện textbox
            TextBoxTaiKhoan.Enabled = false;
            textBoxMatKhau.Enabled = false;
            comboBoxKhuVuc.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DangNhap dangNhapForm = new DangNhap();
            dangNhapForm.Show();
            this.Hide(); // Ẩn MainForm
        }

        private void dataGridViewTaiKhoan_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dataGridViewTaiKhoan.Rows[e.RowIndex];
            int trangThai = Convert.ToInt32(row.Cells["trangThai"].Value);

            if (trangThai == 0)
            {
                row.DefaultCellStyle.BackColor = Color.Gray;  // Màu nền cho dòng chưa thuê
            }
            else if (trangThai == 1)
            {
                row.DefaultCellStyle.BackColor = Color.Lavender;  // Màu nền cho dòng đã thuê
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
