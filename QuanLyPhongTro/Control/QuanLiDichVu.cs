using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DAL;
using DTO;
using BLL;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLiDichVu : UserControl
    {
        private QuanLyDichVuBLL bll = new QuanLyDichVuBLL();

        public QuanLiDichVu()
        {
            InitializeComponent();
            InitializeComboBox();
        }
        private void InitializeComboBox()
        {
            cbbTrangThai.Items.Add("0");
            cbbTrangThai.Items.Add("1");
        }
        private void QuanLiDichVu_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
        private void RefreshDataGridView()
        {
            dataGridView1.DataSource = bll.GetAllServices();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (ValidateServiceInputs())
            {
                ThongTinDichVuDTO service = new ThongTinDichVuDTO
                {
                    MaDichVu = bll.GenerateNewServiceCode(),
                    TenDichVu = txtTenDV.Text,
                    DonGia = float.Parse(txtDonGia.Text),
                    TrangThai = cbbTrangThai.SelectedItem.ToString() == "1"
                };

                if (bll.AddService(service))
                {
                    MessageBox.Show("Thêm dịch vụ thành công");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Mã dịch vụ đã tồn tại.");
                }
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã dịch vụ để xóa.");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                return;
            }

            if (bll.RemoveService(txtMaDV.Text))
            {
                MessageBox.Show("Xóa dịch vụ thành công");
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Xóa dịch vụ thất bại");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ValidateServiceInputs())
            {
                ThongTinDichVuDTO service = new ThongTinDichVuDTO
                {
                    MaDichVu = txtMaDV.Text,
                    TenDichVu = txtTenDV.Text,
                    DonGia = float.Parse(txtDonGia.Text),
                    TrangThai = cbbTrangThai.SelectedItem.ToString() == "1"
                };

                if (bll.EditService(service))
                {
                    MessageBox.Show("Sửa dịch vụ thành công");
                    RefreshDataGridView();
                }
                else
                {
                    MessageBox.Show("Sửa dịch vụ thất bại");
                }
            }
        }

        private bool ValidateServiceInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text) ||
                string.IsNullOrWhiteSpace(cbbTrangThai.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin dịch vụ.");
                return false;
            }
            return true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                txtMaDV.Text = selectedRow.Cells["MaDichVu"].Value.ToString();
                txtTenDV.Text = selectedRow.Cells["TenDichVu"].Value.ToString();
                txtDonGia.Text = selectedRow.Cells["DonGia"].Value.ToString();
                cbbTrangThai.Text = selectedRow.Cells["TrangThai"].Value.ToString();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonGia.Clear();
            cbbTrangThai.SelectedIndex = 1;
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDonGia.Text, out _))
            {
                MessageBox.Show("Vui lòng chỉ nhập số vào trường này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Text = "";
            }
        }
    }
}
