using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FeedBackDTO
    {
        public string MaFB { get; set; }
        public string MaPhong { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayGui {  get; set; }
        public string PhanHoi { get; set; }
        //public DateTime NgayPhanHoi { get; set; }
        public DateTime? NgayPhanHoi { get; set; } // Đổi thành DateTime?
        public int TrangThai { get; set; }
    }
}
