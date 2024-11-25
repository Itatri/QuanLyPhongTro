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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DAL;
using DTO;
using BLL;


namespace QuanLyPhongTro.Control
{
    public partial class QuanLiDichVu : UserControl
    {
        private QuanLyDichVuBLL bll = new QuanLyDichVuBLL();


        public QuanLiDichVu()
        {
            InitializeComponent();
            InitializeComboBox();
        }
        private void InitializeComboBox()
        {
            if (!cbbTrangThai.Items.Contains("Đang hoat động"))
            {
                cbbTrangThai.Items.Add("Đang hoat động");
            }
            if (!cbbTrangThai.Items.Contains("Không hoạt động"))
            {
                cbbTrangThai.Items.Add("Không hoạt động");
            }
        }

        private void QuanLiDichVu_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
            AnHienTextBox(false);
            AnHienButton(true);
            txtMaDV.Enabled = (false);

            //-------------------------------------------------------------------------------- 15/10/2024
            // Định dạng cột DonGia hiển thị số kiểu tiền tệ
            dataGridView1.Columns["DonGia"].DefaultCellStyle.Format = "N0";

            //-------------------------------------------------------------------------------- 15/10/2024
        }

        private void RefreshDataGridView()
        {
           
            dataGridView1.DataSource = bll.GetAllServices();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Định dạng cột DonGia hiển thị số kiểu tiền tệ
            dataGridView1.Columns["DonGia"].DefaultCellStyle.Format = "N0";

            // Ẩn cột MaDichVu
            dataGridView1.Columns["MaDichVu"].Visible = false;

            // Đổi tên các cột thành tiếng Việt
            dataGridView1.Columns["MaDichVu"].HeaderText = "Mã Dịch Vụ";
            dataGridView1.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dataGridView1.Columns["DonGia"].HeaderText = "Đơn Giá";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            // Gọi phương thức sinh mã dịch vụ mới
            LoadNewServiceCode();
            // Hiển thị các TextBox và ẩn các nút không cần thiết
            AnHienTextBox(true);
            AnHienButton(false);
            // Thiết lập focus và xóa các TextBox
            txtMaDV.Focus();
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonGia.Clear();
            // Đặt giá trị mặc định cho ComboBox trạng thái
            cbbTrangThai.SelectedIndex = 1;
            // Đặt flag và sinh mã dịch vụ mới
            flag = 1;
            string MaDichVu = bll.GenerateNewServiceCode();
            txtMaDV.Text = MaDichVu; // Gán mã dịch vụ mới sinh vào TextBox txtMaDV

            // Đặt giá trị mặc định cho ComboBox trạng thái
            int index = cbbTrangThai.Items.IndexOf("Đang hoat động");
            if (index != -1)
            {
                cbbTrangThai.SelectedIndex = index; // Chọn "Đang hoat động"
            }
            else
            {
                // Nếu giá trị "Đang hoat động" không tồn tại trong danh sách, thêm nó vào và chọn nó
                if (!cbbTrangThai.Items.Contains("Đang hoat động"))
                {
                    cbbTrangThai.Items.Add("Đang hoat động");
                    cbbTrangThai.SelectedIndex = cbbTrangThai.Items.Count - 1; // Chọn phần tử cuối cùng (đã thêm)
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            AnHienButton(false);
            AnHienTextBox(false);
            flag = 3;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            AnHienTextBox(true);
            AnHienButton(false);
            cbbTrangThai.SelectedIndex = 1;
            flag = 2;
        }

        private bool ValidateServiceInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                string.IsNullOrWhiteSpace(cbbTrangThai.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin dịch vụ.");
                return false;
            }
            return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtMaDV.Text = selectedRow.Cells["MaDichVu"].Value.ToString();
                txtTenDV.Text = selectedRow.Cells["TenDichVu"].Value.ToString();

                //-------------------------------------------------------------------------------- 15/10/2024

                //txtDonGia.Text = selectedRow.Cells["DonGia"].Value.ToString();

                // Định dạng lại giá trị DonGia để hiển thị đúng trong txtDonGia
                decimal donGia = decimal.Parse(selectedRow.Cells["DonGia"].Value.ToString());
                txtDonGia.Text = string.Format("{0:N0}", donGia);
                //-------------------------------------------------------------------------------- 15/10/2024

                bool trangThai = bool.Parse(selectedRow.Cells["TrangThai"].Value.ToString());

                // Set the ComboBox selected item based on the TrangThai value
                if (trangThai)
                {
                    cbbTrangThai.SelectedItem = "Đang hoat động";
                }
                else
                {
                    cbbTrangThai.SelectedItem = "Không hoạt động";
                }
            }


        }

