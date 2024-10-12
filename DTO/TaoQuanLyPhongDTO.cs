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
        public DateTime? NgayVao { get; set; }
        public float TienCoc { get; set; }
        public string TienPhong { get; set; }
        public float Dien { get; set; }
        public float Nuoc { get; set; }
        public float? CongNo { get; set; }
        public DateTime? HanTro { get; set; }
        public bool? TrangThai { get; set; }
        public string GhiChu { get; set; }


        public TaoQuanLyPhongDTO()
        {

        }

        // Phương thức có tham số
        public TaoQuanLyPhongDTO(string maPhong, string maKhuVuc, string tenPhong, DateTime? ngayVao, float tienCoc, string tienPhong,
                                 float dien, float nuoc, float? congNo, DateTime? hanTro, bool? trangThai, string ghiChu)
        {
            MaPhong = maPhong;
            MaKhuVuc = maKhuVuc;
            TenPhong = tenPhong;
            NgayVao = ngayVao;
            TienCoc = tienCoc;
            TienPhong = tienPhong;
            Dien = dien;
            Nuoc = nuoc;
            CongNo = congNo;
            HanTro = hanTro;
            TrangThai = trangThai;
            GhiChu = ghiChu;
        }
    }
}


