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
                        int count = (int)command.ExecuteScalar(); 
                        return count > 0; 
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false; 
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

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@KhuVuc", khuvuc);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    conn.Open();
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public DataTable LoadPhong(string phong)
        {
            string query = "SELECT * FROM Phong WHERE MaPhong = @MaPhong";
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConn)) 
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", phong);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    conn.Open();
                    da.Fill(dt);
                }
            }

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
                        int result = command.ExecuteNonQuery(); 
                        return result > 0;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false; 
                    }
                }
            }
        }
        public DataTable GetDichVuPhieuThu(string mapt)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM DichVuPhieuThu WHERE MaPT = @MaPT";

            using (SqlConnection conn = new SqlConnection(strConn)) 
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPT", mapt); 

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }

        public void CreateDichVuPhieuThu(ChiTietDichVuPT ct)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) 
            {
                string query = "INSERT INTO DichVuPhieuThu (MaPT, TenDichVu, SoLuong, DonGia, ThanhTien) " +
                               "VALUES (@MaPT, @TenDV, @SoLuong, @DonGia, @ThanhTien)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPT", ct.MaPT);
                    command.Parameters.AddWithValue("@TenDV", ct.TenDV);
                    command.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                    command.Parameters.AddWithValue("@DonGia", ct.DonGia);
                    command.Parameters.AddWithValue("@ThanhTien", ct.ThanhTien);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public void UpdatePhong(string maPhong, float dien, float nuoc, float congno)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) 
            {
                string query = "UPDATE Phong SET Dien = @Dien, Nuoc = @Nuoc, CongNo = @CongNo WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Dien", dien);
                    command.Parameters.AddWithValue("@Nuoc", nuoc);
                    command.Parameters.AddWithValue("@CongNo", congno);
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public void UpdateCongNoPhong(string maPhong, float congno)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) 
            {
                string query = "UPDATE Phong SET CongNo = @CongNo WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CongNo", congno);
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    try
                    {
                        connection.Open(); 
                        command.ExecuteNonQuery(); 
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public int CountKhach(string phong)
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM ThongTinKhach WHERE MaPhong = @MaPhong AND TrangThai = 1";

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", phong);

                    try
                    {
                        connection.Open();
                        count = (int)command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return count;
        }
        public string GetMAByTenPhong(string phong, string makhuvuc)
        {
            string maPhong = string.Empty;
            string query = "SELECT MaPhong FROM Phong WHERE TenPhong = @TenPhong and MaKhuVuc = @MaKhuVuc";

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenPhong", phong);
                    command.Parameters.AddWithValue("@MaKhuVuc", makhuvuc);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            maPhong = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return maPhong;
        }

        ////////////Thông tin phiếu thu
        public DataTable LoadPhieuThu(string maPhieu)
        {
            DataTable dt = new DataTable();

            string query = "SELECT * FROM PhieuThu WHERE MaPT = @MaPT";

            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPT", maPhieu);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dt;
        }

        public DataTable LoadDichVuPhieuThu(string maPhong, string maPT)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetDichVuByPhongAndPhieuThu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaPhong", maPhong);
                        cmd.Parameters.AddWithValue("@MaPT", maPT);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return dt;
        }
        public bool UpdatePhieuThu(PhieuThu phieuThu)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

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

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;    
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public void DeleteDichVuPhieuThu(string mapt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    string query = "DELETE FROM DichVuPhieuThu WHERE MaPT = @MaPT";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPT", mapt);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
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
                        command.Parameters.AddWithValue("@MaPhong", phong);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }

            return dataTable;
        }

        public bool CheckPTMoiNhat(string mapt, string maphong, DateTime ngaylap)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string query = @"
                                SELECT TOP 1 MaPT, NgayLap
                                FROM PhieuThu
                                WHERE MaPhong = @MaPhong
                                ORDER BY NgayLap DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhong", maphong); 

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        string maPhieuThuMoiNhat = reader.GetString(reader.GetOrdinal("MaPT"));
                        DateTime ngayThuMoiNhat = reader.GetDateTime(reader.GetOrdinal("NgayLap"));

                        return mapt == maPhieuThuMoiNhat && ngaylap.Date == ngayThuMoiNhat.Date;

                    }
                }
            }

            return false; 
        }


    }
}
