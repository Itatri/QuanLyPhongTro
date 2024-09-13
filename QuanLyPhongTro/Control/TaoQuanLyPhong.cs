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
    public partial class TaoQuanLyPhong : UserControl
    {
        private TaoQuanLyPhongBLL phongBLL;

        public TaoQuanLyPhong()
        {
            InitializeComponent();
            phongBLL = new TaoQuanLyPhongBLL();
            LoadMaLoaiPhong();
            LoadTrangThai();
            txtMaPhong.Text = phongBLL.GetNewMaPhong(); // Tạo mã phòng mới ngay khi khởi tạo
        }

        private void LoadMaLoaiPhong()
        {
            cbbMaLoaiPhong.Items.Clear();
            List<TaoQuanLyPhongDTO> phongs = phongBLL.GetAllPhong();
            foreach (var phong in phongs)
            {
                cbbMaLoaiPhong.Items.Add(phong.MaLoaiPhong);
            }
        }

        private void LoadTrangThai()
        {
            cbbTrangThai.Items.Add("0");
            cbbTrangThai.Items.Add("1");
            cbbTrangThai.SelectedIndex = 1;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (ValidateServiceInputs())
            {
                TaoQuanLyPhongDTO phong = new TaoQuanLyPhongDTO
                {
                    MaPhong = txtMaPhong.Text,
                    MaLoaiPhong = cbbMaLoaiPhong.SelectedItem?.ToString(),
                    MaKhuVuc = txtMaKhuVuc.Text,
                    TenPhong = txtTenPhong.Text,
                    NgayVao = DTPK_NgayVao.Value,
                    TienCoc = float.TryParse(txtTienCoc.Text, out float tienCoc) ? tienCoc : 0,
                    Dien = float.TryParse(txtSodien.Text, out float dien) ? dien : 0,
                    Nuoc = float.TryParse(txtSoNuoc.Text, out float nuoc) ? nuoc : 0,
                    HanTro = dtpk_NgayHetHan.Value,
                    TrangThai = cbbTrangThai.SelectedItem?.ToString() == "1",
                    GhiChu = RtxtGhiChu.Text
                };

                bool result = phongBLL.InsertPhong(phong);
                MessageBox.Show(result ? "Thêm phòng mới thành công" : "Thêm phòng mới thất bại");
                if (result)
                {
                    RefreshDataGridView(); // Cập nhật lại dữ liệu trong DataGridView
                }
            }
        }

        private bool ValidateServiceInputs()
        {
            if (string.IsNullOrWhiteSpace(cbbTrangThai.Text) ||
                string.IsNullOrWhiteSpace(cbbMaLoaiPhong.Text) ||
                string.IsNullOrWhiteSpace(txtTienCoc.Text) ||
                string.IsNullOrWhiteSpace(txtSoNuoc.Text) ||
                string.IsNullOrWhiteSpace(txtSodien.Text) ||
                string.IsNullOrWhiteSpace(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return false;
            }
            return true;
        }

        private void TaoQuanLyPhong_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            List<TaoQuanLyPhongDTO> phongs = phongBLL.GetAllPhong();
            dataGridView1.DataSource = phongs;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn phòng để xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string maPhong = txtMaPhong.Text;
                bool isDeleted = phongBLL.DeletePhong(maPhong);
                MessageBox.Show(isDeleted ? "Xóa phòng thành công" : "Xóa phòng thất bại");
                if (isDeleted)
                {
                    RefreshDataGridView(); // Cập nhật lại dữ liệu trong DataGridView
                    txtMaPhong.Text = phongBLL.GetNewMaPhong(); // Tạo mã phòng mới
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                txtMaPhong.Text = selectedRow.Cells["MaPhong"].Value?.ToString();
                cbbMaLoaiPhong.SelectedItem = selectedRow.Cells["MaLoaiPhong"].Value?.ToString();
                txtMaKhuVuc.Text = selectedRow.Cells["MaKhuVuc"].Value?.ToString();
                txtTenPhong.Text = selectedRow.Cells["TenPhong"].Value?.ToString();

                if (selectedRow.Cells["NgayVao"].Value != null)
                {
                    if (DateTime.TryParse(selectedRow.Cells["NgayVao"].Value.ToString(), out DateTime ngayVao))
                    {
                        DTPK_NgayVao.Value = ngayVao;
                    }
                    else
                    {
                        DTPK_NgayVao.Value = DateTime.Now;
                    }
                }

                txtTienCoc.Text = selectedRow.Cells["TienCoc"].Value?.ToString();
                cbbTrangThai.SelectedItem = selectedRow.Cells["TrangThai"].Value?.ToString();
                txtSodien.Text = selectedRow.Cells["Dien"].Value?.ToString();
                txtSoNuoc.Text = selectedRow.Cells["Nuoc"].Value?.ToString();
                RtxtGhiChu.Text = selectedRow.Cells["GhiChu"].Value?.ToString();
            }
        }

        private void btnmoi_Click(object sender, EventArgs e)
        {
            txtTenPhong.Clear();
            txtTienCoc.Clear();
            cbbMaLoaiPhong.Text = "";
            txtSodien.Clear();
            txtSoNuoc.Clear();
            RtxtGhiChu.Clear();

        }

        private void txtSodien_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSodien.Text, out _))
            {
                MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSodien.Text = "";
            }
        }

        private void txtSoNuoc_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSoNuoc.Text, out _))
            {
                MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoNuoc.Text = "";
            }
        }

        private void txtTienCoc_TextChanged(object sender, EventArgs e)
        {

            if (!int.TryParse(txtTienCoc.Text, out _))
            {
                MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTienCoc.Text = "";
            }
        }
    }
}
