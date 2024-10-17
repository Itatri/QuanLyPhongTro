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


        //public bool insertDichVuPhong(List<DichVuPhongDTO> lst)
        //{
        //    bool a1 = true;
        //    foreach (DichVuPhongDTO item in lst)
        //    {
        //        a1 = dal.InsertDichVuPhong(item.maphong, item.madichvu);

        //    }
        //    return a1;
        //}

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




        //public DataTable GetDichVuByMaPhong(string maPhong)
        //{
        //    return dal.GetDichVuByMaPhong(maPhong);
        //}

        //public DataTable GetAllDichVu()
        //{
        //    return dal.GetAllDichVu(); // Bạn cần thêm phương thức này trong lớp DAL
        //}

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
