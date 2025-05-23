﻿using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThongTinAdminDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public ThongTinAdminDTO GetThongTinAdminByIdUser(string idUser)
        {
            ThongTinAdminDTO admin = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ThongTinAdmin WHERE IdUser = @IdUser";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUser", idUser);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    admin = new ThongTinAdminDTO
                    {
                        MaAdmin = reader["MaAdmin"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.MinValue,
                        Cccd = reader["Cccd"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        ChuKy = reader["ChuKy"].ToString(),
                        NganHang = reader["NganHang"].ToString(),
                        TaiKhoan = reader["TaiKhoan"].ToString(),
                        ////ChuKyXacNhan = reader["ChuKyXacNhan"].ToString(),
                        IdUser = reader["IdUser"].ToString(),
                        TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToInt32(reader["TrangThai"]) : 0
                    };
                }
            }

            return admin;
        }
        public ThongTinAdminDTO GetThongTinAdminByKhuVuc(string makhuvuc)
        {
            ThongTinAdminDTO admin = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ThongTinAdmin, DangNhap WHERE MaKhuVuc = @MaKhuVuc";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhuVuc", makhuvuc);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    admin = new ThongTinAdminDTO
                    {
                        MaAdmin = reader["MaAdmin"].ToString(),
                        HoTen = reader["HoTen"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.MinValue,
                        Cccd = reader["Cccd"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        ChuKy = reader["ChuKy"].ToString(),
                        NganHang = reader["NganHang"].ToString(),
                        TaiKhoan = reader["TaiKhoan"].ToString(),
                        ////ChuKyXacNhan = reader["ChuKyXacNhan"].ToString(),
                        IdUser = reader["IdUser"].ToString(),
                        TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToInt32(reader["TrangThai"]) : 0
                    };
                }
            }

            return admin;
        }

        public bool KiemTraThongTinAdmin(string idUser)
        {
            string query = "SELECT COUNT(*) FROM ThongTinAdmin WHERE IdUser = @IdUser";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUser", idUser);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }


        public string LayMaAdminMoi()
        {
            string query = "SELECT TOP 1 MaAdmin FROM ThongTinAdmin ORDER BY MaAdmin DESC";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                string lastMaAdmin = (string)command.ExecuteScalar();

                if (string.IsNullOrEmpty(lastMaAdmin))
                {
                    return "A01"; // Trường hợp chưa có mã nào
                }

                // Tăng số thứ tự
                int lastNumber = int.Parse(lastMaAdmin.Substring(1));
                return "A" + (lastNumber + 1).ToString("D2");
            }
        }

       

        public bool ThemThongTinAdmin(ThongTinAdminDTO adminInfo)
        {
            string query = @"
            INSERT INTO ThongTinAdmin 
            (MaAdmin, HoTen, GioiTinh, NgaySinh, Cccd, Phone, DiaChi, ChuKy, IdUser, TrangThai) 
            VALUES 
            (@MaAdmin, @HoTen, @GioiTinh, @NgaySinh, @Cccd, @Phone, @DiaChi, @ChuKy, @IdUser, @TrangThai)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Gán các giá trị cho tham số
                command.Parameters.AddWithValue("@MaAdmin", adminInfo.MaAdmin);
                command.Parameters.AddWithValue("@HoTen", adminInfo.HoTen);
                command.Parameters.AddWithValue("@GioiTinh", adminInfo.GioiTinh);
                command.Parameters.AddWithValue("@NgaySinh", adminInfo.NgaySinh);
                command.Parameters.AddWithValue("@Cccd", adminInfo.Cccd);
                command.Parameters.AddWithValue("@Phone", adminInfo.Phone);
                command.Parameters.AddWithValue("@DiaChi", adminInfo.DiaChi);
                command.Parameters.AddWithValue("@ChuKy", adminInfo.ChuKy ?? (object)DBNull.Value); // Đảm bảo không cập nhật nếu không có ảnh chữ ký
                command.Parameters.AddWithValue("@IdUser", adminInfo.IdUser);
                command.Parameters.AddWithValue("@TrangThai", 1); // Set TrangThai to 1

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0; // Trả về true nếu thêm thành công
            }
        }

       
        public bool UpdateThongTinAdmin(ThongTinAdminDTO admin)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    UPDATE ThongTinAdmin
                    SET 
                        MaAdmin = @MaAdmin,
                        HoTen = @HoTen,
                        GioiTinh = @GioiTinh,
                        NgaySinh = @NgaySinh,
                        NganHang = @NganHang,
                        TaiKhoan = @TaiKhoan,
                        Cccd = @Cccd,
                        Phone = @Phone,
                        DiaChi = @DiaChi, " +
                                    (admin.ChuKy != null ? "ChuKy = @ChuKy, " : "") +
                                @"   TrangThai = @TrangThai
                    WHERE IdUser = @IdUser";

                    if (admin.ChuKy == null)
                    {
                        query = query.Replace(", TrangThai", "TrangThai");
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaAdmin", admin.MaAdmin);
                    command.Parameters.AddWithValue("@HoTen", admin.HoTen);
                    command.Parameters.AddWithValue("@GioiTinh", admin.GioiTinh);
                    command.Parameters.AddWithValue("@NganHang", admin.NganHang);
                    command.Parameters.AddWithValue("@TaiKhoan", admin.TaiKhoan);
                    command.Parameters.AddWithValue("@NgaySinh", admin.NgaySinh != DateTime.MinValue ? (object)admin.NgaySinh : DBNull.Value);
                    command.Parameters.AddWithValue("@Cccd", admin.Cccd);
                    command.Parameters.AddWithValue("@Phone", admin.Phone);
                    command.Parameters.AddWithValue("@DiaChi", admin.DiaChi);

                    if (admin.ChuKy != null)
                    {
                        command.Parameters.AddWithValue("@ChuKy", admin.ChuKy);
                    }

                    command.Parameters.AddWithValue("@TrangThai", 1); 
                    command.Parameters.AddWithValue("@IdUser", admin.IdUser);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin admin: " + ex.Message);
            }
        }

        // Phương thức kiểm tra sự tồn tại của admin
        public bool KiemTraAdminExist(string idUser)
        {
            // Câu truy vấn SQL để kiểm tra sự tồn tại của admin
            string query = "SELECT COUNT(*) FROM ThongTinAdmin WHERE IdUser = @IdUser";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUser", idUser);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Trả về true nếu tồn tại, ngược lại false
            }
        }


        // Phương thức để cập nhật chữ ký của Admin
        public void CapNhatChuKy(string idUser, string chuKy)
        {
            string query = "UPDATE ThongTinAdmin SET ChuKy = @ChuKy WHERE IdUser = @IdUser";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ChuKy", (object)chuKy ?? DBNull.Value);
                command.Parameters.AddWithValue("@IdUser", idUser);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }




    }
}
