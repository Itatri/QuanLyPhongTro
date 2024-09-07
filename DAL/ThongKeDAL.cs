using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThongKeDAL
    {
        string strConn = "Data Source=DESKTOP-OJ39RBQ;Initial Catalog=Test;User ID=sa;Password=123;Encrypt=False;";

        SqlConnection conn;
        public ThongKeDAL()
        {
            conn = new SqlConnection(strConn);
        }
        public DataTable ThongKeTheoThang(int thang, int nam)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand("ThongKePhieuThuTheoThangVaNam", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

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
        public DataTable ThongKeTheoNam(int nam)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ThongKeTongHopTheoThang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ThongKeTheoNam: " + ex.Message);
            }
            conn.Close();
            return dt;
        }
    }
}
