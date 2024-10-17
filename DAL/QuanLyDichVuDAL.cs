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
        private string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTro"].ConnectionString;


        //--------------------------------------------------------------------- 15/10/2024
        public DataTable GetAllServices()
        {
            string query = "SELECT MaDichVu,TenDichVu ,DonGia ,TrangThai  FROM DichVu;";
            //string query = "SELECT MaDichVu,TenDichVu as [TÊN DỊCH VỤ],DonGia AS [ĐƠN GIÁ] ,TrangThai [TRẠNG THÁI]  FROM DichVu;";
            return ExecuteQuery(query);

            
        }
        //--------------------------------------------------------------------- 15/10/2024

        public DataTable GetDichVuFormQLPhong()
        {
            string query = "select * FROM DichVu";
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
            string query = "DELETE DichVu WHERE MaDichVu = @MaDV";
            //string query = "UPDATE DichVu SET TRANGTHAI = 0 WHERE MaDichVu = @MaDV";
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

        public bool ServiceExists(string maDichVu)
        {
            string query = "SELECT COUNT(*) FROM DichVu WHERE MaDichVu = @MaDichVu";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaDichVu", maDichVu);
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public DataTable GetUserPhongByMaPhong(string maPhong)
        {
            string query = "SELECT ID, MatKhau, MaPhong, TrangThai FROM UserPhong WHERE MaPhong = @MaPhong";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaPhong", maPhong);
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

        public DataTable TimKiemDichVu(string keyword)
        {
            string query = "SELECT MaDichVu, TenDichVu, DonGia, TrangThai FROM DichVu WHERE TenDichVu LIKE '%' + @Keyword + '%'";

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

        public DataTable SapXepDichVuTheoTrangThai()
        {
            string query = "SELECT MaDichVu, TenDichVu, DonGia, TrangThai FROM DichVu ORDER BY TrangThai";
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
    }
}
