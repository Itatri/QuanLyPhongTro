using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class ThongTinAdmin : UserControl
    {
        private ThongTinAdminBLL thongTinAdminBLL; // Khởi tạo đối tượng BLL
        private bool isChuKyUpdated = false; // Kiểm tra xem có ảnh chữ ký mới hay không


        public ThongTinAdmin()
        {
            InitializeComponent();
            thongTinAdminBLL = new ThongTinAdminBLL(); // Khởi tạo BLL trong constructor
            SetCBGioiTinh();
          
            pictureBoxChuKy.SizeMode = PictureBoxSizeMode.Zoom;

        }
        public void SetCBGioiTinh()
        {
            // Thiết lập các giá trị cho ComboBoxGioiTinhAdmin
            comboBoxGioiTinhAdmin.Items.Add("Nam");
            comboBoxGioiTinhAdmin.Items.Add("Nữ");
        }
      
        
        public void SetUserInfo(string id, string region)
        {
            try
            {
                // Gọi phương thức BLL để lấy thông tin admin theo IdUser
                ThongTinAdminDTO adminInfo = thongTinAdminBLL.LayThongTinAdminTheoIdUser(id);

                // Nếu không tìm thấy thông tin admin thì chỉ hiển thị IdUser và xóa thông tin khác
                if (adminInfo == null)
                {
                    // Chỉ hiển thị IdUser
                    txtIDUserAdmin.Text = id;  // Gán giá trị IdUser từ DangNhap

                    // Xóa thông tin khác trên giao diện
                    txtMaAdmin.Text = string.Empty;
                    txtHoTenAdmin.Text = string.Empty;
                    comboBoxGioiTinhAdmin.SelectedItem = null;
                    txtCCCDAdmin.Text = string.Empty;
                    txtQueQuanAdmin.Text = string.Empty;
                    txtPhoneAdmin.Text = string.Empty;
                    dateTimePickerNgaySinhAdmin.Value = DateTime.Now;  // Đặt giá trị ngày hiện tại
                    labelAnhChuKy.Text = string.Empty;
                    pictureBoxChuKy.Image = null; // Xóa ảnh chữ ký nếu không có thông tin


                }
                else
                {
                    // Hiển thị thông tin admin lên các control
                    txtMaAdmin.Text = adminInfo.MaAdmin;
                    txtHoTenAdmin.Text = adminInfo.HoTen;
                    comboBoxGioiTinhAdmin.SelectedItem = adminInfo.GioiTinh;
                    txtCCCDAdmin.Text = adminInfo.Cccd;
                    txtQueQuanAdmin.Text = adminInfo.QueQuan;
                    txtPhoneAdmin.Text = adminInfo.Phone;
                    dateTimePickerNgaySinhAdmin.Value = adminInfo.NgaySinh != DateTime.MinValue ? adminInfo.NgaySinh : DateTime.Now;
                    txtIDUserAdmin.Text = adminInfo.IdUser;
                    labelAnhChuKy.Text = adminInfo.ChuKy;

                    // Hiển thị ảnh chữ ký nếu có
                    if (!string.IsNullOrEmpty(adminInfo.ChuKy))
                    {
                        // Lấy đường dẫn thư mục chứa ảnh chữ ký
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhChuKy");
                        imagesFolderPath = Path.GetFullPath(imagesFolderPath);

                        // Tạo đường dẫn đầy đủ tới file ảnh
                        string filePath = Path.Combine(imagesFolderPath, adminInfo.ChuKy);

                        // Kiểm tra xem file ảnh có tồn tại không
                        if (File.Exists(filePath))
                        {
                            // Hiển thị ảnh trong pictureBox
                            pictureBoxChuKy.Image = Image.FromFile(filePath);
                        }
                        else
                        {
                            //MessageBox.Show("Không tìm thấy ảnh chữ ký.");
                            pictureBoxChuKy.Image = null; // Xóa ảnh nếu không tìm thấy
                        }
                    }
                    else
                    {
                        pictureBoxChuKy.Image = null; // Xóa ảnh nếu không có chữ ký
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin admin: {ex.Message}");
            }
        }


        private void buttonChonChuKy_Click(object sender, EventArgs e)
        {
           

            try
            {
                // Khai báo đường dẫn tới thư mục chứa ảnh chữ ký
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhChuKy");
                imagesFolderPath = Path.GetFullPath(imagesFolderPath); // Đảm bảo đường dẫn chính xác

                // Lấy thông tin admin hiện tại để kiểm tra ảnh chữ ký
                ThongTinAdminDTO adminInfo = thongTinAdminBLL.LayThongTinAdminTheoIdUser(txtIDUserAdmin.Text);

                // Kiểm tra xem Admin đã có chữ ký hay chưa
                if (!string.IsNullOrEmpty(adminInfo?.ChuKy))
                {
                    // Hiển thị thông báo xác nhận xóa ảnh cũ
                    var confirmResult = MessageBox.Show("Admin đã có ảnh chữ ký. Bạn có muốn xóa ảnh cũ để cập nhật ảnh mới không?",
                                                         "Xác nhận xóa ảnh chữ ký",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        // Lấy tên ảnh cũ từ cơ sở dữ liệu
                        string oldSignatureFileName = adminInfo.ChuKy;

                        // Xóa ảnh cũ
                        string oldFilePath = Path.Combine(imagesFolderPath, oldSignatureFileName);

                        // Giải phóng tài nguyên ảnh nếu cần
                        pictureBoxChuKy.Image?.Dispose();
                        pictureBoxChuKy.Image = null;

                        // Đảm bảo file không còn được sử dụng
                        GC.Collect(); // Yêu cầu Garbage Collector để thu hồi bộ nhớ không sử dụng
                        GC.WaitForPendingFinalizers(); // Đợi cho Garbage Collector hoàn tất

                        // Xóa file ảnh nếu tồn tại
                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }

                        // Cập nhật tên ảnh trong cơ sở dữ liệu thành null
                        thongTinAdminBLL.CapNhatChuKy(txtIDUserAdmin.Text, null);
                    }
                    else
                    {
                        // Nếu người dùng chọn Hủy, không làm gì cả
                        return;
                    }
                }

                // Bước này sẽ luôn thực hiện để cho phép chọn ảnh mới
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Title = "Chọn hình ảnh";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Hiển thị ảnh mới trong PictureBox
                        Image selectedImage = Image.FromFile(openFileDialog.FileName);
                        pictureBoxChuKy.Image = selectedImage;

                        // Đánh dấu rằng có ảnh chữ ký mới đã được chọn
                        isChuKyUpdated = true; // Biến này cần được khai báo ở nơi khác trong class
                    }
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Lỗi khi xóa file ảnh: " + ioEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể cập nhật chữ ký: " + ex.Message);
            }
        }
        //private string SaveImageToFolderChuKy(Image image, string maKhachTro, string hoTen)
        //{
        //    try
        //    {
        //        // Lấy thư mục gốc của ứng dụng
        //        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //        // Tạo đường dẫn thư mục AnhCuDan trong thư mục gốc của dự án
        //        string imagesFolderPath = Path.Combine(baseDirectory, "..", "..", "AnhChuKy");

        //        // Chuyển đường dẫn lên thư mục gốc của dự án
        //        imagesFolderPath = Path.GetFullPath(imagesFolderPath);

        //        // Tạo thư mục nếu chưa tồn tại
        //        if (!Directory.Exists(imagesFolderPath))
        //        {
        //            Directory.CreateDirectory(imagesFolderPath);
        //        }

        //        // Tạo tên tệp ảnh
        //        string fileName = $"CK_{maKhachTro}_{hoTen}.jpg";
        //        string filePath = Path.Combine(imagesFolderPath, fileName);

        //        // Kiểm tra nếu tệp đã tồn tại
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath); // Xóa tệp nếu nó đã tồn tại
        //        }

        //        // Lưu ảnh
        //        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //        {
        //            image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        }

        //        // Trả về tên tệp ảnh cho cơ sở dữ liệu
        //        return fileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Không thể lưu ảnh: " + ex.Message);
        //        return null;
        //    }
        //}


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

                // Chuyển hoTen thành không dấu
                string hoTenKhongDau = RemoveDiacritics(hoTen);

                // Tạo tên tệp ảnh
                string fileName = $"CK_{maKhachTro}_{hoTenKhongDau}.jpg";
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

        // Phương thức chuyển đổi chuỗi có dấu thành không dấu
        private string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        private void btnCapNhatThongTin_Click(object sender, EventArgs e)
        {

            try
            {
                // Lấy thông tin từ các điều khiển
                string idUser = txtIDUserAdmin.Text;
                string maAdmin = txtMaAdmin.Text;
                string hoTen = txtHoTenAdmin.Text;
                string gioiTinh = comboBoxGioiTinhAdmin.SelectedItem != null ? comboBoxGioiTinhAdmin.SelectedItem.ToString() : "";
                string cccd = txtCCCDAdmin.Text;
                string queQuan = txtQueQuanAdmin.Text;
                string phone = txtPhoneAdmin.Text;
                DateTime ngaySinh = dateTimePickerNgaySinhAdmin.Value;

                // Lưu ảnh chữ ký mới nếu có
                string chuKyFileName = null;

                if (isChuKyUpdated && pictureBoxChuKy.Image != null) // Nếu có ảnh mới và được đánh dấu
                {
                    // Lưu ảnh chữ ký mới và lấy tên tệp
                    chuKyFileName = SaveImageToFolderChuKy(pictureBoxChuKy.Image, maAdmin, hoTen);
                }

                // Tạo đối tượng DTO với các thông tin mới
                ThongTinAdminDTO adminInfo = new ThongTinAdminDTO
                {
                    MaAdmin = maAdmin,
                    HoTen = hoTen,
                    GioiTinh = gioiTinh,
                    NgaySinh = ngaySinh,
                    Cccd = cccd,
                    Phone = phone,
                    QueQuan = queQuan,
                    ChuKy = chuKyFileName ?? labelAnhChuKy.Text, // Nếu không có ảnh mới, giữ lại tên cũ
                    IdUser = idUser
                };

                // Kiểm tra xem Admin đã tồn tại chưa
                bool adminExists = thongTinAdminBLL.KiemTraThongTinAdmin(idUser);

                if (adminExists)
                {
                    // Nếu tồn tại, cập nhật thông tin Admin
                    bool isUpdated = thongTinAdminBLL.CapNhatThongTinAdmin(adminInfo);

                    if (isUpdated)
                    {
                        MessageBox.Show("Cập nhật thông tin Admin thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin Admin thất bại!");
                    }
                }
                else
                {
                    // Thêm mới Admin
                    bool isAdded = thongTinAdminBLL.ThemAdmin(adminInfo);

                    if (isAdded)
                    {
                        MessageBox.Show("Thêm mới thông tin Admin thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới thông tin Admin thất bại!");
                    }
                }

                // Kiểm tra và cập nhật mật khẩu
                string password = txtPassword.Text;
                string rePass = txtRePass.Text;

                if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(rePass))
                {
                    if (password == rePass)
                    {
                        // Gọi phương thức cập nhật mật khẩu trong BLL
                        DangNhapAdminBLL dangNhapBLL = new DangNhapAdminBLL();
                        bool isPasswordUpdated = dangNhapBLL.CapNhatMatKhau(idUser, password);

                        if (isPasswordUpdated)
                        {
                            MessageBox.Show("Cập nhật mật khẩu Admin thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật mật khẩu Admin thất bại!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật mật khẩu Admin thất bại! Nhập lại mật khẩu");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin Admin: {ex.Message}");
            }

        }

    }
}
