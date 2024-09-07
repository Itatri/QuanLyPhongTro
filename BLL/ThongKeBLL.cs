using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ThongKeBLL
    {
        ThongKeDAL tkDal;
        public ThongKeBLL()
        {
            tkDal = new ThongKeDAL();
        }
        public DataTable ThongKeTheoThang(int thang, int nam)
        {
            try
            {
                return tkDal.ThongKeTheoThang(thang, nam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
        public DataTable ThongKeTheoNam(int nam)
        {
            try
            {
                return tkDal.ThongKeTheoNam(nam);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
    }
}
