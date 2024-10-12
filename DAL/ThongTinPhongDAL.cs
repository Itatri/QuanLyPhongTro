using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace DAL
{
    public class ThongTinPhongDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;



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

        public DataTable LayPhongTheoHanTroTangDan(string makhuvuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TrangThai AS [Đã thuê], TenPhong As [Tên Phòng], NgayVao as [Ngày Vào], HanTro as [Hạn trọ], TienCoc as [Tiền cọc], TienPhong as [Tiền phòng], Dien as [Điện], Nuoc as [Nước], CongNo as [Công nợ], GhiChu as [Ghi chú] FROM PHONG WHERE MaKhuVuc = @MaKhuVuc ORDER BY HanTro ASC"; //---------> tăng dần
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MaKhuVuc", makhuvuc);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable LayPhongTheoTrangThai(string makhuvuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT TrangThai AS [Đã thuê], TenPhong As [Tên Phòng], NgayVao as [Ngày Vào], HanTro as [Hạn trọ], TienCoc as [Tiền cọc], TienPhong as [Tiền phòng], Dien as [Điện], Nuoc as [Nước], CongNo as [Công nợ], GhiChu as [Ghi chú] FROM PHONG WHERE MaKhuVuc = @MaKhuVuc ORDER BY TrangThai ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MaKhuVuc", makhuvuc);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
