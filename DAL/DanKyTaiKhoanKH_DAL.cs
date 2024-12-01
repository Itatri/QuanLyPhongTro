using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Configuration;



namespace DAL
{
    public class DanKyTaiKhoanKH_DAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public DataTable GetUserPhongData()
        {
            string query = @"
        SELECT p.TenPhong as [TÊN PHÒNG], up.ID, up.MatKhau as [Mật Khẩu]
        FROM UserPhong up
        INNER JOIN Phong p ON up.MaPhong = p.MaPhong
        WHERE up.TRANGTHAI = 1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
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

        public DataTable GetUserPhongData1(string MaKhuVuc)
        {
            string query = @"
    SELECT p.TenPhong as [TÊN PHÒNG], up.ID, up.MatKhau as [Mật Khẩu]
    FROM UserPhong up
    INNER JOIN Phong p ON up.MaPhong = p.MaPhong
    WHERE up.TRANGTHAI = 1 AND p.MaKhuVuc = @MaKhuVuc"; // Use p.MaKhuVuc here

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaKhuVuc", MaKhuVuc); // Add parameter to command
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


        public bool InsertUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            // Kiểm tra xem mã phòng đã tồn tại hay chưa
            if (IsMaPhongExists(userPhong.MaPhong))
            {
                return false; // Trả về false nếu mã phòng đã tồn tại
            }

            string query = "INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai) VALUES (@ID, @MatKhau, @MaPhong, 1)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", userPhong.ID);
                command.Parameters.AddWithValue("@MatKhau", userPhong.MatKhau);
                command.Parameters.AddWithValue("@MaPhong", userPhong.MaPhong);
                command.Parameters.AddWithValue("@TrangThai", userPhong.TrangThai);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true; // Trả về true nếu chèn thành công
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }

        }


        // Phương thức kiểm tra mã phòng đã tồn tại
        private bool IsMaPhongExists(string maPhong)
        {
            string query = "SELECT COUNT(*) FROM UserPhong WHERE MaPhong = @MaPhong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", maPhong);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar(); // Lấy số lượng mã phòng
                    return count > 0; // Trả về true nếu mã phòng đã tồn tại
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }


        //Phương thức cập nhật chỉ cho người dùng cập nhật mật khẩu tài khoản
        public bool UpdatePassword(string accountId, string newPassword)
        {
            string query = "UPDATE UserPhong SET MatKhau = @MatKhau WHERE ID = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", accountId);
                command.Parameters.AddWithValue("@MatKhau", newPassword);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu cập nhật thành công
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }

        public bool DeleteUserPhong(string id)
        {
            string query = "DELETE FROM UserPhong WHERE ID = @ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }

        public bool PhongExists(string maPhong)
        {
            string query = "SELECT COUNT(*) FROM Phong WHERE MaPhong = @MaPhong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", maPhong);
                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }
        //----22/10/2024
        public DataTable GetPhongData(string khuvuc)
        {
            string query = "SELECT MaPhong, TenPhong FROM Phong WHERE MaKhuVuc = @MaKhuVuc AND trangthai = 0";
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Ensure the connection is opened
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaKhuVuc", khuvuc); // Safely add parameter value
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching room data: " + ex.Message);
            }

            return dataTable;
        }


        public DataTable GetUserPhongByMaPhong(string keyword)
        {
            string query = "SELECT ID, MatKhau, MaPhong, TrangThai FROM UserPhong WHERE MaPhong LIKE @Keyword";
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
    }
}
