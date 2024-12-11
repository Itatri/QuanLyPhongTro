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

            string sql = "SELECT Email FROM ThongTinKhach WHERE MaPhong = @MaPhong";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPhong", maphong);

                    try
                    {
                        conn.Open();

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
