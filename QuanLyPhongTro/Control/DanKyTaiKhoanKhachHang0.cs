using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BLL;
using DTO;
using System.Configuration.Abstractions;
using System.Configuration;
using ConfigManager = System.Configuration.ConfigurationManager;





namespace QuanLyPhongTro.Control
{
    public partial class DanKyTaiKhoanKhachHang : UserControl
    {
        private DanKyTaiKhoanKH_BLL bll = new DanKyTaiKhoanKH_BLL();

        public string makhuvuc { get; set; }

        private string connectionString = ConfigManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        private Dictionary<string, string> tenPhongToMaPhong = new Dictionary<string, string>();


        int flag = 0;
        
        public DanKyTaiKhoanKhachHang()
        {
            InitializeComponent();
        }


        private void DanKyTaiKhoanKhachHang_Load(object sender, EventArgs e)
        {


            LoadTenPhongComboBox();
            LoadUserPhongData();
            AnHienTextBox(false);
            AnHienButton(true);

    

            //------------22/10/2024
            // Thiết lập định dạng ngày tháng cho DateTimePicker
            dtpkNgayBatDau.Format = DateTimePickerFormat.Custom;
            dtpkNgayBatDau.CustomFormat = "dd/MM/yyyy"; // Định dạng ngày tháng theo chuẩn Việt Nam

        }

        private void LoadTenPhongComboBox()
        {
            try
            {
                DataTable dataTable = bll.GetPhongData();

                cbbTenPhong.Items.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    string tenPhong = row["TenPhong"].ToString();
                    string maPhong = row["MaPhong"].ToString();

                    // Tạo một ComboBoxItem và lưu MaPhong trong Tag
                    ComboBoxItem item = new ComboBoxItem
                    {
                        Text = tenPhong,
                        Tag = maPhong
                    };

                    cbbTenPhong.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Tag { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void UpdateTextFields()
        {
            if (cbbTenPhong.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cbbTenPhong.SelectedItem;
                string maPhong = selectedItem.Tag.ToString();
                string ab = maPhong + dtpkNgayBatDau.Value.ToString("ddMMyy");

                txtID.Text = ab;
                txtMatKhau.Text = ab; // Set password to be the same as ID
            }
        }




        private void LoadUserPhongData()
        {
            //DataTable dataTable = bll.GetUserPhongData(); // Lấy dữ liệu từ cơ sở dữ liệu

            //// Thêm cột "NgayThang" tạm thời vào DataTable nếu chưa có
            //if (!dataTable.Columns.Contains("NgayThang"))
            //{
            //    dataTable.Columns.Add("NgayThang", typeof(DateTime));
            //}

            //// Nếu bạn cần gán giá trị mặc định cho cột này, có thể làm như sau:
            //foreach (DataRow row in dataTable.Rows)
            //{
            //    if (row["NgayThang"] == DBNull.Value)
            //    {
            //        row["NgayThang"] = DateTime.Now; // Hoặc giá trị bạn muốn
            //    }
            //}

            //dataGridView1.DataSource = dataTable; // Gán DataTable cho DataGridView

            //// Ẩn cột "NgayThang" không cho hiển thị
            //if (dataGridView1.Columns["NgayThang"] != null)
            //{
            //    dataGridView1.Columns["NgayThang"].Visible = false;
            //}

            DataTable dataTable = bll.GetUserPhongData(); // Lấy dữ liệu từ cơ sở dữ liệu

            // Thêm cột "NgayThang" tạm thời vào DataTable nếu chưa có
            if (!dataTable.Columns.Contains("NgayThang"))
            {
                dataTable.Columns.Add("NgayThang", typeof(DateTime));
            }

            // Nếu bạn cần gán giá trị mặc định cho cột này, có thể làm như sau:
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["NgayThang"] == DBNull.Value)
                {
                    row["NgayThang"] = DateTime.Now; // Hoặc giá trị bạn muốn
                }
            }

            dataGridView1.DataSource = dataTable; // Gán DataTable cho DataGridView

            // Ẩn cột "NgayThang" không cho hiển thị
            if (dataGridView1.Columns["NgayThang"] != null)
            {
                dataGridView1.Columns["NgayThang"].Visible = false;
            }

            // Đảm bảo cột "TÊN PHÒNG" là cột đầu tiên
            if (dataGridView1.Columns["TÊN PHÒNG"] != null)
            {
                dataGridView1.Columns["TÊN PHÒNG"].DisplayIndex = 0;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            flag = 1;
            LoadTenPhongComboBox();
            AnHienTextBox(true);
            AnHienButton(false);
            dataGridView1.Enabled = false;
           
        }

        private void UpdateNgayVao(string MaPhong, DateTime ngayvao)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string query = "UPDATE Phong SET NgayVao = @ngayvao, TrangThai = 1 WHERE MaPhong = @MaPhong";

                string query = "UPDATE Phong SET NgayVao = @ngayvao WHERE MaPhong = @MaPhong";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ngayvao", ngayvao);
                command.Parameters.AddWithValue("@MaPhong", MaPhong); // Chuyển đổi thành string

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật ngày vào cho phòng thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng với mã số đã chọn.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật ngày vào: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AnHienTextBox(true);
            AnHienButton(false);
            flag = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            AnHienButton(false);
            AnHienTextBox(false);
            flag = 3;

        }


        private void cbbTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFields();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng chỉ số hàng hợp lệ
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Hiển thị dữ liệu trong các textbox
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtMatKhau.Text = row.Cells["Mật khẩu"].Value.ToString();
                cbbTenPhong.Text = row.Cells["TÊN PHÒNG"].Value.ToString();
            }
            btnThem.Enabled = false;
            buttonHuy.Enabled = true;
            buttonSua.Enabled = true;
        }

