using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinAdminDTO
    {
        public string MaAdmin { get; set; }     
        public string HoTen { get; set; }       
        public string GioiTinh { get; set; }    
        public DateTime NgaySinh { get; set; }  
        public string Cccd { get; set; }        
        public string Phone { get; set; }       
        public string DiaChi { get; set; }    
        public string NganHang { get; set; }
        public string TaiKhoan { get; set; }
        public string ChuKy { get; set; }      
        //public string ChuKyXacNhan { get; set; } 
        public string IdUser { get; set; }    
        public int TrangThai { get; set; }
    }
}
