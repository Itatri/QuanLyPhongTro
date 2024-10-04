using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class KhuVucBLL
    {
        private KhuVucDAL khuVucDAL;

        public KhuVucBLL()
        {
            khuVucDAL = new KhuVucDAL();
        }

        
        public KhuVucDTO GetKhuVucByMaKhuVuc(string maKhuVuc)
        {
            return khuVucDAL.GetKhuVucByMaKhuVuc(maKhuVuc);
        }

    }
}
