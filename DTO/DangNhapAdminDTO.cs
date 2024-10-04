using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DangNhapAdminDTO
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string MaKhuVuc { get; set; }
        public int TrangThai { get; set; }

        public DangNhapAdminDTO() { }

        public DangNhapAdminDTO(string id, string password, string makhucvuc, int trangThai)
        {
            ID = id;
            Password = password;
            MaKhuVuc = makhucvuc;
            TrangThai = trangThai;
        }
    }
}
