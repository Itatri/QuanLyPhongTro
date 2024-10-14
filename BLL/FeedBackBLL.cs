using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FeedBackBLL
    {
        private FeedBackDAL feedbackDAL = new FeedBackDAL();

        public List<FeedBackDTO> LayTatCaFeedBack()
        {
            return feedbackDAL.LayTatCaFeedBack();
        }

        //public List<FeedBackDTO> TimKiemFeedBack(string searchValue)
        //{
        //    return feedbackDAL.TimKiemFeedBack(searchValue);
        //}

        public bool CapNhatPhanHoi(string maFB, string phanHoi, DateTime ngayPhanHoi)
        {
            return feedbackDAL.CapNhatPhanHoi(maFB, phanHoi, ngayPhanHoi);
        }

      

        public List<FeedBackDTO> LocFeedBack(int trangThai)
        {
            return feedbackDAL.LocFeedBack(trangThai);
        }




    }
}
