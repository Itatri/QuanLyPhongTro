using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class QuanLiPhongBLL
    {
        private QuanLiPhongDAL phongDAL = new QuanLiPhongDAL();

        public DataTable LayTatCaPhong()
        {
            return phongDAL.LayTatCaPhong();
        }

        public DataTable LayPhongTheoHanTroTangDan(string makhuvuc)
        {
            return phongDAL.LayPhongTheoHanTroTangDan(makhuvuc);
        }

        public DataTable LayPhongTheoTrangThai(string makhuvuc)
        {
            return phongDAL.LayPhongTheoTrangThai(makhuvuc);
        }

        public DataTable TimKiemPhong(string keyword)
        {
            return phongDAL.TimKiemPhong(keyword);
        }
        public DataTable LayDichVuPhong(string maPhong)
        {
            return phongDAL.LayDichVuPhong(maPhong);
        }

        public DataTable LayDichVuTheoMaPhong(string maPhong)
        {
            return phongDAL.LayDichVuTheoMaPhong(maPhong) ;
        }
    }
}
