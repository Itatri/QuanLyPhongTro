using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.ThongTinKhachDAL;

namespace DAL
{
    public class ThongTinKhachDAL
    {
        private string connectionString = "Data Source=TRIS72\\VANTRI;Initial Catalog=QuanLyPhongTro;Integrated Security=True;Encrypt=False";

        public List<ThongTinKhachDTO> LayTatCaThongTinKhach()
        {
            List<ThongTinKhachDTO> danhSachKhach = new List<ThongTinKhachDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ThongTinKhach";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThongTinKhachDTO khach = new ThongTinKhachDTO
                        {
                            MaKhachTro = reader["MaKhachTro"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            AnhNhanDien = reader["AnhNhanDien"] as byte[],
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            CCCD = reader["CCCD"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            QueQuan = reader["QueQuan"].ToString(),
                            ChuKy = reader["ChuKy"] as byte[],
                            MaPhong = reader["MaPhong"].ToString(),
                            TrangThai = reader["TrangThai"].ToString()
                        };

                        danhSachKhach.Add(khach);
                    }
                }
            }

            return danhSachKhach;
        }
    }
}
