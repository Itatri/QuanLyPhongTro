using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class ThongTinPhongDTO
    {
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public float DonGia { get; set; }
        public bool TrangThai { get; set; }


        public ThongTinPhongDTO(string maDichVu, string tenDichVu, float donGia, bool trangThai)
        {
             MaDichVu = maDichVu;
            TenDichVu = tenDichVu;
            DonGia = donGia;
            TrangThai = trangThai;
        }
    }
}
