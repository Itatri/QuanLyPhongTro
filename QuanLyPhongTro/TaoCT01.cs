using BLL;
using DTO;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace QuanLyPhongTro
{
    public class TaoCT01
    {
        List<string> khach = new List<string>();
        private ThongTinKhachBLL khachbll = new ThongTinKhachBLL();
        private ThongTinAdminBLL admin = new ThongTinAdminBLL();
        private KhuVucBLL khuvucbll = new KhuVucBLL();
        private ThongTinPhongBLL phongbll = new ThongTinPhongBLL();
        int con = -1;
        ThongTinKhachDTO me = null;
        XuLyAnh xuly = new XuLyAnh();
        public string idadmin { get; set; }
        public string makhuvuc { get; set; }
        public string maphong { get; set; }

        private List<ThongTinKhachDTO> lst;
        ThongTinAdminDTO ttadmin = new ThongTinAdminDTO();
        KhuVucDTO khuvuc = new KhuVucDTO();
        System.Data.DataTable dt = new System.Data.DataTable();
        /// Tạo CT01 và Hợp đồng
        public int TaoTamTru()
        {
            lst = khachbll.LayThongTinKhachTheoMaPhong(maphong);
            ttadmin = admin.LayThongTinAdminTheoIdUser(idadmin);
            khuvuc = khuvucbll.GetKhuVucByMaKhuVuc(makhuvuc);
            dt = phongbll.GetAllPhong(maphong);
            CheckCon(lst);
            ThongTinKhachDTO chuho = ChuHo(lst);
            khach.Add(chuho.MaKhachTro);
            if (ttadmin == null)
            {
                MessageBox.Show("Admin chưa kê khai đủ thông tin");
                return -1;
            }
            if (chuho == null)
            {
                MessageBox.Show("Chủ hộ chưa kê khai đủ thông tin");
                return -1;
            }

            int n = lst.Count;
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string duongdankhuvuc = string.Empty;
            if (checkFolder(makhuvuc, desktop) == false)
            {
                duongdankhuvuc = TaoFolder(makhuvuc, desktop);
            }
            else
            {
                duongdankhuvuc = Path.Combine(desktop, makhuvuc);
            }
            string tenPhong = dt.Rows[0]["TenPhong"].ToString();
            string duongdan = string.Empty;
            bool check = checkFolder(tenPhong, duongdankhuvuc);
            if (check == true)
            {

                if (MessageBox.Show("Bạn có muốn xóa và tạo lại hợp đồng và ct01 của phòng " + tenPhong + " không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string temp = Path.Combine(duongdankhuvuc, tenPhong);
                    DeleteFolder(temp);
                }
                else
                {
                    return -1;
                }
            }
            duongdan = TaoFolder(tenPhong, duongdankhuvuc);
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFilePath = Path.Combine(binPath, @"..\..\..\CT01.dotx");
            string sourceFileHongDong = Path.Combine(binPath, @"..\..\..\HopDong.dotx");
            XuatPDF(chuho, chuho, sourceFilePath, duongdan);
            foreach (ThongTinKhachDTO k in lst)
            {
                if (!khach.Contains(k.MaKhachTro))
                {
                    XuatPDF(k, chuho, sourceFilePath, duongdan);
                    con = -1;
                    me = null;
                }
            }

            XuatHopDong(khuvuc, chuho, sourceFileHongDong, duongdan);
            return 0;
        }
        //Tạo CT01
        private void XuatPDF(ThongTinKhachDTO tt, ThongTinKhachDTO chuho, string sourceFilePath, string duongdan)
        {
            string dd = DateTime.Now.Day.ToString();
            string MM = DateTime.Now.Month.ToString();
            string yyyy = DateTime.Now.Year.ToString();
            string date = chuho.NgaySinh.Value.Date.ToString();



            Bitmap chukyCha = null;
            Bitmap chukyMe = null;
            List<string> cccdchuho = chuyencccd(chuho.CCCD);
            List<string> cccdnguoikhai = chuyencccd(tt.CCCD);

            khach.Add(tt.MaKhachTro); // Thêm mã khách vào danh sách

            Dictionary<string, string> dic = new Dictionary<string, string>
                    {
                        { "HoTenNguoiKhai", tt.HoTen },
                        { "NgaySinhNguoiKhai", tt.NgaySinh.Value.Date.ToString("dd/MM/yyyy") },
                        { "GioiTinhNguoiKhai", tt.GioiTinh },
                        { "SDT", tt.Phone },
                        { "Email", tt.Email },
                        { "HoTenChuHo", chuho.HoTen },
                        { "QuanHe", tt.QuanHe },
                        { "dd", dd },
                        { "MM", MM },
                        { "yyyy", yyyy },
                        { "HoTenChuSoHuu","(7) Họ và tên: "+ttadmin.HoTen},
                        {"CCCDChuSoHuu","(7) Số định danh cá nhân: "+ttadmin.Cccd},

                    };

            System.Data.DataTable dt = KhachCungThuongTru(lst, tt);
            WordExport wd = new WordExport(sourceFilePath, false);

            if (!string.IsNullOrEmpty(chuho.ChuKy))
            {
                Bitmap chukychuho = xuly.XuLy(chuho.ChuKy);
                if(chukychuho != null)
                    wd.ReplaceFieldWithImage("ChuKyChuHo", chukychuho);
            }else
            {
                dic.Add("ChuKyChuHo", "");
            }
            if (!string.IsNullOrEmpty(tt.ChuKy))
            {
                Bitmap chukynguoikhai = xuly.XuLy(tt.ChuKy);
                if(chukynguoikhai != null)
                    wd.ReplaceFieldWithImage("ChuKyNguoiKhai", chukynguoikhai);
            }
            else
            {
                dic.Add("ChuKyNguoiKhai", "");
            }
            if (!string.IsNullOrEmpty(ttadmin.ChuKy))
            {
                Bitmap chukychusohuu = xuly.XuLy(ttadmin.ChuKy);
                wd.ReplaceFieldWithImage("ChuKyChuSoHuu", chukychusohuu);
            }
            else
            {
                dic.Add("ChuKyChuSoHuu", "");
            }
            if (con == -1)
            {
                string temp = " ";
                dic.Add("HoTenCha", "(7) Họ và tên: " + temp);
                dic.Add("CCCDCha", "(7) Số định danh cá nhân:" + temp);
                dic.Add("HotenMe", temp);
                dic.Add("CCCDMe", temp);
                dic.Add("ChuKyCha", temp);
                dic.Add("ChuKyMe", temp);
            }
            else if (con <= 16)
            {
                dic.Add("HoTenCha", "(7) Họ và tên: " + chuho.HoTen);
                dic.Add("CCCDCha", "(7) Số định danh cá nhân:" + chuho.CCCD);
                if (!string.IsNullOrEmpty(chuho.ChuKy))
                {
                    chukyCha = xuly.XuLy(chuho.ChuKy);
                    wd.ReplaceFieldWithImage("ChuKyCha", chukyCha);
                }
                if (me != null)
                {
                    dic.Add("HotenMe", "(7) Họ và tên: " + me.HoTen);
                    dic.Add("CCCDMe", "(7) Số định danh cá nhân:" + me.CCCD);
                    if (!string.IsNullOrEmpty(me.ChuKy))
                    {
                        chukyMe = xuly.XuLy(me.ChuKy);
                        wd.ReplaceFieldWithImage("ChuKyMe", chukyMe);
                    }
                }
                else
                {
                    string temp = "";
                    dic.Add("CCCDMe", temp);
                    dic.Add("ChuKyCha", temp);
                    dic.Add("ChuKyMe", temp);
                }
            }


            wd.WriteFields(dic);
            wd.WriteToTable(cccdnguoikhai, 1);
            wd.WriteToTable(cccdchuho, 2);
            wd.WriteDataTableToWordTable(dt, 3);
            string newFileName = "CT01_" + tt.HoTen + ".docx"; 
            string newFilePath = Path.Combine(duongdan, newFileName); 

            wd.SaveAs(newFilePath);

            wd.Close();
            ExportToPDF(newFilePath, duongdan, tt.HoTen);
            wd = null; 
            con = -1;
            me = null;
        }

        //Tạo hợp đồng
        private void XuatHopDong(KhuVucDTO kv, ThongTinKhachDTO chuho, string sourceFilePath, string duongdan)
        {
            string dd = DateTime.Now.Day.ToString();
            string MM = DateTime.Now.Month.ToString();
            string yyyy = DateTime.Now.Year.ToString();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("HoTenChuSoHuu", ttadmin.HoTen);
            dic.Add("CCCDChuSoHuu", ttadmin.Cccd);
            dic.Add("DiaChi", khuvuc.TenKhuVuc);
            dic.Add("NoiSongChuSoHuu", ttadmin.DiaChi);
            dic.Add("HoTenChuHo", chuho.HoTen);
            dic.Add("CCCDChuHo", chuho.CCCD);
            dic.Add("ThuongTruChuHo", chuho.ThuongTru);
            dic.Add("dd", dd);
            dic.Add("MM", MM);
            dic.Add("yyyy", yyyy);
            dic.Add("DienTichPhong", dt.Rows[0]["DienTich"].ToString());
            dic.Add("TienPhong", Convert.ToDecimal(dt.Rows[0]["TienPhong"]).ToString("N0"));
            dic.Add("SoNguoi", lst.Count.ToString());
            khach.Clear();
            khach.Add(chuho.MaKhachTro);
            System.Data.DataTable bang = BangHopDong(lst);
            WordExport wd = new WordExport(sourceFilePath, false);
            wd.WriteFields(dic);
            wd.WriteDataTableToWordTable(bang, 1);
            if (!string.IsNullOrEmpty(chuho.ChuKy))
            {
                Bitmap chukychuho = xuly.XuLy(chuho.ChuKy);
                wd.InsertImageAndTextInTableCell(2, 2, 1, chuho.HoTen, chukychuho);
            }
            if (!string.IsNullOrEmpty(ttadmin.ChuKy))
            {
                Bitmap chukychusohuu = xuly.XuLy(ttadmin.ChuKy);
                wd.InsertImageAndTextInTableCell(2, 2, 2, ttadmin.HoTen, chukychusohuu);
            }
            int n = 3;
            foreach (ThongTinKhachDTO th in lst)
            {
                if (th.MaKhachTro != chuho.MaKhachTro)
                {
                    if (!string.IsNullOrEmpty(th.ChuKy))
                    {
                        continue;
                    }
                    else
                    {
                        Bitmap anh = xuly.XuLy(th.ChuKy);
                        if (anh != null)
                        {
                            wd.InsertImageAndTextInTableCell(2, n, 1, th.HoTen, anh);
                            n++;
                        }
                    }
                }
            }
            string newFileName = "HopDong" + dt.Rows[0]["TenPhong"].ToString() + ".docx"; 
            string newFilePath = Path.Combine(duongdan, newFileName); 
            wd.SaveAs(newFilePath);
            wd.Close();
            ExportToPDF(newFilePath, duongdan, newFileName);
            wd = null; 
        }
        private ThongTinKhachDTO ChuHo(List<ThongTinKhachDTO> lst)
        {
            ThongTinKhachDTO chuho = new ThongTinKhachDTO();
            foreach (ThongTinKhachDTO t in lst)
            {
                if (t.QuanHe.Equals("Chủ hộ"))
                    chuho = t; break;
            }
            return chuho;
        }
        //Kiểm tra folder đã tạo chưa
        private bool checkFolder(string key, string path)
        {
            string desktopPath = path;

            string folderName = key;

            string folderPath = Path.Combine(desktopPath, folderName);
            if (!Directory.Exists(folderPath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private string TaoFolder(string maphong, string path)
        {
            string desktopPath = path;

            string folderName = maphong;

            string folderPath = Path.Combine(desktopPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                return folderPath;
            }
            else
            {
                return null;
            }
        }
        private void DeleteFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    string[] directories = Directory.GetDirectories(path);
                    foreach (string directory in directories)
                    {
                        DeleteFolder(directory); 
                    }

                    Directory.Delete(path, true);
                }
                else
                {
                    MessageBox.Show("Thư mục không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xóa thư mục: {ex.Message}");
            }
        }


        private void ExportToPDF(string wordFilePath, string folderPath, string maphong)
        {
            // Tạo đường dẫn cho file PDF
            string pdfFileName = maphong + ".pdf";
            string pdfFilePath = Path.Combine(folderPath, pdfFileName);

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            Document wordDocument = null;
            try
            {
                wordApp.Visible = false;
                wordDocument = wordApp.Documents.Open(wordFilePath);
                wordDocument.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}");
            }
            finally
            {
                wordDocument?.Close(false);
                wordApp.Quit();
            }
        }
        private List<string> chuyencccd(string cccd)
        {
            List<string> result = new List<string>();

            foreach (char digit in cccd)
            {
                if (char.IsDigit(digit))
                {
                    result.Add(digit.ToString());
                }
            }

            return result;
        }
        private System.Data.DataTable KhachCungThuongTru(List<ThongTinKhachDTO> list, ThongTinKhachDTO nguoikhai)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("STT", typeof(int));      
            dt.Columns.Add("HoTen", typeof(string));  
            dt.Columns.Add("NgaySinh", typeof(string)); 
            dt.Columns.Add("GioiTinh", typeof(string)); 
            dt.Columns.Add("CCCD", typeof(string));   
            dt.Columns.Add("QuanHe", typeof(string));  

            int stt = 1;

            foreach (ThongTinKhachDTO th in list)
            {
                if (!khach.Contains(th.MaKhachTro))
                {
                    if (th.ThuongTru == nguoikhai.ThuongTru && th.TrangThai == 1)
                    {
                        var row = dt.NewRow();

                        row["STT"] = stt++;
                        row["HoTen"] = th.HoTen;
                        row["NgaySinh"] = th.NgaySinh.Value.ToString("dd/MM/yyyy"); 
                        row["GioiTinh"] = th.GioiTinh; 
                        row["CCCD"] = th.CCCD; 
                        row["QuanHe"] = th.QuanHe; 
                        khach.Add(th.MaKhachTro);

                        dt.Rows.Add(row);
                    }
                }
            }

            return dt; 
        }
        private System.Data.DataTable BangHopDong(List<ThongTinKhachDTO> list)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("STT", typeof(int));        
            dt.Columns.Add("Họ tên", typeof(string));  
            dt.Columns.Add("Ngày sinh", typeof(string)); 
            dt.Columns.Add("Quan hệ", typeof(string));  

            int stt = 1;
            foreach (ThongTinKhachDTO th in list)
            {
                if (!khach.Contains(th.MaKhachTro) && th.TrangThai == 1)
                {
                    var row = dt.NewRow();
                    row["STT"] = stt++;
                    row["Họ tên"] = th.HoTen; 
                    row["Ngày sinh"] = th.NgaySinh.Value.ToString("dd/MM/yyyy"); 
                    row["Quan hệ"] = th.QuanHe;
                                               
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        private void CheckCon(List<ThongTinKhachDTO> lst)
        {
            List<string> list = new List<string> { "Con", "Con chồng", "Con đẻ", "Con nuôi", "Con vợ" };

            foreach (ThongTinKhachDTO k in lst)
            {
                if (list.Contains(k.QuanHe))
                {
                    con = TinhTuoiCon(k.NgaySinh.Value);
                }
                if (k.QuanHe == "Vợ")
                {
                    me = k;
                }
            }
        }
        private int TinhTuoiCon(DateTime ngaysinh)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - ngaysinh.Year;
            return age;
        }

    }
}
