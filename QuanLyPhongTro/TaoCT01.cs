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
            if (!checkFolder(makhuvuc, desktop))
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
                    return -1;// Code khi người dùng chọn No
                }
            }
            duongdan = TaoFolder(tenPhong, duongdankhuvuc);
            // Đường dẫn tới file Word trong thư mục bin
            string binPath = AppDomain.CurrentDomain.BaseDirectory;
            string sourceFilePath = Path.Combine(binPath, @"..\..\..\CT01.dotx");
            string sourceFileHongDong = Path.Combine(binPath, @"..\..\..\HopDong.dotx");
            MessageBox.Show(duongdan);
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
            ///// Tao hop dong


            return 0;
        }

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
            // Xuất dữ liệu vào file Word
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
            // Tạo tên file mới để lưu
            string newFileName = "CT01_" + tt.HoTen + ".docx"; // Đổi sang định dạng docx
            string newFilePath = Path.Combine(duongdan, newFileName); // Đường dẫn tới file mới

            // Lưu file với tên mới trong thư mục mới
            wd.SaveAs(newFilePath);

            // Đóng file Word
            wd.Close();
            ExportToPDF(newFilePath, duongdan, tt.HoTen);
            wd = null; // Giải phóng tài nguyên
            con = -1;
            me = null;
        }

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
            string newFileName = "HopDong" + dt.Rows[0]["TenPhong"].ToString() + ".docx"; // Đổi sang định dạng docx
            string newFilePath = Path.Combine(duongdan, newFileName); // Đường dẫn tới file mới

            // Lưu file với tên mới trong thư mục mới
            wd.SaveAs(newFilePath);

            // Đóng file Word
            wd.Close();
            ExportToPDF(newFilePath, duongdan, newFileName);
            wd = null; // Giải phóng tài nguyên
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
        private bool checkFolder(string key, string path)
        {
            string desktopPath = path;

            // Tên của thư mục muốn tạo
            string folderName = key;

            // Kết hợp đường dẫn với tên thư mục
            string folderPath = Path.Combine(desktopPath, folderName);
            // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
            if (!Directory.Exists(path))
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

            // Tên của thư mục muốn tạo
            string folderName = maphong;

            // Kết hợp đường dẫn với tên thư mục
            string folderPath = Path.Combine(desktopPath, folderName);

            // Kiểm tra nếu thư mục chưa tồn tại thì tạo mới
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
                // Kiểm tra xem thư mục có tồn tại không
                if (Directory.Exists(path))
                {
                    // Xóa tất cả các tệp trong thư mục
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    // Xóa tất cả các thư mục con trong thư mục
                    string[] directories = Directory.GetDirectories(path);
                    foreach (string directory in directories)
                    {
                        DeleteFolder(directory); // Gọi đệ quy để xóa thư mục con
                    }

                    // Xóa thư mục chính
                    Directory.Delete(path, true); // true để xóa thư mục không rỗng
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

            // Duyệt qua từng ký tự trong chuỗi cccd
            foreach (char digit in cccd)
            {
                // Chỉ thêm vào danh sách nếu ký tự là số
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

            // Thêm các cột vào DataTable
            dt.Columns.Add("STT", typeof(int));        // Cột STT
            dt.Columns.Add("HoTen", typeof(string));   // Cột Họ Tên
            dt.Columns.Add("NgaySinh", typeof(string)); // Cột Ngày Sinh
            dt.Columns.Add("GioiTinh", typeof(string)); // Cột Giới Tính
            dt.Columns.Add("CCCD", typeof(string));    // Cột CCCD
            dt.Columns.Add("QuanHe", typeof(string));  // Cột Quan Hệ

            // Biến đếm số thứ tự
            int stt = 1;

            // Duyệt qua danh sách và thêm dữ liệu vào DataTable
            foreach (ThongTinKhachDTO th in list)
            {
                // Tạo một hàng mới cho DataTable
                if (!khach.Contains(th.MaKhachTro))
                {
                    if (th.ThuongTru == nguoikhai.ThuongTru && th.TrangThai == 1)
                    {
                        var row = dt.NewRow();

                        // Gán giá trị cho các cột
                        row["STT"] = stt++;
                        row["HoTen"] = th.HoTen; // Giả sử HoTen là thuộc tính trong ThongTinKhachDTO
                        row["NgaySinh"] = th.NgaySinh.Value.ToString("dd/MM/yyyy"); // Giả sử NgaySinh là thuộc tính DateTime
                        row["GioiTinh"] = th.GioiTinh; // Giả sử GioiTinh là thuộc tính trong ThongTinKhachDTO
                        row["CCCD"] = th.CCCD; // Giả sử CCCD là thuộc tính trong ThongTinKhachDTO
                        row["QuanHe"] = th.QuanHe; // Giả sử QuanHe là thuộc tính trong ThongTinKhachDTO
                        khach.Add(th.MaKhachTro);

                        // Thêm hàng vào DataTable
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt; // Trả về DataTable chứa dữ liệu
        }
        private System.Data.DataTable BangHopDong(List<ThongTinKhachDTO> list)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            // Thêm các cột vào DataTable
            dt.Columns.Add("STT", typeof(int));        // Cột STT
            dt.Columns.Add("Họ tên", typeof(string));   // Cột Họ Tên
            dt.Columns.Add("Ngày sinh", typeof(string)); // Cột Ngày Sinh
            dt.Columns.Add("Quan hệ", typeof(string));  // Cột Quan Hệ

            // Biến đếm số thứ tự
            int stt = 1;
            foreach (ThongTinKhachDTO th in list)
            {
                if (!khach.Contains(th.MaKhachTro) && th.TrangThai == 1)
                {
                    var row = dt.NewRow();
                    // Gán giá trị cho các cột
                    row["STT"] = stt++;
                    row["Họ tên"] = th.HoTen; // Giả sử HoTen là thuộc tính trong ThongTinKhachDTO
                    row["Ngày sinh"] = th.NgaySinh.Value.ToString("dd/MM/yyyy"); // Giả sử NgaySinh là thuộc tính DateTime
                    row["Quan hệ"] = th.QuanHe; // Giả sử QuanHe là thuộc tính trong ThongTinKhachDTO
                                               // Thêm hàng vào DataTable
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
            // Calculate the age
            int age = today.Year - ngaysinh.Year;
            return age;
        }

    }
}
