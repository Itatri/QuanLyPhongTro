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
    }


}
