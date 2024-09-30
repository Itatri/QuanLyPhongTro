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
    public partial class MainForm_TrangChu : UserControl
    {
        private QuanLiDanCu controlQuanLyDanCu;

        public MainForm_TrangChu()
        {
            InitializeComponent();
            controlQuanLyDanCu = new QuanLiDanCu();
            controlQuanLyDanCu.Dock = DockStyle.Fill; // Đặt control hiển thị chiếm toàn bộ panel
        }

        private void btnQLDanCu_Click(object sender, EventArgs e)
        {
            this.Controls.Clear(); // Xóa các control hiện có
            this.Controls.Add(controlQuanLyDanCu); // Thêm control QuanLyDanCu

        }
    }
}
