using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        SqlConnection conn;
        public EmailDAL()
        {
            conn = new SqlConnection(connectionString);
        }
        public List<string> LayDSEmail(string maphong)
        {
            List<string> emails = new List<string>();

            // Câu lệnh SQL
            string sql = "SELECT Email FROM ThongTinKhach WHERE MaPhong = @MaPhong";

            // Kết nối với cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Gắn tham số @MaPhong
                    cmd.Parameters.AddWithValue("@MaPhong", maphong);

                    try
                    {
                        // Mở kết nối
                        conn.Open();

                        // Thực thi câu lệnh và đọc dữ liệu
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string email = reader["Email"].ToString();
                                emails.Add(email);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi truy vấn dữ liệu: {ex.Message}");
                    }
                }
            }

            return emails;
        }
    }
}
