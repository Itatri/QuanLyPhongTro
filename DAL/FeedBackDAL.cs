using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedBackDAL
    {
        private string connectionString = "Data Source=TRIS72\\VANTRI;Initial Catalog=QuanLyPhongTro;Integrated Security=True;Encrypt=False";

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
    }
}
