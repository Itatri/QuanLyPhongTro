using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLiDanCu : UserControl
    {
        private ThongTinKhachBLL thongTinKhachBLL = new ThongTinKhachBLL();
        public QuanLiDanCu()
        {
            InitializeComponent();
            LoadData();
            LoadPhongComboBox();
            // Đăng ký sự kiện CellClick cho DataGridView
            dataGridViewDanCu.CellClick += dataGridViewDanCu_CellClick;
            // Thiết lập chế độ hiển thị ảnh cho PictureBox
            pictureBoxAnhCuDan.SizeMode = PictureBoxSizeMode.Zoom;
            // Thiết lập trạng thái của các trường dữ liệu
            SetControlsEnabled(false);

            SetComboBoxGioiTinh();
            SetComboBoxTrangThai();
        }
        private void SetComboBoxTrangThai()
        {
            comboBoxTrangThai.Items.Clear();
            comboBoxTrangThai.Items.Add("Đang hoạt động"); // Chỉ mục 0
            comboBoxTrangThai.Items.Add("Ngưng hoạt động"); // Chỉ mục 1
        }

        private void LoadPhongComboBox()
        {
            ThongTinPhongBLL phongBLL = new ThongTinPhongBLL();
            DataTable dt = phongBLL.LayTatCaPhong();

            comboBoxPhong.DisplayMember = "TenPhong";
            comboBoxPhong.ValueMember = "MaPhong";

            var phongList = dt.AsEnumerable()
                              .Select(row => new
                              {
                                  MaPhong = row.Field<string>("MaPhong"),
                                  DisplayText = $"{row.Field<string>("MaPhong")} - {row.Field<string>("TenPhong")}"
                              }).ToList();

            comboBoxPhong.DataSource = phongList;
            comboBoxPhong.DisplayMember = "DisplayText";
            comboBoxPhong.ValueMember = "MaPhong";
        }

        private void SetComboBoxGioiTinh()
        {
            comboBoxGioiTinh.Items.Add("Nam");
            comboBoxGioiTinh.Items.Add("Nu");
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtMaCuDan.Enabled = enabled;
            txtHoTenCuDan.Enabled = enabled;
            comboBoxGioiTinh.Enabled = enabled;
            txtCCCD.Enabled = enabled;
            txtSDT.Enabled = enabled;
            txtQueQuan.Enabled = enabled;
            comboBoxTrangThai.Enabled = enabled;
            comboBoxPhong.Enabled = enabled;
            dateTimePickerNgaySinh.Enabled = enabled;
            buttonChonAnh.Enabled = enabled;
        }

        private void LoadData()
        {
            var danhSachKhach = thongTinKhachBLL.LayTatCaThongTinKhach();
            dataGridViewDanCu.DataSource = danhSachKhach;

            // Đặt lại tên các cột
            dataGridViewDanCu.Columns["MaKhachTro"].HeaderText = "Mã Khách Trọ";
            dataGridViewDanCu.Columns["HoTen"].HeaderText = "Họ Tên";
            dataGridViewDanCu.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridViewDanCu.Columns["CCCD"].HeaderText = "CCCD";
            dataGridViewDanCu.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dataGridViewDanCu.Columns["QueQuan"].HeaderText = "Quê Quán";
            dataGridViewDanCu.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dataGridViewDanCu.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dataGridViewDanCu.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridViewDanCu.Columns["AnhNhanDien"].HeaderText = "Ảnh Nhận Diện";
        }
        private void buttonThemCD_Click(object sender, EventArgs e)
        {
            // Làm mới các trường dữ liệu để nhập thông tin
            SetControlsEnabled(true);

            // Đếm số lượng khách hiện tại
            int soLuongKhach = thongTinKhachBLL.DemSoLuongKhach();

            // Tạo mã khách trọ mới với tiền tố "KH" + STT (e.g., KH001)
            string maCuDanMoi = "KH" + (soLuongKhach + 1).ToString("D3"); // Số thứ tự có 3 chữ số

            txtMaCuDan.Text = maCuDanMoi;
            txtMaCuDan.Enabled = false; // Không cho phép chỉnh sửa mã khách trọ mới
            txtHoTenCuDan.Clear();
            comboBoxGioiTinh.SelectedIndex = -1;
            txtCCCD.Clear();
            txtSDT.Clear();
            txtQueQuan.Clear();
            comboBoxTrangThai.SelectedIndex = -1;
            comboBoxPhong.SelectedIndex = -1;
            dateTimePickerNgaySinh.Value = DateTime.Now;
            pictureBoxAnhCuDan.Image = null; // Xóa ảnh cũ
        }

        private void buttonXoaCD_Click(object sender, EventArgs e)
        {

        }

        private void buttonSuaCD_Click(object sender, EventArgs e)
        {
            // Kích hoạt các trường dữ liệu để chỉnh sửa thông tin
            SetControlsEnabled(true);
            txtMaCuDan.Enabled = false; // Không cho phép chỉnh sửa mã khách trọ
        }

        private void buttonLuuCD_Click(object sender, EventArgs e)
        {
            
            try
            {
                // Lấy thông tin từ các trường dữ liệu
                string maKhachTro = txtMaCuDan.Text;
                string hoTen = txtHoTenCuDan.Text;
                string gioiTinh = comboBoxGioiTinh.Text;
                string cccd = txtCCCD.Text;
                string phone = txtSDT.Text;
                string queQuan = txtQueQuan.Text;

                // Chuyển đổi giá trị TrangThai từ string sang int
                int trangThai = (comboBoxTrangThai.SelectedIndex == 1) ? 0 : 1; // Ngưng hoạt động: 0, Đang hoạt động: 1

                string maPhong = comboBoxPhong.SelectedValue.ToString();
                DateTime ngaySinh = dateTimePickerNgaySinh.Value;

                // Lấy đường dẫn ảnh từ PictureBox (nếu có)
                string imagePath = null;
                if (pictureBoxAnhCuDan.Image != null)
                {
                    // Lưu ảnh vào thư mục và lấy đường dẫn tương đối
                    imagePath = SaveImageToFolder(pictureBoxAnhCuDan.Image, maKhachTro, hoTen);
                }

                // Tạo đối tượng DTO và gọi phương thức cập nhật dữ liệu
                var khachDTO = new ThongTinKhachDTO
                {
                    MaKhachTro = maKhachTro,
                    HoTen = hoTen,
                    GioiTinh = gioiTinh,
                    CCCD = cccd,
                    Phone = phone,
                    QueQuan = queQuan,
                    TrangThai = trangThai, // Đã chuyển đổi thành int
                    MaPhong = maPhong,
                    NgaySinh = ngaySinh,
                    AnhNhanDien = imagePath // Đường dẫn ảnh
                };

                // Gọi phương thức cập nhật từ BLL
                thongTinKhachBLL.CapNhatThongTinKhach(khachDTO);

                // Cập nhật lại dữ liệu trên DataGridView
                LoadData();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thông tin đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể cập nhật thông tin: " + ex.Message);
            }
        }

        private void dataGridViewDanCu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

            if (e.RowIndex >= 0) // Đảm bảo người dùng không nhấn vào tiêu đề cột
            {
                DataGridViewRow selectedRow = dataGridViewDanCu.Rows[e.RowIndex];

                // Hiển thị thông tin từ hàng được chọn lên các điều khiển
                txtMaCuDan.Text = selectedRow.Cells["MaKhachTro"].Value.ToString();
                txtHoTenCuDan.Text = selectedRow.Cells["HoTen"].Value.ToString();
                comboBoxGioiTinh.Text = selectedRow.Cells["GioiTinh"].Value.ToString();
                txtCCCD.Text = selectedRow.Cells["CCCD"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["Phone"].Value.ToString();
                txtQueQuan.Text = selectedRow.Cells["QueQuan"].Value.ToString();

                // Hiển thị trạng thái
                int trangThaiValue = Convert.ToInt32(selectedRow.Cells["TrangThai"].Value);
                comboBoxTrangThai.SelectedIndex = (trangThaiValue == 0) ? 1 : 0; // Ngưng hoạt động: 1, Đang hoạt động: 0

                comboBoxPhong.Text = selectedRow.Cells["MaPhong"].Value.ToString();

                // Xử lý ngày sinh
                if (selectedRow.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dateTimePickerNgaySinh.Value = (DateTime)selectedRow.Cells["NgaySinh"].Value;
                }
                else
                {
                    dateTimePickerNgaySinh.Value = DateTime.Now;
                }

                // Hiển thị hình ảnh trực tiếp nếu có
                string imagePath = selectedRow.Cells["AnhNhanDien"].Value.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    pictureBoxAnhCuDan.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBoxAnhCuDan.Image = null; // Xóa ảnh nếu không có dữ liệu hình ảnh
                }

                // Kích hoạt các trường dữ liệu để chỉnh sửa (trừ MaKhachTro)
                SetControlsEnabled(false);
            }
        }

        private void dataGridViewDanCu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private string SaveImageToFolder(Image image, string maKhachTro, string hoTen)
        {
            try
            {
                string imagesFolderPath = @"QuanLyPhongTro\QuanLyPhongTro\Images"; // Thư mục Images trong thư mục dự án
                string fullFolderPath = Path.Combine(Application.StartupPath, imagesFolderPath);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                // Tạo tên tệp ảnh
                string fileName = $"{maKhachTro}_{hoTen}.jpg";
                string filePath = Path.Combine(fullFolderPath, fileName);

                // Lưu ảnh
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Trả về đường dẫn tương đối
                return Path.Combine(imagesFolderPath, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu ảnh: " + ex.Message);
                return null;
            }
        }



        private void buttonChonAnh_Click(object sender, EventArgs e)
        {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Ẩn ảnh mặc định trong PictureBox
                    pictureBoxAnhCuDan.Image = null;

                    // Hiển thị ảnh trong PictureBox
                    try
                    {
                        Image selectedImage = Image.FromFile(openFileDialog.FileName);
                        pictureBoxAnhCuDan.Image = selectedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải ảnh: " + ex.Message);
                    }
                }
            }
        }
    }
}