        private void dtpkNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
          
            // Chỉ cập nhật nếu có một hàng hiện tại được chọn trong DataGridView
            if (dataGridView1.CurrentRow != null)
            {
                // Gán giá trị DateTime từ DateTimePicker vào DataGridView
                dataGridView1.CurrentRow.Cells["NgayThang"].Value = dtpkNgayBatDau.Value;

                // Cập nhật dữ liệu trong DataTable liên kết với DataGridView
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                if (dataTable != null && dataGridView1.CurrentRow.Index < dataTable.Rows.Count)
                {
                    dataTable.Rows[dataGridView1.CurrentRow.Index]["NgayThang"] = dtpkNgayBatDau.Value;
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLiPhong f = new QuanLiPhong();
                f.khuvuc = makhuvuc;
                mainForm.ShowControl(f);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLiPhong f = new QuanLiPhong();
                f.khuvuc = makhuvuc;
                mainForm.ShowControl(f);
            }
        }
        // Tìm kiếm tài khoản phòng
        private void textBoxTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemTaiKhoanPhong();
        }

        private void TimKiemTaiKhoanPhong()
        {
            string keyword = textBoxTimKiem.Text.Trim();
            DataTable result = bll.TimTaiKhoanPhong(keyword);

            if (result.Rows.Count > 0)
            {
                dataGridView1.DataSource = result;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Không tìm thấy dịch vụ nào với từ khóa này.");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (cbbTenPhong.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng và trạng thái.");
                    return;
                }

                // Lấy mã phòng từ Tag của item đã chọn
                ComboBoxItem selectedItem = (ComboBoxItem)cbbTenPhong.SelectedItem;
                string selectedMaPhong = selectedItem.Tag.ToString();
                if (!bll.PhongExists(selectedMaPhong))
                {
                    MessageBox.Show("Phòng được chọn không tồn tại trong bảng Phòng.");
                    return;
                }

                string ab = selectedMaPhong + dtpkNgayBatDau.Value.ToString("ddMMyy");
                string id = ab;
                string password = id; // Set password to be the same as ID

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm bản ghi này không?", "Xác nhận bổ sung", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DanKyTaiKhoanKH_DTO userPhong = new DanKyTaiKhoanKH_DTO
                    {
                        ID = id,
                        MatKhau = password,
                        MaPhong = selectedMaPhong,
                        NgayCapNhat = dtpkNgayBatDau.Value // Lấy giá trị từ DateTimePicker
                    };

                    try
                    {
                        if (bll.InsertUserPhong(userPhong))
                        {
                            UpdateNgayVao(selectedMaPhong, dtpkNgayBatDau.Value);
                            LoadUserPhongData();
                            MessageBox.Show("Đã thêm bản ghi thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Mã phòng đã tồn tại. Vui lòng chọn mã phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }




            if (flag == 2)
            {
                string accountId = txtID.Text;
                string newPassword = txtMatKhau.Text;

                if (string.IsNullOrWhiteSpace(accountId) || string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật mật khẩu này không?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (bll.UpdatePassword(accountId, newPassword))
                    {
                        //MessageBox.Show("Cập nhật mật khẩu thành công.");
                        // Cập nhật lại DataGridView nếu cần
                        LoadUserPhongData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật mật khẩu thất bại.");
                    }
                }
            }

            if (flag == 3)
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn bản ghi để xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (bll.DeleteUserPhong(txtID.Text))
                        {
                            LoadUserPhongData();
                            MessageBox.Show("Đã xóa bản ghi thành công.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            // Sau khi lưu, thiết lập định dạng hiển thị của dtpkNgayBatDau
            dtpkNgayBatDau.Format = DateTimePickerFormat.Custom;
            dtpkNgayBatDau.CustomFormat = "dd/MM/yyyy";


            LoadTenPhongComboBox();
            LoadUserPhongData();
            AnHienTextBox(false);
            AnHienButton(true);
            txtID.Clear();
            txtMatKhau.Clear();
        
            dataGridView1.Enabled = true;
        }

        private void textBoxTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Ngăn không cho tiếng bíp phát ra khi nhấn Enter
                TimKiemTaiKhoanPhong();
            }
        }

        //(1) dựa vào java viết ra
        private void AnHienButton(bool t)
        {

            btnLuu.Enabled = !t;
            buttonSua.Enabled = t;
            btnThem.Enabled = t;
        }

        private void AnHienTextBox(bool t)
        {
            dtpkNgayBatDau.Enabled = t;
            cbbTenPhong.Enabled = t;
            txtID.Enabled = t;
            txtMatKhau.Enabled = t;

        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng để cập nhật thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AnHienTextBox(false);
            txtID.Enabled = false;
            txtMatKhau.Enabled= true;
            AnHienButton(false);

            dataGridView1.Enabled = false;
            flag = 2;

        }

        private void buttonHuy_Click(object sender, EventArgs e)
        {
            //cbbTenPhong.Items.Clear();
            txtID.Clear();
            txtMatKhau.Clear();
            
            AnHienButton(true);
            AnHienTextBox(false);
            LoadUserPhongData();
            dataGridView1.Enabled = true;

        }

        private void dtpkNgayBatDau_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateTextFields();
        }
    }
}
