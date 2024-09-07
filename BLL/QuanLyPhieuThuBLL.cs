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
    public class QuanLyPhieuThuBLL
    {
        QuanLyPhieuThuDAL tkDal;
        public QuanLyPhieuThuBLL()
        {
            tkDal = new QuanLyPhieuThuDAL();
        }
        public DataTable GetThongKePhieuThu()
        {
            
            try
            {
                return tkDal.GetThongKePhieuThu();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thống kê theo tháng: " + ex.Message);
                return null;
            }
        }
        public int GetCountPT()
        {
            return tkDal.CountPT();
        }
        public void XoaPT(string ma)
        {
            QuanLyPhieuThuDAL dal = new QuanLyPhieuThuDAL();
            dal.XoaPT(ma);
        }
        public List<DichVu> GetDichVu()
        {
            QuanLyPhieuThuDAL dal = new QuanLyPhieuThuDAL();
            return dal.GetDichVu();
        }
        public DataTable GetPhong(string ten)
        {
            return tkDal.GetPhongByName(ten);
        }
        public bool CreatePhieuThu(PhieuThu phieuThu)
        {
            return tkDal.CreatePhieuThu(phieuThu);
        }

        public bool CreateChiTietDichVu(ChiTietDichVu chiTietDichVu)
        {
            if (tkDal.CheckChiTietDichVuExists(chiTietDichVu.MaPhong, chiTietDichVu.MaDichVu))
            {
                return false;
            }
            return tkDal.CreateChiTietDichVu(chiTietDichVu);
        }

        public string GetMaP(string ten)
        {
            return tkDal.getMaP(ten);
        }
        public bool CheckChiTietDichVuExists(string maPT, string maDichVu)
        {
            return tkDal.CheckChiTietDichVuExists(maPT, maDichVu);
        }
        public void UpdateDienNuoc(string maPhong, float dien, float nuoc)
        {
            tkDal.UpdateDienNuoc(maPhong, dien, nuoc);
        }
        public DataTable GetPhieuThuByMaPT(string maPT)
        {
            return tkDal.GetPhieuThuByMaPT(maPT);
        }
        public DataTable GetDichVuByMaPhong(string maPhong)
        {
            return tkDal.GetDichVuByMaPhong(maPhong);
        }


    }
}
