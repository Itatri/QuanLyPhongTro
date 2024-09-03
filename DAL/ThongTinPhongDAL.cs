using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThongTinPhongDAL
    {
        private string connectionString = "Data Source=TRIS72\\VANTRI;Initial Catalog=QuanLyPhongTro;Integrated Security=True;Encrypt=False";

        public DataTable LayTatCaPhong()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MaPhong, TenPhong FROM Phong";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
