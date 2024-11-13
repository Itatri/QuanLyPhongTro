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
using DAL;

namespace QuanLyPhongTro.Control
{
    public partial class QuanLiPhong : UserControl
    {
        private QuanLiPhongBLL thongTinPhongBLL = new QuanLiPhongBLL();
        //private ThongTinPhongBLL thongTinPhongBLL = new ThongTinPhongBLL();
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        private SqlConnection con;
        public string id { get; set; }

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

            buttonTraPhong.Click += new EventHandler(buttonTraPhong_Click); // Thêm sự kiện Click cho buttonTraPhong

            LoadData();
        }

        private void LoadData()
        {

            if (dataGridView1.Columns["btnXemChiTiet"] == null)
            {
                DataGridViewButtonColumn btnXemChiTiet = new DataGridViewButtonColumn();
                btnXemChiTiet.Name = "btnXemChiTiet";
                btnXemChiTiet.HeaderText = "Hành Động";
                btnXemChiTiet.Text = "Xem Chi Tiết";
                btnXemChiTiet.UseColumnTextForButtonValue = true;
                btnXemChiTiet.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng cho nút
                dataGridView1.Columns.Add(btnXemChiTiet);
            }

            // Thêm cột nút "Tạo Hợp Đồng" nếu chưa tồn tại
            if (dataGridView1.Columns["btnTaoHopDong"] == null)
            {
                DataGridViewButtonColumn btnTaoHopDong = new DataGridViewButtonColumn();
                btnTaoHopDong.Name = "btnTaoHopDong";
                btnTaoHopDong.HeaderText = "Hành Động";
                btnTaoHopDong.Text = "Tạo Hợp Đồng";
                btnTaoHopDong.UseColumnTextForButtonValue = true;
                btnTaoHopDong.FlatStyle = FlatStyle.Flat; // Đặt kiểu phẳng cho nút
                dataGridView1.Columns.Add(btnTaoHopDong);
            }

            // Thiết lập tự động điều chỉnh kích thước cột
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đăng ký sự kiện CellClick cho DataGridView
            dataGridView1.CellClick += dataGridView1_CellClick;

            dataGridView1.Columns["btnXemChiTiet"].DefaultCellStyle.BackColor = Color.Blue;
            dataGridView1.Columns["btnXemChiTiet"].DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionBackColor = Color.Green;
            dataGridView1.Columns["btnXemChiTiet"].DefaultCellStyle.SelectionForeColor = Color.White;
        }



