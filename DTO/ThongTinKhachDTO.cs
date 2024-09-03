using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinKhachDTO
    {
        public string MaKhachTro { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public byte[] AnhNhanDien { get; set; }
        public DateTime NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string Phone { get; set; }
        public string QueQuan { get; set; }
        public byte[] ChuKy { get; set; }
        public string MaPhong { get; set; }
        public string TrangThai { get; set; }
    }
}
