using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDichVuPT
    {
        public string MaPT { get; set; }
        public string TenDV { get; set; }
        public float DonGia { get; set; }
        public int SoLuong { get; set; }
        public float ThanhTien { get; set; }

        public ChiTietDichVuPT(string maPhong, string tenDV, float donGia, int soLuong, float thanhTien)
        {
            MaPT = maPhong;
            TenDV = tenDV;
            DonGia = donGia;
            SoLuong = soLuong;
            ThanhTien = thanhTien;
        }
        public ChiTietDichVuPT() { }
    }
}
