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
        private bool isAddingNew = false; // Biến trạng thái để theo dõi chế độ thêm mới hay sửa
        public QuanLiDanCu()
        {
            InitializeComponent();
            LoadData();
            LoadPhongComboBox();
            // Đăng ký sự kiện CellClick cho DataGridView
            dataGridViewDanCu.CellClick += dataGridViewDanCu_CellClick;
            // Thiết lập chế độ hiển thị ảnh cho PictureBox
            //pictureBoxAnhCuDan.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxChuKy.SizeMode = PictureBoxSizeMode.Zoom;
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
            //buttonChonAnh.Enabled = enabled;
            buttonChonChuKy.Enabled = enabled;
        }

        private void LoadData()
        {
            var danhSachKhach = thongTinKhachBLL.LayTatCaThongTinKhach();
            dataGridViewDanCu.DataSource = danhSachKhach;

            // Đặt lại tên các cột
            dataGridViewDanCu.Columns["MaKhachTro"].HeaderText = "Mã Cư Dân";
            dataGridViewDanCu.Columns["HoTen"].HeaderText = "Họ Tên";
            dataGridViewDanCu.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridViewDanCu.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dataGridViewDanCu.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Ẩn các cột không cần thiết
            foreach (DataGridViewColumn column in dataGridViewDanCu.Columns)
            {
                if (column.Name != "MaKhachTro" &&
                    column.Name != "HoTen" &&
                    column.Name != "GioiTinh" &&
                    column.Name != "Phone" &&
                    column.Name != "TrangThai" &&
                    column.Name != "btnXemChiTiet")
                {
                    column.Visible = false;
                }
            }

            // Xóa cột "TrangThai" hiện tại nếu có
            if (dataGridViewDanCu.Columns["TrangThai"] != null)
            {
                dataGridViewDanCu.Columns.Remove("TrangThai");
            }

            // Thêm cột checkbox "TrangThai"
            DataGridViewCheckBoxColumn chkTrangThai = new DataGridViewCheckBoxColumn();
            chkTrangThai.Name = "TrangThai";
            chkTrangThai.HeaderText = "Trạng Thái";
            chkTrangThai.DataPropertyName = "TrangThai"; // Tên thuộc tính trong DataSource
            dataGridViewDanCu.Columns.Add(chkTrangThai);

            // Thêm cột nút "Xem Chi Tiết" nếu chưa tồn tại
            if (dataGridViewDanCu.Columns["btnXemChiTiet"] == null)
            {
                DataGridViewButtonColumn btnXemChiTiet = new DataGridViewButtonColumn();
                btnXemChiTiet.Name = "btnXemChiTiet";
                btnXemChiTiet.HeaderText = "Hành Động";
                btnXemChiTiet.Text = "Xem Chi Tiết";
                btnXemChiTiet.UseColumnTextForButtonValue = true;
                btnXemChiTiet.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng cho nút
                dataGridViewDanCu.Columns.Add(btnXemChiTiet);
            }

            // Tùy chỉnh màu sắc cho cột nút
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.BackColor = Color.Black;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.ForeColor = Color.White;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionBackColor = Color.Green;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionForeColor = Color.White;

            // Thiết lập tự động điều chỉnh kích thước cột
            dataGridViewDanCu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }




        private void buttonThemCD_Click(object sender, EventArgs e)
        {
            isAddingNew = true; // Đặt chế độ thêm mới

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
            ////pictureBoxAnhCuDan.Image = null; // Xóa ảnh cũ
            pictureBoxChuKy.Image = null; // Xóa ảnh cũ
        }

        private void buttonXoaCD_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã khách hàng từ txtMaCuDan
                string maKhachTro = txtMaCuDan.Text;

                // Kiểm tra xem mã khách hàng có hợp lệ không
                if (!string.IsNullOrEmpty(maKhachTro))
                {
                    // Xác nhận việc xóa
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        // Lấy thông tin khách hàng hiện tại để lấy tên file ảnh
                        var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);

                        if (currentCustomer != null)
                        {
                            // Xóa ảnh của khách hàng nếu có
                            if (!string.IsNullOrEmpty(currentCustomer.AnhNhanDien))
                            {
                                // Tạo đường dẫn đầy đủ tới ảnh
                                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                                string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhCuDan");
                                string filePath = Path.Combine(imagesFolderPath, currentCustomer.AnhNhanDien);

                                //// Giải phóng tài nguyên ảnh nếu cần
                                //pictureBoxAnhCuDan.Image?.Dispose();
                                //pictureBoxAnhCuDan.Image = null;

                                // Đảm bảo file không còn được sử dụng
                                GC.Collect(); // Yêu cầu Garbage Collector để thu hồi bộ nhớ không sử dụng
                                GC.WaitForPendingFinalizers(); // Đợi cho Garbage Collector hoàn tất

                                // Xóa file ảnh nếu tồn tại
                                if (File.Exists(filePath))
                                {
                                    File.Delete(filePath);
                                }
                            }
                            // Xóa chữ ký của khách hàng nếu có
                            if (!string.IsNullOrEmpty(currentCustomer.ChuKy))
                            {
                                // Tạo đường dẫn đầy đủ tới ảnh
                                string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
                                string imagesFolderPathChuKy = Path.Combine(baseDirectoryChuKy, "..", "..", "AnhChuKy");
                                string filePathChuKy = Path.Combine(imagesFolderPathChuKy, currentCustomer.ChuKy);

                                // Giải phóng tài nguyên ảnh nếu cần
                                pictureBoxChuKy.Image?.Dispose();
                                pictureBoxChuKy.Image = null;

                                // Đảm bảo file không còn được sử dụng
                                GC.Collect(); // Yêu cầu Garbage Collector để thu hồi bộ nhớ không sử dụng
                                GC.WaitForPendingFinalizers(); // Đợi cho Garbage Collector hoàn tất

                                // Xóa file ảnh nếu tồn tại
                                if (File.Exists(filePathChuKy))
                                {
                                    File.Delete(filePathChuKy);
                                }
                            }
                            // Xóa thông tin khách hàng khỏi cơ sở dữ liệu
                            thongTinKhachBLL.XoaThongTinKhach(maKhachTro);

                            // Cập nhật lại DataGridView
                            LoadData();

                            MessageBox.Show("Khách hàng và ảnh của họ đã được xóa thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin khách hàng.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Lỗi khi xóa file ảnh: " + ioEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa khách hàng: " + ex.Message);
            }
        }

        private void buttonSuaCD_Click(object sender, EventArgs e)
        {
            isAddingNew = false; // Đặt chế độ sửa
            // Kích hoạt các trường dữ liệu để chỉnh sửa thông tin
            SetControlsEnabled(true);
            txtMaCuDan.Enabled = false; // Không cho phép chỉnh sửa mã khách trọ

            ////// Giải phóng tài nguyên ảnh nhận diện hiện tại 
            ////if (pictureBoxAnhCuDan.Image != null)
            ////{
            ////    pictureBoxAnhCuDan.Image.Dispose();
            ////    pictureBoxAnhCuDan.Image = null;
            ////}

            // Giải phóng tài nguyên ảnh chũ ký hiện tại 
            if (pictureBoxChuKy.Image != null)
            {
                pictureBoxChuKy.Image.Dispose();
                pictureBoxChuKy.Image = null;
            }
        }

        private void buttonLuuCD_Click(object sender, EventArgs e)
        {
            
            try
            {
                //// Kiểm tra nếu chưa có ảnh trong pictureBoxAnhCuDan
                //if (pictureBoxAnhCuDan.Image == null)
                //{
                //    MessageBox.Show("Vui lòng chọn ảnh cho khách trọ.");
                //    return; 
                //}

                // Kiểm tra nếu chưa có ảnh trong pictureBoxChuKy
                if (pictureBoxChuKy.Image == null)
                {
                    MessageBox.Show("Vui lòng chọn chữ ký cho khách trọ.");
                    return; 
                }

                string maKhachTro = txtMaCuDan.Text;
                string hoTen = txtHoTenCuDan.Text;
                string gioiTinh = comboBoxGioiTinh.Text;
                string cccd = txtCCCD.Text;
                string phone = txtSDT.Text;
                string queQuan = txtQueQuan.Text;
                int trangThai = (comboBoxTrangThai.SelectedItem.ToString() == "Ngưng hoạt động") ? 0 : 1;
                string maPhong = comboBoxPhong.SelectedValue.ToString();
                DateTime ngaySinh = dateTimePickerNgaySinh.Value;
                string newImagePath = null;
                string newImagePathChuKy = null;

                //// Handle the image in pictureBoxAnhCuDan
                //if (pictureBoxAnhCuDan.Image != null)
                //{
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        pictureBoxAnhCuDan.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //        using (Image img = Image.FromStream(ms))
                //        {
                //            newImagePath = SaveImageToFolder(img, maKhachTro, hoTen);
                //        }
                //    }
                //}

                // Handle the image in pictureBoxChuKy
                if (pictureBoxChuKy.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBoxChuKy.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        using (Image img = Image.FromStream(ms))
                        {
                            newImagePathChuKy = SaveImageToFolderChuKy(img, maKhachTro, hoTen);
                        }
                    }
                }

                // Create DTO
                var khachDTO = new ThongTinKhachDTO
                {
                    MaKhachTro = maKhachTro,
                    HoTen = hoTen,
                    GioiTinh = gioiTinh,
                    CCCD = cccd,
                    Phone = phone,
                    QueQuan = queQuan,
                    TrangThai = trangThai,
                    MaPhong = maPhong,
                    NgaySinh = ngaySinh,
                    AnhNhanDien = newImagePath,
                    ChuKy = newImagePathChuKy
                };

                if (isAddingNew)
                {
                    thongTinKhachBLL.ThemThongTinKhach(khachDTO); // Add new record
                }
                else
                {
                    var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);

                    if (currentCustomer != null)
                    {
                        // Handle AnhNhanDien
                        if (newImagePath == null)
                        {
                            khachDTO.AnhNhanDien = currentCustomer.AnhNhanDien;
                        }

                        // Handle ChuKy
                        if (newImagePathChuKy == null)
                        {
                            khachDTO.ChuKy = currentCustomer.ChuKy;
                        }
                    }

                    thongTinKhachBLL.CapNhatThongTinKhach(khachDTO); // Update record
                }

                LoadData();
                MessageBox.Show("Thông tin đã được lưu thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu thông tin: " + ex.Message);
            }
        }

        private void dataGridViewDanCu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0) // Đảm bảo người dùng không nhấn vào tiêu đề cột
            //{
            //    DataGridViewRow selectedRow = dataGridViewDanCu.Rows[e.RowIndex];

            //    // Hiển thị thông tin từ hàng được chọn lên các điều khiển
            //    txtMaCuDan.Text = selectedRow.Cells["MaKhachTro"].Value.ToString();
            //    txtHoTenCuDan.Text = selectedRow.Cells["HoTen"].Value.ToString();
            //    comboBoxGioiTinh.Text = selectedRow.Cells["GioiTinh"].Value.ToString();
            //    txtCCCD.Text = selectedRow.Cells["CCCD"].Value.ToString();
            //    txtSDT.Text = selectedRow.Cells["Phone"].Value.ToString();
            //    txtQueQuan.Text = selectedRow.Cells["QueQuan"].Value.ToString();

            //    // Hiển thị trạng thái
            //    int trangThaiValue = Convert.ToInt32(selectedRow.Cells["TrangThai"].Value);
            //    comboBoxTrangThai.SelectedIndex = (trangThaiValue == 0) ? 1 : 0; // Ngưng hoạt động: 1, Đang hoạt động: 0

            //    comboBoxPhong.Text = selectedRow.Cells["MaPhong"].Value.ToString();

            //    // Xử lý ngày sinh
            //    if (selectedRow.Cells["NgaySinh"].Value != DBNull.Value)
            //    {
            //        dateTimePickerNgaySinh.Value = (DateTime)selectedRow.Cells["NgaySinh"].Value;
            //    }
            //    else
            //    {
            //        dateTimePickerNgaySinh.Value = DateTime.Now;
            //    }

            //    // Hiển thị hình ảnh trực tiếp nếu có
            //    string imageFileName = selectedRow.Cells["AnhNhanDien"].Value.ToString();
            //    if (!string.IsNullOrEmpty(imageFileName))
            //    {
            //        // Tạo đường dẫn đầy đủ tới ảnh
            //        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //        string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhCuDan");
            //        string filePath = Path.Combine(imagesFolderPath, imageFileName);

            //        if (File.Exists(filePath))
            //        {
            //            pictureBoxAnhCuDan.Image = Image.FromFile(filePath);
            //        }
            //        else
            //        {
            //            pictureBoxAnhCuDan.Image = null; // Xóa ảnh nếu không có dữ liệu hình ảnh
            //        }
            //    }
            //    else
            //    {
            //        pictureBoxAnhCuDan.Image = null; // Xóa ảnh nếu không có dữ liệu hình ảnh
            //    }

            //    // Hiển thị chữ ký trực tiếp nếu có
            //    string imageChuKy = selectedRow.Cells["ChuKy"].Value.ToString();
            //    if (!string.IsNullOrEmpty(imageChuKy))
            //    {
            //        // Tạo đường dẫn đầy đủ tới ảnh
            //        string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
            //        string imagesFolderPathChuKy = Path.Combine(baseDirectoryChuKy, "..", "..", "AnhChuKy");
            //        string filePathChuKy = Path.Combine(imagesFolderPathChuKy, imageChuKy);

            //        if (File.Exists(filePathChuKy))
            //        {
            //            pictureBoxChuKy.Image = Image.FromFile(filePathChuKy);
            //        }
            //        else
            //        {
            //            pictureBoxChuKy.Image = null; // Xóa ảnh nếu không có dữ liệu hình ảnh
            //        }
            //    }
            //    else
            //    {
            //        pictureBoxChuKy.Image = null; // Xóa ảnh nếu không có dữ liệu hình ảnh
            //    }

            //    // Kích hoạt các trường dữ liệu để chỉnh sửa (trừ MaKhachTro)
            //    SetControlsEnabled(false);
            //}

            // Kiểm tra xem người dùng có nhấn vào cột nút không
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewDanCu.Columns["btnXemChiTiet"].Index)
            {
                // Lấy thông tin từ hàng đã chọn
                DataGridViewRow selectedRow = dataGridViewDanCu.Rows[e.RowIndex];

                // Hiển thị thông tin lên các điều khiển
                txtMaCuDan.Text = selectedRow.Cells["MaKhachTro"].Value.ToString();
                txtHoTenCuDan.Text = selectedRow.Cells["HoTen"].Value.ToString();
                comboBoxGioiTinh.Text = selectedRow.Cells["GioiTinh"].Value.ToString();
                txtCCCD.Text = selectedRow.Cells["CCCD"].Value.ToString();
                txtSDT.Text = selectedRow.Cells["Phone"].Value.ToString();
                txtQueQuan.Text = selectedRow.Cells["QueQuan"].Value.ToString();

                int trangThaiValue = Convert.ToInt32(selectedRow.Cells["TrangThai"].Value);
                comboBoxTrangThai.SelectedIndex = (trangThaiValue == 0) ? 1 : 0;

                comboBoxPhong.Text = selectedRow.Cells["MaPhong"].Value.ToString();

                if (selectedRow.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dateTimePickerNgaySinh.Value = (DateTime)selectedRow.Cells["NgaySinh"].Value;
                }
                else
                {
                    dateTimePickerNgaySinh.Value = DateTime.Now;
                }

                ////string imageFileName = selectedRow.Cells["AnhNhanDien"].Value.ToString();
                ////if (!string.IsNullOrEmpty(imageFileName))
                ////{
                ////    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                ////    string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhCuDan");
                ////    string filePath = Path.Combine(imagesFolderPath, imageFileName);

                ////    if (File.Exists(filePath))
                ////    {
                ////        pictureBoxAnhCuDan.Image = Image.FromFile(filePath);
                ////    }
                ////    else
                ////    {
                ////        pictureBoxAnhCuDan.Image = null;
                ////    }
                ////}
                ////else
                ////{
                ////    pictureBoxAnhCuDan.Image = null;
                ////}

                string imageChuKy = selectedRow.Cells["ChuKy"].Value.ToString();
                if (!string.IsNullOrEmpty(imageChuKy))
                {
                    string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolderPathChuKy = Path.Combine(baseDirectoryChuKy, "..", "..", "AnhChuKy");
                    string filePathChuKy = Path.Combine(imagesFolderPathChuKy, imageChuKy);

                    if (File.Exists(filePathChuKy))
                    {
                        pictureBoxChuKy.Image = Image.FromFile(filePathChuKy);
                    }
                    else
                    {
                        pictureBoxChuKy.Image = null;
                    }
                }
                else
                {
                    pictureBoxChuKy.Image = null;
                }

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
                // Lấy thư mục gốc của ứng dụng
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Tạo đường dẫn thư mục AnhCuDan trong thư mục gốc của dự án
                string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhCuDan");

                // Chuyển đường dẫn lên thư mục gốc của dự án
                imagesFolderPath = Path.GetFullPath(imagesFolderPath);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                // Tạo tên tệp ảnh
                string fileName = $"{maKhachTro}_{hoTen}.jpg";
                string filePath = Path.Combine(imagesFolderPath, fileName);

                // Kiểm tra nếu tệp đã tồn tại
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // Xóa tệp nếu nó đã tồn tại
                }

                // Lưu ảnh
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                // Trả về tên tệp ảnh cho cơ sở dữ liệu
                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu ảnh: " + ex.Message);
                return null;
            }
        }

        private string SaveImageToFolderChuKy(Image image, string maKhachTro, string hoTen)
        {
            try
            {
                // Lấy thư mục gốc của ứng dụng
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Tạo đường dẫn thư mục AnhCuDan trong thư mục gốc của dự án
                string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhChuKy");

                // Chuyển đường dẫn lên thư mục gốc của dự án
                imagesFolderPath = Path.GetFullPath(imagesFolderPath);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                // Tạo tên tệp ảnh
                string fileName = $"CK_{maKhachTro}_{hoTen}.jpg";
                string filePath = Path.Combine(imagesFolderPath, fileName);

                // Kiểm tra nếu tệp đã tồn tại
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // Xóa tệp nếu nó đã tồn tại
                }

                // Lưu ảnh
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                // Trả về tên tệp ảnh cho cơ sở dữ liệu
                return fileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lưu ảnh: " + ex.Message);
                return null;
            }
        }

        private void buttonChonAnh_Click(object sender, EventArgs e)
        {
            //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            //    openFileDialog.Title = "Chọn hình ảnh";

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        // Ẩn ảnh mặc định trong PictureBox
            //        pictureBoxAnhCuDan.Image = null;

            //        // Hiển thị ảnh trong PictureBox
            //        try
            //        {
            //            Image selectedImage = Image.FromFile(openFileDialog.FileName);
            //            pictureBoxAnhCuDan.Image = selectedImage;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            //        }
            //    }
            //}
        }

        private void buttonChonChuKy_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Ẩn ảnh mặc định trong PictureBox
                    pictureBoxChuKy.Image = null;

                    // Hiển thị ảnh trong PictureBox
                    try
                    {
                        Image selectedImage = Image.FromFile(openFileDialog.FileName);
                        pictureBoxChuKy.Image = selectedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải ảnh: " + ex.Message);
                    }
                }
            }
        }

        private void buttonTimKiemCuDan_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtTimKiemCuDan.Text.Trim();

                // Gọi phương thức tìm kiếm từ BLL
                var searchResults = thongTinKhachBLL.TimKiemThongTinKhach(searchValue);

                // Hiển thị kết quả tìm kiếm trên DataGridView
                dataGridViewDanCu.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void buttonRefesh_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa giá trị tìm kiếm
                txtTimKiemCuDan.Text = string.Empty;

                // Gọi phương thức để tải toàn bộ dữ liệu và cập nhật DataGridView
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể làm mới dữ liệu: " + ex.Message);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxChuKy_Click(object sender, EventArgs e)
        {

        }

        private void labelAnhChuKy_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtMaCuDan_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtHoTenCuDan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelThongTinDanCu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
