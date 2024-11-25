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
        public DataTable GetPTTheoThangNam(int thang, int nam, string maKhuVuc)
        {
            return tkDal.GetPTTheoThangNam(thang, nam, maKhuVuc);
        }

        public DataTable GetPTTheoThangNamPhong(int thang, int nam, string maKhuVuc,string key)
        {
            return tkDal.GetPTTheoThangNamPhong(thang, nam, maKhuVuc,key);
        }
        public DataTable LayPhongChuaCoPhieuThu(DateTime date, string maKhuVuc)
        {
            return tkDal.LayPhongChuaCoPhieuThu(date, maKhuVuc);
        }
        public bool CheckPTDaTao(string ma)
        {
            
            return tkDal.CheckPTDaTao(ma);
        }
        public DataTable GetALLPT(string khuvuc)
        {
            return tkDal.GetALLPT(khuvuc);
        }

        //////////////Tạo PT
        public DataTable GetPhong(string khuvuc)
        {
            return tkDal.GetPhong(khuvuc);
        }
        public DataTable LoadPhong(string phong)
        {
            return tkDal.LoadPhong(phong);
        }
        public DataTable LoadDVPhong(string phong)
        {
            return tkDal.LoadDVPhong(phong);
        }
        public DataTable GetDichVuPhieuThu(string mapt)
        {
            return tkDal.GetDichVuPhieuThu(mapt);
        }
        public bool CreatePhieuThu(PhieuThu phieu)
        {
            return tkDal.CreatePhieuThu(phieu);
        }
        public void CreateDichVuPhieuThu(List<ChiTietDichVuPT> lst)
        {
            foreach (ChiTietDichVuPT ct in lst)
            {
                tkDal.CreateDichVuPhieuThu(ct);
            }
        }
        public void UpdatePhong(string maPhong, float dien, float nuoc, float congno)
        {
            tkDal.UpdatePhong(maPhong, dien, nuoc, congno);
        }
        public int CountKhach(string phong)
        {
            return tkDal.CountKhach(phong);
        }
        public string GetMAByTenPhong(string phong)
        {
            return tkDal.GetMAByTenPhong(phong);
        }



        ////////Thông tin phiếu thu
        public DataTable LoadPhieuThu(string maPhieu)
        {
            return tkDal.LoadPhieuThu(maPhieu);
        }
        public DataTable LoadDichVuPhieuThu(string map,string mapt)
        {
            return tkDal.LoadDichVuPhieuThu(map,mapt);
        }
        public bool UpdatePhieuThu(PhieuThu phieuThu)
        {
            return tkDal.UpdatePhieuThu(phieuThu);
        }
        public void DeleteDichVuPhieuThu(string mapt)
        {
            tkDal.DeleteDichVuPhieuThu(mapt);
        }
        public DataTable GetCongNo(string phong)
        {
            return tkDal.GetCongNo(phong);
        }
    }
}
