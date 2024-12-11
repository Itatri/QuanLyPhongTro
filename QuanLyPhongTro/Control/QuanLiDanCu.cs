using BLL;
using DTO;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLiDanCu : UserControl
    {
        private ThongTinKhachBLL thongTinKhachBLL = new ThongTinKhachBLL();
        private bool isAddingNew = false; // Biến trạng thái để theo dõi chế độ thêm mới hay sửa
        public string khuvuc { get; set; }

        public string makhuvucdancu { get; set; }
        private bool IsChuHo = false;
        public class Province
        {
            public string name { get; set; }
        }
        public QuanLiDanCu()
        {


            InitializeComponent();
            LoadProvinces();
        
            dataGridViewDanCu.CellFormatting += dataGridViewDanCu_CellFormatting;

            dataGridViewDanCu.CellClick += dataGridViewDanCu_CellClick;
            pictureBoxChuKy.SizeMode = PictureBoxSizeMode.Zoom;
            SetControlsEnabled(false);
            
            SetComboBoxGioiTinh();
            SetComboBoxTrangThai();
            PopulateComboBoxQuanHe();
        }

        private async void LoadProvinces()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetFromJsonAsync<Province[]>("https://provinces.open-api.vn/api/p");

                    if (response != null)
                    {
                        foreach (var province in response)
                        {
                            cbbQueQuan.Items.Add(province.name);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu tỉnh thành");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lấy danh sách tỉnh thành: {ex.Message}");
            }
        }

        private void SetComboBoxTrangThai()
        {
            comboBoxTrangThai.Items.Clear();
            comboBoxTrangThai.Items.Add("Đang cư trú"); // Chỉ mục 0
            comboBoxTrangThai.Items.Add("Đã rời đi"); // Chỉ mục 1
        }

        private void PopulateComboBoxQuanHe()
        {
            comboboxQuanHe.Items.Clear();
            comboboxQuanHe.Items.AddRange(new string[]
            {
                "Anh",
                "Anh chồng",
                "Anh họ",
                "Anh rể",
                "Anh ruột",
                "Anh vợ",
                "Anh",
                "Ba",
                "Bà",
                "Bà ngoại",
                "Bà nội",
                "Bác",
                "Bạn",
                "Bố",
                "Cha",
                "Cha chồng",
                "Cha đẻ",
                "Cha nuôi",
                "Cha vợ",
                "Cháu",
                "Cháu dâu",
                "Cháu họ",
                "Cháu ngoại",
                "Cháu nội",
                "Cháu rể",
                "Chắt",
                "Chị",
                "Chị chồng",
                "Chị dâu",
                "Chị họ",
                "Chị ruột",
                "Chị vợ",
                "Chồng",
                "Chủ hộ",
                "Chưa có thông tin",
                "Con",
                "Con chồng",
                "Con dâu",
                "Con đẻ",
                "Con nuôi",
                "Con rể",
                "Con vợ",
                "Cô",
                "Cụ",
                "Cùng ở thuê",
                "Dì",
                "Đồng nghiệp - CA",
                "Đồng nghiệp - QĐ",
                "Em",
                "Em chồng",
                "Em dâu",
                "Em họ",
                "Em rể",
                "Em ruột",
                "Em vợ",
                "Khác",
                "Mẹ",
                "Mẹ chồng",
                "Mẹ đẻ",
                "Mẹ nuôi",
                "Mẹ vợ",
                "Người được chăm sóc",
                "Người được giám hộ",
                "Người được nuôi dưỡng",
                "Người được trợ giúp",
                "Người giám hộ",
                "Người mượn nhà",
                "Người ở nhờ",
                "Người thuê nhà",
                "Nhân khẩu tập thể",
                "Ông",
                "Ông ngoại",
                "Ông nội",
                "Thím",
                "Tía",
                "Vợ"
            });


            comboboxQuanHe.SelectedIndex = -1;
        }



        private void LoadPhongComboBox()
        {
            QuanLiPhongBLL phongBLL = new QuanLiPhongBLL();
            DataTable dt = phongBLL.LayTatCaPhong(khuvuc);
            comboBoxPhong.DisplayMember = "TenPhong";
            comboBoxPhong.ValueMember = "MaPhong";

            comboBoxLocPhong.DisplayMember = "TenPhong";
            comboBoxLocPhong.ValueMember = "MaPhong";

            var phongList = dt.AsEnumerable()
                              .Select(row => new
                              {
                                  MaPhong = row.Field<string>("MaPhong"),
                                  DisplayText = $"{row.Field<string>("MaPhong")} - {row.Field<string>("TenPhong")}"
                              }).ToList();

            comboBoxPhong.DataSource = phongList;
            comboBoxPhong.DisplayMember = "DisplayText";
            comboBoxPhong.ValueMember = "MaPhong";

            comboBoxLocPhong.DataSource = phongList;
            comboBoxLocPhong.DisplayMember = "DisplayText";
            comboBoxLocPhong.ValueMember = "MaPhong";
        }

        private void SetComboBoxGioiTinh()
        {
            comboBoxGioiTinh.Items.Add("Nam");
            comboBoxGioiTinh.Items.Add("Nữ");
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtMaCuDan.Enabled = enabled;
            txtHoTenCuDan.Enabled = enabled;
            comboBoxGioiTinh.Enabled = enabled;
            txtCCCD.Enabled = enabled;
            txtSDT.Enabled = enabled;
            cbbQueQuan.Enabled = enabled;

            comboBoxTrangThai.Enabled = enabled;
            comboBoxPhong.Enabled = enabled;
            dateTimePickerNgaySinh.Enabled = enabled;
            //buttonChonAnh.Enabled = enabled;
            buttonChonChuKy.Enabled = enabled;
            txtEmail.Enabled = enabled;
            txtNoiCap.Enabled = enabled;
            dateTimePickerNgayCap.Enabled = enabled;
            //txtQuanHe.Enabled = enabled;
            comboboxQuanHe.Enabled = enabled;
            txtThuongTru.Enabled = enabled;
        }



        private void LoadData()
        {
            //var danhSachKhach = thongTinKhachBLL.LayTatCaThongTinKhach1(makhuvucdancu);
            var danhSachKhach = thongTinKhachBLL.LayTatCaThongTinKhach(khuvuc);
            dataGridViewDanCu.DataSource = danhSachKhach;

            dataGridViewDanCu.Columns["MaKhachTro"].HeaderText = "Mã Cư Dân";
            dataGridViewDanCu.Columns["MaKhachTro"].Visible = false;
            dataGridViewDanCu.Columns["ChuKy"].HeaderText = "Chữ Ký";
            dataGridViewDanCu.Columns["ChuKy"].Visible = false;
            dataGridViewDanCu.Columns["HoTen"].HeaderText = "Họ Tên";
            dataGridViewDanCu.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridViewDanCu.Columns["Cccd"].HeaderText = "Số Căn Cước";
            dataGridViewDanCu.Columns["Phone"].HeaderText = "Số Điện Thoại";
            dataGridViewDanCu.Columns["QueQuan"].HeaderText = "Quê Quán";
            dataGridViewDanCu.Columns["QuanHe"].HeaderText = "Quan Hệ";
            dataGridViewDanCu.Columns["MaPhong"].HeaderText = "Phòng";
            dataGridViewDanCu.Columns["Email"].HeaderText = "Email";
            dataGridViewDanCu.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dataGridViewDanCu.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridViewDanCu.Columns["NgayCap"].HeaderText = "Ngày Cấp";
            dataGridViewDanCu.Columns["NoiCap"].HeaderText = "Nơi Cấp";
            dataGridViewDanCu.Columns["ThuongTru"].HeaderText = "Thường Trú";
            if (dataGridViewDanCu.Columns["TrangThai"] == null)
            {
                DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
                colTrangThai.Name = "TrangThai";
                colTrangThai.HeaderText = "Trạng Thái";
                colTrangThai.DataPropertyName = "TrangThai"; 
                dataGridViewDanCu.Columns.Add(colTrangThai);
            }

            if (dataGridViewDanCu.Columns["btnXemChiTiet"] == null)
            {
                DataGridViewButtonColumn btnXemChiTiet = new DataGridViewButtonColumn();
                btnXemChiTiet.Name = "btnXemChiTiet";
                btnXemChiTiet.HeaderText = "Hành Động";
                btnXemChiTiet.Text = "Xem Chi Tiết";
                btnXemChiTiet.UseColumnTextForButtonValue = true;
                btnXemChiTiet.FlatStyle = FlatStyle.Flat;
                dataGridViewDanCu.Columns.Add(btnXemChiTiet);
            }

            dataGridViewDanCu.Columns["btnXemChiTiet"].DisplayIndex = dataGridViewDanCu.Columns.Count - 1;

            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.BackColor = Color.Black;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.ForeColor = Color.White;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionBackColor = Color.Green;
            dataGridViewDanCu.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridViewDanCu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }





        private void buttonThemCD_Click(object sender, EventArgs e)
        {
            isAddingNew = true;

            SetControlsEnabled(true);

            string MaKhachMoiNhat = thongTinKhachBLL.DemSoLuongKhach();
            int soLuongKhach = int.Parse(MaKhachMoiNhat.Substring(2));
            // Tạo mã khách trọ mới với tiền tố "KH" + STT (e.g., KH001)
            string maCuDanMoi = "KH" + (soLuongKhach + 1).ToString("D3");

            txtMaCuDan.Text = maCuDanMoi;
            txtMaCuDan.Enabled = false; 
            txtHoTenCuDan.Clear();
            comboBoxGioiTinh.SelectedIndex = -1;
            txtCCCD.Clear();
            txtSDT.Clear();
            cbbQueQuan.SelectedIndex = -1;
            comboBoxTrangThai.SelectedIndex = -1;
            comboBoxPhong.SelectedIndex = -1;
            dateTimePickerNgaySinh.Value = DateTime.Now;
            ////pictureBoxAnhCuDan.Image = null; // Xóa ảnh cũ
            pictureBoxChuKy.Image = null; // Xóa ảnh cũ
            labelTenAnhChuKy.Text = "AnhChuKy.jpg";
            txtEmail.Clear();
            txtNoiCap.Clear();
            dateTimePickerNgayCap.Value = DateTime.Now;
            //txtQuanHe.Clear();
            comboboxQuanHe.SelectedIndex = -1;
            txtThuongTru.Clear();
            comboBoxTrangThai.SelectedIndex = 0;

        }

        private void buttonXoaCD_Click(object sender, EventArgs e)
        {
            try
            {
                string maKhachTro = txtMaCuDan.Text;

                if (!string.IsNullOrEmpty(maKhachTro))
                {
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);

                        if (currentCustomer != null)
                        {

                            if (!string.IsNullOrEmpty(currentCustomer.ChuKy))
                            {
                                string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
                                string imagesFolderPathChuKy = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;
                                string filePathChuKy = Path.Combine(imagesFolderPathChuKy, currentCustomer.ChuKy);

                                pictureBoxChuKy.Image?.Dispose();
                                pictureBoxChuKy.Image = null;

                                GC.Collect(); 
                                GC.WaitForPendingFinalizers(); 

                                if (File.Exists(filePathChuKy))
                                {
                                    File.Delete(filePathChuKy);
                                }
                            }
                            thongTinKhachBLL.XoaThongTinKhach(maKhachTro);

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
            isAddingNew = false; 
            SetControlsEnabled(true);
            txtMaCuDan.Enabled = false;



            
            if (pictureBoxChuKy.Image != null)
            {
                pictureBoxChuKy.Image.Dispose();
                pictureBoxChuKy.Image = null;
            }
        }



        private void buttonLuuCD_Click(object sender, EventArgs e)
        {



            ////try
            ////{
            ////    string maKhachTro = txtMaCuDan.Text;
            ////    string hoTen = txtHoTenCuDan.Text;
            ////    string gioiTinh = comboBoxGioiTinh.Text;
            ////    string cccd = txtCCCD.Text;
            ////    string phone = txtSDT.Text;
            ////    string queQuan =  cbbQueQuan.Text;
            ////    int trangThai = (comboBoxTrangThai.SelectedItem.ToString() == "Đã rời đi") ? 0 : 1;
            ////    //string maPhong = comboBoxPhong.SelectedValue.ToString();
            ////    DateTime ngaySinh = dateTimePickerNgaySinh.Value;
            ////    string email = txtEmail.Text;
            ////    string noiCap = txtNoiCap.Text;
            ////    DateTime ngayCap = dateTimePickerNgayCap.Value;
            ////    string quanHe = txtQuanHe.Text;
            ////    string newImagePathChuKy = null;
            ////    // Lấy giá trị maPhong nếu SelectedValue không phải là null
            ////    string maPhong = comboBoxPhong.SelectedValue != null ? comboBoxPhong.SelectedValue.ToString() : null;

            ////    // Check if a new signature image is selected in pictureBoxChuKy
            ////    if (pictureBoxChuKy.Image != null)
            ////    {
            ////        using (MemoryStream ms = new MemoryStream())
            ////        {
            ////            pictureBoxChuKy.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ////            using (Image img = Image.FromStream(ms))
            ////            {
            ////                newImagePathChuKy = SaveImageToFolderChuKy(img, maKhachTro, hoTen);
            ////            }
            ////        }
            ////    }

            ////    var khachDTO = new ThongTinKhachDTO
            ////    {
            ////        MaKhachTro = maKhachTro,
            ////        HoTen = hoTen,
            ////        GioiTinh = gioiTinh,
            ////        CCCD = cccd,
            ////        Phone = phone,
            ////        QueQuan = queQuan,
            ////        TrangThai = trangThai,
            ////        MaPhong = maPhong,
            ////        NgaySinh = ngaySinh,
            ////        ChuKy = newImagePathChuKy, // Set the new signature path
            ////        Email = email,
            ////        NoiCap = noiCap,
            ////        NgayCap = ngayCap,
            ////        QuanHe = quanHe
            ////    };

            ////    if (!isAddingNew)
            ////    {
            ////        // If editing and no new image was selected, keep the existing ChuKy
            ////        var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);
            ////        if (currentCustomer != null && newImagePathChuKy == null)
            ////        {
            ////            khachDTO.ChuKy = currentCustomer.ChuKy;
            ////        }

            ////        // Update the customer's information
            ////        thongTinKhachBLL.CapNhatThongTinKhach(khachDTO);
            ////    }
            ////    else
            ////    {
            ////        // Add the new customer information
            ////        thongTinKhachBLL.ThemThongTinKhach(khachDTO);
            ////    }

            ////    LoadData();
            ////    MessageBox.Show("Thông tin đã được lưu thành công.");
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show("Không thể lưu thông tin: " + ex.Message);
            ////}
            ///

            try
            {
                string maKhachTro = txtMaCuDan.Text;
                string hoTen = txtHoTenCuDan.Text;
                string gioiTinh = comboBoxGioiTinh.Text;
                string cccd = txtCCCD.Text;
                string phone = txtSDT.Text;
                string queQuan = cbbQueQuan.Text;
                int trangThai = (comboBoxTrangThai.SelectedItem.ToString() == "Đã rời đi") ? 0 : 1;
                DateTime ngaySinh = dateTimePickerNgaySinh.Value;
                string email = txtEmail.Text;
                string noiCap = txtNoiCap.Text;
                DateTime ngayCap = dateTimePickerNgayCap.Value;
                //string quanHe = txtQuanHe.Text;
                string quanHe = comboboxQuanHe.SelectedItem?.ToString();
                string thuongtru = txtThuongTru.Text;
                string newImagePathChuKy = null;
                string maPhong = comboBoxPhong.SelectedValue != null ? comboBoxPhong.SelectedValue.ToString() : null;
                if (string.IsNullOrEmpty(maPhong))
                {
                    MessageBox.Show("Chưa chọn phòng!"); return;
                }
                if (string.IsNullOrEmpty(gioiTinh))
                {
                    MessageBox.Show("Chưa nhập giới tính"); return;
                }
                if (string.IsNullOrEmpty(maPhong))
                {
                    MessageBox.Show("Chưa chọn phòng!"); return;
                }
                if (string.IsNullOrEmpty(cccd))
                {
                    MessageBox.Show("Chưa nhập căn cước công dân"); return;
                }
                if (KiemTraSoDienThoai(phone) == false)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!"); return;
                }
                if (KiemTraEmail(email) == false)
                {
                    MessageBox.Show("Email không hợp lệ!"); return;
                }
                if (string.IsNullOrEmpty(quanHe))
                {
                    MessageBox.Show("Chưa chọn quan hệ"); return;
                }
                if (string.IsNullOrEmpty(thuongtru))
                {
                    MessageBox.Show("Chưa nhập thương trú"); return;
                }
                if (KiemTraCCCD(cccd) == false)
                {
                    MessageBox.Show("Căn cước công dân không hợp lệ!");
                    return;
                }

                // Chỉ lưu ảnh chữ ký nếu có ảnh trong pictureBoxChuKy
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
                    Email = email,
                    NoiCap = noiCap,
                    NgayCap = ngayCap,
                    QuanHe = quanHe,
                    ThuongTru = thuongtru
                };

                // Nếu có đường dẫn ảnh chữ ký mới, thêm vào DTO
                if (newImagePathChuKy != null)
                {
                    khachDTO.ChuKy = newImagePathChuKy;
                }

                if (!isAddingNew)
                {
                    // Nếu đang cập nhật và không có ảnh mới, giữ nguyên ảnh cũ
                    var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);
                    if (currentCustomer != null && newImagePathChuKy == null)
                    {
                        khachDTO.ChuKy = currentCustomer.ChuKy;
                    }

                    // Cập nhật thông tin khách hàng
                    thongTinKhachBLL.CapNhatThongTinKhach(khachDTO);
                }
                else
                {
                    if (IsChuHo == true && quanHe == "Chủ hộ")
                    {
                        MessageBox.Show("Phòng đã có chủ hộ! Một phòng chỉ có 1 chủ hộ!");
                        return;
                    }
                    // Thêm thông tin khách hàng mới
                    thongTinKhachBLL.ThemThongTinKhach(khachDTO);
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
                cbbQueQuan.Text = selectedRow.Cells["QueQuan"].Value.ToString();
                labelTenAnhChuKy.Text = selectedRow.Cells["ChuKy"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                //txtQuanHe.Text = selectedRow.Cells["QuanHe"].Value.ToString();
                comboboxQuanHe.Text = selectedRow.Cells["QuanHe"].Value.ToString();
                txtThuongTru.Text = selectedRow.Cells["ThuongTru"].Value.ToString();
                txtNoiCap.Text = selectedRow.Cells["NoiCap"].Value.ToString(); // Assuming "NoiCap" is a column in your DataGridView.

                //if (selectedRow.Cells["NgayCap"].Value != DBNull.Value)
                //{
                //    dateTimePickerNgayCap.Value = (DateTime)selectedRow.Cells["NgayCap"].Value;
                //}
                //else
                //{
                //    dateTimePickerNgayCap.Value = DateTime.Now;
                //}

                //if (selectedRow.Cells["NgayCap"].Value != DBNull.Value)
                //{
                //    if (selectedRow.Cells["NgayCap"].Value != null && selectedRow.Cells["NgayCap"].Value.ToString() != string.Empty)
                //    {
                //        dateTimePickerNgayCap.Value = (DateTime)selectedRow.Cells["NgayCap"].Value;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Ngày cấp rỗng. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        dateTimePickerNgayCap.Value = DateTime.Now;
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Ngày cấp rỗng. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    dateTimePickerNgayCap.Value = DateTime.Now;
                //}


                //int trangThaiValue = Convert.ToInt32(selectedRow.Cells["TrangThai"].Value);
                //comboBoxTrangThai.SelectedIndex = (trangThaiValue == 0) ? 1 : 0;

                //comboBoxPhong.Text = selectedRow.Cells["MaPhong"].Value.ToString();

                //if (selectedRow.Cells["NgaySinh"].Value != DBNull.Value)
                //{
                //    dateTimePickerNgaySinh.Value = (DateTime)selectedRow.Cells["NgaySinh"].Value;
                //}
                //else
                //{
                //    dateTimePickerNgaySinh.Value = DateTime.Now;
                //}

                if (selectedRow.Cells["NgayCap"].Value != null && selectedRow.Cells["NgayCap"].Value != DBNull.Value)
                {
                    dateTimePickerNgayCap.Value = (DateTime)selectedRow.Cells["NgayCap"].Value;
                }
                else
                {
                    dateTimePickerNgayCap.Value = DateTime.Now;
                }


                int trangThaiValue = Convert.ToInt32(selectedRow.Cells["TrangThai"].Value);
                comboBoxTrangThai.SelectedIndex = (trangThaiValue == 0) ? 1 : 0;

                comboBoxPhong.Text = selectedRow.Cells["MaPhong"].Value.ToString();

                if (selectedRow.Cells["NgaySinh"].Value != null && selectedRow.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dateTimePickerNgaySinh.Value = (DateTime)selectedRow.Cells["NgaySinh"].Value;
                }
                else
                {
                    dateTimePickerNgaySinh.Value = DateTime.Now;
                }

                string imageChuKy = selectedRow.Cells["ChuKy"].Value.ToString();
                if (!string.IsNullOrEmpty(imageChuKy))
                {
                    string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
                    string imagesFolderPathChuKy = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;
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
        //        //string fileName = $"CK_{maKhachTro}_{hoTen}.jpg";
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




        private void buttonChonAnh_Click(object sender, EventArgs e)
        {

        }

        private void buttonChonChuKy_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //{
            //    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            //    openFileDialog.Title = "Chọn hình ảnh";

            //    if (openFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        // Ẩn ảnh mặc định trong PictureBox
            //        pictureBoxChuKy.Image = null;

            //        // Hiển thị ảnh trong PictureBox
            //        try
            //        {
            //            Image selectedImage = Image.FromFile(openFileDialog.FileName);
            //            pictureBoxChuKy.Image = selectedImage;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            //        }
            //    }
            //}

            try
            {
                string maKhachTro = txtMaCuDan.Text;
                var currentCustomer = thongTinKhachBLL.LayThongTinKhachTheoMa(maKhachTro);

                if (currentCustomer != null && !string.IsNullOrEmpty(currentCustomer.ChuKy))
                {
                    var result = MessageBox.Show("Xóa ảnh cũ để cập nhật ảnh mới?", "Xác nhận", MessageBoxButtons.OKCancel);

                    if (result == DialogResult.OK)
                    {
                        string baseDirectoryChuKy = AppDomain.CurrentDomain.BaseDirectory;
                        string imagesFolderPathChuKy = ConfigurationManager.ConnectionStrings["imagesPath"].ConnectionString;
                        string filePathChuKy = Path.Combine(imagesFolderPathChuKy, currentCustomer.ChuKy);

                        pictureBoxChuKy.Image?.Dispose();
                        pictureBoxChuKy.Image = null;

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        if (File.Exists(filePathChuKy))
                        {
                            File.Delete(filePathChuKy);
                        }

                        thongTinKhachBLL.CapNhatChuKyKhachHang(maKhachTro, null);

                        MessageBox.Show("Ảnh cũ đã được xóa. Vui lòng chọn ảnh mới.");
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            }
        }

        private void buttonTimKiemCuDan_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtTimKiemCuDan.Text.Trim();

                var searchResults = thongTinKhachBLL.TimKiemThongTinKhach(searchValue);

           
                dataGridViewDanCu.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void buttonRefesh_Click(object sender, EventArgs e)
        {
            SetControlsEnabled(false);

            try
            {
                txtTimKiemCuDan.Text = string.Empty;
                txtMaCuDan.Text = string.Empty;
                txtHoTenCuDan.Text = string.Empty;
                comboBoxGioiTinh.Text = null;
                txtCCCD.Text = string.Empty;
                cbbQueQuan.Text = string.Empty;
                comboBoxPhong.Text = null;
                comboBoxTrangThai.Text = null;
                txtSDT.Text = string.Empty;
                dateTimePickerNgaySinh.Text = string.Empty;
                labelTenAnhChuKy.Text = "AnhChuKy.jpg";
                pictureBoxChuKy.Image = null;

                LoadData();

                dataGridViewDanCu.Columns["btnXemChiTiet"].DisplayIndex = dataGridViewDanCu.Columns.Count - 1;
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

        private void QuanLiDanCu_Load(object sender, EventArgs e)
        {
            dateTimePickerNgayCap.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgayCap.CustomFormat = "dd/MM/yyyy";
            dateTimePickerNgaySinh.Format = DateTimePickerFormat.Custom;
            dateTimePickerNgaySinh.CustomFormat = "dd/MM/yyyy";
            LoadPhongComboBox();
            LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewDanCu_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonLoc_Click(object sender, EventArgs e)
        {


            string selectedMaPhong = comboBoxLocPhong.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(selectedMaPhong))
            {
                var danhSachKhach = thongTinKhachBLL.LayThongTinKhachTheoMaPhong(selectedMaPhong);

                if (danhSachKhach.Count == 0)
                {
                    MessageBox.Show("Phòng trống.");
                }
                else
                {
                    dataGridViewDanCu.DataSource = danhSachKhach;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để lọc.");
            }
        }

        private void dataGridViewDanCu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            if (dataGridViewDanCu.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value.ToString() == "1")
                {
                    e.Value = "Đang cư trú";
                    e.CellStyle.ForeColor = Color.Green; // Màu xanh
                    e.CellStyle.Font = new Font(dataGridViewDanCu.Font, FontStyle.Bold | FontStyle.Regular); // Chữ đậm
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 10, FontStyle.Bold); // Kích thước chữ lớn hơn
                }
                else if (e.Value.ToString() == "0")
                {
                    e.Value = "Đã rời đi";
                    e.CellStyle.ForeColor = Color.Red; // Màu đỏ
                    e.CellStyle.Font = new Font(dataGridViewDanCu.Font, FontStyle.Bold | FontStyle.Regular); // Chữ đậm
                    e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 10, FontStyle.Bold); // Kích thước chữ lớn hơn
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cbbQueQuan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPhong_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxPhong.SelectedIndex != -1)
            {
                //MessageBox.Show(comboBoxPhong.SelectedValue.ToString());
                IsChuHo = thongTinKhachBLL.CheckCoChuHoChua(comboBoxPhong.SelectedValue.ToString());
                //MessageBox.Show(IsChuHo.ToString());
                if (IsChuHo == false)
                {
                    comboboxQuanHe.Text = "Chủ hộ";
                }
                else
                {
                    comboboxQuanHe.Text = "Cùng ở thuê";
                }
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
    }
}
