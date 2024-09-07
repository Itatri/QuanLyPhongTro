using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DichVu
    {
        string maDichVu;
        string tenDichVu;
        string gia;


        public string MaDichVu { get => maDichVu; set => maDichVu = value; }
        public string TenDichVu { get => tenDichVu; set => tenDichVu = value; }
        public string Gia { get => gia; set => gia = value; }

        public DichVu(string maDichVu, string tenDichVu, string gia)
        {
            this.maDichVu = maDichVu;
            this.tenDichVu = tenDichVu;
            this.gia = gia;
        }
        public DichVu() 
        { 
        
        }
    }
}
