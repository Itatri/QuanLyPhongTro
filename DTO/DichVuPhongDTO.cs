using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DichVuPhongDTO
    {
        public string maphong { get; set; }
        public string madichvu { get; set; }
        public DichVuPhongDTO()
        {

        }

        public DichVuPhongDTO(string maphong, string madichvu)
        {
            maphong = this.maphong;
            madichvu = this.madichvu;
        }
    }
}
