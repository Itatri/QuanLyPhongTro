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
        public DataTable ThongKeDoanhThuTheoThang(int nam, string khuvuc)
        {
            try
            {
                return tkDal.ThongKeDoanhThuTheoThang(nam, khuvuc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
        public DataTable ThongKeDichVuTheoNamVaKhuVuc(int nam, string khuvuc)
        {
            try
            {
                return tkDal.ThongKeDichVuTheoNamVaKhuVuc(nam,khuvuc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
        public DataTable ThongKeDichVuTheoThangVaKhuVuc(int thang, int nam, string khuvuc)
        {
            try
            {
                return tkDal.ThongKeDichVuTheoThangVaKhuVuc(thang, nam, khuvuc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
    }
}
