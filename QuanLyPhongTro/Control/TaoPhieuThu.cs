using BLL;
using DTO;
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
    public partial class TaoPhieuThu : UserControl
    {
        public string khuvuc {  get; set; }
        private QuanLyPhieuThuBLL bll = new QuanLyPhieuThuBLL();
        DataTable datatable;
        public TaoPhieuThu()
        {
            InitializeComponent();
        }

        private void TaoPhieuThu_Load(object sender, EventArgs e)
        {
            LoadPhong(khuvuc);
        }
        private void LoadPhong (string khuvuc)
        {
            DataTable dt = bll.GetPhong(khuvuc);
            foreach (DataRow dr in dt.Rows)
            {
                cboPhong.Items.Add(dr[0].ToString());
            }
        }
        private void LoadDVPhong(string phong)
        {
            datatable = new DataTable();
            // Gọi phương thức lấy dữ liệu dịch vụ từ lớp BLL
            datatable = bll.LoadDVPhong(phong);
            // Xóa tất cả các dòng hiện có trong DataGridView trước khi thêm mới
            dgvDichVu.Rows.Clear();

            foreach (DataRow dr in datatable.Rows)
            {

                if (dr["TenDichVu"].Equals("Điện") || dr["TenDichVu"].Equals("Nước"))
                    continue;
                else dgvDichVu.Rows.Add(1, dr["TenDichVu"], dr["DonGia"], dr["MaDichVu"]);
            }
        }

        private void OpenCloseText(bool b)
        {
            txtDM.Enabled = b;
            txtNM.Enabled = b;
            txtKhachTra.Enabled = b;
            btnTao.Enabled = b;
        }
        private void cboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboPhong.SelectedIndex != -1)
            {
                OpenCloseText(true);
                txtma.Text = "PT_" + cboPhong.Text + DateTime.Now.ToString("ddMMyy");
                DataTable dt = bll.LoadPhong(cboPhong.Text);
                if (dt.Rows.Count > 0)
                {
                    txtCongNo.Text = dt.Rows[0]["CongNo"].ToString();
                    txtTienNha.Text = dt.Rows[0]["TienPhong"].ToString();
                    txtDC.Text = dt.Rows[0]["Dien"].ToString();
                    txtNC.Text = dt.Rows[0]["Nuoc"].ToString();
                }
                LoadDVPhong(cboPhong.Text);
            }
        }

        private void txtDM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Nếu không phải là số hoặc phím Backspace, thì hủy sự kiện
                e.Handled = true;
            }
        }

        private void txtNM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Nếu không phải là số hoặc phím Backspace, thì hủy sự kiện
                e.Handled = true;
            }
        }

        private void txtKhachTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Nếu không phải là số hoặc phím Backspace, thì hủy sự kiện
                e.Handled = true;
            }
        }

        private void txtDM_Leave(object sender, EventArgs e)
        {
            if (txtDM.Text.Length > 0)
            {
                float dien = float.Parse(txtDM.Text) - float.Parse(txtDC.Text);
                float dongia = 0;
                foreach(DataRow dr in datatable.Rows)
                {
                    if (dr["TenDichVu"].Equals("Điện"))
                        dongia = float.Parse(dr["DonGia"].ToString());
                }
                txtTienDien.Text = (dien * dongia).ToString();
            }
            TongTien();
        }

        private void txtNM_Leave(object sender, EventArgs e)
        {
            if (txtDM.Text.Length > 0)
            {
                float nuoc = float.Parse(txtNM.Text) - float.Parse(txtNC.Text);
                float dongia = 0;
                foreach (DataRow dr in datatable.Rows)
                {
                    if (dr["TenDichVu"].Equals("Nước"))
                        dongia = float.Parse(dr["DonGia"].ToString());
                }
                txtTienNuoc.Text = (nuoc * dongia).ToString();
            }
            TongTien();
        }
        private float TienDichVu()
        {
            float tongTien = 0;
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value)) 
                {
                    tongTien += Convert.ToSingle(row.Cells[2].Value); 
                }
            }

            return tongTien; 
        }

        private void TongTien()
        {
            float dien = 0, nuoc = 0,tienp = 0;
            float.TryParse(txtTienNha.Text,out tienp);
            float tienDV = TienDichVu();
            float.TryParse(txtTienDien.Text, out dien);
            float.TryParse(txtTienNuoc.Text,out nuoc);
            txtTongTien.Text = (dien + nuoc + tienDV + tienp).ToString();
        }

        private void txtKhachTra_TextChanged(object sender, EventArgs e)
        {
            if (txtKhachTra.Text.Length > 0)
            {
                float tong = 0,khachtra =0;
                float.TryParse(txtTongTien.Text,out tong);
                float.TryParse(txtKhachTra.Text,out khachtra);
                txtDu.Text = (khachtra-tong).ToString();
            }
        }
        private PhieuThu chuyendoi()
        {
            PhieuThu pt = new PhieuThu();
            if (txtDM.Text.Length == 0)
            {
                return pt = null;
            }
            if (txtNM.Text.Length == 0)
            {
                return pt = null;
            }
            pt.MaPT = txtma.Text;
            pt.MaPhong = cboPhong.Text;
            pt.TienNha = float.Parse(txtTienNha.Text);
            pt.NgayLap = DateTime.Now;
            pt.NgayThu = DateTime.Now;
            pt.DienCu = float.Parse(txtDC.Text);
            pt.DienMoi = float.Parse(txtDM.Text);
            pt.TienDien = float.Parse(txtTienDien.Text);
            pt.NuocCu = float.Parse(txtNC.Text);
            pt.NuocMoi = float.Parse(txtNM.Text);
            pt.TienNuoc = float.Parse(txtTienNuoc.Text);
            pt.TienDV = TienDichVu();
            pt.TongTien = float.Parse(txtTongTien.Text);
            if(txtKhachTra.Text.Length > 0)
            {
                pt.ThanhToan = float.Parse(txtKhachTra.Text);
                pt.TrangThai = 1;
            }
            else
            {
                pt.TrangThai = 0;
            }
            return pt;
        }
        private List<ChiTietDichVuPT> chuyendoidichvu()
        {
            List<ChiTietDichVuPT> lst = new List<ChiTietDichVuPT>();
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    ChiTietDichVuPT ct = new ChiTietDichVuPT();
                    ct.MaPT = txtma.Text;
                    ct.TenDV = row.Cells[1].Value.ToString();
                    ct.DonGia = float.Parse(row.Cells[2].Value.ToString());
                    lst.Add(ct);
                }
            }
            return lst;
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLyPhieuThu f = new QuanLyPhieuThu();
                f.khuvuc = khuvuc;
                mainForm.ShowControl(f);
            }
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            PhieuThu pt = chuyendoi();
            List<ChiTietDichVuPT> lst = chuyendoidichvu();
            if (pt != null)
            {
                if (bll.CreatePhieuThu(pt))
                {
                    float khachtra = 0;
                    float.TryParse(txtKhachTra.Text, out khachtra);
                    float congno = float.Parse(txtCongNo.Text) - (khachtra - float.Parse(txtTongTien.Text));
                    bll.CreateDichVuPhieuThu(lst);
                    bll.UpdatePhong(cboPhong.Text, float.Parse(txtDM.Text), float.Parse(txtNM.Text), congno);
                    MessageBox.Show("Thành Công");
                }

            }
            else
            {
                MessageBox.Show("Thất bại");
                return;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            cboPhong.SelectedIndex = -1;
            txtma.Clear();
            txtCongNo.Clear();
            txtTienNha.Clear();
            txtDC.Clear();
            txtDM.Clear();
            txtNC.Clear();
            txtNM.Clear();
            txtTienDien.Clear();
            txtTienNuoc.Clear();
            txtTongTien.Clear();
            txtKhachTra.Clear();
            txtDu.Clear();
            dgvDichVu.Rows.Clear();
            OpenCloseText(false);
            cboPhong.Focus();
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Cập nhật giá trị của CheckBox ngay lập tức
                dgvDichVu.CommitEdit(DataGridViewDataErrorContexts.Commit);

                // Sau khi giá trị CheckBox được cập nhật, gọi hàm tính tổng tiền
                TongTien();
            }

        }
    }
}
