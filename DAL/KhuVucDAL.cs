using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhuVucDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;


        public KhuVucDTO GetKhuVucByMaKhuVuc(string maKhuVuc)
        {
            KhuVucDTO khuVuc = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM KhuVuc WHERE MaKhuVuc = @MaKhuVuc";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    khuVuc = new KhuVucDTO
                    {
                        MaKhuVuc = reader["MaKhuVuc"].ToString(),
                        TenKhuVuc = reader["TenKhuVuc"].ToString(),
                        TrangThai = reader["TrangThai"] != DBNull.Value && Convert.ToBoolean(reader["TrangThai"])
                    };
                }
            }

            return khuVuc;
        }

    }
}
