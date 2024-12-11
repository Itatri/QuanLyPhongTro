using BLL;
using DTO;
using Microsoft.Office.Interop.Word;
using System;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            SetNganHang();
            pictureBoxChuKy.SizeMode = PictureBoxSizeMode.Zoom;

        }
        public void SetCBGioiTinh()
        {
            // Thiết lập các giá trị cho ComboBoxGioiTinhAdmin
            comboBoxGioiTinhAdmin.Items.Add("Nam");
            comboBoxGioiTinhAdmin.Items.Add("Nữ");
        }
        public void SetNganHang()
        {
            string[] nganHangList = new string[]
                {
                    "Vietcombank", "ViettinBank", "MBBank", "ACB", "VPBank", "TPBank", "MSB",
                    "NamABank", "LienVietPostBank", "VietCapitalBank", "BIDV", "Sacombank", "VIB",
                    "HDBank", "SeABank", "GPBank", "PVComBank", "NCB", "ShinhanBank", "SCB",
                    "PGBank", "AgriBank", "Techcombank", "SaigonBank", "DongABank", "BacABank",
                    "StandardChartened", "Oceanbank", "VRB", "ABBANK", "VietABank", "Eximbank",
                    "VietBank", "IndovinaBank", "BaoVietBank", "PublicBank", "SHB", "CBBank",
                    "OCB", "KienLongBank", "CIMB", "HSBC", "DBSBank", "Nonghyup", "HongLeong",
                    "Woori", "UnitedOverseas", "KookminHN", "KookminHCM", "COOPBANK"
                };

            cboNganHang.Items.Clear();
            cboNganHang.Items.AddRange(nganHangList);

        }

        public void SetUserInfo(string id, string region)
        {
            try
            {
                ThongTinAdminDTO adminInfo = thongTinAdminBLL.LayThongTinAdminTheoIdUser(id);

                if (adminInfo == null)
                {
                    txtIDUserAdmin.Text = id; 

                    txtMaAdmin.Text = string.Empty;
                    txtHoTenAdmin.Text = string.Empty;
                    comboBoxGioiTinhAdmin.SelectedItem = null;
                    txtCCCDAdmin.Text = string.Empty;
                    txtDiaChiAdmin.Text = string.Empty;
                    txtTaiKhoan.Text = string.Empty;
                    cboNganHang.Text = string.Empty;
                    txtPhoneAdmin.Text = string.Empty;
                    dateTimePickerNgaySinhAdmin.Value = DateTime.Now; 
                    labelAnhChuKy.Text = string.Empty;
                    pictureBoxChuKy.Image = null; 


                }
                else
                {
                    txtMaAdmin.Text = adminInfo.MaAdmin;
                    txtHoTenAdmin.Text = adminInfo.HoTen;
                    comboBoxGioiTinhAdmin.SelectedItem = adminInfo.GioiTinh;
                    txtCCCDAdmin.Text = adminInfo.Cccd;
                    txtDiaChiAdmin.Text = adminInfo.DiaChi;
                    txtPhoneAdmin.Text = adminInfo.Phone;
                    txtTaiKhoan.Text = adminInfo.TaiKhoan;
                    cboNganHang.Text = adminInfo.NganHang;
                    dateTimePickerNgaySinhAdmin.Value = adminInfo.NgaySinh != DateTime.MinValue ? adminInfo.NgaySinh : DateTime.Now;
                    txtIDUserAdmin.Text = adminInfo.IdUser;
                    labelAnhChuKy.Text = adminInfo.ChuKy;

                    if (!string.IsNullOrEmpty(adminInfo.ChuKy))
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string imagesFolderPath = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;
                        imagesFolderPath = Path.GetFullPath(imagesFolderPath);


                        string filePath = Path.Combine(imagesFolderPath, adminInfo.ChuKy);

                        if (File.Exists(filePath))
                        {
                            pictureBoxChuKy.Image = Image.FromFile(filePath);
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
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagesFolderPath = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;
                imagesFolderPath = Path.GetFullPath(imagesFolderPath); 

                ThongTinAdminDTO adminInfo = thongTinAdminBLL.LayThongTinAdminTheoIdUser(txtIDUserAdmin.Text);

                if (!string.IsNullOrEmpty(adminInfo?.ChuKy))
                {
                    var confirmResult = MessageBox.Show("Admin đã có ảnh chữ ký. Bạn có muốn xóa ảnh cũ để cập nhật ảnh mới không?",
                                                         "Xác nhận xóa ảnh chữ ký",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Warning);

                    if (confirmResult == DialogResult.Yes)
                    {
                        string oldSignatureFileName = adminInfo.ChuKy;

                        string oldFilePath = Path.Combine(imagesFolderPath, oldSignatureFileName);

                        pictureBoxChuKy.Image?.Dispose();
                        pictureBoxChuKy.Image = null;

                        GC.Collect(); 
                        GC.WaitForPendingFinalizers(); 

                        if (File.Exists(oldFilePath))
                        {
                            File.Delete(oldFilePath);
                        }

                        thongTinAdminBLL.CapNhatChuKy(txtIDUserAdmin.Text, null);
                    }
                    else
                    {
                        return;
                    }
                }

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    openFileDialog.Title = "Chọn hình ảnh";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Image selectedImage = Image.FromFile(openFileDialog.FileName);
                        pictureBoxChuKy.Image = selectedImage;

                        isChuKyUpdated = true; 
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
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                string imagesFolderPath = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;

                imagesFolderPath = Path.GetFullPath(imagesFolderPath);

                if (!Directory.Exists(imagesFolderPath))
                {
                    Directory.CreateDirectory(imagesFolderPath);
                }

                string hoTenKhongDau = RemoveDiacritics(hoTen);

                string fileName = $"CK_{maKhachTro}_{hoTenKhongDau}.jpg";
                string filePath = Path.Combine(imagesFolderPath, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath); 
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

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
            text = text.Replace("Đ", "D").Replace("đ", "d");

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

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC).Replace(" ", "");
        }


        private void btnCapNhatThongTin_Click(object sender, EventArgs e)
        {
            if (!KiemTraSoDienThoai(txtPhoneAdmin.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!");
                return;
            }

            if (!KiemTraCCCD(txtCCCDAdmin.Text))
            {
                MessageBox.Show("Căn cước công dân không hợp lệ!");
                return;
            }
            try
            {
                string idUser = txtIDUserAdmin.Text;
                string maAdmin = txtMaAdmin.Text;
                string hoTen = txtHoTenAdmin.Text;
                string gioiTinh = comboBoxGioiTinhAdmin.SelectedItem != null ? comboBoxGioiTinhAdmin.SelectedItem.ToString() : "";
                string cccd = txtCCCDAdmin.Text;
                string nganhang = cboNganHang.Text;
                string taikhoan = txtTaiKhoan.Text;
                string diachi = txtDiaChiAdmin.Text;
                string phone = txtPhoneAdmin.Text;
                DateTime ngaySinh = dateTimePickerNgaySinhAdmin.Value;

                string chuKyFileName = null;

                if (isChuKyUpdated && pictureBoxChuKy.Image != null) 
                {
                    chuKyFileName = SaveImageToFolderChuKy(pictureBoxChuKy.Image, maAdmin, hoTen);
                }

                ThongTinAdminDTO adminInfo = new ThongTinAdminDTO
                {
                    MaAdmin = maAdmin,
                    HoTen = hoTen,
                    GioiTinh = gioiTinh,
                    NgaySinh = ngaySinh,
                    Cccd = cccd,
                    NganHang = nganhang,
                    TaiKhoan = taikhoan,
                    Phone = phone,
                    DiaChi = diachi,
                    ChuKy = chuKyFileName ?? labelAnhChuKy.Text, 
                    IdUser = idUser
                };

                bool adminExists = thongTinAdminBLL.KiemTraThongTinAdmin(idUser);

                if (adminExists)
                {
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

                string password = txtPassword.Text;
                string rePass = txtRePass.Text;

                if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(rePass))
                {
                    if (password == rePass)
                    {
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

        public bool KiemTraSoDienThoai(string soDienThoai)
        {
            string pattern = @"^(0[3|5|7|8|9])\d{8}$"; 

            return Regex.IsMatch(soDienThoai, pattern);
        }
        public bool KiemTraEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public bool KiemTraCCCD(string cccd)
        {
            string pattern = @"^\d{12}$"; 

            return Regex.IsMatch(cccd, pattern);
        }

        private void ThongTinAdmin_Load(object sender, EventArgs e)
        {
            dateTimePickerNgaySinhAdmin.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgaySinhAdmin.CustomFormat = "dd/MM/yyyy";
        }

        private void panelThongTinDanCu_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
