using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinDichVuDTO
    {
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public float DonGia { get; set; }
        public bool TrangThai { get; set; }


        public ThongTinDichVuDTO()
        {
            MaDichVu = this.MaDichVu;
            TenDichVu = this.TenDichVu;
            DonGia = this.DonGia;
            TrangThai = this.TrangThai;
        }

        public ThongTinDichVuDTO(string MaDichVu, string TenDichVu, float DonGia, bool TrangThai)
        {
            MaDichVu = this.MaDichVu;
            TenDichVu = this.TenDichVu;
            DonGia = this.DonGia;
            TrangThai = this.TrangThai;
        }
    }
}
