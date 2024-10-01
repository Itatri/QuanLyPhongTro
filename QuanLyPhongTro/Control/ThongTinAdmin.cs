using BLL;
using DAL;
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
    public partial class ThongTinAdmin : UserControl
    {
        private ThongTinAdminBLL thongTinAdminBLL; // Khởi tạo đối tượng BLL
        private bool isChuKyUpdated = false; // Kiểm tra xem có ảnh chữ ký mới hay không


        public ThongTinAdmin()
        {
            InitializeComponent();
            thongTinAdminBLL = new ThongTinAdminBLL(); // Khởi tạo BLL trong constructor
            SetCBGioiTinh();
            //pictureBoxAnhCuDan.SizeMode = PictureBoxSizeMode.Zoom;
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

                // Hiển thị thông tin admin lên các control
                txtMaAdmin.Text = adminInfo.MaAdmin;
                txtHoTenAdmin.Text = adminInfo.HoTen;
                comboBoxGioiTinhAdmin.SelectedItem = adminInfo.GioiTinh; // ComboBox
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
                        MessageBox.Show("Không tìm thấy ảnh chữ ký.");
                        pictureBoxChuKy.Image = null; // Xóa ảnh nếu không tìm thấy
                    }
                }
                else
                {
                    pictureBoxChuKy.Image = null; // Xóa ảnh nếu không có chữ ký
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin admin: {ex.Message}");
            }

        }

        private void ThongTinAdmin_Load(object sender, EventArgs e)
        {

        }

        private void panelThongTinDanCu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMaAdmin_TextChanged(object sender, EventArgs e)
        {

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
                        isChuKyUpdated = true; // Đánh dấu rằng người dùng đã chọn ảnh mới
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể tải ảnh: " + ex.Message);
                    }
                }
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

                // Kiểm tra nếu có ảnh chữ ký trong PictureBox
                string chuKyFileName = null;
                bool isChuKyUpdated = false; // Đánh dấu xem ảnh chữ ký có được cập nhật hay không

                if (pictureBoxChuKy.Image != null)
                {
                    // Kiểm tra xem ảnh chữ ký có phải là ảnh mới hay không
                    if (isChuKyUpdated) // Nếu ảnh chữ ký đã được chọn mới
                    {
                        // Lưu ảnh chữ ký mới và lấy tên tệp
                        chuKyFileName = SaveImageToFolderChuKy(pictureBoxChuKy.Image, maAdmin, hoTen);
                        isChuKyUpdated = true; // Đánh dấu rằng ảnh đã được cập nhật
                    }
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
                    ChuKy = isChuKyUpdated ? chuKyFileName : null, // Cập nhật ảnh chữ ký nếu có
                    IdUser = idUser
                };

                // Gọi phương thức cập nhật thông tin trong BLL
                bool isUpdated = thongTinAdminBLL.CapNhatThongTinAdmin(adminInfo);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin Admin thành công!");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin Admin thất bại!");
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

        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {

        }
    }
}
