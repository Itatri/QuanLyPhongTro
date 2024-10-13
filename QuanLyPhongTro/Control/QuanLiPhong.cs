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
using BLL;
using System.Configuration;





namespace QuanLyPhongTro.Control
{
    public partial class QuanLiPhong : UserControl

    {
        
        private ThongTinPhongBLL thongTinPhongBLL = new ThongTinPhongBLL();
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        private SqlConnection con;

        public string khuvuc { get; set; }


        public QuanLiPhong()
        {
            InitializeComponent();
            // Thêm các item vào ComboBox
            cbbSapXep.Items.Add("Tăng dần theo hạn trọ");
            cbbSapXep.Items.Add("Lọc phòng trống");

            // Đặt giá trị mặc định là "Thời hạn phòng"
            cbbSapXep.SelectedItem = "Thời hạn phòng";

            // Thêm sự kiện SelectedIndexChanged
            cbbSapXep.SelectedIndexChanged += new EventHandler(cbbSapXep_SelectedIndexChanged);
        }

        private void QuanLiPhong_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(khuvuc))
            {
                con = new SqlConnection(connectionString);
                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Khu vực chưa được thiết lập.");
            }



        }
        private void RefreshDataGridView()
        {

            // Cập nhật câu truy vấn SQL để bao gồm cột MaKhuVuc và lọc theo khuvuc
            string query = "SELECT TrangThai AS [Đã thuê], TenPhong As [Tên Phòng], NgayVao as [Ngày Vào], HanTro as [Hạn trọ], TienCoc as [Tiền cọc], TienPhong as [Tiền phòng], Dien as [Điện], Nuoc as [Nước], CongNo as [Công nợ], GhiChu as [Ghi chú] from Phong WHERE MaKhuVuc = @MaKhuVuc";
            DataTable dataTable = ExecuteQuery(query, new SqlParameter("@MaKhuVuc", khuvuc));
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



        }
        //

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                if (parameters != null && parameters.Length > 0) // Thay đổi kiểm tra nếu parameters không null và có ít nhất một phần tử
                {
                    foreach (var param in parameters)
                    {
                        adapter.SelectCommand.Parameters.Add(param);
                    }
                }
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        //
        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        private void btnDangKyTaiKhoan_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                DanKyTaiKhoanKhachHang f = new DanKyTaiKhoanKhachHang();
                f.makhuvuc = khuvuc;
                mainForm.ShowControl(f); // Truyền đối tượng f vào phương thức ShowControl
            }
        }

        private void btnTaoPhong_Click(object sender, EventArgs e)
        {
            if (this.ParentForm is MainForm mainForm)
            {
                TaoQuanLyPhong1 f = new TaoQuanLyPhong1();
                f.makhuvuc = khuvuc;
                mainForm.ShowControl(f);
            }
        }

        private void cbbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cbbSapXep.SelectedItem.ToString();
            DataTable dt = null;

            // Mã khu vực, bạn có thể thay thế bằng mã khu vực thực tế
            string makhuvuc = khuvuc;

            // Gọi phương thức tương ứng từ BLL dựa trên lựa chọn
            if (selectedValue == "Tăng dần theo hạn trọ")
            {
                dt = thongTinPhongBLL.LayPhongTheoHanTroTangDan(makhuvuc);
            }
            else if (selectedValue == "Lọc phòng trống")
            {
                dt = thongTinPhongBLL.LayPhongTheoTrangThai(makhuvuc);
            }

            // Hiển thị dữ liệu trong DataGridView (hoặc Control khác)
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }
    }
}
