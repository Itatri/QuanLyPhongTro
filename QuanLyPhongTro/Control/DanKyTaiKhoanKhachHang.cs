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




namespace QuanLyPhongTro.Control
{
    public partial class DanKyTaiKhoanKhachHang : UserControl
    {
        //private DanKyTaiKhoanKH_BLL bll = new DanKyTaiKhoanKH_BLL();

        //public DanKyTaiKhoanKhachHang()
        //{
        //    InitializeComponent();
        //    cbbTrangThai.Items.Add("0");
        //    cbbTrangThai.Items.Add("1");
        //    cbbTrangThai.SelectedIndex = 1; // Default value is 1
        //}

        //private void DanKyTaiKhoanKhachHang_Load(object sender, EventArgs e)
        //{
        //    LoadTenPhongComboBox();
        //    LoadUserPhongData();
        //}

        //private void LoadTenPhongComboBox()
        //{
        //    try
        //    {
        //        // Fetch the data using BLL method
        //        DataTable dataTable = bll.GetPhongData();

        //        // Clear existing items
        //        cbbTenPhong.Items.Clear();

        //        // Add items to ComboBox
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            string maPhong = row["MaPhong"].ToString();
        //            cbbTenPhong.Items.Add(maPhong);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}



        //private void LoadUserPhongData()
        //{
        //    try
        //    {
        //        DataTable dataTable = bll.GetUserPhongData();
        //        dataGridView1.DataSource = dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //private void btnThem_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtID.Text) ||
        //        string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
        //        cbbTenPhong.SelectedItem == null ||
        //        cbbTrangThai.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
        //        return;
        //    }

        //    string selectedPhong = cbbTenPhong.SelectedItem.ToString();
        //    if (!bll.PhongExists(selectedPhong))
        //    {
        //        MessageBox.Show("Phòng được chọn không tồn tại trong bảng Phong.");
        //        return;
        //    }

        //    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm bản ghi này không?", "Xác nhận bổ sung", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        DanKyTaiKhoanKH_DTO userPhong = new DanKyTaiKhoanKH_DTO
        //        {
        //            ID = txtID.Text,
        //            MatKhau = txtMatKhau.Text,
        //            MaPhong = selectedPhong,
        //            TrangThai = cbbTrangThai.SelectedIndex
        //        };

        //        try
        //        {
        //            if (bll.InsertUserPhong(userPhong))
        //            {
        //                LoadUserPhongData();
        //                MessageBox.Show("Đã thêm bản ghi thành công.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật hồ sơ này không?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (string.IsNullOrWhiteSpace(txtID.Text) ||
        //        string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
        //        cbbTenPhong.SelectedItem == null ||
        //        cbbTrangThai.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
        //        return;
        //    }

        //    DanKyTaiKhoanKH_DTO userPhong = new DanKyTaiKhoanKH_DTO
        //    {
        //        ID = txtID.Text,
        //        MatKhau = txtMatKhau.Text,
        //        MaPhong = cbbTenPhong.SelectedItem.ToString(),
        //        TrangThai = cbbTrangThai.SelectedIndex
        //    };

        //    try
        //    {
        //        if (bll.UpdateUserPhong(userPhong))
        //        {
        //            LoadUserPhongData();
        //            MessageBox.Show("Bản ghi đã được cập nhật thành công.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //private void btnXoa_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtID.Text))
        //    {
        //        MessageBox.Show("Vui lòng chọn bản ghi để xóa.");
        //        return;
        //    }

        //    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            if (bll.DeleteUserPhong(txtID.Text))
        //            {
        //                LoadUserPhongData();
        //                MessageBox.Show("Đã xóa bản ghi thành công.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

        //private void txtID_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void txtMatKhau_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //private void cbbTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}


        private DanKyTaiKhoanKH_BLL bll = new DanKyTaiKhoanKH_BLL();

        public DanKyTaiKhoanKhachHang()
        {
            InitializeComponent();
            cbbTrangThai.Items.Add("0");
            cbbTrangThai.Items.Add("1");
            cbbTrangThai.SelectedIndex = 1; // Default value is 1
        }

        private void DanKyTaiKhoanKhachHang_Load(object sender, EventArgs e)
        {
            LoadTenPhongComboBox();
            LoadUserPhongData();
        }

        private void LoadTenPhongComboBox()
        {
            try
            {
                // Fetch the data using BLL method
                DataTable dataTable = bll.GetPhongData();

                // Clear existing items
                cbbTenPhong.Items.Clear();

                // Add items to ComboBox
                foreach (DataRow row in dataTable.Rows)
                {
                    string maPhong = row["MaPhong"].ToString();
                    cbbTenPhong.Items.Add(maPhong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateTextFields()
        {
            if (cbbTenPhong.SelectedItem != null)
            {
                // Get selected room code
                string selectedPhong = cbbTenPhong.SelectedItem.ToString();
                // Generate ID from room code, current month, and year
                string id = $"{selectedPhong}{DateTime.Now.Month:D2}{DateTime.Now.Year}";

                // Update txtID and txtMatKhau
                txtID.Text = id;
                txtMatKhau.Text = id; // Set password to be the same as ID

                // Set default status
                cbbTrangThai.SelectedIndex = 1; // Default value is 1
            }
        }

        private void LoadUserPhongData()
        {
            try
            {
                DataTable dataTable = bll.GetUserPhongData();
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbTenPhong.SelectedItem == null ||
                cbbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng và trạng thái.");
                return;
            }

            string selectedPhong = cbbTenPhong.SelectedItem.ToString();
            if (!bll.PhongExists(selectedPhong))
            {
                MessageBox.Show("Phòng được chọn không tồn tại trong bảng Phong.");
                return;
            }

            // Generate ID from selected room, month, and year
            string id = $"{selectedPhong}{DateTime.Now.Month}{DateTime.Now.Year}";
            string password = id; // Set password to be the same as ID

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm bản ghi này không?", "Xác nhận bổ sung", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DanKyTaiKhoanKH_DTO userPhong = new DanKyTaiKhoanKH_DTO
                {
                    ID = id,
                    MatKhau = password,
                    MaPhong = selectedPhong,
                    TrangThai = cbbTrangThai.SelectedIndex
                };

                try
                {
                    if (bll.InsertUserPhong(userPhong))
                    {
                        LoadUserPhongData();
                        MessageBox.Show("Đã thêm bản ghi thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật hồ sơ này không?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                cbbTenPhong.SelectedItem == null ||
                cbbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            DanKyTaiKhoanKH_DTO userPhong = new DanKyTaiKhoanKH_DTO
            {
                ID = txtID.Text,
                MatKhau = txtMatKhau.Text,
                MaPhong = cbbTenPhong.SelectedItem.ToString(),
                TrangThai = cbbTrangThai.SelectedIndex
            };

            try
            {
                if (bll.UpdateUserPhong(userPhong))
                {
                    LoadUserPhongData();
                    MessageBox.Show("Bản ghi đã được cập nhật thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
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

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFields();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure that the row index is valid
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Display data in textboxes
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                cbbTenPhong.SelectedItem = row.Cells["MaPhong"].Value.ToString();
                cbbTrangThai.SelectedIndex = Convert.ToInt32(row.Cells["TrangThai"].Value);
            }
        }
    }
}
