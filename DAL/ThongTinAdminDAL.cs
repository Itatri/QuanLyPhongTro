using DTO;
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
                        QueQuan = reader["QueQuan"].ToString(),
                        ChuKy = reader["ChuKy"].ToString(),
                        ////ChuKyXacNhan = reader["ChuKyXacNhan"].ToString(),
                        IdUser = reader["IdUser"].ToString(),
                        TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToInt32(reader["TrangThai"]) : 0
                    };
                }
            }

            return admin;
        }



        //public bool UpdateThongTinAdmin(ThongTinAdminDTO admin)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string query = @"
        //    UPDATE ThongTinAdmin
        //    SET 
        //        MaAdmin = @MaAdmin,
        //        HoTen = @HoTen,
        //        GioiTinh = @GioiTinh,
        //        NgaySinh = @NgaySinh,
        //        Cccd = @Cccd,
        //        Phone = @Phone,
        //        QueQuan = @QueQuan,
        //        ChuKy = @ChuKy,
        //        TrangThai = @TrangThai
        //    WHERE IdUser = @IdUser";

        //        SqlCommand command = new SqlCommand(query, connection);

        //        // Thêm các tham số cho câu truy vấn
        //        command.Parameters.AddWithValue("@MaAdmin", admin.MaAdmin);
        //        command.Parameters.AddWithValue("@HoTen", admin.HoTen);
        //        command.Parameters.AddWithValue("@GioiTinh", admin.GioiTinh);
        //        command.Parameters.AddWithValue("@NgaySinh", admin.NgaySinh != DateTime.MinValue ? (object)admin.NgaySinh : DBNull.Value);
        //        command.Parameters.AddWithValue("@Cccd", admin.Cccd);
        //        command.Parameters.AddWithValue("@Phone", admin.Phone);
        //        command.Parameters.AddWithValue("@QueQuan", admin.QueQuan);
        //        command.Parameters.AddWithValue("@ChuKy", admin.ChuKy);
        //        command.Parameters.AddWithValue("@TrangThai", admin.TrangThai);
        //        command.Parameters.AddWithValue("@IdUser", admin.IdUser);

        //        // Thực hiện truy vấn
        //        int rowsAffected = command.ExecuteNonQuery();

        //        // Trả về true nếu có bản ghi bị ảnh hưởng (cập nhật thành công)
        //        return rowsAffected > 0;
        //    }
        //}

        public bool UpdateThongTinAdmin(ThongTinAdminDTO admin)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu lệnh SQL động, chỉ cập nhật cột ChuKy nếu có giá trị mới
                string query = @"
        UPDATE ThongTinAdmin
        SET 
            MaAdmin = @MaAdmin,
            HoTen = @HoTen,
            GioiTinh = @GioiTinh,
            NgaySinh = @NgaySinh,
            Cccd = @Cccd,
            Phone = @Phone,
            QueQuan = @QueQuan, " +
                    (admin.ChuKy != null ? "ChuKy = @ChuKy, " : "") + // Chỉ thêm ChuKy nếu có giá trị mới
                @"   TrangThai = @TrangThai
        WHERE IdUser = @IdUser";

                SqlCommand command = new SqlCommand(query, connection);

                // Thêm các tham số cho câu truy vấn
                command.Parameters.AddWithValue("@MaAdmin", admin.MaAdmin);
                command.Parameters.AddWithValue("@HoTen", admin.HoTen);
                command.Parameters.AddWithValue("@GioiTinh", admin.GioiTinh);
                command.Parameters.AddWithValue("@NgaySinh", admin.NgaySinh != DateTime.MinValue ? (object)admin.NgaySinh : DBNull.Value);
                command.Parameters.AddWithValue("@Cccd", admin.Cccd);
                command.Parameters.AddWithValue("@Phone", admin.Phone);
                command.Parameters.AddWithValue("@QueQuan", admin.QueQuan);

                if (admin.ChuKy != null)
                {
                    command.Parameters.AddWithValue("@ChuKy", admin.ChuKy); // Thêm tham số cho ChuKy nếu không null
                }

                command.Parameters.AddWithValue("@TrangThai", admin.TrangThai);
                command.Parameters.AddWithValue("@IdUser", admin.IdUser);

                // Thực hiện truy vấn
                int rowsAffected = command.ExecuteNonQuery();

                // Trả về true nếu có bản ghi bị ảnh hưởng (cập nhật thành công)
                return rowsAffected > 0;
            }
        }



    }
}
