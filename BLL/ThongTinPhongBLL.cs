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
    public class ThongTinPhongBLL
    {
        private ThongTinPhongDAL dal = new ThongTinPhongDAL();
        private TaoQuanLyPhongDTO dto = new TaoQuanLyPhongDTO();
        public DataTable LayDichVuPhong(string maPhong)
        {
            return dal.LayDichVuPhong(maPhong);
        }


        public DataTable GetDichVuByMaPhong(string maPhong)
        {
            return dal.GetDichVuByMaPhong(maPhong);
        }


        public DataTable GetAllPhong(string maPhong)
        {
            return dal.GetAllPhong(maPhong);
        }

        public DataTable GetAllDichVu()
        {
            return dal.GetAllDichVu(); // Bạn cần thêm phương thức này trong lớp DAL
        }

        public void UpdatePhong(TaoQuanLyPhongDTO phong)
        {
            dal.UpdatePhong(phong);
        }

        public void UpdateDichVuPhong(string maPhong, List<DichVuPhongDTO> dichVuPhongs)
        {
            dal.UpdateDichVuPhong(maPhong, dichVuPhongs);
        }

    }
}
