using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using DAL;

namespace QuanLyPhongTro.Control
{
    public partial class TaoQuanLyPhong1 : UserControl
    {
        //private DataGridView dataGridViewDichVu;

        private TaoQuanLyPhongBLL phongBLL = new TaoQuanLyPhongBLL();
        private TaoQuanLyPhongDAL phongDAL = new TaoQuanLyPhongDAL(); // Khai báo phongDAL



        public string makhuvuctaophong { get; set; }


        public TaoQuanLyPhong1()
        {
            InitializeComponent();
            phongDAL = new TaoQuanLyPhongDAL(); // Initialize DAL
            phongBLL = new TaoQuanLyPhongBLL();
            this.makhuvuctaophong = region; // Set the region
            SetNewMaPhong(); // Tạo mã phòng mới ngay khi khởi tạo

        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            AnHienButton(false);
            AnHienTextBox(false);
            flag = 3;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AnHienTextBox(true);
            AnHienButton(false);
            flag = 2;

        }

        private string region;

        public void Set(string region)
        {
            this.region = region;
        }

        int flag = 0;
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            SetNewMaPhong();
            txtTenPhong.Enabled = true;
            AnHienTextBox(true);
            AnHienButton(false);
            flag = 1;

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLiPhong f = new QuanLiPhong();
                f.khuvuc = makhuvuctaophong;
                mainForm.ShowControl(f);
            }
        }

        private bool ValidateServiceInputs()
        {
            if (
            string.IsNullOrWhiteSpace(txtSoNuoc.Text) ||
            string.IsNullOrWhiteSpace(txtSodien.Text) ||
            string.IsNullOrWhiteSpace(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return false;
            }

            // Validate numeric inputs
            if (/*!float.TryParse(txtTienCoc.Text, out float tienCoc) || tienCoc < 0 ||*/
                !float.TryParse(txtSodien.Text, out float dien) || dien < 0 ||
                !float.TryParse(txtSoNuoc.Text, out float nuoc) || nuoc < 0)
            {
                MessageBox.Show("Vui lòng nhập số dương cho các trường tiền cọc, điện, nước.");
                return false;
            }
            return true;
        }

        private void RefreshDataGridView()
        {
            var phongs = phongBLL.LayDanhSachPhong();
            dataGridView1.DataSource = phongs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn cột "MaPhong"
            if (dataGridView1.Columns["MaPhong"] != null)
            {
                dataGridView1.Columns["MaPhong"].Visible = false;
            }
        }




        private void SetNewMaPhong()
        {
            string newMaPhong = phongBLL.GetNewMaPhong();
            txtMaPhong.Text = newMaPhong;
        }


        private void txtSodien_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = txtSodien.SelectionStart;
            int selectionLength = txtSodien.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtSodien.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtSodien.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtSodien.SelectionStart = Math.Max(0, selectionStart + txtSodien.Text.Length - text.Length);
                txtSodien.SelectionLength = selectionLength;
            }

        }

        private void txtSoNuoc_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = txtSoNuoc.SelectionStart;
            int selectionLength = txtSoNuoc.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtSoNuoc.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtSoNuoc.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtSoNuoc.SelectionStart = Math.Max(0, selectionStart + txtSoNuoc.Text.Length - text.Length);
                txtSoNuoc.SelectionLength = selectionLength;
            }

        }


        private void TaoQuanLyPhong_Load(object sender, EventArgs e)
        {
            txtMaPhong.Visible = false;
            labelMaPhong.Visible = false;
            // Gán giá trị tên phòng bằng mã phòng cho minh dễ quản lý người dùng dễ sử dụng
            //txtTenPhong.Text = txtMaPhong.Text;
            //txtTenPhong.Enabled = ;

            RefreshDataGridView();
            AnHienTextBox(false);
            AnHienButton(true);
            txtTenPhong.Enabled = false;

            SetNewMaPhong();


            LoadDichVu();


            dataGridView1.Columns["TienCoc"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["TienPhong"].DefaultCellStyle.Format = "N0";

            dataGridView1.Columns["Nuoc"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Dien"].DefaultCellStyle.Format = "N0";


            dataGridViewDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";

        }


        private void AnHienButton(bool t)
        {
            buttonLuu.Enabled = !t;
            btnDangKy.Enabled = t;
            //btnXoa.Enabled = t;
            //btnUpdate.Enabled = t;
        }


        private void AnHienTextBox(bool t)
        {
            txtSodien.Enabled = t;
            txtTenPhong.Enabled = t;
            textBoxDienTich.Enabled = t;
            textBoxTienCoc.Enabled = t;
            textBoxTienPhong.Enabled = t;
            txtSoNuoc.Enabled = t;
            //txtTenPhong.Enabled = !t;
            RtxtGhiChu.Enabled = t;

        }



        private void buttonLuu_Click(object sender, EventArgs e)
        {
            txtMaPhong.Enabled = false;
            txtMaPhong.Enabled = false;



            if (flag == 1)
            {
                //DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm dịch vụ này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.No)
                //{
                //    return;
                //}

                if (!ValidateServiceInputs())
                {
                    return;
                }

                try
                {
                    var phong = new TaoQuanLyPhongDTO
                    {
                        MaPhong = txtMaPhong.Text,
                        TenPhong = txtTenPhong.Text,
                        DienTich = float.TryParse(textBoxDienTich.Text, out float dientich) ? dientich : 0,
                        TienPhong = float.TryParse(textBoxTienPhong.Text, out float tienphong) ? tienphong : 0,
                        Dien = float.TryParse(txtSodien.Text, out float dien) ? dien : 0,
                        Nuoc = float.TryParse(txtSoNuoc.Text, out float nuoc) ? nuoc : 0,
                        TienCoc = float.TryParse(textBoxTienCoc.Text, out float tienCoc) ? tienCoc : 0,
                        GhiChu = RtxtGhiChu.Text,
                        MaKhuVuc = makhuvuctaophong // Ensure makhuvuc is valid
                    };

                    bool insertResult = phongBLL.InsertPhong(phong);
                    //MessageBox.Show(insertResult ? "Thêm phòng thành công!" : "Thêm phòng thất bại!");

                    if (insertResult)
                    {
                        // Chuyển đổi danh sách dịch vụ thành DataTable
                        DataTable dtDichVuPhong = chuyendoidichvu();

                        // Gọi phương thức insertDichVuPhong trong BLL
                        bool insertDichVuResult = phongBLL.InsertDichVuPhong(dtDichVuPhong);

                        if (insertDichVuResult)
                        {
                            //MessageBox.Show("Thành công");
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Thêm dịch vụ thất bại!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }

            }
            if (flag == 3)
            {

                if (string.IsNullOrWhiteSpace(txtMaPhong.Text))
                {
                    MessageBox.Show("Vui lòng chọn phòng để xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string maPhong = txtMaPhong.Text;
                    bool isDeleted = phongBLL.DeletePhong(maPhong);
                    MessageBox.Show(isDeleted ? "Xóa phòng thành công" : "Xóa phòng thất bại");
                    if (isDeleted)
                    {
                        RefreshDataGridView(); // Cập nhật lại dữ liệu trong DataGridView
                        txtMaPhong.Text = phongBLL.GetNewMaPhong(); // Tạo mã phòng mới
                    }
                }
            }

            txtSoNuoc.Clear();
            txtSodien.Clear();
            textBoxTienPhong.Clear();
            textBoxTienCoc.Clear();
            textBoxDienTich.Clear();
            RtxtGhiChu.Clear();
            AnHienButton(true);
            txtTenPhong.Clear();


            AnHienTextBox(false);
            RtxtGhiChu.Enabled = false;


            // Xóa toàn bộ dịch vụ trong dataGridViewDichVu
            dataGridViewDichVu.Rows.Clear();
            LoadDichVu();

        }


        private void textBoxTienPhong_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = textBoxTienPhong.SelectionStart;
            int selectionLength = textBoxTienPhong.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = textBoxTienPhong.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                textBoxTienPhong.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                textBoxTienPhong.SelectionStart = Math.Max(0, selectionStart + textBoxTienPhong.Text.Length - text.Length);
                textBoxTienPhong.SelectionLength = selectionLength;
            }
        }

        private void textBoxTienCoc_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = textBoxTienCoc.SelectionStart;
            int selectionLength = textBoxTienCoc.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = textBoxTienCoc.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                textBoxTienCoc.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                textBoxTienCoc.SelectionStart = Math.Max(0, selectionStart + textBoxTienCoc.Text.Length - text.Length);
                textBoxTienCoc.SelectionLength = selectionLength;
            }
        }







        private void LoadDichVu()
        {
            // Giả sử bạn đã định nghĩa lớp BLL và DAL cho dịch vụ
            var dichVuBLL = new QuanLyDichVuBLL();
            DataTable dichVuData = dichVuBLL.GetDichVuFormQLPhong(); // Lấy tất cả dịch vụ từ BLL

            foreach (DataRow item in dichVuData.Rows)
            {
                dataGridViewDichVu.Rows.Add
                    (1, item["TenDichVu"], item["DonGia"], item["MaDichVu"]);
            }
        }



        //private List<DichVuPhongDTO> chuyendoidichvu()
        //{
        //    List<DichVuPhongDTO> lst = new List<DichVuPhongDTO>();

        //    // Duyệt qua tất cả các dòng trong DataGridView
        //    foreach (DataGridViewRow row in dataGridViewDichVu.Rows)
        //    {
        //        // Bỏ qua dòng mới
        //        if (row.IsNewRow)
        //            continue;

        //        // Kiểm tra ô checkbox "chon" có được chọn không
        //        if (Convert.ToBoolean(row.Cells["chon"].Value))
        //        {
        //            // Kiểm tra các ô trong dòng có dữ liệu không
        //            if (row.Cells["MaDichVu"].Value != null && row.Cells["MaDichVu"].Value.ToString() != "")
        //            {
        //                DichVuPhongDTO dichVuPhong = new DichVuPhongDTO
        //                {
        //                    maphong = txtMaPhong.Text,
        //                    madichvu = row.Cells["MaDichVu"].Value.ToString()
        //                };

        //                lst.Add(dichVuPhong);
        //            }
        //        }
        //    }

        //    return lst;
        //}

        private DataTable chuyendoidichvu()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaPhong", typeof(string));
            dt.Columns.Add("MaDichVu", typeof(string));

            // Duyệt qua tất cả các dòng trong DataGridView
            foreach (DataGridViewRow row in dataGridViewDichVu.Rows)
            {
                // Bỏ qua dòng mới
                if (row.IsNewRow)
                    continue;

                // Kiểm tra ô checkbox "chon" có được chọn không
                if (Convert.ToBoolean(row.Cells["chon"].Value))
                {
                    // Kiểm tra các ô trong dòng có dữ liệu không
                    if (row.Cells["MaDichVu"].Value != null && row.Cells["MaDichVu"].Value.ToString() != "")
                    {
                        DataRow dr = dt.NewRow();
                        dr["MaPhong"] = txtMaPhong.Text;
                        dr["MaDichVu"] = row.Cells["MaDichVu"].Value.ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void RtxtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePickerHanTro_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxTienPhong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtSodien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSoNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBoxTienCoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                txtMaPhong.Text = selectedRow.Cells["MaPhong"].Value?.ToString(); // Lấy giá trị từ cột "MaPhong"
                txtTenPhong.Text = selectedRow.Cells["TenPhong"].Value?.ToString();
                textBoxTienPhong.Text = selectedRow.Cells["TienPhong"].Value?.ToString();

                textBoxDienTich.Text = selectedRow.Cells["dientich"].Value?.ToString();

                txtSodien.Text = selectedRow.Cells["Dien"].Value?.ToString();
                txtSoNuoc.Text = selectedRow.Cells["Nuoc"].Value?.ToString();
                textBoxTienCoc.Text = selectedRow.Cells["TienCoc"].Value?.ToString();
                RtxtGhiChu.Text = selectedRow.Cells["GhiChu"].Value?.ToString();

                // Gọi phương thức LoadDichVuByMaPhong
                LoadDichVuByMaPhong(txtMaPhong.Text);
            }
        }








        private void LoadDichVuByMaPhong(string maPhong)
        {
            // Lấy tất cả dịch vụ
            DataTable allDichVuData = phongBLL.GetAllDichVu(); // Phương thức này sẽ được thêm vào BLL

            // Lấy các dịch vụ đã đăng ký cho mã phòng cụ thể
            DataTable registeredDichVuData = phongBLL.GetDichVuByMaPhong(maPhong);

            // Tạo danh sách để lưu các mã dịch vụ đã đăng ký
            HashSet<string> registeredDichVuMa = new HashSet<string>();
            foreach (DataRow row in registeredDichVuData.Rows)
            {
                registeredDichVuMa.Add(row["MaDichVu"].ToString());
            }

            // Xóa dữ liệu cũ trong dataGridViewDichVu
            dataGridViewDichVu.Rows.Clear();

            foreach (DataRow item in allDichVuData.Rows)
            {
                // Thêm hàng với checkbox
                int rowIndex = dataGridViewDichVu.Rows.Add();
                DataGridViewRow row = dataGridViewDichVu.Rows[rowIndex];

                // Đánh dấu checkbox nếu dịch vụ đã đăng ký
                row.Cells["chon"].Value = registeredDichVuMa.Contains(item["MaDichVu"].ToString());

                // Thêm các thông tin khác
                row.Cells["TenDichVu"].Value = item["TenDichVu"];
                row.Cells["DonGia"].Value = item["DonGia"];
                row.Cells["MaDichVu"].Value = item["MaDichVu"];
            }
        }

        private void textBoxDienTich_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
