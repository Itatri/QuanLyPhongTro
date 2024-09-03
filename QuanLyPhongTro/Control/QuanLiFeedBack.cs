using BLL;
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
    }
}
