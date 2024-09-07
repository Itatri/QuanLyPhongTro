using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class QuanLyPhieuThuDAL
    {
        string strConn = "Data Source=DESKTOP-OJ39RBQ;Initial Catalog=Test;User ID=sa;Password=123;Encrypt=False;";

        SqlConnection conn;
        public QuanLyPhieuThuDAL()
        {
            conn = new SqlConnection(strConn);
        }
        public DataTable GetThongKePhieuThu()
        {
            DataTable dataTable = new DataTable();
            string query = "GetThongKePhieuThu";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            return dataTable;
        }
        public int CountPT()
        {
            int count = 0;
            string query = "SELECT COUNT(*) FROM PhieuThu";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        count = (int)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while counting the records: " + ex.Message);
                }
            }

            return count;
        }
        public string getMaP(string tenPhong)
        {
            string ma = null;
            string query = "SELECT MaPhong FROM Phong WHERE TenPhong = @TenPhong";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenPhong", tenPhong);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        ma = result.ToString();
                    }
                }
            }

            return ma;
        }

        public void XoaPT(string ma)
        {
            string query = "DELETE FROM PhieuThu WHERE MaPT = @MaPT";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPT", ma);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while deleting the record: " + ex.Message);
                }
            }
        }
        public List<DichVu> GetDichVu()
        {
            List<DichVu> dichVuList = new List<DichVu>();
            string query = "SELECT MaDichVu, TenDichVu, DonGia FROM DichVu where TrangThai = 1";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DichVu dichVu = new DichVu
                            {
                                MaDichVu = reader["MaDichVu"].ToString(),
                                TenDichVu = reader["TenDichVu"].ToString(),
                                Gia = reader["DonGia"].ToString()
                            };
                            dichVuList.Add(dichVu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving DichVu data: " + ex.Message);
            }
            conn.Close();
            return dichVuList;
        }
        public DataTable GetPhongByName(string ten)
        {
            DataTable dt = new DataTable();
            string query = "GetPhongByName ";

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TenPhong", ten);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }

            return dt;
        }

        public bool CreatePhieuThu(PhieuThu phieuThu)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = @"
                    INSERT INTO PhieuThu (MaPT, MaPhong, NgayLap, NgayThu, TienNha, SkDien, TienDien, SkNuoc, TienNuoc, TienDV, TongTien, TrangThai)
                    VALUES (@MaPT, @MaPhong, @NgayLap, @NgayThu, @TienNha, @SkDien, @TienDien, @SkNuoc, @TienNuoc, @TienDV, @TongTien, @TrangThai)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPT", phieuThu.MaPT);
                    command.Parameters.AddWithValue("@MaPhong", phieuThu.MaPhong);
                    command.Parameters.AddWithValue("@NgayLap", phieuThu.NgayLap);
                    command.Parameters.AddWithValue("@NgayThu", phieuThu.NgayThu);
                    command.Parameters.AddWithValue("@TienNha", phieuThu.TienNha);
                    command.Parameters.AddWithValue("@SkDien", phieuThu.SkDien);
                    command.Parameters.AddWithValue("@TienDien", phieuThu.TienDien);
                    command.Parameters.AddWithValue("@SkNuoc", phieuThu.SkNuoc);
                    command.Parameters.AddWithValue("@TienNuoc", phieuThu.TienNuoc);
                    command.Parameters.AddWithValue("@TienDV", phieuThu.TienDV);
                    command.Parameters.AddWithValue("@TongTien", phieuThu.TongTien);
                    command.Parameters.AddWithValue("@TrangThai", phieuThu.TrangThai);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CreateChiTietDichVu(ChiTietDichVu chiTietDichVu)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = @"INSERT INTO ChiTietDichVu (MaPhong, MaDichVu, SoLuong, TongTien) VALUES (@MaPhong, @MaDichVu, @SoLuong, @TongTien)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", chiTietDichVu.MaPhong);
                    command.Parameters.AddWithValue("@MaDichVu", chiTietDichVu.MaDichVu);
                    command.Parameters.AddWithValue("@SoLuong", chiTietDichVu.SoLuong);
                    command.Parameters.AddWithValue("@TongTien", chiTietDichVu.TongTien);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CheckChiTietDichVuExists(string maPhong, string maDichVu)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = @"SELECT COUNT(*) FROM ChiTietDichVu WHERE MaPhong = @MaPhong AND MaDichVu = @MaDichVu";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MaDichVu", maDichVu ?? (object)DBNull.Value);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        public void UpdateDienNuoc(string maPhong, float dien, float nuoc)
        {
            string query = "UPDATE Phong SET Dien = @Dien, Nuoc = @Nuoc WHERE MaPhong = @MaPhong";

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Dien", dien);
                        command.Parameters.AddWithValue("@Nuoc", nuoc);
                        command.Parameters.AddWithValue("@MaPhong", maPhong);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while updating Dien and Nuoc: " + ex.Message);
                }
            }
        }
        public DataTable GetPhieuThuByMaPT(string maPT)
        {
            using (SqlConnection connection = new SqlConnection(strConn)) 
            {
                string query = @"SELECT *  FROM PhieuThu  WHERE MaPT = @MaPT";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPT", maPT ?? (object)DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        public DataTable GetDichVuByMaPhong(string maPhong)
        {
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string query = @"SELECT MaPhong, MaDichVu, SoLuong, ThanhTien FROM ChiTietDichVu WHERE MaPhong = @MaPhong";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaPhong", maPhong ?? (object)DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    connection.Open();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }


    }
}
