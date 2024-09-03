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
    }
}
