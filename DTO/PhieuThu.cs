using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuThu
    {
        public string MaPT { get; set; }
        public string MaPhong { get; set; }
        public DateTime NgayLap { get; set; }
        public DateTime NgayThu { get; set; }
        public float TienNha { get; set; }
        public float DienCu { get; set; }
        public float DienMoi { get; set; }
        public float? TienDien { get; set; }
        public float NuocCu { get; set; }
        public float NuocMoi { get; set; }

        public float? TienNuoc { get; set; }
        public float TienDV { get; set; }
        public float? TongTien { get; set; }
        public float? ThanhToan {  get; set; }
        public int TrangThai { get; set; }

        public PhieuThu(string mapt,string phong,DateTime nl,DateTime nt, float tiennha,float dc,float dm,float tiend,float nc,float nm, float tienn,float tiendv,float tongtien, float thanhtoan,int tt)
        {
            this.MaPT = mapt;
            this.MaPhong = phong;
            this.NgayLap = nl;
            this.NgayThu = nt;
            this.DienCu = dc;
            this.DienMoi = dm;
            this.TienDien = tiend;
            this.NuocCu = nc;
            this.NuocMoi = nm;
            this.TienNuoc = tienn;
            this.TienDV = tiendv;
            this.TongTien = tongtien;
            this.ThanhToan = thanhtoan;
            this.TrangThai = tt;
        }
        public PhieuThu()
        { }
    }
}
