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


        //public List<ThongTinKhachDTO> LayTatCaThongTinKhach1(string MaKhuVuc)
        //{
        //    return thongTinKhachDAL.LayTatCaThongTinKhach1(MaKhuVuc);
        //}

        public void CapNhatThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            thongTinKhachDAL.CapNhatThongTinKhach(khachDTO);
        }

        public int DemSoLuongKhach()
        {
            // Gọi DAL để thực hiện truy vấn đếm số lượng khách
            return thongTinKhachDAL.DemSoLuongKhach();
        }

        public void ThemThongTinKhach(ThongTinKhachDTO khachDTO)
        {
            try
            {
                // Tạo đối tượng DAL để thực hiện truy vấn
                ThongTinKhachDAL khachDAL = new ThongTinKhachDAL();

                // Gọi phương thức thêm khách hàng vào cơ sở dữ liệu
                khachDAL.ThemThongTinKhach(khachDTO);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm khách hàng: " + ex.Message);
            }
        }
        public ThongTinKhachDTO LayThongTinKhachTheoMa(string maKhachTro)
        {
            ThongTinKhachDAL dal = new ThongTinKhachDAL();
            return dal.LayThongTinKhachTheoMa(maKhachTro);
        }

        public void XoaThongTinKhach(string maKhachTro)
        {
            thongTinKhachDAL.XoaThongTinKhach(maKhachTro);
        }

        public List<ThongTinKhachDTO> TimKiemThongTinKhach(string searchValue)
        {
            // Gọi phương thức trong DAL để tìm kiếm
            return thongTinKhachDAL.TimKiemThongTinKhach(searchValue);
        }

        public List<ThongTinKhachDTO> LayThongTinKhachTheoMaPhong(string maPhong)
        {
            return thongTinKhachDAL.LayThongTinKhachTheoMaPhong(maPhong);
        }

        public void CapNhatChuKyKhachHang(string maKhachTro, string chuKyMoi)
        {
            thongTinKhachDAL.CapNhatChuKyKhachHang(maKhachTro, chuKyMoi);
        }



    }
}
