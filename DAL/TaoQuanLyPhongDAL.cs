using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;
using System.Configuration;


namespace DAL
{
    public class TaoQuanLyPhongDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public DataTable LayDanhSachThonTinPhong()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MaPhong, TenPhong, TienPhong, Dien, Nuoc, TienCoc, GhiChu, TrangThai FROM PHONG";

                //string query = "SELECT TenPhong, TienPhong, Dien, Nuoc, TienCoc, GhiChu, TrangThai FROM PHONG";


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

        // Láy danh sách ơhongf thoe khu vực (29/10/2024)
        public DataTable LayDanhSachThonTinPhong1(string makhuvuc)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MaPhong, TenPhong, TienPhong, dientich, Dien, Nuoc, TienCoc, GhiChu, TrangThai FROM PHONG WHERE makhuvuc = @makhuvuc";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu lệnh SQL
                    command.Parameters.AddWithValue("@makhuvuc", makhuvuc);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }



        public bool InsertPhong(TaoQuanLyPhongDTO phong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Phong (MaPhong, TenPhong, TienPhong, DienTich, Dien, Nuoc, TienCoc, GhiChu, trangthai, makhuvuc, CongNo) " +
                               "VALUES (@MaPhong, @TenPhong, @TienPhong,@DienTich, @Dien, @Nuoc, @TienCoc, @GhiChu, 0, @makhuvuc, 0)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                    command.Parameters.AddWithValue("@makhuvuc", phong.MaKhuVuc);
                    command.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
                    command.Parameters.AddWithValue("@TienPhong", phong.TienPhong);
                    command.Parameters.AddWithValue("@DienTich", phong.DienTich);
                    command.Parameters.AddWithValue("@Dien", phong.Dien);
                    command.Parameters.AddWithValue("@Nuoc", phong.Nuoc);
                    command.Parameters.AddWithValue("@TienCoc", phong.TienCoc);
                    command.Parameters.AddWithValue("@GhiChu", phong.GhiChu);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeletePhong(string maPhong)
        {
            string query = "DELETE FROM PHONG WHERE MaPhong = @MaPhong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", maPhong);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public string GetNewMaPhong()
        {
            string newMaPhong = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SinhMaPhong", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Execute the command and get the new MaPhong
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            newMaPhong = reader["NewMaPhong"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving new MaPhong: " + ex.Message);
                }
            }

            return newMaPhong;
        }

        public bool UpdatePhong(TaoQuanLyPhongDTO phong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //, NgayVao = @NgayVao, HanTro = @HanTro, TrangThai = @TrangThai
                connection.Open();
                string query = "UPDATE PHONG SET TenPhong = @TenPhong, Dien = @Dien, Nuoc = @Nuoc, GhiChu = @GhiChu " +
                               "WHERE MaPhong = @MaPhong";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                //command.Parameters.AddWithValue("@MaLoaiPhong", phong.MaLoaiPhong);
                command.Parameters.AddWithValue("@TenPhong", phong.TenPhong);


                //command.Parameters.AddWithValue("@TienCoc", phong.TienCoc);
                command.Parameters.AddWithValue("@Dien", phong.Dien);
                command.Parameters.AddWithValue("@Nuoc", phong.Nuoc);
                command.Parameters.AddWithValue("@GhiChu", phong.GhiChu);
                //command.Parameters.AddWithValue("@NgayVao", phong.NgayVao);
                //command.Parameters.AddWithValue("@HanTro", phong.HanTro);
                //command.Parameters.AddWithValue("@TrangThai", phong.TrangThai);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public DataTable TatCaDichVu()
        {
            string query = "SELECT tendichvu, dongia FROM DichVu";
            return ExecuteQuery(query);
        }

        private DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
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

        public DataTable GetAllDichVu()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT MaDichVu, TenDichVu, DonGia FROM DichVu";

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
    }
}
