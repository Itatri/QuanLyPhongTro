using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ThongTinAdminBLL
    {
        private ThongTinAdminDAL thongTinAdminDAL;

        public ThongTinAdminBLL()
        {
            thongTinAdminDAL = new ThongTinAdminDAL();
        }

        public ThongTinAdminDTO LayThongTinAdminTheoIdUser(string idUser)
        {
            if (string.IsNullOrEmpty(idUser))
            {
                throw new ArgumentException("IdUser không được để trống.");
            }

            ThongTinAdminDTO admin = thongTinAdminDAL.GetThongTinAdminByIdUser(idUser);

            if (admin == null)
            {
                throw new Exception($"Không tìm thấy Admin với IdUser: {idUser}");
            }

            return admin;
        }

        // Hàm cập nhật thông tin Admin
        public bool CapNhatThongTinAdmin(ThongTinAdminDTO admin)
        {
            if (admin == null || string.IsNullOrEmpty(admin.IdUser))
            {
                throw new ArgumentException("Thông tin Admin không hợp lệ hoặc IdUser bị trống.");
            }

            return thongTinAdminDAL.UpdateThongTinAdmin(admin);
        }


    }


}
