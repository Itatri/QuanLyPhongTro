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
    public class TaoQuanLyPhongBLL
    {
        private TaoQuanLyPhongDAL dal = new TaoQuanLyPhongDAL();

        //public List<TaoQuanLyPhongDTO> LayDanhSachPhong()
        //{
        //    return dal.LayDanhSachThonTinPhong();
        //}

        public DataTable LayDanhSachPhong()
        {
            return dal.LayDanhSachThonTinPhong();
        }


        public bool InsertPhong(TaoQuanLyPhongDTO phong)
        {
            return dal.InsertPhong(phong);
        }

        public bool DeletePhong(string maPhong)
        {
            return dal.DeletePhong(maPhong);
        }

        public string GetNewMaPhong()
        {
            return dal.GetNewMaPhong();
        }

        public bool UpdatePhong(TaoQuanLyPhongDTO phong)
        {
            return dal.UpdatePhong(phong);
        }

        public DataTable TatCaDichVu()
        {
            return dal.TatCaDichVu();
        }


        public bool ínertDichVuPhong(List<DichVuPhongDTO> lst)
        {
            bool a1 = true;
            foreach (DichVuPhongDTO item in lst)
            {
                a1 = dal.InsertDichVuPhong(item.maphong, item.madichvu);

            }
            return a1;
        }

        //-----------------------------------------------------------------------------------------------------

    }
}
