using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThongTinKhachBLL
    {
        private ThongTinKhachDAL thongTinKhachDAL = new ThongTinKhachDAL();

        public List<ThongTinKhachDTO> LayTatCaThongTinKhach()
        {
            return thongTinKhachDAL.LayTatCaThongTinKhach();
        }

        public void CapNhatThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            thongTinKhachDAL.CapNhatThongTinKhach(khachDTO);
        }

        public int DemSoLuongKhach()
        {
            // Gọi DAL để thực hiện truy vấn đếm số lượng khách
            return thongTinKhachDAL.DemSoLuongKhach();
        }

    }
}
