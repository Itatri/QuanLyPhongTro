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
        public string MaLoaiPhong { get; set; }
        public string MaKhuVuc { get; set; }
        public string TenPhong { get; set; }
        public DateTime NgayVao { get; set; }
        public float TienCoc { get; set; }
        public float Dien { get; set; }
        public float Nuoc { get; set; }
        public DateTime HanTro { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }

        // Phương thức khởi tạo không tham số
        public TaoQuanLyPhongDTO() { }

        // Phương thức khởi tạo có tham số
        public TaoQuanLyPhongDTO(string maPhong, string maLoaiPhong, string maKhuVuc, string tenPhong, DateTime ngayVao, float tienCoc, float dien, float nuoc, DateTime hanTro, bool trangThai, string ghiChu)
        {
            MaPhong = maPhong;
            MaLoaiPhong = maLoaiPhong;
            MaKhuVuc = maKhuVuc;
            TenPhong = tenPhong;
            NgayVao = ngayVao;
            TienCoc = tienCoc;
            Dien = dien;
            Nuoc = nuoc;
            HanTro = hanTro;
            TrangThai = trangThai;
            GhiChu = ghiChu;
        }
    }
}
