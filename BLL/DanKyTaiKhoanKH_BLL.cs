using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using DTO;

namespace BLL
{
    public class DanKyTaiKhoanKH_BLL
    {
        private DanKyTaiKhoanKH_DAL dal = new DanKyTaiKhoanKH_DAL();

        public DataTable GetUserPhongData()
        {
            return dal.GetUserPhongData();
        }

        public bool InsertUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            return dal.InsertUserPhong(userPhong);
        }

        public bool UpdateUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            return dal.UpdateUserPhong(userPhong);
        }

        public bool DeleteUserPhong(string id)
        {
            return dal.DeleteUserPhong(id);
        }

        public bool PhongExists(string maPhong)
        {
            return dal.PhongExists(maPhong);
        }

        public DataTable GetPhongData()
        {
            return dal.GetPhongData();
        }

        //Tìm tài khoản phòng theo mã phòng
        public DataTable TimTaiKhoanPhong(string keyword)
        {
            return dal.GetUserPhongByMaPhong(keyword);
        }



    }
}
