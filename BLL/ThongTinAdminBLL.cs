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

          

            return admin;
        }


        public bool CapNhatThongTinAdmin(ThongTinAdminDTO admin)
        {
            try
            {
                if (admin == null || string.IsNullOrEmpty(admin.IdUser))
                {
                    throw new ArgumentException("Thông tin Admin không hợp lệ hoặc IdUser bị trống.");
                }

                return thongTinAdminDAL.UpdateThongTinAdmin(admin);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Lỗi khi cập nhật thông tin admin: " + ex.Message);
            }
        }

        // Phương thức thêm mới Admin
        public bool ThemAdmin(ThongTinAdminDTO adminInfo)
        {
            if (adminInfo == null || string.IsNullOrEmpty(adminInfo.IdUser))
            {
                throw new ArgumentException("Thông tin Admin không hợp lệ hoặc IdUser bị trống.");
            }

            // Tạo mã Admin mới
            adminInfo.MaAdmin = thongTinAdminDAL.LayMaAdminMoi(); // Gọi để lấy mã mới

            return thongTinAdminDAL.ThemThongTinAdmin(adminInfo);
        }

        // Phương thức kiểm tra sự tồn tại của Admin
        public bool KiemTraThongTinAdmin(string idUser)
        {
            return thongTinAdminDAL.KiemTraAdminExist(idUser);
        }

        // Phương thức thêm hoặc cập nhật thông tin Admin
        public bool ThemHoacCapNhatThongTinAdmin(ThongTinAdminDTO adminInfo)
        {
            if (KiemTraThongTinAdmin(adminInfo.IdUser))
            {
                return CapNhatThongTinAdmin(adminInfo); // Nếu tồn tại thì cập nhật
            }
            else
            {
                return ThemAdmin(adminInfo); // Nếu không tồn tại thì thêm mới
            }
        }



    }


}
