using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLiFeedBack : UserControl
    {
        private FeedBackBLL feedbackBLL = new FeedBackBLL();
        public QuanLiFeedBack()
        {
            InitializeComponent();
            LoadData();
            // Gắn sự kiện CellContentClick
            dataGridViewFeedBack.CellContentClick += dataGridViewFeedBack_CellContentClick;
        }
        private void LoadData()
        {
            var danhSachFeedBack = feedbackBLL.LayTatCaFeedBack();
            dataGridViewFeedBack.DataSource = danhSachFeedBack;

            // Đặt lại tên các cột
            dataGridViewFeedBack.Columns["MaFB"].HeaderText = "Mã Feedback";
            dataGridViewFeedBack.Columns["MaPhong"].HeaderText = "Mã Phòng";
            dataGridViewFeedBack.Columns["MoTa"].HeaderText = "Mô Tả";

            // Thiết lập tự động điều chỉnh kích thước cột
            dataGridViewFeedBack.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Kiểm tra nếu cột "Xem Chi Tiết" đã tồn tại thì không cần thêm lại
            if (dataGridViewFeedBack.Columns["btnXemChiTiet"] == null)
            {
                // Thêm cột nút vào DataGridView
                DataGridViewButtonColumn btnXemChiTiet = new DataGridViewButtonColumn();
                btnXemChiTiet.Name = "btnXemChiTiet";
                btnXemChiTiet.HeaderText = "Hành động";
                btnXemChiTiet.Text = "Xem chi tiết";
                btnXemChiTiet.UseColumnTextForButtonValue = true; // Hiển thị văn bản trong tất cả các dòng
                btnXemChiTiet.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng cho nút
                dataGridViewFeedBack.Columns.Add(btnXemChiTiet);
            }

            // Tùy chỉnh màu sắc cho cột nút
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.BackColor = Color.Black;
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.ForeColor = Color.White;
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionBackColor = Color.Green; // Màu khi được chọn
            dataGridViewFeedBack.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionForeColor = Color.White; // Màu chữ khi được chọn
        }

        private void buttonTimKiemFeedBack_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtTimKiemFeedBack.Text.Trim();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var result = feedbackBLL.TimKiemFeedBack(searchValue);
                    dataGridViewFeedBack.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá trị tìm kiếm.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tìm kiếm phản hồi: " + ex.Message);
            }
        }

        private void buttonRefeshFB_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa giá trị tìm kiếm
                txtTimKiemFeedBack.Text = string.Empty;
                txtMaPhong.Text = string.Empty;
                txtNoiDungPhanHoi.Text = string.Empty;

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

                // Hiển thị dữ liệu lên các TextBox
                txtMaPhong.Text = maPhong;
                txtNoiDungPhanHoi.Text = moTa;
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
    }
}
