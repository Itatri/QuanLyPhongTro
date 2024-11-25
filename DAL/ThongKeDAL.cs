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
    public class ThongKeDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        SqlConnection conn;
        public ThongKeDAL()
        {
            conn = new SqlConnection(connectionString);
        }
        public DataTable ThongKeDoanhThuTheoNam(int nam, string khuvuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC ThongKeDoanhThuTheoNam @Nam, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
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
        public DataTable ThongKeDoanhThuTheoThang(int nam, int thang, string khuvuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC ThongKeDoanhThuTheoThangVaKhuVuc @Nam, @Thang, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
                        cmd.Parameters.AddWithValue("@Thang", thang);
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
        public DataTable ThongKeDichVuTheoNamVaKhuVuc(int nam,string khuvuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC ThongKeDichVuTheoNamVaKhuVuc @Nam, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
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
        public DataTable ThongKeDichVuTheoThangVaKhuVuc(int thang,int nam, string khuvuc)
        {
            DataTable dt = new DataTable();
            string query = "EXEC ThongKeDichVuThang @Nam, @Thang, @MaKhuVuc";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", nam);
                        cmd.Parameters.AddWithValue("@Thang", thang);
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
    }
}
