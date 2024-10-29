using BLL;
using DTO;
using Microsoft.Office.Interop.Word;
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
    public partial class ThongTinPhieuThu : UserControl
    {
        public string mapt {  get; set; }
        public string khuvuc {  get; set; }
        private QuanLyPhieuThuBLL bll = new QuanLyPhieuThuBLL();
        private float soducu;
        System.Data.DataTable dt;
        public ThongTinPhieuThu()
        {
            InitializeComponent();
        }

        private void ThongTinPhieuThu_Load(object sender, EventArgs e)
        {
            dtpNgayLap.Format = DateTimePickerFormat.Custom;
            dtpNgayLap.CustomFormat = "dd/MM/yyyy";
            dtpNgayThu.Format = DateTimePickerFormat.Custom;
            dtpNgayThu.CustomFormat = "dd/MM/yyyy";
            LoadPT();
            LoadDichVuPhieuThu();
        }
        private void LoadPT()
        {
            System.Data.DataTable dt = bll.LoadPhieuThu(mapt);
            if (dt.Rows.Count > 0)
            {
                txtma.Text = dt.Rows[0]["MaPT"].ToString();
                txtPhong.Text = dt.Rows[0]["MaPhong"].ToString();
                dtpNgayLap.Text = Convert.ToDateTime(dt.Rows[0]["NgayLap"]).ToString("dd/MM/yyyy");
                dtpNgayThu.Text = Convert.ToDateTime(dt.Rows[0]["NgayThu"]).ToString("dd/MM/yyyy");
                txtTienNha.Text = Convert.ToDecimal(dt.Rows[0]["TienNha"]).ToString("N0");
                txtTienDien.Text = Convert.ToDecimal(dt.Rows[0]["TienDien"]).ToString("N0");
                txtTienNuoc.Text = Convert.ToDecimal(dt.Rows[0]["TienNuoc"]).ToString("N0");
                txtTongTien.Text = Convert.ToDecimal(dt.Rows[0]["TongTien"]).ToString("N0");
                txtDC.Text = Convert.ToDecimal(dt.Rows[0]["DienCu"]).ToString("N0");
                txtDM.Text = Convert.ToDecimal(dt.Rows[0]["DienMoi"]).ToString("N0");
                txtNC.Text = Convert.ToDecimal(dt.Rows[0]["NuocCu"]).ToString("N0");
                txtNM.Text = Convert.ToDecimal(dt.Rows[0]["NuocMoi"]).ToString("N0");
                txtKhachTra.Text = Convert.ToDecimal(dt.Rows[0]["ThanhToan"]).ToString("N0");
                if (txtKhachTra.Text.Length != 0 )
                {
                    txtDu.Text = ((decimal)(float.Parse(dt.Rows[0]["ThanhToan"].ToString()) - float.Parse(dt.Rows[0]["TongTien"].ToString()))).ToString("N0");
                    soducu = float.Parse(dt.Rows[0]["ThanhToan"].ToString()) - float.Parse(dt.Rows[0]["TongTien"].ToString());
                }
                else
                {
                    txtDu.Text = (-float.Parse(txtTongTien.Text)).ToString("N0");
                    soducu = -float.Parse(txtTongTien.Text);
                }
                LoadCongNo(txtPhong.Text);
            }
        }
        private void LoadCongNo(string phong)
        {
            System.Data.DataTable dt = bll.GetCongNo(phong);
            if (dt.Rows.Count > 0)
            {
                txtCongNo.Text = dt.Rows[0]["CongNo"].ToString();
            }
        }
        private void LoadDichVuPhieuThu()
        {
            dt = bll.LoadDichVuPhieuThu(txtPhong.Text,mapt);
            dgvDichVu.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {

                if (dr["TenDichVu"].Equals("Dịch vụ điện") || dr["TenDichVu"].Equals("Dịch vụ nước"))
                    continue;
                else dgvDichVu.Rows.Add(dr["Chon"], dr["TenDichVu"], dr["DonGia"]);
            }
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
        private void TongTien()
        {
            float dien = 0, nuoc = 0, tienp = 0;
            float.TryParse(txtTienNha.Text, out tienp);
            float tienDV = TienDichVu();
            float.TryParse(txtTienDien.Text, out dien);
            float.TryParse(txtTienNuoc.Text, out nuoc);
            txtTongTien.Text = (dien + nuoc + tienDV + tienp).ToString();
            float khachtra = 0;
            float.TryParse(txtKhachTra.Text, out khachtra);
            txtDu.Text = (khachtra - (dien + nuoc + tienDV + tienp)).ToString();
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
            pt.MaPT = mapt;
            pt.NgayThu = dtpNgayThu.Value;
            pt.DienMoi = float.Parse(txtDM.Text);
            pt.TienDien = float.Parse(txtTienDien.Text);
            pt.NuocMoi = float.Parse(txtNM.Text);
            pt.TienNuoc = float.Parse(txtTienNuoc.Text);
            pt.TienDV = TienDichVu();
            pt.TongTien = float.Parse(txtTongTien.Text);
            if (txtKhachTra.Text.Length > 0)
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

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            PhieuThu pt = chuyendoi();
            List<ChiTietDichVuPT> lst = chuyendoidichvu();
            if(pt ==null)
            {
                return;
            }else
            {
                float congno = float.Parse(txtCongNo.Text);
                float sodumoi = float.Parse(txtDu.Text);
                bool check = bll.UpdatePhieuThu(pt);
                bll.DeleteDichVuPhieuThu(txtma.Text);
                bll.UpdatePhong(txtPhong.Text, float.Parse(txtDM.Text), float.Parse(txtNM.Text), (congno - (sodumoi - soducu)));
                bll.CreateDichVuPhieuThu(lst);
                MessageBox.Show("Cập nhật thành công");
            }
            LoadPT();
            LoadDichVuPhieuThu();
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                QuanLyPhieuThu f = new QuanLyPhieuThu();
                f.khuvuc = khuvuc;
                mainForm.ShowControl(f);
            }
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
                float tong = 0, khachtra = 0;
                float.TryParse(txtTongTien.Text, out tong);
                float.TryParse(txtKhachTra.Text, out khachtra);
                txtDu.Text = (khachtra - tong).ToString();
            }
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

        private void txtKhachTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void txtDM_Leave(object sender, EventArgs e)
        {
            if (txtDM.Text.Length > 0)
            {
                float dienmoi = 0;
                float.TryParse(txtDM.Text, out dienmoi);
                float diencu = float.Parse(txtDC.Text);
                if (dienmoi < diencu)
                {
                    txtDM.Text = diencu.ToString();
                    txtTienDien.Text = "0";
                }
                else
                {
                    float dien = dienmoi - diencu;
                    float dongia = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["TenDichVu"].Equals("Dịch vụ điện"))
                            dongia = float.Parse(dr["DonGia"].ToString());
                    }
                    txtTienDien.Text = (dien * dongia).ToString();
                }
            }
            TongTien();
        }

        private void txtNM_Leave(object sender, EventArgs e)
        {
            if (txtDM.Text.Length > 0)
            {
                float nuocmoi = 0;
                float.TryParse(txtNM.Text, out nuocmoi);
                float nuoccu = float.Parse(txtNC.Text);
                if (nuocmoi < nuoccu)
                {
                    txtNM.Text = nuoccu.ToString();
                    txtTienNuoc.Text = "0";
                }
                else
                {
                    float nuoc = nuocmoi - nuoccu;
                    float dongia = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["TenDichVu"].Equals("Dịch vụ nước"))
                            dongia = float.Parse(dr["DonGia"].ToString());
                    }
                    txtTienNuoc.Text = (nuoc * dongia).ToString();
                }
            }
            TongTien();
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
    }
}
