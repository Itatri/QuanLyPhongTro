using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DanKyTaiKhoanKH_DAL
    {
        private string connectionString = "Data Source=DOHHUY\\SQLEXPRESS01;Initial Catalog=Test;Integrated Security=True;Encrypt=False";

        public DataTable GetUserPhongData()
        {
            string query = "SELECT ID, MatKhau, MaPhong, TrangThai FROM UserPhong";
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

        public bool InsertUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            string query = "INSERT INTO UserPhong (ID, MatKhau, MaPhong, TrangThai) VALUES (@ID, @MatKhau, @MaPhong, @TrangThai)";
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
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message);
                }
            }
        }

        public bool UpdateUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            string query = "UPDATE UserPhong SET MatKhau = @MatKhau, MaPhong = @MaPhong, TrangThai = @TrangThai WHERE ID = @ID";
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
                    return true;
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

        public DataTable GetPhongData()
        {
            string query = "SELECT MaPhong FROM Phong WHERE TrangThai = 1";
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching room data: " + ex.Message);
            }

            return dataTable;
        }
    }
}
