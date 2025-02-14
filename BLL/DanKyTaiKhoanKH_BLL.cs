﻿using System;
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

        public DataTable GetUserPhongData1(string makhuvuc)
        {
            return dal.GetUserPhongData1(makhuvuc);
        }


        public bool InsertUserPhong(DanKyTaiKhoanKH_DTO userPhong)
        {
            return dal.InsertUserPhong(userPhong);
        }

        public bool UpdatePassword(string accountId, string newPassword)
        {
            // Thêm các logic kiểm tra nếu cần thiết (ví dụ: độ dài mật khẩu, ký tự đặc biệt, v.v.)
            return dal.UpdatePassword(accountId, newPassword);
        }

        public bool DeleteUserPhong(string id)
        {
            return dal.DeleteUserPhong(id);
        }

        public bool PhongExists(string maPhong)
        {
            return dal.PhongExists(maPhong);
        }

  
        // BLL: Gọi phương thức DAL
        public DataTable GetPhongData(string khuvuc) //-----22/10/2024
        {
            return dal.GetPhongData(khuvuc);
        }


        //Tìm tài khoản phòng theo mã phòng
        public DataTable TimTaiKhoanPhong(string keyword)
        {
            return dal.GetUserPhongByMaPhong(keyword);
        }
    }
}
