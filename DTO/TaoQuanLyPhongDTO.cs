using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaoQuanLyPhongDTO
    {


        public string MaPhong { get; set; }
        public string MaKhuVuc { get; set; }
        public string TenPhong { get; set; }
        public float TienPhong { get; set; }
        public DateTime? NgayVao { get; set; }
        public float TienCoc { get; set; }
        public float Dien { get; set; }
        public float Nuoc { get; set; }
        public float? CongNo { get; set; }
        public DateTime? HanTro { get; set; }
        public bool? TrangThai { get; set; }
        public string GhiChu { get; set; }


        public TaoQuanLyPhongDTO()
        {

        }
        // Constructor with parameters
        public TaoQuanLyPhongDTO(string maPhong, string maKhuVuc, string tenPhong, float tienPhong, DateTime? ngayVao, float tienCoc, float dien, float nuoc, float? congNo, DateTime? hanTro, bool? trangThai, string ghiChu)
        {
            MaPhong = maPhong;
            MaKhuVuc = maKhuVuc;
            TenPhong = tenPhong;
            TienPhong = tienPhong;
            NgayVao = ngayVao;
            TienCoc = tienCoc;
            Dien = dien;
            Nuoc = nuoc;
            CongNo = congNo;
            HanTro = hanTro;
            TrangThai = trangThai;
            GhiChu = ghiChu;
        }

    }
}


