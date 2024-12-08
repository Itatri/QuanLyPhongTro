using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{

    public partial class QuanLiFeedBack : UserControl
    {

        public string makhuvuc {  get; set; }
        private FeedBackBLL feedbackBLL = new FeedBackBLL();
        public QuanLiFeedBack()
        {
            InitializeComponent();

            setConboboxTrangThai();
            // Gắn sự kiện CellContentClick
            dataGridViewFeedBack.CellContentClick += dataGridViewFeedBack_CellContentClick;
        }

        private void setConboboxTrangThai()
        {
            // Thêm các lựa chọn vào comboBoxTrangThai
            comboboxTrangThai.Items.Add("Đã phản hồi");
            comboboxTrangThai.Items.Add("Chưa phản hồi");
            comboboxTrangThai.SelectedIndex = 0; // Chọn mặc định là "Đã phản hồi"
        }
        private void LoadData()
        {
            var danhSachFeedBack = feedbackBLL.LayTatCaFeedBack(makhuvuc);
            dataGridViewFeedBack.DataSource = danhSachFeedBack;

            // Đặt lại tên các cột
            dataGridViewFeedBack.Columns["MaFB"].HeaderText = "Mã Feedback";
            dataGridViewFeedBack.Columns["MaFB"].Visible = false;
            dataGridViewFeedBack.Columns["MaPhong"].HeaderText = "Phòng";
            dataGridViewFeedBack.Columns["MoTa"].HeaderText = "Mô Tả";
            dataGridViewFeedBack.Columns["NgayGui"].HeaderText = "Ngày Gửi";
            dataGridViewFeedBack.Columns["PhanHoi"].HeaderText = "Phản Hồi";
            dataGridViewFeedBack.Columns["NgayPhanHoi"].HeaderText = "Ngày Phản Hồi";
            dataGridViewFeedBack.Columns["TrangThai"].HeaderText = "Trạng Thái";

            // Thiết lập tự động điều chỉnh kích thước cột
            dataGridViewFeedBack.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Kiểm tra nếu cột "Xem Chi Tiết" đã tồn tại thì không cần thêm lại
            if (dataGridViewFeedBack.Columns["btnXemChiTiet"] == null)
            {
                DataGridViewButtonColumn btnXemChiTiet = new DataGridViewButtonColumn
                {
                    Name = "btnXemChiTiet",
                    HeaderText = "Hành động",
                    Text = "Xem chi tiết",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat
                };
                dataGridViewFeedBack.Columns.Add(btnXemChiTiet);
            }

            // Tùy chỉnh màu sắc cho cột nút
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.BackColor = Color.Black;
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.ForeColor = Color.White;
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionBackColor = Color.Green;
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void buttonTimKiemFeedBack_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefeshFB_Click(object sender, EventArgs e)
        {
            try
            {

                txtMaPhong.Text = string.Empty;
                txtNoiDungPhanHoi.Text = string.Empty;
                txtPhanHoi.Text = string.Empty;
                // Gọi phương thức để tải toàn bộ dữ liệu và cập nhật DataGridView
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể làm mới dữ liệu: " + ex.Message);
            }
        }

        private void dataGridViewFeedBack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem cột được nhấn có phải là cột "Xem Chi Tiết" không
            if (e.ColumnIndex == dataGridViewFeedBack.Columns["btnXemChiTiet"].Index && e.RowIndex >= 0)
            {
                // Lấy dữ liệu của dòng hiện tại
                string maPhong = dataGridViewFeedBack.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();
                string moTa = dataGridViewFeedBack.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
                string PhanHoi = dataGridViewFeedBack.Rows[e.RowIndex].Cells["PhanHoi"].Value.ToString();
                // Hiển thị dữ liệu lên các TextBox
                txtMaPhong.Text = maPhong;
                txtNoiDungPhanHoi.Text = moTa;
                txtPhanHoi.Text = PhanHoi;
            }

        }


        private void txtHoTenCuDan_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelThongTinDanCu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLoc_Click(object sender, EventArgs e)
        {

            try
            {
                string selectedValue = comboboxTrangThai.SelectedItem.ToString();
                List<FeedBackDTO> filteredList;

                // Kiểm tra xem người dùng chọn "Đã phản hồi" hay "Chưa phản hồi"
                if (selectedValue == "Đã phản hồi")
                {
                    filteredList = feedbackBLL.LocFeedBack(1); // Truyền vào giá trị 1 để lọc
                }
                else
                {
                    filteredList = feedbackBLL.LocFeedBack(0); // Truyền vào giá trị 0 để lọc
                }

                // Nếu danh sách phản hồi null thì khởi tạo một danh sách rỗng
                if (filteredList == null)
                {
                    filteredList = new List<FeedBackDTO>();
                }

                // Hiển thị dữ liệu vào DataGridView
                dataGridViewFeedBack.DataSource = filteredList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lọc thông tin: " + ex.Message);
            }
        }

        private void btnGuiPhanHoi_Click(object sender, EventArgs e)
        {


            try
            {
                // Lấy Mã Feedback từ dòng hiện tại trong DataGridView
                if (dataGridViewFeedBack.CurrentRow != null)
                {
                    string maFB = dataGridViewFeedBack.CurrentRow.Cells["MaFB"].Value.ToString();
                    string phanHoi = txtPhanHoi.Text.Trim();

                    if (!string.IsNullOrEmpty(phanHoi))
                    {
                        // Lấy thời gian hiện tại
                        DateTime ngayPhanHoi = DateTime.Now;

                        // Gọi phương thức cập nhật phản hồi
                        bool result = feedbackBLL.CapNhatPhanHoi(maFB, phanHoi, ngayPhanHoi);

                        if (result)
                        {
                            MessageBox.Show("Phản hồi đã được gửi thành công.");
                            // Làm mới lại dữ liệu trong DataGridView
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Không thể gửi phản hồi. Vui lòng thử lại.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập nội dung phản hồi.");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phản hồi để gửi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi phản hồi: " + ex.Message);
            }
        }

        private void dataGridViewFeedBack_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewFeedBack.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value.ToString() == "1")
                {
                    e.Value = "Đã phản hồi";
                    e.CellStyle.ForeColor = Color.Green; // Màu xanh
                    e.CellStyle.Font = new Font(dataGridViewFeedBack.Font, FontStyle.Bold);
                }
                else if (e.Value.ToString() == "0" || e.Value == DBNull.Value)
                {
                    e.Value = "Chưa phản hồi";
                    e.CellStyle.ForeColor = Color.Red; // Màu đỏ
                    e.CellStyle.Font = new Font(dataGridViewFeedBack.Font, FontStyle.Bold);
                }
            }
        }

        private void dataGridViewFeedBack_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewFeedBack.CurrentRow != null)
            {
                int trangThai = Convert.ToInt32(dataGridViewFeedBack.CurrentRow.Cells["TrangThai"].Value);

                if (trangThai == 1)
                {
                    txtPhanHoi.ReadOnly = true; // Chỉ đọc
                    txtPhanHoi.BackColor = Color.WhiteSmoke; // Màu nền xanh lá
                }
                else
                {
                    txtPhanHoi.ReadOnly = false; // Cho phép nhập
                    txtPhanHoi.BackColor = Color.White; // Màu nền mặc định
                }
            }
        }

        private void QuanLiFeedBack_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
