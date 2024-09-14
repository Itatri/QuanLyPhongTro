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
    public class QuanLyDichVuDAL
    {
        // Lấy chuỗi kết nối từ tệp App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;

        public DataTable GetAllServices()
        {
            string query = "SELECT * FROM DichVu";
            return ExecuteQuery(query);
        }

        public int InsertService(ThongTinDichVuDTO service)
        {
            string query = "INSERT INTO DichVu (MaDichVu, TenDichVu, DonGia, TrangThai) VALUES (@MaDV, @TenDichVu, @DonGia, @TrangThai)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaDV", service.MaDichVu),
                new SqlParameter("@TenDichVu", service.TenDichVu),
                new SqlParameter("@DonGia", service.DonGia),
                new SqlParameter("@TrangThai", service.TrangThai)
            };
            return ExecuteNonQuery(query, parameters);
        }

        public int DeleteService(string maDV)
        {
            string query = "DELETE FROM DichVu WHERE MaDichVu = @MaDV";
            SqlParameter[] parameters = { new SqlParameter("@MaDV", maDV) };
            return ExecuteNonQuery(query, parameters);
        }

        public int UpdateService(ThongTinDichVuDTO service)
        {
            string query = "UPDATE DichVu SET TenDichVu = @TenDichVu, DonGia = @DonGia, TrangThai = @TrangThai WHERE MaDichVu = @MaDichVu";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaDichVu", service.MaDichVu),
                new SqlParameter("@TenDichVu", service.TenDichVu),
                new SqlParameter("@DonGia", service.DonGia),
                new SqlParameter("@TrangThai", service.TrangThai)
            };
            return ExecuteNonQuery(query, parameters);
        }

        public string GenerateNewServiceCode()
        {
            string query = "EXEC SinhMaDichVu";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        public bool DoesServiceExist(string maDV)
        {
            string query = "SELECT COUNT(*) FROM DichVu WHERE MaDichVu = @MaDV";
            using (SqlCommand cmd = new SqlCommand(query, new SqlConnection(connectionString)))
            {
                cmd.Parameters.AddWithValue("@MaDV", maDV);
                cmd.Connection.Open();
                int count = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
                return count > 0;
            }
        }

        private DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        private int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
