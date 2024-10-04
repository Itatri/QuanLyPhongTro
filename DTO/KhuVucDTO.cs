using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhuVucDTO
    {
        public string MaKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public bool TrangThai { get; set; }
        // Constructor không tham số
        public KhuVucDTO()
        {
        }

        // Constructor có tham số
        public KhuVucDTO(string maKhuVuc, string tenKhuVuc, bool trangThai)
        {
            MaKhuVuc = maKhuVuc;
            TenKhuVuc = tenKhuVuc;
            TrangThai = trangThai;
        }


    }
}
