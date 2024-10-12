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
    public class ThongTinPhongBLL
    {
        private ThongTinPhongDAL phongDAL = new ThongTinPhongDAL();

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
    }


}
