using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class TaoQuanLyPhongBLL
    {
     private TaoQuanLyPhongDAL phongDAL;

        public TaoQuanLyPhongBLL()
        {
            phongDAL = new TaoQuanLyPhongDAL();
        }

        public List<TaoQuanLyPhongDTO> GetAllPhong()
        {
            return phongDAL.GetAllPhong();
        }

        public bool InsertPhong(TaoQuanLyPhongDTO phong)
        {
            // Thêm các logic kiểm tra nghiệp vụ tại đây (nếu cần)
            return phongDAL.InsertPhong(phong);
        }

        public bool DeletePhong(string maPhong)
        {
            return phongDAL.DeletePhong(maPhong);
        }

        public string GetNewMaPhong()
        {
            return phongDAL.GetNewMaPhong();
        }
    }
}
