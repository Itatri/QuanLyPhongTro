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
    public class QuanLyDichVuBLL
    {
        private QuanLyDichVuDAL dal = new QuanLyDichVuDAL();

        public DataTable GetAllServices()
        {
            return dal.GetAllServices();
        }

        public DataTable GetDichVuFormQLPhong()
        {
            return dal.GetDichVuFormQLPhong();
        }

        public bool AddService(ThongTinDichVuDTO service)
        {
            if (dal.DoesServiceExist(service.MaDichVu))
            {
                return false;
            }
            return dal.InsertService(service) > 0;
        }
        public void AddServiceToRoom(List<DichVuPhongDTO> lst)
        {
            foreach (DichVuPhongDTO d in lst)
            {
                dal.AddServiceToRoom(d);
            }
        }
        public bool RemoveService(string maDV)
        {
            if (!CanRemoveService(maDV, out string message))
            {
                return false; // Không cho phép xóa
            }
            return dal.DeleteService(maDV) > 0;
        }

        private bool CanRemoveService(string maDV, out string message)
        {
            if (maDV == "DV0000" || maDV == "DV0001" || maDV == "DV0002")
            {
                message = "Không thể xóa dịch vụ có MaDichVu là DV0000 hoặc DV0001 hoặc DV0002.";
                return false;
            }
            message = string.Empty;
            return true;
        }

        public bool EditService(ThongTinDichVuDTO service)
        {
            return dal.UpdateService(service) > 0;
        }

        public string GenerateNewServiceCode()
        {
            return dal.GenerateNewServiceCode();
        }

        public bool ServiceExists(string maDichVu)
        {
            return dal.ServiceExists(maDichVu);
        }

        public DataTable TimKiemDichVu(string keyword)
        {
            return dal.TimKiemDichVu(keyword);
        }

        public DataTable SapXepDichVuTheoTrangThai()
        {
            return dal.SapXepDichVuTheoTrangThai();
        }
    }
}
