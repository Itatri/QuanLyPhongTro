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
        }
        private void dataGridViewFeedBack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

                // Gọi phương thức để tải toàn bộ dữ liệu và cập nhật DataGridView
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể làm mới dữ liệu: " + ex.Message);
            }
        }

       
    }
}
