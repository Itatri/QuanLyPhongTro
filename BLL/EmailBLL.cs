using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmailBLL
    {
        private EmailDAL dal = new EmailDAL();
        public List<string> LayDSEmail(string maphong)
        {
            return dal.LayDSEmail(maphong);
        }
    }
}