        // Hiển thị phan cấp giá tiền người dung nhập vào
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = txtDonGia.SelectionStart;
            int selectionLength = txtDonGia.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtDonGia.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtDonGia.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtDonGia.SelectionStart = Math.Max(0, selectionStart + txtDonGia.Text.Length - text.Length);
                txtDonGia.SelectionLength = selectionLength;
            }
        }



        private void LoadNewServiceCode()
        {
            txtMaDV.Text = bll.GenerateNewServiceCode();
        }

        private void txtMaDV_TextChanged(object sender, EventArgs e)
        {

        }

        int flag = 0;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            txtMaDV.Enabled = false;

            string tenDV = txtTenDV.Text.Trim(); // Lấy giá trị tên dịch vụ và loại bỏ khoảng trắng thừa
            if (string.IsNullOrWhiteSpace(tenDV))
            {
                MessageBox.Show("Tên dịch vụ là thông tin bắt buộc, vui lòng nhập thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng thực hiện phương thức nếu thông tin không hợp lệ
            }
            string Dongia = txtDonGia.Text; // Lấy giá trị tên dịch vụ và loại bỏ khoảng trắng thừa
            if (string.IsNullOrWhiteSpace(Dongia))
            {
                MessageBox.Show("Đơn giá là thông tin bắt buộc, vui lòng nhập thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng thực hiện phương thức nếu thông tin không hợp lệ
            }
            DTO.ThongTinDichVuDTO m = chuyendoi();
            if (flag == 1)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm dịch vụ này?", "Xác nhận thêm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }

                ThongTinDichVuDTO service = new ThongTinDichVuDTO
                {
                    MaDichVu = bll.GenerateNewServiceCode(),
                    TenDichVu = txtTenDV.Text,
                    DonGia = float.Parse(txtDonGia.Text),
                    TrangThai = cbbTrangThai.SelectedItem.ToString() == "Đang hoat động"
                };

                if (bll.AddService(service))
                {
                    MessageBox.Show("Thêm dịch vụ thành công");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Mã dịch vụ đã tồn tại.");
                }

            }

            if (flag == 2)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thay đổi dịch vụ này?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }

                ThongTinDichVuDTO service = new ThongTinDichVuDTO
                {
                    MaDichVu = txtMaDV.Text,
                    TenDichVu = txtTenDV.Text,
                    DonGia = float.Parse(txtDonGia.Text),
                    TrangThai = cbbTrangThai.SelectedItem.ToString() == "Đang hoat động"
                };

                if (bll.EditService(service))
                {
                    MessageBox.Show("Sửa dịch vụ thành công");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Sửa dịch vụ thất bại");
                    RefreshDataGridView();
                }

            }

            if (flag == 3)
            {


                if (string.IsNullOrWhiteSpace(txtMaDV.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã dịch vụ để xóa.");
                    return;
                }

                // Kiểm tra mã dịch vụ
                if (txtMaDV.Text == "DV0001")
                {
                    MessageBox.Show("Dịch vụ nước không thể xóa. Vui lòng sửa nếu cần.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }


                if (txtMaDV.Text == "DV0000")
                {
                    MessageBox.Show("Dịch vụ điện không thể xóa. Vui lòng sửa nếu cần.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }

                if (txtMaDV.Text == "DV0002")
                {
                    MessageBox.Show("Dịch vụ vệ sinh không thể xóa. Vui lòng sửa nếu cần.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    AnHienButton(true);
                    RefreshDataGridView();
                    return;
                }

                if (bll.RemoveService(txtMaDV.Text))
                {
                    MessageBox.Show("Xóa dịch vụ thành công");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Xóa dịch vụ thất bại");
                }
            }
            AnHienTextBox(false);
            AnHienButton(true);
            RefreshDataGridView();
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonGia.Clear();

            // Đặt giá trị mặc định cho ComboBox trạng thái
            int index = cbbTrangThai.Items.IndexOf("Đang hoat động");
            if (index != -1)
            {
                cbbTrangThai.SelectedIndex = index; // Chọn "Đang hoat động"
            }
            else
            {
                // Nếu giá trị "Đang hoat động" không tồn tại trong danh sách, thêm nó vào và chọn nó
                if (!cbbTrangThai.Items.Contains("Đang hoat động"))
                {
                    cbbTrangThai.Items.Add("Đang hoat động");
                    cbbTrangThai.SelectedIndex = cbbTrangThai.Items.Count - 1; // Chọn phần tử cuối cùng (đã thêm)
                }
            }



        }
        //(1) dựa vào java viết ra
        private void AnHienButton(bool t)
        {
            
            btnLuu.Enabled = !t;
            btnThem.Enabled = t;
            btnXoa.Enabled = t;
            btnSua.Enabled = t;
        }

        private void AnHienTextBox(bool t)
        {
            //txtMaDV.Enabled = !t;
            txtTenDV.Enabled = t;
            txtDonGia.Enabled = t;
            cbbTrangThai.Enabled = t;

        }

        private DTO.ThongTinDichVuDTO chuyendoi()
        {
            DTO.ThongTinDichVuDTO m = new DTO.ThongTinDichVuDTO();
            m.MaDichVu = txtMaDV.Text;
            m.TenDichVu = txtTenDV.Text;


            if (float.TryParse(txtDonGia.Text, out float donGia))
            {
                m.DonGia = donGia;
            }
            else
            {
                MessageBox.Show("Đơn giá không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            //m.TrangThai = cbbTrangThai.SelectedItem.ToString() == "1";

            return m;
        }


        private void textBoxTimDichVu_TextChanged(object sender, EventArgs e)
        {
            TimKiemDichVu();


        }



        private void TimKiemDichVu()
        {
            string keyword = textBoxTimDichVu.Text.Trim();
            DataTable result = bll.TimKiemDichVu(keyword);

            if (result.Rows.Count > 0)
            {
                dataGridView1.DataSource = result;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Không tìm thấy dịch vụ nào với từ khóa này.");
                RefreshDataGridView();
                textBoxTimDichVu.Clear();
            }
        }

        //-------------------------------------------------------------------------------- 15/10/2024
        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là chữ số hoặc không phải là ký tự điều khiển (như backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Ngăn không cho ký tự được nhập vào TextBox
                e.Handled = true;

                // Hiển thị thông báo cho người dùng biết phải nhập số
                MessageBox.Show("Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LoadDichVuSapXepTheoTrangThai()
        {
            try
            {
                DataTable dataTable = bll.SapXepDichVuTheoTrangThai();
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
        private void cbbSapXepDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

  
    }
}

