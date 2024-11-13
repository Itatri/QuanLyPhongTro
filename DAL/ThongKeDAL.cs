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
        public DataTable ThongKeDoanhThuTheoThang(int nam,string khuvuc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ThongKeDoanhThuTheoThangVaKhuVuc", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@KhuVuc", khuvuc);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            conn.Close();
            return dt;
        }
        public DataTable ThongKeDichVuTheoNamVaKhuVuc(int nam,string khuvuc)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ThongKeDichVuTheoNamVaKhuVuc", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@KhuVuc", khuvuc);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ThongKeDichVuTheoNamVaKhuVuc: " + ex.Message);
            }
            conn.Close();
            return dt;
        }
        public DataTable ThongKeDichVuTheoThangVaKhuVuc(int thang,int nam, string khuvuc)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ThongKeDichVuTheoThangVaKhuVuc", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@KhuVuc", khuvuc);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ThongKeDichVuTheoThangVaKhuVuc: " + ex.Message);
            }
            conn.Close();
            return dt;
        }
    }
}
