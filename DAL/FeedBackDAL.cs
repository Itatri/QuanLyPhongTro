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
    public class FeedBackDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;
        public List<FeedBackDTO> LayTatCaFeedBack(string makhuvuc)
        {
            List<FeedBackDTO> danhSachFeedBack = new List<FeedBackDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM FeedBack,Phong WHERE FeedBack.MaPhong = Phong.MaPhong AND MaKhuVuc = @makhuvuc";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@makhuvuc", makhuvuc); // Thêm tham số @makhuvuc

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FeedBackDTO feedback = new FeedBackDTO
                        {
                            MaFB = reader["MaFB"].ToString(),
                            MaPhong = reader["MaPhong"].ToString(),
                            MoTa = reader["MoTa"].ToString(),
                            NgayGui = reader.GetDateTime(reader.GetOrdinal("NgayGui")),
                            PhanHoi = reader["PhanHoi"].ToString(),
                            NgayPhanHoi = reader.IsDBNull(reader.GetOrdinal("NgayPhanHoi")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("NgayPhanHoi")),
                            TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? 0 : reader.GetInt32(reader.GetOrdinal("TrangThai"))
                        };

                        danhSachFeedBack.Add(feedback);
                    }
                }
            }

            return danhSachFeedBack;
        }





        //public List<FeedBackDTO> TimKiemFeedBack(string searchValue)
        //{
        //    List<FeedBackDTO> list = new List<FeedBackDTO>();
        //    string query = "SELECT * FROM FeedBack WHERE MaFB LIKE @searchValue OR MaPhong LIKE @searchValue OR MoTa LIKE @searchValue";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            FeedBackDTO feedback = new FeedBackDTO
        //            {
        //                MaFB = reader["MaFB"].ToString(),
        //                MaPhong = reader["MaPhong"].ToString(),
        //                MoTa = reader["MoTa"].ToString(),
        //                NgayGui = reader.GetDateTime(reader.GetOrdinal("NgayGui")),
        //                PhanHoi = reader["PhanHoi"].ToString(),
        //                NgayPhanHoi = reader.GetDateTime(reader.GetOrdinal("NgayPhanHoi"))
        //            };

        //            list.Add(feedback);
        //        }
        //    }

        //    return list;
        //}

        public bool CapNhatPhanHoi(string maFB, string phanHoi, DateTime ngayPhanHoi)
        {
            string query = "UPDATE FeedBack SET PhanHoi = @phanHoi, NgayPhanHoi = @ngayPhanHoi, TrangThai = @trangThai WHERE MaFB = @maFB";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@phanHoi", phanHoi);
                command.Parameters.AddWithValue("@ngayPhanHoi", ngayPhanHoi);
                command.Parameters.AddWithValue("@trangThai", 1); // Đặt giá trị TrangThai là 1
                command.Parameters.AddWithValue("@maFB", maFB);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

      

        public List<FeedBackDTO> LocFeedBack(int trangThai)
        {
            List<FeedBackDTO> list = new List<FeedBackDTO>();
            string query = "SELECT * FROM FeedBack WHERE TrangThai = @trangThai";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@trangThai", trangThai);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FeedBackDTO feedback = new FeedBackDTO
                    {
                        MaFB = reader["MaFB"].ToString(),
                        MaPhong = reader["MaPhong"].ToString(),
                        MoTa = reader["MoTa"].ToString(),
                        NgayGui = reader.GetDateTime(reader.GetOrdinal("NgayGui")),
                        PhanHoi = reader["PhanHoi"].ToString(),
                        TrangThai = reader.GetInt32(reader.GetOrdinal("TrangThai")), // Thêm trường TrangThai
                        NgayPhanHoi = reader["NgayPhanHoi"] != DBNull.Value
                            ? reader.GetDateTime(reader.GetOrdinal("NgayPhanHoi"))
                            : (DateTime?)null // Chuyển đổi về null nếu giá trị là DBNull
                    };

                    list.Add(feedback);
                }
            }

            return list;
        }


    }
}
