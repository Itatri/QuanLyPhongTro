using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DanKyTaiKhoanKH_DTO
    {
        public string ID { get; set; }
        public string MatKhau { get; set; }
        public string MaPhong { get; set; }
        public int TrangThai { get; set; }
        public DateTime NgayCapNhat { get; set; } // Thêm thuộc tính Ngày cập nhật

        public DanKyTaiKhoanKH_DTO() { }

        public DanKyTaiKhoanKH_DTO(string id, string matKhau, string maPhong, int trangThai)
        {
            ID = id;
            MatKhau = matKhau;
            MaPhong = maPhong;
            TrangThai = trangThai;
        }
    }
}
