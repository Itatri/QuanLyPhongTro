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
    }
}
