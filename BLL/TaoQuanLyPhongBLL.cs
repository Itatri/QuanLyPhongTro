using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class TaoQuanLyPhongBLL
    {
        private TaoQuanLyPhongDAL dal = new TaoQuanLyPhongDAL();

        public DataTable LayDanhSachPhong()
        {
            return dal.LayDanhSachThonTinPhong();
        }

        // Láy danh sách phòng theo khu vực (29/10/2024)
        public DataTable LayDanhSachPhong1(string makhuvuc)
        {
            return dal.LayDanhSachThonTinPhong1(makhuvuc);
        }

        public bool InsertPhong(TaoQuanLyPhongDTO phong)
        {
            return dal.InsertPhong(phong);
        }

        public bool DeletePhong(string maPhong)
        {
            return dal.DeletePhong(maPhong);
        }

        public string GetNewMaPhong()
        {
            return dal.GetNewMaPhong();
        }

        public bool UpdatePhong(TaoQuanLyPhongDTO phong)
        {
            return dal.UpdatePhong(phong);
        }

        public DataTable TatCaDichVu()
        {
            return dal.TatCaDichVu();
        }

        public bool InsertDichVuPhong(DataTable dt)
        {
            bool allSuccess = true;

            foreach (DataRow row in dt.Rows)
            {
                bool result = dal.InsertDichVuPhong(row["MaPhong"].ToString(), row["MaDichVu"].ToString());
                if (!result)
                {
                    allSuccess = false;
                    break; // Nếu một lần chèn thất bại, dừng lại và trả về false
                }
            }

            return allSuccess;
        }

        public DataTable GetAllDichVu()
        {
            return dal.GetAllDichVu();
        }

        public DataTable GetDichVuByMaPhong(string maPhong)
        {
            return dal.GetDichVuByMaPhong(maPhong);
        }

    }
}
