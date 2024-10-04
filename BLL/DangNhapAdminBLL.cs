using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DangNhapAdminBLL
    {
         private DangNhapAdminDAL dangNhapDAL;

        public DangNhapAdminBLL()
        {
            dangNhapDAL = new DangNhapAdminDAL();
        }

        public bool CapNhatMatKhau(string id, string newPassword)
        {
            // Gọi phương thức DAL để cập nhật mật khẩu
            return dangNhapDAL.UpdatePassword(id, newPassword);
        }

    }
}
