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
        public float SkDien { get; set; }
        public float TienDien { get; set; }
        public float SkNuoc { get; set; }
        public float TienNuoc { get; set; }
        public float TienDV { get; set; }
        public float TongTien { get; set; }
        public int TrangThai { get; set; }
    }
}
