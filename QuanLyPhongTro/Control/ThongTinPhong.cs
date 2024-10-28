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
    public partial class ThongTinPhong : UserControl
    {

        public string MAPHONG { get; set; }
        public ThongTinPhongBLL thongtinphongBLL = new ThongTinPhongBLL();
        public ThongTinPhongDAL thongTinPhongDAL = new ThongTinPhongDAL();
        public TaoQuanLyPhongDTO taoquanliphongDTO = new TaoQuanLyPhongDTO();
        public string KhuVuc { get; set; }
        public DataTable DichVuPhongTable { get; set; }

        public ThongTinPhong()
        {
            InitializeComponent();
            thongTinPhongDAL = new ThongTinPhongDAL(); // Initialize DAL
            thongtinphongBLL = new ThongTinPhongBLL();
            this.KhuVuc = region; // Set the KhuVuc property
        }
        public void SetRegion(string region)
        {
            this.KhuVuc = region;
        }



        private string region;

        public void Set(string region)
        {
            this.region = region;
        }

        // Phương thức để cập nhật thông tin phòng từ DataTable
        public void UpdateThongTinPhong(DataRow dataRow)
        {   
            if (dataRow != null)
            {
                taoquanliphongDTO.MaPhong = dataRow["MaPhong"].ToString();
                taoquanliphongDTO.TenPhong = dataRow["TenPhong"].ToString();
                taoquanliphongDTO.TienPhong = Convert.ToSingle(dataRow["TienPhong"]);
                taoquanliphongDTO.DienTich = Convert.ToSingle(dataRow["DienTich"]);
                taoquanliphongDTO.Dien = Convert.ToSingle(dataRow["Dien"]);
                taoquanliphongDTO.Nuoc = Convert.ToSingle(dataRow["Nuoc"]);
                taoquanliphongDTO.TienCoc = Convert.ToSingle(dataRow["TienCoc"]);
                taoquanliphongDTO.HanTro = dataRow["HanTro"] != DBNull.Value ? Convert.ToDateTime(dataRow["HanTro"]) : (DateTime?)null;
                taoquanliphongDTO.GhiChu = dataRow["GhiChu"].ToString();
            }
        }


        private void LoadDichVu()
        {
            var dichVuBLL = new QuanLyDichVuBLL();
            DataTable dichVuData = dichVuBLL.GetDichVuFormQLPhong();

            foreach (DataRow item in dichVuData.Rows)
            {
                dataGridViewDichVu1.Rows.Add(1, item["TenDichVu"], item["DonGia"], item["MaDichVu"]);
            }
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLiPhong f = new QuanLiPhong();
                f.khuvuc = this.KhuVuc; // Truyền lại giá trị khu vực
                mainForm.ShowControl(f);
            }
        }





        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLiPhong quanLiPhongControl = new QuanLiPhong
                {
                    khuvuc = this.KhuVuc // Truyền lại giá trị khu vực
                };
                mainForm.ShowControl(quanLiPhongControl);
            }
        }

        private void dataGridViewDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ThongTinPhong_Load(object sender, EventArgs e)
        {
            txtMaPhong.Text = MAPHONG;
            LoadPhong();
            LoadDichVuByMaPhong(MAPHONG);
            buttonLuu.Enabled = false;
            
            txtMaPhong.Enabled = false;
            txtTenPhong.Enabled = false;
            txtSodien.Enabled = false;
            txtDienTich.Enabled = false;
            txtSoNuoc.Enabled = false;
            RtxtGhiChu.Enabled = false;
            dateTimePickerHanTro.Enabled = false;
            textBoxTienCoc.Enabled = false;
            textBoxTienPhong.Enabled = false;
        }
   

        private void LoadPhong()
        {
            DataTable dt = thongtinphongBLL.GetAllPhong(MAPHONG);
            if (dt.Rows.Count > 0)
            {
                txtMaPhong.Text = dt.Rows[0]["MaPhong"].ToString();
                txtTenPhong.Text = dt.Rows[0]["TenPhong"].ToString();
                textBoxTienPhong.Text = dt.Rows[0]["TienPhong"].ToString();
                txtDienTich.Text = dt.Rows[0]["DienTich"].ToString(); // Ensure DienTich is loaded
                txtSodien.Text = dt.Rows[0]["Dien"].ToString();
                txtSoNuoc.Text = dt.Rows[0]["Nuoc"].ToString();
                textBoxTienCoc.Text = dt.Rows[0]["TienCoc"].ToString();
                dateTimePickerHanTro.Text = dt.Rows[0]["HanTro"].ToString();
                RtxtGhiChu.Text = dt.Rows[0]["GhiChu"].ToString();
            }
        }

        private List<DichVuPhongDTO> chuyendoidichvu()
        {
            List<DichVuPhongDTO> lst = new List<DichVuPhongDTO>();

            // Duyệt qua tất cả các dòng trong DataGridView
            foreach (DataGridViewRow row in dataGridViewDichVu1.Rows)
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

        private void LoadDichVuByMaPhong(string maPhong)
        {
            DataTable allDichVuData = thongtinphongBLL.GetAllDichVu();
            DataTable registeredDichVuData = thongtinphongBLL.GetDichVuByMaPhong(maPhong);

            HashSet<string> registeredDichVuMa = new HashSet<string>();
            foreach (DataRow row in registeredDichVuData.Rows)
            {
                registeredDichVuMa.Add(row["MaDichVu"].ToString());
            }

            dataGridViewDichVu1.Rows.Clear();

            foreach (DataRow item in allDichVuData.Rows)
            {
                int rowIndex = dataGridViewDichVu1.Rows.Add();
                DataGridViewRow row = dataGridViewDichVu1.Rows[rowIndex];

                row.Cells["chon"].Value = registeredDichVuMa.Contains(item["MaDichVu"].ToString());
                row.Cells["TenDichVu"].Value = item["TenDichVu"];
                row.Cells["DonGia"].Value = item["DonGia"];
                row.Cells["MaDichVu"].Value = item["MaDichVu"];
            }
        }
        //-----------------------------------------------------------------------------------------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
           buttonLuu.Enabled =  true;

            btnUpdate.Enabled = false;

            //txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtSodien.Enabled = true;
            txtSoNuoc.Enabled = true;
            txtDienTich.Enabled = true;
            RtxtGhiChu.Enabled = true;
            dateTimePickerHanTro.Enabled = true;
            textBoxTienCoc.Enabled = true;
            textBoxTienPhong.Enabled = true;
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {

            //// Kiểm tra giá trị của các trường nhập liệu
            //if (string.IsNullOrEmpty(txtMaPhong.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            //{
            //    MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!");
            //    return;
            //}

            //// Hiển thị hộp thoại xác nhận
            //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin phòng?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //if (result == DialogResult.Yes)
            //{
            //    TaoQuanLyPhongDTO phong = new TaoQuanLyPhongDTO
            //    {
            //        MaPhong = txtMaPhong.Text,
            //        MaKhuVuc = KhuVuc,
            //        TenPhong = txtTenPhong.Text,
            //        NgayVao = DateTime.Now, // Hoặc giá trị khác
            //        TienCoc = Convert.ToSingle(textBoxTienCoc.Text),
            //        TienPhong = Convert.ToSingle(textBoxTienPhong.Text),
            //        Dien = Convert.ToSingle(txtSodien.Text),
            //        Nuoc = Convert.ToSingle(txtSoNuoc.Text),
            //        CongNo = 0, // Hoặc giá trị khác
            //        HanTro = dateTimePickerHanTro.Value, // Lấy giá trị từ DateTimePicker
            //        TrangThai = true, // Hoặc giá trị khác
            //        GhiChu = RtxtGhiChu.Text
            //    };

            //    // Kiểm tra giá trị hạn trọ
            //    MessageBox.Show($"Giá trị hạn trọ: {phong.HanTro}");

            //    // Cập nhật thông tin phòng
            //    try
            //    {
            //        thongtinphongBLL.UpdatePhong(phong);
            //        // Cập nhật thông tin dịch vụ
            //        List<DichVuPhongDTO> dichVuPhongs = chuyendoidichvu();
            //        thongtinphongBLL.UpdateDichVuPhong(phong.MaPhong, dichVuPhongs);
            //        MessageBox.Show($"Giá trị hạn trọ: {dateTimePickerHanTro.Value}");
            //        MessageBox.Show("Cập nhật thông tin phòng thành công!");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Cập nhật không thành công: " + ex.Message);
            //    }
            //}


            // Kiểm tra giá trị của các trường nhập liệu
            if (string.IsNullOrEmpty(txtMaPhong.Text) || string.IsNullOrEmpty(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin phòng!");
                return;
            }

            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin phòng?", "Xác nhận cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DateTime? hanTro = dateTimePickerHanTro.Value;
                if (hanTro == dateTimePickerHanTro.MinDate) // Kiểm tra nếu người dùng không chọn ngày hạn trọ
                {
                    hanTro = null; // Đặt giá trị null để trigger tự động cập nhật
                }

                TaoQuanLyPhongDTO phong = new TaoQuanLyPhongDTO
                {
                    MaPhong = txtMaPhong.Text,
                    MaKhuVuc = KhuVuc,
                    TenPhong = txtTenPhong.Text,
                    NgayVao = DateTime.Now, // Hoặc giá trị khác
                    TienCoc = Convert.ToSingle(textBoxTienCoc.Text),
                    DienTich   = Convert.ToSingle(txtDienTich.Text),
                    TienPhong = Convert.ToSingle(textBoxTienPhong.Text),
                    Dien = Convert.ToSingle(txtSodien.Text),
                    Nuoc = Convert.ToSingle(txtSoNuoc.Text),
                    CongNo = 0, // Hoặc giá trị khác
                    HanTro = hanTro, // Sử dụng giá trị từ DateTimePicker
                    TrangThai = true, // Hoặc giá trị khác
                    GhiChu = RtxtGhiChu.Text
                };

                // Kiểm tra giá trị hạn trọ
                MessageBox.Show($"Giá trị hạn trọ: {phong.HanTro}");

                // Cập nhật thông tin phòng
                try
                {
                    thongtinphongBLL.UpdatePhong(phong);
                    // Cập nhật thông tin dịch vụ
                    List<DichVuPhongDTO> dichVuPhongs = chuyendoidichvu();
                    thongtinphongBLL.UpdateDichVuPhong(phong.MaPhong, dichVuPhongs);
                    MessageBox.Show($"Giá trị hạn trọ: {dateTimePickerHanTro.Value}");
                    MessageBox.Show("Cập nhật thông tin phòng thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cập nhật không thành công: " + ex.Message);
                }
            }
        }
    }
}
