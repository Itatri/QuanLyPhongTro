using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAL.ThongTinKhachDAL;

namespace DAL
{
    public class ThongTinKhachDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public List<ThongTinKhachDTO> LayTatCaThongTinKhach(string makhuvuc)
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
                            //MaKhachTro = reader["MaKhachTro"].ToString(),
                            //HoTen = reader["HoTen"].ToString(),
                            //GioiTinh = reader["GioiTinh"].ToString(),

                            //NgaySinh = (DateTime)reader["NgaySinh"],
                            //CCCD = reader["CCCD"].ToString(),
                            //Phone = reader["Phone"].ToString(),
                            //QueQuan = reader["QueQuan"].ToString(),
                            //ChuKy = reader["ChuKy"].ToString(),
                            //MaPhong = reader["MaPhong"].ToString(),
                            //TrangThai = (int)reader["TrangThai"]

                            MaKhachTro = reader["MaKhachTro"].ToString(),
                            HoTen = reader["HoTen"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            CCCD = reader["CCCD"].ToString(),
                            NgayCap = (DateTime)reader["NgayCap"],
                            NoiCap = reader["NoiCap"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            ThuongTru = reader["ThuongTru"].ToString(),
                            QueQuan = reader["QueQuan"].ToString(),
                            QuanHe = reader["QuanHe"].ToString(),
                            ChuKy = reader["ChuKy"].ToString(),
                            MaPhong = reader["MaPhong"].ToString(),
                            TrangThai = (int)reader["TrangThai"],
                            Email = reader["Email"].ToString()
                        };

                        danhSachKhach.Add(khach);
                    }
                }
            }
            return danhSachKhach;
        }





        //public void CapNhatThongTinKhach(ThongTinKhachDTO khachDTO)
        //{
        //    // Thực hiện câu lệnh SQL để cập nhật thông tin
        //    string query = "UPDATE ThongTinKhach SET HoTen = @HoTen, GioiTinh = @GioiTinh, CCCD = @CCCD, Phone = @Phone, QueQuan = @QueQuan, TrangThai = @TrangThai, MaPhong = @MaPhong, NgaySinh = @NgaySinh, ChuKy = @ChuKy WHERE MaKhachTro = @MaKhachTro";

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@MaKhachTro", khachDTO.MaKhachTro);
        //        cmd.Parameters.AddWithValue("@HoTen", khachDTO.HoTen);
        //        cmd.Parameters.AddWithValue("@GioiTinh", khachDTO.GioiTinh);
        //        cmd.Parameters.AddWithValue("@CCCD", khachDTO.CCCD);
        //        cmd.Parameters.AddWithValue("@Phone", khachDTO.Phone);
        //        cmd.Parameters.AddWithValue("@QueQuan", khachDTO.QueQuan);
        //        cmd.Parameters.AddWithValue("@TrangThai", khachDTO.TrangThai);
        //        cmd.Parameters.AddWithValue("@MaPhong", khachDTO.MaPhong);
        //        cmd.Parameters.AddWithValue("@NgaySinh", khachDTO.NgaySinh);
        //        //cmd.Parameters.AddWithValue("@AnhNhanDien", khachDTO.AnhNhanDien);
        //        cmd.Parameters.AddWithValue("@ChuKy", khachDTO.ChuKy);

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        public void CapNhatThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            string query = "UPDATE ThongTinKhach SET HoTen = @HoTen, GioiTinh = @GioiTinh, CCCD = @CCCD, Phone = @Phone, QueQuan = @QueQuan, TrangThai = @TrangThai, MaPhong = @MaPhong, NgaySinh = @NgaySinh, ChuKy = @ChuKy, Email = @Email, NoiCap = @NoiCap, NgayCap = @NgayCap, QuanHe = @QuanHe WHERE MaKhachTro = @MaKhachTro";

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
                cmd.Parameters.AddWithValue("@ChuKy", khachDTO.ChuKy);
                cmd.Parameters.AddWithValue("@Email", khachDTO.Email);
                cmd.Parameters.AddWithValue("@NoiCap", khachDTO.NoiCap);
                cmd.Parameters.AddWithValue("@NgayCap", khachDTO.NgayCap);
                cmd.Parameters.AddWithValue("@QuanHe", khachDTO.QuanHe);
                cmd.Parameters.AddWithValue("@ThuongTru", khachDTO.ThuongTru);
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


       
        //public void ThemThongTinKhach(ThongTinKhachDTO khachDTO)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            string query = "INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, CCCD, Phone, QueQuan, TrangThai, MaPhong, NgaySinh, ChuKy, Email, NoiCap, NgayCap, QuanHe) " +
        //                           "VALUES (@MaKhachTro, @HoTen, @GioiTinh, @CCCD, @Phone, @QueQuan, @TrangThai, @MaPhong, @NgaySinh, @ChuKy, @Email, @NoiCap, @NgayCap, @QuanHe)";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                cmd.Parameters.AddWithValue("@MaKhachTro", khachDTO.MaKhachTro);
        //                cmd.Parameters.AddWithValue("@HoTen", khachDTO.HoTen);
        //                cmd.Parameters.AddWithValue("@GioiTinh", khachDTO.GioiTinh);
        //                cmd.Parameters.AddWithValue("@CCCD", khachDTO.CCCD);
        //                cmd.Parameters.AddWithValue("@Phone", khachDTO.Phone);
        //                cmd.Parameters.AddWithValue("@QueQuan", khachDTO.QueQuan);
        //                cmd.Parameters.AddWithValue("@TrangThai", khachDTO.TrangThai);
        //                cmd.Parameters.AddWithValue("@MaPhong", khachDTO.MaPhong);
        //                cmd.Parameters.AddWithValue("@NgaySinh", khachDTO.NgaySinh);
        //                cmd.Parameters.AddWithValue("@ChuKy", khachDTO.ChuKy);
        //                cmd.Parameters.AddWithValue("@Email", khachDTO.Email);
        //                cmd.Parameters.AddWithValue("@NoiCap", khachDTO.NoiCap);
        //                cmd.Parameters.AddWithValue("@NgayCap", khachDTO.NgayCap);
        //                cmd.Parameters.AddWithValue("@QuanHe", khachDTO.QuanHe);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi khi thêm khách hàng vào cơ sở dữ liệu: " + ex.Message);
        //    }
        //}

        public void ThemThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Prepare the base query
                    string query = "INSERT INTO ThongTinKhach (MaKhachTro, HoTen, GioiTinh, CCCD, Phone, QueQuan, TrangThai, MaPhong, NgaySinh, Email, NoiCap, NgayCap, QuanHe,ThuongTru";
                    string values = "VALUES (@MaKhachTro, @HoTen, @GioiTinh, @CCCD, @Phone, @QueQuan, @TrangThai, @MaPhong, @NgaySinh, @Email, @NoiCap, @NgayCap, @QuanHe, @ThuongTru";

                    // Include ChuKy only if it is not null
                    if (!string.IsNullOrEmpty(khachDTO.ChuKy))
                    {
                        query += ", ChuKy";
                        values += ", @ChuKy";
                    }

                    query += ") " + values + ")";

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
                        cmd.Parameters.AddWithValue("@Email", khachDTO.Email);
                        cmd.Parameters.AddWithValue("@NoiCap", khachDTO.NoiCap);
                        cmd.Parameters.AddWithValue("@NgayCap", khachDTO.NgayCap);
                        cmd.Parameters.AddWithValue("@QuanHe", khachDTO.QuanHe);
                        cmd.Parameters.AddWithValue("@ThuongTru", khachDTO.ThuongTru);
                        if (!string.IsNullOrEmpty(khachDTO.ChuKy))
                        {
                            cmd.Parameters.AddWithValue("@ChuKy", khachDTO.ChuKy);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm khách hàng vào cơ sở dữ liệu: " + ex.Message);
            }
        }


        public ThongTinKhachDTO LayThongTinKhachTheoMa(string maKhachTro)
        {
            ThongTinKhachDTO khachDTO = null;
            string query = "SELECT * FROM ThongTinKhach WHERE MaKhachTro = @MaKhachTro";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKhachTro", maKhachTro);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    khachDTO = new ThongTinKhachDTO
                    {
                        MaKhachTro = reader["MaKhachTro"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        CCCD = reader["CCCD"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        QueQuan = reader["QueQuan"].ToString(),
                        TrangThai = Convert.ToInt32(reader["TrangThai"]),
                        MaPhong = reader["MaPhong"].ToString(),
                        NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                        ThuongTru = reader["ThuongTru"].ToString(),
                        //AnhNhanDien = reader["AnhNhanDien"].ToString(),
                        ChuKy = reader["ChuKy"].ToString()
                    };
                }
            }

            return khachDTO;
        }

        public void XoaThongTinKhach(string maKhachTro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM ThongTinKhach WHERE MaKhachTro = @MaKhachTro";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhachTro", maKhachTro);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

     
        public List<ThongTinKhachDTO> TimKiemThongTinKhach(string searchValue)
        {
            List<ThongTinKhachDTO> results = new List<ThongTinKhachDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM ThongTinKhach WHERE " +
                               "MaKhachTro LIKE @searchValue OR " +
                               "HoTen LIKE @searchValue OR " +
                               "GioiTinh LIKE @searchValue OR " +
                               "CCCD LIKE @searchValue OR " +
                               "Phone LIKE @searchValue OR " +
                               "Phone LIKE @searchValue OR " +
                               "QueQuan LIKE @searchValue OR " +
                               "MaPhong LIKE @searchValue";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongTinKhachDTO item = new ThongTinKhachDTO
                            {
                                MaKhachTro = reader["MaKhachTro"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                QueQuan = reader["QueQuan"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                MaPhong = reader["MaPhong"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                // AnhNhanDien = reader["AnhNhanDien"].ToString(),
                                ChuKy = reader["ChuKy"].ToString(),
                                Email = reader["Email"].ToString(),
                                QuanHe = reader["QuanHe"].ToString(),
                                NoiCap = reader["NoiCap"].ToString(),
                                NgayCap = Convert.ToDateTime(reader["NgayCap"]),
                                ThuongTru = reader["ThuongTru"].ToString(),
                            };

                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }


        public List<ThongTinKhachDTO> LayThongTinKhachTheoMaPhong(string searchValue)
        {
            List<ThongTinKhachDTO> results = new List<ThongTinKhachDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM ThongTinKhach WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Sử dụng đúng tên tham số là "@MaPhong"
                    command.Parameters.AddWithValue("@MaPhong", searchValue);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ThongTinKhachDTO item = new ThongTinKhachDTO
                            {
                                MaKhachTro = reader["MaKhachTro"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                QueQuan = reader["QueQuan"].ToString(),
                                TrangThai = Convert.ToInt32(reader["TrangThai"]),
                                MaPhong = reader["MaPhong"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                // AnhNhanDien = reader["AnhNhanDien"].ToString(),
                                ChuKy = reader["ChuKy"].ToString(),
                                Email = reader["Email"].ToString(),
                                QuanHe = reader["QuanHe"].ToString(),
                                NoiCap = reader["NoiCap"].ToString(),
                                NgayCap = Convert.ToDateTime(reader["NgayCap"]),
                                ThuongTru = reader["ThuongTru"].ToString(),
                            };

                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }


        public void CapNhatChuKyKhachHang(string maKhachTro, string chuKyMoi)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE ThongTinKhach SET ChuKy = @ChuKy WHERE MaKhachTro = @MaKhachTro";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhachTro", maKhachTro);
                command.Parameters.AddWithValue("@ChuKy", (object)chuKyMoi ?? DBNull.Value); // Nếu chuKyMoi là null, sử dụng DBNull.Value để xóa giá trị

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
