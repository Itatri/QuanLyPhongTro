using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DTO;


namespace DAL
{
    public class ThongTinPhongDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public DataTable LayDichVuPhong(string maPhong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT dv.MaDichVu, dv.TenDichVu, dv.DonGia, dv.TrangThai " +
                               "FROM DichVuPhong dvp " +
                               "JOIN DichVu dv ON dvp.MaDichVu = dv.MaDichVu " +
                               "WHERE dvp.MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public DataTable GetAllPhong(string maPhong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Phong WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public bool InsertDichVuPhong(string maPhong, string maDichVu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO DichVuPhong (MaPhong, MaDichVu) VALUES (@MaPhong, @MaDichVu)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    command.Parameters.AddWithValue("@MaDichVu", maDichVu);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu thành công
                }
            }
        }

        public DataTable GetAllDichVu()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM DichVu";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public DataTable GetDichVuByMaPhong(string maPhong)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
        SELECT dv.TenDichVu, dv.DonGia, dvp.MaDichVu
        FROM DichVu dv
        INNER JOIN DichVuPhong dvp ON dv.MaDichVu = dvp.MaDichVu
        WHERE dvp.MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        //public void UpdatePhong(TaoQuanLyPhongDTO phong)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = @"
        //    UPDATE Phong 
        //    SET 
        //        MaKhuVuc = @MaKhuVuc, 
        //        TenPhong = @TenPhong, 
        //        NgayVao = @NgayVao, 
        //        TienCoc = @TienCoc, 
        //        TienPhong = @TienPhong, 
        //        Dien = @Dien, 
        //        Nuoc = @Nuoc, 
        //        CongNo = @CongNo, 
        //        HanTro = @HanTro, 
        //        TrangThai = @TrangThai, 
        //        GhiChu = @GhiChu 
        //    WHERE MaPhong = @MaPhong";

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
        //        cmd.Parameters.AddWithValue("@MaKhuVuc", phong.MaKhuVuc);
        //        cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
        //        cmd.Parameters.AddWithValue("@NgayVao", phong.NgayVao ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@TienCoc", phong.TienCoc);
        //        cmd.Parameters.AddWithValue("@TienPhong", phong.TienPhong);
        //        cmd.Parameters.AddWithValue("@Dien", phong.Dien);
        //        cmd.Parameters.AddWithValue("@Nuoc", phong.Nuoc);
        //        cmd.Parameters.AddWithValue("@CongNo", phong.CongNo ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@HanTro", phong.HanTro); // Cập nhật trường HanTro
        //        cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@GhiChu", phong.GhiChu);

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public void UpdatePhong(TaoQuanLyPhongDTO phong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
        UPDATE Phong 
        SET 
            MaKhuVuc = @MaKhuVuc, 
            TenPhong = @TenPhong, 
            NgayVao = @NgayVao, 
            TienCoc = @TienCoc, 
            TienPhong = @TienPhong, 
            Dien = @Dien, 
            Nuoc = @Nuoc, 
            CongNo = @CongNo, 
            HanTro = @HanTro, 
            TrangThai = @TrangThai, 
            GhiChu = @GhiChu 
        WHERE MaPhong = @MaPhong";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                cmd.Parameters.AddWithValue("@MaKhuVuc", phong.MaKhuVuc);
                cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
                cmd.Parameters.AddWithValue("@NgayVao", phong.NgayVao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TienCoc", phong.TienCoc);
                cmd.Parameters.AddWithValue("@TienPhong", phong.TienPhong);
                cmd.Parameters.AddWithValue("@Dien", phong.Dien);
                cmd.Parameters.AddWithValue("@Nuoc", phong.Nuoc);
                cmd.Parameters.AddWithValue("@CongNo", phong.CongNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HanTro", phong.HanTro ?? (object)DBNull.Value); // Cập nhật trường HanTro
                cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", phong.GhiChu);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }





        public void UpdateDichVuPhong(string maPhong, List<DichVuPhongDTO> dichVuPhongs)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Xóa các dịch vụ cũ
                    string deleteQuery = "DELETE FROM DichVuPhong WHERE MaPhong = @MaPhong";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction);
                    deleteCmd.Parameters.AddWithValue("@MaPhong", maPhong);
                    deleteCmd.ExecuteNonQuery();

                    // Thêm các dịch vụ mới
                    foreach (var dichVuPhongDTO in dichVuPhongs)
                    {
                        string insertQuery = "INSERT INTO DichVuPhong (MaPhong, MaDichVu) VALUES (@MaPhong, @MaDichVu)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction);
                        insertCmd.Parameters.AddWithValue("@MaPhong", dichVuPhongDTO.maphong);
                        insertCmd.Parameters.AddWithValue("@MaDichVu", dichVuPhongDTO.madichvu);
                        insertCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
