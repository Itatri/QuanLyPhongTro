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
        //List<ThongTinDichVuDTO> lstDV;
        //ThongTinDichVuDTO DVtam;
        //List<ThongTinDichVuDTO> lst;

        public string makhuvuc { get; set; }


        public TaoQuanLyPhong1()
        {
            InitializeComponent();
            phongDAL = new TaoQuanLyPhongDAL(); // Initialize DAL
            phongBLL = new TaoQuanLyPhongBLL();
            this.makhuvuc = region; // Set the region
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
            txtTenPhong.Enabled = false;
            AnHienTextBox(true);
            AnHienButton(false);
            flag = 1;

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
            dataGridView1.DataSource  =    phongs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


     

        private void SetNewMaPhong()
        {
            string newMaPhong = phongBLL.GetNewMaPhong();
            txtMaPhong.Text = newMaPhong;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                txtMaPhong.Text = selectedRow.Cells["MaPhong"].Value?.ToString();

                txtTenPhong.Text = selectedRow.Cells["TenPhong"].Value?.ToString();

                if (selectedRow.Cells["NgayVao"].Value != null)
                {
                    if (DateTime.TryParse(selectedRow.Cells["NgayVao"].Value.ToString(), out DateTime ngayVao))
                    {
                    }
                    else
                    {
                    }
                }

                txtSodien.Text = selectedRow.Cells["Dien"].Value?.ToString();
                txtSoNuoc.Text = selectedRow.Cells["Nuoc"].Value?.ToString();
                RtxtGhiChu.Text = selectedRow.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void txtSodien_TextChanged(object sender, EventArgs e)
        {
            //if (!int.TryParse(txtSodien.Text, out _))
            //{
            //    MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtSodien.Text = "";
            //}
        }

        private void txtSoNuoc_TextChanged(object sender, EventArgs e)
        {
            //if (!int.TryParse(txtSoNuoc.Text, out _))
            //{
            //    MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtSoNuoc.Text = "";
            //}
        }


        private void TaoQuanLyPhong_Load(object sender, EventArgs e)
        {
            txtMaPhong.Visible = false;
            labelMaPhong.Visible=false;
            // Gán giá trị tên phòng bằng mã phòng cho minh dễ quản lý người dùng dễ sử dụng
            txtTenPhong.Text = txtMaPhong.Text;
            txtTenPhong.Enabled = false;

            RefreshDataGridView();
            AnHienTextBox(false);
            AnHienButton(true);
            txtTenPhong.Enabled=false;
            SetNewMaPhong();


            LoadDichVu();
           
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
            //txtTienphong.en
            textBoxTienCoc.Enabled = t;
            textBoxTienPhong.Enabled = t;
            txtSoNuoc.Enabled = t;
            txtTenPhong.Enabled = !t;
            RtxtGhiChu.Enabled = t;

        }



        private void buttonLuu_Click(object sender, EventArgs e)
        {
            txtMaPhong.Enabled = false;



            //if (flag == 1)
            //{

            //    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm dịch vụ này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (dialogResult == DialogResult.No)
            //    {
            //        return;
            //    }

            //    if (!ValidateServiceInputs())
            //    {
            //        return;
            //    }

            //    try
            //    {
            //        var phong = new TaoQuanLyPhongDTO
            //        {
            //            MaPhong = txtMaPhong.Text,
            //            TenPhong = txtTenPhong.Text,
            //            TienPhong = textBoxTienPhong.Text,
            //            Dien = float.TryParse(txtSodien.Text, out float dien) ? dien : 0,
            //            Nuoc = float.TryParse(txtSoNuoc.Text, out float nuoc) ? nuoc : 0,
            //            TienCoc = float.TryParse(textBoxTienCoc.Text, out float tienCoc) ? tienCoc : 0,
            //            HanTro = dateTimePickerHanTro.Value,
            //            GhiChu = RtxtGhiChu.Text,
            //            MaKhuVuc = this.makhuvuc // Assign the region value
            //        };

            //        bool insertResult = phongBLL.InsertPhong(phong);
            //        MessageBox.Show(insertResult ? "Thêm phòng thành công!" : "Thêm phòng thất bại!");
            //        if (insertResult)
            //        {
            //            // Lưu thông tin dịch vụ vào DichVuPhong
            //            foreach (var dichVu in lstDV) // lstDV là danh sách dịch vụ đã chọn
            //            {
            //                phongDAL.InsertDichVuPhong(phong.MaPhong, dichVu.MaDichVu);
            //            }
            //            RefreshDataGridView();
            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            //    }
            //}

            txtMaPhong.Enabled = false;

            if (flag == 1)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thêm dịch vụ này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                if (!ValidateServiceInputs())
                {
                    return;
                }

                try
                {

                    MessageBox.Show(makhuvuc);
                    var phong = new TaoQuanLyPhongDTO
                    {
                        MaPhong = txtMaPhong.Text,
                        TenPhong = txtTenPhong.Text,
                        TienPhong = textBoxTienPhong.Text,
                        Dien = float.TryParse(txtSodien.Text, out float dien) ? dien : 0,
                        Nuoc = float.TryParse(txtSoNuoc.Text, out float nuoc) ? nuoc : 0,
                        TienCoc = float.TryParse(textBoxTienCoc.Text, out float tienCoc) ? tienCoc : 0,
                        //HanTro = dateTimePickerHanTro.Value,
                        GhiChu = RtxtGhiChu.Text,
                        MaKhuVuc = makhuvuc // Ensure makhuvuc is valid
                    };

                    bool insertResult = phongBLL.InsertPhong(phong);
                    MessageBox.Show(insertResult ? "Thêm phòng thành công!" : "Thêm phòng thất bại!");
                    bool a2 = phongBLL.ínertDichVuPhong(chuyendoidichvu());
                    

                    if (insertResult && a2 )
                    {
                        MessageBox.Show("Thành công");
                        //// Lưu thông tin dịch vụ vào DichVuPhong
                        ////foreach (var dichVu in lstDV) // Ensure lstDV is not null and has values
                        ////{
                        //    phongDAL.InsertDichVuPhong(phong.MaPhong, dichVu.MaDichVu);
                        //}
                        RefreshDataGridView();
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
            txtSodien.Clear ();
            textBoxTienPhong.Clear();
            textBoxTienCoc.Clear();
            AnHienButton(true);
            //AnHienTextBox(false);

            
        }


        private void textBoxTienPhong_TextChanged(object sender, EventArgs e)
        {


            //string input = textBoxTienPhong.Text;
            //if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^[0-9]*$"))
            //{
            //    MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    textBoxTienPhong.Text = ""; // Reset text
            //}


        }

        private void textBoxTienCoc_TextChanged(object sender, EventArgs e)
        {

            //string input = textBoxTienCoc.Text;
            //if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^[0-9]*$"))
            //{
            //    MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    textBoxTienCoc.Text = ""; // Reset text
            //}


        }

      




        //if (flag == 2)
        //{
        //    //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thay đổi thông tin phòng này?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    //if (result == DialogResult.No)
        //    //{
        //    //    return;
        //    //}

        //    //TaoQuanLyPhongDTO phong = new TaoQuanLyPhongDTO
        //    //{
        //    //    MaPhong = txtMaPhong.Text,
        //    //    TenPhong = txtTenPhong.Text,
        //    //    Dien = float.Parse(txtSodien.Text),
        //    //    Nuoc = float.Parse(txtSoNuoc.Text),
        //    //    GhiChu = RtxtGhiChu.Text,
        //    //    //NgayVao = dtpNgayVao.Value,
        //    //    //HanTro = dtpHanTro.Value,
        //    //    //TrangThai = chkTrangThai.Checked
        //    //};

        //    //bool success = phongBLL.UpdatePhong(phong);
        //    //if (success)
        //    //{
        //    //    MessageBox.Show("Cập nhật phòng thành công.");
        //    //    RefreshDataGridView();
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Cập nhật phòng thất bại.");
        //    //}
        //}

        //if (flag == 3)
        //{

        //    //if (string.IsNullOrWhiteSpace(txtMaPhong.Text))
        //    //{
        //    //    MessageBox.Show("Vui lòng chọn phòng để xóa.");
        //    //    return;
        //    //}

        //    //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //    //if (result == DialogResult.Yes)
        //    //{
        //    //    string maPhong = txtMaPhong.Text;
        //    //    bool isDeleted = phongBLL.DeletePhong(maPhong);
        //    //    MessageBox.Show(isDeleted ? "Xóa phòng thành công" : "Xóa phòng thất bại");
        //    //    if (isDeleted)
        //    //    {
        //    //        RefreshDataGridView(); // Cập nhật lại dữ liệu trong DataGridView
        //    //        txtMaPhong.Text = phongBLL.GetNewMaPhong(); // Tạo mã phòng mới
        //    //    }
        //    //}
        //}


        //------------------------------

        private void LoadDichVu()
        {
            // Giả sử bạn đã định nghĩa lớp BLL và DAL cho dịch vụ
            var dichVuBLL = new QuanLyDichVuBLL();
            DataTable dichVuData = dichVuBLL.GetDichVuFormQLPhong(); // Lấy tất cả dịch vụ từ BLL

            foreach ( DataRow item in dichVuData.Rows)
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
        //       // Bỏ qua dòng mới
        //        if (row.IsNewRow)
        //            continue;

        //        // Kiểm tra các ô trong dòng có dữ liệu không
        //        if (row.Cells["chon"].Value == true)
        //        {
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

        private List<DichVuPhongDTO> chuyendoidichvu()
        {
            List<DichVuPhongDTO> lst = new List<DichVuPhongDTO>();

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
                        DichVuPhongDTO dichVuPhong = new DichVuPhongDTO
                        {
                            maphong = txtMaPhong.Text,
                            madichvu = row.Cells["MaDichVu"].Value.ToString()
                        };

                        lst.Add(dichVuPhong);
                    }
                }
            }

            return lst;
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
            // Kiểm tra nếu ký tự không phải là chữ số hoặc không phải là ký tự điều khiển (như backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;
            }
        }

        private void txtSodien_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là chữ số hoặc không phải là ký tự điều khiển (như backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;
            }
        }

        private void txtSoNuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là chữ số hoặc không phải là ký tự điều khiển (như backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;
            }
        }

        private void textBoxTienCoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là chữ số hoặc không phải là ký tự điều khiển (như backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;
            }
        }
    }
}
