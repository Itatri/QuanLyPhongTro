using BLL;
using DTO;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyPhongTro.Control
{
    public partial class TaoPhieuThu : UserControl
    {
        public string khuvuc {  get; set; }
        private QuanLyPhieuThuBLL bll = new QuanLyPhieuThuBLL();
        System.Data.DataTable datatable;
        const int sokhoinuoc = 2;
        string maphong = string.Empty;
        public TaoPhieuThu()
        {
            InitializeComponent();
        }

        private void TaoPhieuThu_Load(object sender, EventArgs e)
        {
            dtpNgayLap.Format = DateTimePickerFormat.Custom;
            dtpNgayLap.CustomFormat = "dd/MM/yyyy";
            LoadPhongChuaPT(khuvuc);
        }
        private void LoadPhong(string khuvuc)
        {
            cboPhong.Items.Clear();
            System.Data.DataTable dt = bll.GetPhong(khuvuc);
            foreach (DataRow dr in dt.Rows)
            {
                cboPhong.Items.Add(dr[0].ToString());
            }
        }
        private void LoadPhongChuaPT(string khuvuc)
        {
            cboPhong.Items.Clear();
            DateTime date = dtpNgayLap.Value;
            System.Data.DataTable dt = bll.LayPhongChuaCoPhieuThu(date, khuvuc);
            foreach (DataRow dr in dt.Rows)
            {
                cboPhong.Items.Add(dr[1].ToString());
            }
        }
        private void LoadDVPhong(string phong)
        {
            datatable = new System.Data.DataTable();
            // Gọi phương thức lấy dữ liệu dịch vụ từ lớp BLL
            datatable = bll.LoadDVPhong(phong);
            // Xóa tất cả các dòng hiện có trong DataGridView trước khi thêm mới
            dgvDichVu.Rows.Clear();

            foreach (DataRow dr in datatable.Rows)
            {

                if (dr["TenDichVu"].Equals("Dịch vụ điện") || dr["TenDichVu"].Equals("Dịch vụ nước"))
                    continue;
                else dgvDichVu.Rows.Add(1, dr["TenDichVu"], dr["DonGia"], dr["MaDichVu"]);
            }
            dgvDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
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
                txtma.Text = "PT_" + cboPhong.Text + dtpNgayLap.Value.Date.ToString("ddMMyyyy");
                maphong = bll.GetMAByTenPhong(cboPhong.Text);
                int dem = bll.CountKhach(maphong);
                System.Data.DataTable dt = bll.LoadPhong(maphong);
                if (dt.Rows.Count > 0)
                {
                    txtCongNo.Text = dt.Rows[0]["CongNo"].ToString();
                    txtTienNha.Text = Convert.ToDecimal(dt.Rows[0]["TienPhong"]).ToString("N0");
                    txtDC.Text = Convert.ToDecimal(dt.Rows[0]["Dien"]).ToString("N0");
                    txtNC.Text = Convert.ToDecimal(dt.Rows[0]["Nuoc"]).ToString("N0");
                    txtNM.Text = (float.Parse(dt.Rows[0]["Nuoc"].ToString()) + (sokhoinuoc * dem)).ToString("N0");
                }
                LoadDVPhong(maphong);
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
                float dienmoi = 0;
                float.TryParse(txtDM.Text, out dienmoi);
                float diencu = float.Parse(txtDC.Text);
                if(dienmoi<diencu)
                {
                    txtDM.Text = diencu.ToString();
                    txtTienDien.Text = "0";
                }    
                else
                {
                    float dien = dienmoi - diencu;
                    float dongia = 0;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        if (dr["TenDichVu"].Equals("Dịch vụ điện"))
                            dongia = float.Parse(dr["DonGia"].ToString());
                    }
                    txtTienDien.Text = ((decimal)(dien * dongia)).ToString("N0");
                }
            }
        }

        private void txtNM_Leave(object sender, EventArgs e)
        {
            if (txtDM.Text.Length > 0)
            {
                float nuocmoi = 0;
                float.TryParse(txtNM.Text, out nuocmoi);
                float nuoccu = float.Parse(txtNC.Text);
                if(nuocmoi<nuoccu)
                {
                    txtNM.Text = nuoccu.ToString();
                    txtTienNuoc.Text = "0";
                }else
                {
                    float nuoc = nuocmoi - nuoccu;
                    float dongia = 0;
                    foreach (DataRow dr in datatable.Rows)
                    {
                        if (dr["TenDichVu"].Equals("Dịch vụ nước"))
                            dongia = float.Parse(dr["DonGia"].ToString());
                    }
                    txtTienNuoc.Text = ((decimal)(nuoc * dongia)).ToString("N0");
                }
            }
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



        private void txtKhachTra_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = txtKhachTra.SelectionStart;
            int selectionLength = txtKhachTra.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtKhachTra.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtKhachTra.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtKhachTra.SelectionStart = Math.Max(0, selectionStart + txtKhachTra.Text.Length - text.Length);
                txtKhachTra.SelectionLength = selectionLength;
            }
            if (txtKhachTra.Text.Length > 0)
            {
                float tong = 0,khachtra =0;
                float.TryParse(txtTongTien.Text,out tong);
                float.TryParse(txtKhachTra.Text,out khachtra);
                txtDu.Text = ((decimal)(khachtra - tong)).ToString("N0");
            }
        }
        private PhieuThu chuyendoi()
        {
            PhieuThu pt = new PhieuThu();
            if (txtDM.Text.Length == 0)
            {
                pt.DienMoi = float.Parse(txtDC.Text);
            }else
            {
                pt.DienMoi = float.Parse(txtDM.Text);
            }
            if (txtNM.Text.Length == 0)
            {
                return pt = null;
            }
            pt.MaPT = txtma.Text;
            pt.MaPhong = maphong;
            pt.TienNha = float.Parse(txtTienNha.Text);
            pt.NgayLap = dtpNgayLap.Value;
            pt.NgayThu = dtpNgayLap.Value;
            pt.DienCu = float.Parse(txtDC.Text);
            if(txtTienDien.Text.Length > 0)
            {
                pt.TienDien = float.Parse(txtTienDien.Text);
            }
            pt.NuocCu = float.Parse(txtNC.Text);
            pt.NuocMoi = float.Parse(txtNM.Text);
            if(txtTienNuoc.Text.Length > 0)
            {
                pt.TienNuoc = float.Parse(txtTienNuoc.Text);
            }
            pt.TienDV = TienDichVu();
            if(txtTongTien.Text.Length > 0)
            {
                pt.TongTien = float.Parse(txtTongTien.Text);
            }
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
            foreach (DataRow dr in datatable.Rows)
            {
                if(dr["TenDichVu"].Equals("Dịch vụ nước")|| dr["TenDichVu"].Equals("Dịch vụ điện"))
                {
                    ChiTietDichVuPT ct = new ChiTietDichVuPT();
                    ct.MaPT = txtma.Text;
                    ct.TenDV = dr["TenDichVu"].ToString();
                    ct.DonGia = float.Parse(dr["DonGia"].ToString());
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

                if (!bll.CheckPTDaTao(pt.MaPT))
                {
                    if (bll.CreatePhieuThu(pt))
                    {
                        float khachtra = 0;
                        float congno = 0;
                        float.TryParse(txtKhachTra.Text, out khachtra);
                        if (txtTongTien.Text.Length > 0)
                        {
                            congno = float.Parse(txtCongNo.Text) - (khachtra - float.Parse(txtTongTien.Text));
                        }
                        else { congno = float.Parse(txtCongNo.Text); }
                        bll.CreateDichVuPhieuThu(lst);
                        if (txtDM.Text.Length > 0 && txtNM.Text.Length > 0)
                        {
                            bll.UpdatePhong(maphong, float.Parse(txtDM.Text), float.Parse(txtNM.Text), congno);
                        }
                        MessageBox.Show("Tạo phiếu thành công");
                        Email email = new Email();
                        email.SendEmailTaoPhieu(pt, cboPhong.Text);
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Phòng này đã tạo phiếu");
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
            Clear();
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Cập nhật giá trị của CheckBox ngay lập tức
                dgvDichVu.CommitEdit(DataGridViewDataErrorContexts.Commit);

            }

        }
        private void Clear()
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
        private void dtpNgayLap_ValueChanged(object sender, EventArgs e)
        {
            if(ckbPTP.Checked)
            {
                LoadPhong(khuvuc);
                txtma.Text = "PT_" + cboPhong.Text + dtpNgayLap.Value.Date.ToString("ddMMyyyy");
            }
            else
            {
                LoadPhongChuaPT(khuvuc);
                txtma.Text = "PT_" + cboPhong.Text + dtpNgayLap.Value.Date.ToString("ddMMyyyy");
            }
            

        }

        private void txtDM_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = txtDM.SelectionStart;
            int selectionLength = txtDM.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtDM.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtDM.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtDM.SelectionStart = Math.Max(0, selectionStart + txtDM.Text.Length - text.Length);
                txtDM.SelectionLength = selectionLength;
            }
        }

        private void txtNM_TextChanged(object sender, EventArgs e)
        {
            int selectionStart = txtNM.SelectionStart;
            int selectionLength = txtNM.SelectionLength;

            // Xóa bỏ dấu phân cách hiện tại
            string text = txtNM.Text.Replace(",", "");

            // Chuyển đổi chuỗi sang số
            if (decimal.TryParse(text, out decimal value))
            {
                // Định dạng lại số với dấu phân cách
                txtNM.Text = string.Format("{0:N0}", value);

                // Đặt lại vị trí con trỏ
                txtNM.SelectionStart = Math.Max(0, selectionStart + txtNM.Text.Length - text.Length);
                txtNM.SelectionLength = selectionLength;
            }
        }

        private void btnTongTien_Click(object sender, EventArgs e)
        {
            float dien = 0, nuoc = 0, tienp = 0;
            float.TryParse(txtTienNha.Text, out tienp);
            float tienDV = TienDichVu();
            float.TryParse(txtTienDien.Text, out dien);
            float.TryParse(txtTienNuoc.Text, out nuoc);
            txtTongTien.Text = ((decimal)(dien + nuoc + tienDV + tienp)).ToString("N0");
        }

        private void ckbPTP_CheckedChanged(object sender, EventArgs e)
        {

            if (ckbPTP.Checked)
            {
                LoadPhong(khuvuc);
            }
            else
            {
                LoadPhongChuaPT(khuvuc);
            }
        }
    }
}
