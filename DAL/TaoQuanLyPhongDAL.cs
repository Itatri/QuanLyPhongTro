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
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public List<TaoQuanLyPhongDTO> GetAllPhong()
        {
            List<TaoQuanLyPhongDTO> phongs = new List<TaoQuanLyPhongDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM PHONG";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TaoQuanLyPhongDTO phong = new TaoQuanLyPhongDTO
                    {
                        MaPhong = reader["MaPhong"].ToString(),
                        MaLoaiPhong = reader["MaLoaiPhong"].ToString(),
                        MaKhuVuc = reader["MaKhuVuc"].ToString(),
                        TenPhong = reader["TenPhong"].ToString(),
                        NgayVao = reader["NgayVao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayVao"]) : DateTime.MinValue,
                        TienCoc = reader["TienCoc"] != DBNull.Value ? float.Parse(reader["TienCoc"].ToString()) : 0,
                        Dien = reader["Dien"] != DBNull.Value ? float.Parse(reader["Dien"].ToString()) : 0,
                        Nuoc = reader["Nuoc"] != DBNull.Value ? float.Parse(reader["Nuoc"].ToString()) : 0,
                        HanTro = reader["HanTro"] != DBNull.Value ? Convert.ToDateTime(reader["HanTro"]) : DateTime.MinValue,
                        TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToBoolean(reader["TrangThai"]) : false,
                        GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : string.Empty
                    };

                    phongs.Add(phong);
                }
            }

            return phongs;
        }

        public bool InsertPhong(TaoQuanLyPhongDTO phong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO PHONG (MaPhong, MaLoaiPhong, TenPhong, NgayVao, TienCoc, Dien, Nuoc, HanTro, TrangThai, GhiChu) " +
                               "VALUES (@MaPhong, @MaLoaiPhong, @TenPhong, @NgayVao, @TienCoc, @Dien, @Nuoc, @HanTro, @TrangThai, @GhiChu)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", phong.MaPhong);
                command.Parameters.AddWithValue("@MaLoaiPhong", phong.MaLoaiPhong);
                //command.Parameters.AddWithValue("@MaKhuVuc", phong.MaKhuVuc);
                command.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
                command.Parameters.AddWithValue("@NgayVao", phong.NgayVao);
                command.Parameters.AddWithValue("@TienCoc", phong.TienCoc);
                command.Parameters.AddWithValue("@Dien", phong.Dien);
                command.Parameters.AddWithValue("@Nuoc", phong.Nuoc);
                command.Parameters.AddWithValue("@HanTro", phong.HanTro);
                command.Parameters.AddWithValue("@TrangThai", phong.TrangThai);
                command.Parameters.AddWithValue("@GhiChu", phong.GhiChu);

                return command.ExecuteNonQuery() > 0;
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
                    using (SqlCommand command = new SqlCommand("SinhMaPhong01", connection))
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

    }
}
