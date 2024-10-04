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
    public class DangNhapAdminDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;


        public bool UpdatePassword(string id, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"
            UPDATE DangNhap
            SET PassWord = @Password
            WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số cho câu truy vấn
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@Password", newPassword);

                // Thực hiện truy vấn
                int rowsAffected = command.ExecuteNonQuery();

                // Trả về true nếu có bản ghi bị ảnh hưởng (cập nhật thành công)
                return rowsAffected > 0;
            }
        }


    }
}
