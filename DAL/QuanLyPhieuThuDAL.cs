using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QuanLyPhieuThuDAL
    {
        private string strConn = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        SqlConnection conn;

        public DataTable GetPTTheoThangNam(int thang, int nam, string maKhuVuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC GETPTTHeoThangNam @Thang, @Nam, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Thang", thang);
                        cmd.Parameters.AddWithValue("@Nam", nam);
                        cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }
        public DataTable GetALLPT(string khuvuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC GETALLPT @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhuVuc", khuvuc);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }

        public DataTable LayPhongChuaCoPhieuThu(DateTime date, string maKhuVuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC LayPhongChuaCoPhieuThu @NgayLap, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NgayLap", date);
                        cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }
        public bool CheckPTDaTao(string ma)
        {
            string query = "SELECT COUNT(*) FROM PhieuThu WHERE MaPT = @MaPT";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@MaPT", ma);

                    try
                    {
                        conn.Open();
                        int count = (int)command.ExecuteScalar(); // Thực thi câu lệnh trả về giá trị đơn
                        return count > 0; // Trả về true nếu phiếu thu đã tồn tại
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false; // Có lỗi xảy ra
                    }
                }
            }
        }
        public DataTable GetPTTheoThangNamPhong(int thang, int nam, string maKhuVuc, string key)
        {
            DataTable dt = new DataTable();
            string query = "EXEC GETPTTHeoThangNamPhong @Thang, @Nam, @MaKhuVuc, @Phong";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Thang", thang);
                        cmd.Parameters.AddWithValue("@Nam", nam);
                        cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                        cmd.Parameters.AddWithValue("@Phong", key);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu cần
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }
        /////////////Tạo PT
        public DataTable GetPhong(string khuvuc)
        {
            string query = "SELECT TenPhong FROM Phong WHERE MaKhuVuc = @KhuVuc";

            // Tạo một DataTable để chứa kết quả
            DataTable dt = new DataTable();

            // Sử dụng SqlConnection và SqlCommand để kết nối và thực thi truy vấn
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@KhuVuc", khuvuc);

                    // Tạo SqlDataAdapter để điền dữ liệu vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Mở kết nối, thực thi truy vấn và điền dữ liệu
                    conn.Open();
                    da.Fill(dt);
                }
            }

            // Trả về DataTable kết quả
            return dt;
        }
        public DataTable LoadPhong(string phong)
        {
            string query = "SELECT * FROM Phong WHERE MaPhong = @MaPhong";
            DataTable dt = new DataTable();

            // Sử dụng SqlConnection và SqlCommand để kết nối và thực thi truy vấn
            using (SqlConnection conn = new SqlConnection(strConn)) // Đặt chuỗi kết nối của bạn ở đây
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@MaPhong", phong);

                    // Tạo SqlDataAdapter để điền dữ liệu vào DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    // Mở kết nối, thực thi truy vấn và điền dữ liệu
                    conn.Open();
                    da.Fill(dt);
                }
            }

            // Trả về DataTable chứa dữ liệu
            return dt;
        }
        public DataTable LoadDVPhong(string phong)
        {
            DataTable dataTable = new DataTable();
            string query = @"
                            SELECT dv.MaDichVu, dv.TenDichVu, 1 AS SoLuong, dv.DonGia, 1 * dv.DonGia AS ThanhTien 
                            FROM DichVu dv
                            JOIN 
                                DichVuPhong dvp ON dv.MaDichVu = dvp.MaDichVu
                            WHERE 
                                dvp.MaPhong = @MaPhong 
                                AND dv.TrangThai = 1;
                            ";

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào câu lệnh
                    command.Parameters.AddWithValue("@MaPhong", phong);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
        public bool CreatePhieuThu(PhieuThu phieu)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string query = @"INSERT INTO PhieuThu 
                         (MaPT, MaPhong, NgayLap, NgayThu, TienNha, DienCu, DienMoi, TienDien, NuocCu, NuocMoi, TienNuoc, TienDV, TongTien, ThanhToan, TrangThai)
                         VALUES 
                         (@MaPT, @MaPhong, @NgayLap, @NgayThu, @TienNha, @DienCu, @DienMoi, @TienDien, @NuocCu, @NuocMoi, @TienNuoc, @TienDV, @TongTien, @ThanhToan, @TrangThai)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu lệnh
                    command.Parameters.AddWithValue("@MaPT", phieu.MaPT);
                    command.Parameters.AddWithValue("@MaPhong", phieu.MaPhong);
                    command.Parameters.AddWithValue("@NgayLap", phieu.NgayLap);
                    command.Parameters.AddWithValue("@NgayThu", phieu.NgayThu);
                    command.Parameters.AddWithValue("@TienNha", phieu.TienNha);
                    command.Parameters.AddWithValue("@DienCu", phieu.DienCu);
                    command.Parameters.AddWithValue("@DienMoi", phieu.DienMoi);
                    command.Parameters.AddWithValue("@TienDien", phieu.TienDien.HasValue ? (object)phieu.TienDien.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@NuocCu", phieu.NuocCu);
                    command.Parameters.AddWithValue("@NuocMoi", phieu.NuocMoi);
                    command.Parameters.AddWithValue("@TienNuoc", phieu.TienNuoc.HasValue ? (object)phieu.TienNuoc.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@TienDV", phieu.TienDV);
                    command.Parameters.AddWithValue("@TongTien", phieu.TongTien.HasValue ? (object)phieu.TongTien.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@ThanhToan", phieu.ThanhToan.HasValue ? (object)phieu.ThanhToan.Value : DBNull.Value); // Xử lý ThanhToan có thể là null
                    command.Parameters.AddWithValue("@TrangThai", phieu.TrangThai);

                    try
                    {
                        conn.Open();
                        int result = command.ExecuteNonQuery(); // Thực thi câu lệnh
                        return result > 0; // Trả về true nếu đã thêm thành công
                    }
                    catch (SqlException ex)
                    {
                        // Xử lý lỗi (ghi log, hiển thị thông báo, v.v.)
                        Console.WriteLine("Error: " + ex.Message);
                        return false; // Trả về false nếu có lỗi
                    }
                }
            }
        }
        public DataTable GetDichVuPhieuThu(string mapt)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM DichVuPhieuThu WHERE MaPT = @MaPT";

            using (SqlConnection conn = new SqlConnection(strConn)) // Thay bằng chuỗi kết nối của bạn
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPT", mapt); // Truyền tham số

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt); // Điền dữ liệu vào DataTable
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }

        public void CreateDichVuPhieuThu(ChiTietDichVuPT ct)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) // Đảm bảo connectionString là chuỗi kết nối của bạn
            {
                string query = "INSERT INTO DichVuPhieuThu (MaPT, TenDichVu, SoLuong, DonGia, ThanhTien) " +
                               "VALUES (@MaPT, @TenDV, @SoLuong, @DonGia, @ThanhTien)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào truy vấn
                    command.Parameters.AddWithValue("@MaPT", ct.MaPT);
                    command.Parameters.AddWithValue("@TenDV", ct.TenDV); // Ký tự có dấu
                    command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                    command.Parameters.AddWithValue("@DonGia", ct.DonGia);
                    command.Parameters.AddWithValue("@ThanhTien", ct.ThanhTien);

                    try
                    {
                        connection.Open(); // Mở kết nối
                        command.ExecuteNonQuery(); // Thực thi truy vấn
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public void UpdatePhong(string maPhong, float dien, float nuoc, float congno)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) // Đảm bảo connectionString là chuỗi kết nối của bạn
            {
                string query = "UPDATE Phong SET Dien = @Dien, Nuoc = @Nuoc, CongNo = @CongNo WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào truy vấn
                    command.Parameters.AddWithValue("@Dien", dien);
                    command.Parameters.AddWithValue("@Nuoc", nuoc);
                    command.Parameters.AddWithValue("@CongNo", congno);
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    try
                    {
                        connection.Open(); // Mở kết nối
                        command.ExecuteNonQuery(); // Thực thi truy vấn
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public void UpdateCongNoPhong(string maPhong, float congno)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) // Đảm bảo connectionString là chuỗi kết nối của bạn
            {
                string query = "UPDATE Phong SET CongNo = @CongNo WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào truy vấn
                    command.Parameters.AddWithValue("@CongNo", congno);
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    try
                    {
                        connection.Open(); // Mở kết nối
                        command.ExecuteNonQuery(); // Thực thi truy vấn
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public int CountKhach(string phong)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM ThongTinKhach WHERE MaPhong = @MaPhong AND TrangThai = 1";

            // Sử dụng đối tượng kết nối và lệnh (ví dụ SqlConnection, SqlCommand)
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số để tránh lỗi SQL Injection
                    command.Parameters.AddWithValue("@MaPhong", phong);

                    try
                    {
                        connection.Open();
                        // Thực thi truy vấn và ép kiểu kết quả về int
                        count = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi, ghi log hoặc hiển thị thông báo nếu cần
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return count;
        }
        public string GetMAByTenPhong(string phong)
        {
            string maPhong = string.Empty;
            string query = "SELECT MaPhong FROM Phong WHERE TenPhong = @TenPhong";

            // Sử dụng đối tượng kết nối và lệnh (ví dụ SqlConnection, SqlCommand)
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số để tránh lỗi SQL Injection
                    command.Parameters.AddWithValue("@TenPhong", phong);

                    try
                    {
                        connection.Open();
                        // Thực thi truy vấn và lấy kết quả
                        object result = command.ExecuteScalar();

                        // Kiểm tra nếu kết quả không phải là null, chuyển thành chuỗi
                        if (result != null)
                        {
                            maPhong = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi, ghi log hoặc hiển thị thông báo nếu cần
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return maPhong;
        }

        ////////////Thông tin phiếu thu
        public DataTable LoadPhieuThu(string maPhieu)
        {
            // Tạo đối tượng DataTable để lưu kết quả
            DataTable dt = new DataTable();

            // Tạo truy vấn SQL để lấy thông tin chi tiết từ bảng PhieuThu
            string query = "SELECT * FROM PhieuThu WHERE MaPT = @MaPT";

            // Sử dụng try-catch để xử lý ngoại lệ
            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    // Tạo đối tượng SqlCommand và thiết lập truy vấn, kết nối
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số vào truy vấn
                        cmd.Parameters.AddWithValue("@MaPT", maPhieu);

                        // Thực hiện truy vấn và lưu kết quả vào DataTable
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi hoặc xử lý ngoại lệ nếu có
                Console.WriteLine("Error: " + ex.Message);
            }

            // Trả về kết quả
            return dt;
        }

        public DataTable LoadDichVuPhieuThu(string maPhong, string maPT)
        {
            DataTable dt = new DataTable();

            // Chuỗi kết nối SQL của bạn
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetDichVuByPhongAndPhieuThu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số
                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaPT", maPT);

                        // Tạo SqlDataAdapter để điền dữ liệu vào DataTable
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ (nếu có)
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    // Đảm bảo kết nối được đóng
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            // Trả về DataTable chứa thông tin dịch vụ của phiếu thu
            return dt;
        }
        public bool UpdatePhieuThu(PhieuThu phieuThu)
        {
            try
            {
                // Chuỗi kết nối tới cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    // Mở kết nối
                    conn.Open();

                    // Tạo câu lệnh SQL để cập nhật thông tin phiếu thu
                    string query = @"
                UPDATE PhieuThu
                SET NgayThu = @NgayThu,
                    DienMoi = @DienMoi,
                    TienDien = @TienDien,
                    NuocMoi = @NuocMoi,
                    TienNuoc = @TienNuoc,
                    TienDV = @TienDV,
                    TongTien = @TongTien,
                    ThanhToan = @ThanhToan,
                    TrangThai = @TrangThai
                WHERE MaPT = @MaPT";

                    // Tạo đối tượng SqlCommand
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số
                        cmd.Parameters.AddWithValue("@MaPT", phieuThu.MaPT);
                        cmd.Parameters.AddWithValue("@NgayThu", phieuThu.NgayThu);
                        cmd.Parameters.AddWithValue("@DienMoi", phieuThu.DienMoi);
                        cmd.Parameters.AddWithValue("@TienDien", phieuThu.TienDien);
                        cmd.Parameters.AddWithValue("@NuocMoi", phieuThu.NuocMoi);
                        cmd.Parameters.AddWithValue("@TienNuoc", phieuThu.TienNuoc);
                        cmd.Parameters.AddWithValue("@TienDV", phieuThu.TienDV);
                        cmd.Parameters.AddWithValue("@TongTien", phieuThu.TongTien);
                        cmd.Parameters.AddWithValue("@ThanhToan", phieuThu.ThanhToan.HasValue ? (object)phieuThu.ThanhToan.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", phieuThu.TrangThai);

                        // Thực thi lệnh
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra số lượng bản ghi được cập nhật
                        return rowsAffected > 0;    
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public void DeleteDichVuPhieuThu(string mapt)
        {
            try
            {
                // Chuỗi kết nối tới cơ sở dữ liệu
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    // Mở kết nối
                    conn.Open();

                    // Tạo câu lệnh SQL để xóa dịch vụ phiếu thu dựa trên MaPT
                    string query = "DELETE FROM DichVuPhieuThu WHERE MaPT = @MaPT";

                    // Tạo đối tượng SqlCommand
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm tham số
                        cmd.Parameters.AddWithValue("@MaPT", mapt);

                        // Thực thi lệnh
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public DataTable GetCongNo(string phong)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT CongNo FROM Phong WHERE MaPhong = @MaPhong";

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số @MaPhong vào câu lệnh SQL
                        command.Parameters.AddWithValue("@MaPhong", phong);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Điền kết quả vào DataTable
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (ghi log, hiển thị thông báo, v.v.)
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }

            return dataTable;
        }

        public bool CheckPTMoiNhat(string mapt, string maphong, DateTime ngaylap)
        {
            // Kết nối cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                // Truy vấn để lấy phiếu thu mới nhất của phòng
                string query = @"
                                SELECT TOP 1 MaPT, NgayLap
                                FROM PhieuThu
                                WHERE MaPhong = @MaPhong
                                ORDER BY NgayLap DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhong", maphong); // Thêm tham số MaPhong

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Nếu có kết quả
                    {
                        // Lấy thông tin phiếu thu mới nhất từ cơ sở dữ liệu
                        string maPhieuThuMoiNhat = reader.GetString(reader.GetOrdinal("MaPT")); // Lấy giá trị cột MaPT
                        DateTime ngayThuMoiNhat = reader.GetDateTime(reader.GetOrdinal("NgayLap")); // Lấy giá trị cột NgayLap

                        // So sánh mã phiếu thu và ngày lập (chỉ so sánh ngày, bỏ qua giờ)
                        return mapt == maPhieuThuMoiNhat && ngaylap.Date == ngayThuMoiNhat.Date;

                    }
                }
            }

            return false; // Không tìm thấy phiếu thu nào trong cơ sở dữ liệu
        }


    }
}
