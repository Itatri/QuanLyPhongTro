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
    public class QuanLiPhongDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;



        public DataTable LayTatCaPhong()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //string query = "SELECT MaPhong, TenPhong FROM Phong";
                string query = "SELECT * FROM Phong where TrangThai = 1";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }


        public DataTable TimKiemPhong(string keyword)
        {


            string query = "SELECT TrangThai AS [Đã thuê], TenPhong AS [Tên Phòng]," +
             "NgayVao AS [Ngày Vào], HanTro AS [Hạn trọ], TienCoc AS [Tiền cọc], " +
                 "TienPhong AS [Tiền phòng]," +
                 "CongNo AS [Công nợ], GhiChu AS [Ghi chú] " +
                 "FROM Phong " +
                 "WHERE MaPhong LIKE @Keyword OR TenPhong LIKE @Keyword";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
                return dataTable;
            }
        }



        public DataTable LayPhongTheoHanTroTangDan(string makhuvuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Thêm cột MaPhong vào truy vấn
                string query = "SELECT MaPhong, TrangThai AS [Đã thuê], TenPhong AS [Tên Phòng], NgayVao AS [Ngày Vào], HanTro AS [Hạn trọ], TienCoc AS [Tiền cọc], TienPhong AS [Tiền phòng], Dien AS [Điện], Nuoc AS [Nước], CongNo AS [Công nợ], GhiChu AS [Ghi chú] FROM PHONG WHERE MaKhuVuc = @MaKhuVuc ORDER BY HanTro ASC";
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
                // Thêm cột MaPhong vào truy vấn
                string query = "SELECT MaPhong, TrangThai AS [Đã thuê], TenPhong AS [Tên Phòng], NgayVao AS [Ngày Vào], HanTro AS [Hạn trọ], TienCoc AS [Tiền cọc], TienPhong AS [Tiền phòng], Dien AS [Điện], Nuoc AS [Nước], CongNo AS [Công nợ], GhiChu AS [Ghi chú] FROM PHONG WHERE MaKhuVuc = @MaKhuVuc ORDER BY TrangThai ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MaKhuVuc", makhuvuc);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }




        public DataTable LayDichVuPhong(string maPhong)
        {
            string query = "SELECT * FROM DichVuPhong WHERE MaPhong = @MaPhong";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPhong", maPhong)
            };

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                foreach (var param in parameters)
                {
                    adapter.SelectCommand.Parameters.Add(param);
                }

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public DataTable LayDichVuTheoMaPhong(string maPhong)
        {
            string query = "SELECT * FROM DichVuPhong WHERE MaPhong = @MaPhong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@MaPhong", maPhong);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

    }
}