        private void QuanLiPhong_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(khuvuc))
            {
                con = new SqlConnection(connectionString);
                RefreshDataGridView();

                // Định dạng tiền
                dataGridView1.Columns["Công nợ"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["Giá phòng"].DefaultCellStyle.Format = "N0";
                dataGridView1.Columns["Tiền cọc"].DefaultCellStyle.Format = "N0";

                // Định dạng ngày tháng
                dataGridView1.Columns["Ngày Vào"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns["Hạn trọ"].DefaultCellStyle.Format = "dd/MM/yyyy";

            }
            else
            {
                MessageBox.Show("Khu vực chưa được thiết lập.");
            }


            ///////////// Định dạng tiền 
            //dataGridView1.Columns["Công nợ"].DefaultCellStyle.Format = "N0";
            //dataGridView1.Columns["Giá phòng"].DefaultCellStyle.Format = "N0";
            //dataGridView1.Columns["Tiền cọc"].DefaultCellStyle.Format = "N0";

            //// Đặt AutoSizeMode cho các cột trong DataGridView để độ rộng của cột phù hợp với nội dung cửa cột thôi
            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //}


            // Bật tính năng AutoFilter cho các cột trong DataGridView, tạo các sort cho các cột trong datagirdview
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            //// Hiển thị form toàn màn hình
            //this.WindowState = FormWindowState.Maximized;
            ////this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = true;

        }




        private void RefreshDataGridView()
        {
            string query = @"
        SELECT p.MaPhong, 
               p.TrangThai AS [Đã thuê], 
               p.TenPhong AS [Tên Phòng], 
               COUNT(k.MaKhachTro) AS [Số lượng người],
               p.DienTich AS [Diện tích],  -- Thêm cột Diện tích
               p.NgayVao AS [Ngày Vào], 
               p.HanTro AS [Hạn trọ], 
               p.TienCoc AS [Tiền cọc], 
               p.TienPhong AS [Giá phòng], 
               p.CongNo AS [Công nợ], 
               p.GhiChu AS [Ghi chú]
        FROM Phong p
        LEFT JOIN ThongTinKhach k ON p.MaPhong = k.MaPhong
        WHERE p.MaKhuVuc = @MaKhuVuc
        GROUP BY p.MaPhong, p.TrangThai, p.TenPhong, p.DienTich, p.NgayVao, p.HanTro, p.TienCoc, p.TienPhong, p.CongNo, p.GhiChu";

            DataTable dataTable = ExecuteQuery(query, new SqlParameter("@MaKhuVuc", khuvuc));
            dataGridView1.DataSource = dataTable;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn cột MaPhong
            if (dataGridView1.Columns["MaPhong"] != null)
            {
                dataGridView1.Columns["MaPhong"].Visible = false; // Ẩn cột bằng tên
            }

            LoadData(); // Gọi LoadData để thêm các nút
        }






        private DataTable ExecuteQuery(string query, SqlParameter parameter)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(parameter);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }


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
                f.makhuvucuserphong = khuvuc;
                mainForm.ShowControl(f); // Truyền đối tượng f vào phương thức ShowControl
            }
        }

        private void btnTaoPhong_Click(object sender, EventArgs e)
        {
            // Láy danh sách phòng theo khu vực (29/10/2024)
            if (this.ParentForm is MainForm mainForm)
            {
                TaoQuanLyPhong1 f = new TaoQuanLyPhong1();
                f.makhuvuctaophong = khuvuc;
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

                // Ẩn cột MaPhong
                dataGridView1.Columns["MaPhong"].Visible = false; // Ẩn cột bằng tên
                                                                  // Hoặc nếu bạn biết chỉ số cột
                                                                  // dataGridView1.Columns[0].Visible = false; // Ẩn cột theo chỉ số (nếu MaPhong là cột đầu tiên)
            }
        }



        //------------------------ Do này lấy tên phòng bổ vô để truyền dữ liệu vì ban ddaaud mã phòng và tên phòng giống nhau
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn vào cột nút hay không
            //if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["btnXemChiTiet"].Index || e.ColumnIndex == dataGridView1.Columns["btnTaoHopDong"].Index))
            //{
            //    // Lấy giá trị của cột MaPhong từ hàng được chọn
            //    string maPhong = dataGridView1.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();

            //    if (e.ColumnIndex == dataGridView1.Columns["btnXemChiTiet"].Index)
            //    {
            //        if (this.ParentForm is MainForm mainForm)
            //        {
            //            ThongTinPhong ThongTinPhongControl = new ThongTinPhong
            //            {
            //                KhuVuc = this.khuvuc,
            //                // Truyền lại giá trị khu vực

            //                //MAPHONG = dataGridView1.Rows[e.RowIndex].Cells["Tên Phòng"].Value.ToString(),
            //                MAPHONG = maPhong, // Sử dụng giá trị của MaPhong
            //            };
            //            mainForm.ShowControl(ThongTinPhongControl);
            //        }
            //    }
            //    else if (e.ColumnIndex == dataGridView1.Columns["btnTaoHopDong"].Index)
            //    {
            //        // Xử lý logic khi nhấn nút "Tạo Hợp Đồng"
            //        TaoHopDongPhong(maPhong);
            //    }

            //    //// Cập nhật trạng thái tài khoản phòng
            //    //UpdateRoomAccountStatus(maPhong, false);


            //}
        }

        // Phương thức để xem chi tiết phòng
        private void XemChiTietPhong(string maPhong)
        {
            MessageBox.Show($"Xem chi tiết phòng: {maPhong}");
            // Thực hiện các hành động cần thiết để hiển thị chi tiết phòng
        }

        // Phương thức để tạo hợp đồng phòng
        private void TaoHopDongPhong(string maPhong)
        {
            MessageBox.Show($"Tạo hợp đồng cho phòng: {maPhong}");
            // Thực hiện các hành động cần thiết để tạo hợp đồng
        }





        //}

        private void TimKiemPhong()
        {

            string keyword = textBoxTimPhong.Text.Trim();

            if (!string.IsNullOrEmpty(keyword))
            {
                DataTable result = thongTinPhongBLL.TimKiemPhong(keyword); // Gọi phương thức tìm kiếm từ BLL

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result; // Hiển thị kết quả trong DataGridView
                }
                else
                {
                    dataGridView1.DataSource = null; // Không có kết quả
                    MessageBox.Show("Không tìm thấy phòng nào với từ khóa này.");
                    textBoxTimPhong.Clear();
                    RefreshDataGridView();
                }
            }
            else if (textBoxTimPhong.Text.Length == 0)
            {

                textBoxTimPhong.Clear();
                RefreshDataGridView();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void textBoxTimPhong_TextChanged(object sender, EventArgs e)
        {
            TimKiemPhong(); // Gọi phương thức tìm kiếm khi văn bản thay đổi
        }

        private void buttonTraPhong_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maPhong = dataGridView1.SelectedRows[0].Cells["MaPhong"].Value.ToString();
                UpdateRoomStatus(maPhong, false); // Cập nhật trạng thái phòng thành false
                DeleteRoomAccount(maPhong); // Xóa tài khoản phòng
                CapNhatthongtinkhach(maPhong, 0); // Cập nhật trạng thái khách trọ thành đã rời đi
                RefreshDataGridView(); // Làm mới DataGridView
            }
            else
            {
                //MessageBox.Show("Vui lòng chọn một phòng để trả.");
            }
        }



        private void UpdateRoomStatus(string maPhong, bool trangThai)
        {
            string query = "UPDATE Phong SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = { new SqlParameter("@TrangThai", trangThai), new SqlParameter("@MaPhong", maPhong) };
            ExecuteNonQuery(query, parameters);
        }



        private void DeleteRoomAccount(string maPhong)
        {
            string query = "DELETE FROM UserPhong WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = { new SqlParameter("@MaPhong", maPhong) };
            ExecuteNonQuery(query, parameters);
        }

        private void CapNhatthongtinkhach(string maPhong, int trangThai)
        {
            string query = "UPDATE ThongTinKhach SET TrangThai = @TrangThai WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = {
        new SqlParameter("@TrangThai", trangThai),
        new SqlParameter("@MaPhong", maPhong)
    };
            ExecuteNonQuery(query, parameters);
        }



        ///-------------22_10_2024
        // tô màu cho datagirdview
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            int trangThai = Convert.ToInt32(row.Cells["Đã thuê"].Value);

            if (trangThai == 0)
            {
                row.DefaultCellStyle.BackColor = Color.PowderBlue;  // Màu nền cho dòng chưa thuê
            }
            else if (trangThai == 1)
            {
                row.DefaultCellStyle.BackColor = Color.Lavender;  // Màu nền cho dòng đã thuê
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị của cột MaPhong từ hàng được chọn
                string maPhong = dataGridView1.Rows[e.RowIndex].Cells["MaPhong"].Value.ToString();

                if (e.ColumnIndex == dataGridView1.Columns["btnXemChiTiet"].Index)
                {
                    if (this.ParentForm is MainForm mainForm)
                    {
                        ThongTinPhong ThongTinPhongControl = new ThongTinPhong
                        {
                            KhuVuc = this.khuvuc,
                            // Truyền lại giá trị khu vực

                            //MAPHONG = dataGridView1.Rows[e.RowIndex].Cells["Tên Phòng"].Value.ToString(),
                            MAPHONG = maPhong, // Sử dụng giá trị của MaPhong
                        };
                        mainForm.ShowControl(ThongTinPhongControl);
                    }
                }
                else if (e.ColumnIndex == dataGridView1.Columns["btnTaoHopDong"].Index)
                {
                    // Xử lý logic khi nhấn nút "Tạo Hợp Đồng"
                    if ((bool)dataGridView1.Rows[e.RowIndex].Cells["Đã thuê"].Value == true)
                    {
                        if ((int)dataGridView1.Rows[e.RowIndex].Cells["Số lượng người"].Value > 0)
                        {
                            TaoCT01 tao = new TaoCT01();
                            tao.maphong = maPhong;
                            tao.idadmin = this.id;
                            tao.makhuvuc = khuvuc;
                            int check = tao.TaoTamTru();
                            if (check == 0)
                                MessageBox.Show("Xong");
                            else if (check == 1)
                                MessageBox.Show("không có chủ hộ");
                            else if (check == 2)
                                MessageBox.Show("trùng lập folder phòng");
                        }
                    }

                }

                //// Cập nhật trạng thái tài khoản phòng
                //UpdateRoomAccountStatus(maPhong, false);

                // Xóa tài khoản phòng
                //DeleteRoomAccount(maPhong);
            }
        }
    }
}
