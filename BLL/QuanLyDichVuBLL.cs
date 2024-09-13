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

        public bool AddService(ThongTinDichVuDTO service)
        {
            if (dal.DoesServiceExist(service.MaDichVu))
            {
                return false;
            }
            return dal.InsertService(service) > 0;
        }

        public bool RemoveService(string maDV)
        {
            return dal.DeleteService(maDV) > 0;
        }

        public bool EditService(ThongTinDichVuDTO service)
        {
            return dal.UpdateService(service) > 0;
        }

        public string GenerateNewServiceCode()
        {
            return dal.GenerateNewServiceCode();
        }
    }
}
