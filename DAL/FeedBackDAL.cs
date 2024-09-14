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

        public List<FeedBackDTO> LayTatCaFeedBack()
        {
            List<FeedBackDTO> danhSachFeedBack = new List<FeedBackDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM FeedBack";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                        };

                        danhSachFeedBack.Add(feedback);
                    }
                }
            }

            return danhSachFeedBack;
        }

        public List<FeedBackDTO> TimKiemFeedBack(string searchValue)
        {
            List<FeedBackDTO> list = new List<FeedBackDTO>();
            // Câu lệnh SQL để tìm kiếm
            string query = "SELECT * FROM FeedBack WHERE MaFB LIKE @searchValue OR MaPhong LIKE @searchValue OR MoTa LIKE @searchValue";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FeedBackDTO feedBack = new FeedBackDTO
                    {
                        MaFB = reader["MaFB"].ToString(),
                        MaPhong = reader["MaPhong"].ToString(),
                        MoTa = reader["MoTa"].ToString()
                    };
                    list.Add(feedBack);
                }
            }

            return list;
        }
    }
}
