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
                            AnhNhanDien = reader["AnhNhanDien"].ToString(),
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            CCCD = reader["CCCD"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            QueQuan = reader["QueQuan"].ToString(),
                            ChuKy = reader["ChuKy"].ToString(),
                            MaPhong = reader["MaPhong"].ToString(),
                            TrangThai = (int)reader["TrangThai"]
                        };

                        danhSachKhach.Add(khach);
                    }
                }
            }

            return danhSachKhach;
        }

        public void CapNhatThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            // Thực hiện câu lệnh SQL để cập nhật thông tin
            string query = "UPDATE ThongTinKhach SET HoTen = @HoTen, GioiTinh = @GioiTinh, CCCD = @CCCD, Phone = @Phone, QueQuan = @QueQuan, TrangThai = @TrangThai, MaPhong = @MaPhong, NgaySinh = @NgaySinh, AnhNhanDien = @AnhNhanDien WHERE MaKhachTro = @MaKhachTro";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaKhachTro", khachDTO.MaKhachTro);
                cmd.Parameters.AddWithValue("@HoTen", khachDTO.HoTen);
                cmd.Parameters.AddWithValue("@GioiTinh", khachDTO.GioiTinh);
                cmd.Parameters.AddWithValue("@CCCD", khachDTO.CCCD);
                cmd.Parameters.AddWithValue("@Phone", khachDTO.Phone);
                cmd.Parameters.AddWithValue("@QueQuan", khachDTO.QueQuan);
                cmd.Parameters.AddWithValue("@TrangThai", khachDTO.TrangThai);
                cmd.Parameters.AddWithValue("@MaPhong", khachDTO.MaPhong);
                cmd.Parameters.AddWithValue("@NgaySinh", khachDTO.NgaySinh);
                cmd.Parameters.AddWithValue("@AnhNhanDien", khachDTO.AnhNhanDien);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int DemSoLuongKhach()
        {
            int soLuongKhach = 0;
            string query = "SELECT COUNT(*) FROM ThongTinKhach";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                soLuongKhach = (int)cmd.ExecuteScalar();
            }

            return soLuongKhach;
        }


    }
}
