using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using BLL;
using DTO;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using iText.Layout;
using iText.Layout.Element;
using System.Data;
using iText.IO.Font;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
namespace QuanLyPhongTro
{
    public class Email
    {
        private EmailBLL bll = new EmailBLL();
        public void SendEmailTaoPhieu(PhieuThu pt, string phong)
        {
            if(pt.MaPhong == null)
            {
                MessageBox.Show("Không tìm thấy phòng!");
                return;
            }
            List<string> lst = bll.LayDSEmail(pt.MaPhong);
            if (lst.Count == 0 || lst.All(string.IsNullOrWhiteSpace))
            {
                //MessageBox.Show("Phòng chưa kê khai thông tin!");
                return;
            }
            try
            {
                // Đọc cấu hình từ App.config
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                string emailSender = ConfigurationManager.AppSettings["EmailSender"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];

                foreach (string s in lst)
                {
                    // Tạo đối tượng MailMessage
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailSender); // Email người gửi
                    mail.To.Add(s); // Email người nhận
                    mail.Subject = "Phòng "+phong+" - Mã phiếu: "+pt.MaPT;
                    mail.Body = "Nội dung: Phiếu thu của phòng "+phong+" đã được tạo.\n"+"Được tạo vào ngày: "+pt.NgayLap;
                    mail.IsBodyHtml = true; // Nếu email có HTML, đặt là true

                    // Cấu hình SMTP
                    SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                    smtp.Credentials = new NetworkCredential(emailSender, emailPassword);
                    smtp.EnableSsl = true;

                    // Gửi email
                    smtp.Send(mail);
                }
                
                MessageBox.Show("Email đã được gửi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }
        public async Task ExportAndSendPhieuThuToEmailAsync(PhieuThu pt, string phong, string khuvuc)
        {
            if (pt.MaPhong == null)
            {
                MessageBox.Show("Không tìm thấy phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Lấy danh sách email từ mã phòng
            List<string> emailList = bll.LayDSEmail(pt.MaPhong);
            if (emailList.Count == 0 || emailList.All(string.IsNullOrWhiteSpace))
            {
                //MessageBox.Show("Phòng chưa kê khai thông tin email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đường dẫn tạm thời lưu file PDF
                string tempPdfPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"PhieuThu_{pt.MaPT}.pdf");

                // Bước 1: Xuất phiếu thu sang file PDF
                await ExportPhieuThuToPdfAsync(pt, tempPdfPath, khuvuc);

                if (!File.Exists(tempPdfPath))
                {
                    MessageBox.Show("Xuất file PDF thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Đọc cấu hình email từ App.config
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                string emailSender = ConfigurationManager.AppSettings["EmailSender"];
                string emailPassword = ConfigurationManager.AppSettings["EmailPassword"];

                

                // Nội dung email
                string subject = $"Phòng {phong} - Mã phiếu: {pt.MaPT}";
                string body = $"<p>Phiếu thu của phòng <strong>{phong}</strong>.</p>" +
                              $"<p><strong>Mã phiếu:</strong> {pt.MaPT}</p>" +
                              $"<p><strong>Ngày lập:</strong> {pt.NgayLap:dd/MM/yyyy}</p>" +
                              "<p>File PDF của phiếu thu được đính kèm theo email này.</p>";

                // Bước 2: Gửi email với file PDF đính kèm
                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                {
                    smtp.Credentials = new NetworkCredential(emailSender, emailPassword);
                    smtp.EnableSsl = true;

                    foreach (string recipientEmail in emailList)
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailSender, "Hệ thống quản lý phòng");
                            mail.To.Add(recipientEmail);
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true; // Cho phép nội dung HTML
                            mail.Attachments.Add(new Attachment(tempPdfPath)); // Đính kèm file PDF

                            try
                            {
                                await smtp.SendMailAsync(mail);
                                MessageBox.Show($"Email đã gửi thành công đến: {recipientEmail}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Không thể gửi email đến {recipientEmail}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }

                // Xóa file tạm sau khi gửi email
                if (File.Exists(tempPdfPath))
                {
                    File.Delete(tempPdfPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private readonly string _fontPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\fonts\ARIAL.TTF");
        private readonly string _logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Images\apartment.png");

        private QuanLyPhieuThuBLL ptbll = new QuanLyPhieuThuBLL();
        private ThongTinAdminBLL adminbll = new ThongTinAdminBLL();
        public async Task ExportPhieuThuToPdfAsync(PhieuThu phieuThu, string tempPdfPath,string makhuvuc)
        {
            DataTable dichVuList = ptbll.GetDichVuPhieuThu(phieuThu.MaPT);
            DataTable phong = ptbll.LoadPhong(phieuThu.MaPhong);
            ThongTinAdminDTO admin = adminbll.GetThongTinAdminByKhuVuc(makhuvuc);
            using (var ms = new FileStream(tempPdfPath, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (var writer = new PdfWriter(ms)) // Tạo PdfWriter với MemoryStream
                    {
                        using (var pdf = new PdfDocument(writer)) // Tạo PdfDocument từ PdfWriter
                        {
                            var document = new Document(pdf, PageSize.A4); // Tạo Document
                            var font = PdfFontFactory.CreateFont(_fontPath, PdfEncodings.IDENTITY_H); // Đảm bảo font hợp lệ
                            Image logoImage = null;
                            // Thêm logo nếu tồn tại
                            if (File.Exists(_logoPath))
                            {
                                 logoImage = new Image(ImageDataFactory.Create(_logoPath))
                                    .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                                    .ScaleToFit(50, 50);
                                document.Add(logoImage);
                            }

                            // Thêm tiêu đề
                            document.Add(new Paragraph("HÓA ĐƠN TIỀN PHÒNG")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(font)
                                .SetFontSize(20));

                            // Thêm thông tin phiếu thu
                            document.Add(new Paragraph($"Mã Phiếu Thu: {phieuThu.MaPT}")
                                .SetFont(font)
                                .SetFontSize(12));

                            // Phòng và trạng thái thanh toán
                            var roomStatusLine = new Paragraph()
                                .Add(new Text($"Phòng: {phieuThu.MaPhong} ").SetFont(font).SetFontSize(12))
                                .Add(new Text("     Trạng Thái: ").SetFont(font).SetFontSize(12))
                                .Add(new Text(phieuThu.TrangThai == 1 ? "Đã thanh toán" : "Chưa thanh toán")
                                    .SetFont(font)
                                    .SetFontSize(12)
                                    .SetFontColor(phieuThu.TrangThai == 1 ? iText.Kernel.Colors.ColorConstants.GREEN : iText.Kernel.Colors.ColorConstants.RED));
                            document.Add(roomStatusLine);

                            // Ngày lập và ngày thu
                            var dateLine = new Paragraph()
                                .Add(new Text($"Ngày Lập: {phieuThu.NgayLap:dd/MM/yyyy}     ").SetFont(font).SetFontSize(12))
                                .Add(new Text($"Ngày Thu: {phieuThu.NgayThu:dd/MM/yyyy}").SetFont(font).SetFontSize(12));
                            document.Add(dateLine);

                            // Tổng tiền còn nợ
                            decimal conno = 0;
                            if (phong.Rows[0]["Congno"] != DBNull.Value)
                            {
                                conno = Convert.ToDecimal(phong.Rows[0]["Congno"]);
                            }
                            document.Add(new Paragraph($"Số tiền còn nợ : {conno.ToString("#,0")} VND")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFont(font)
                            .SetFontSize(12));

                            int stt = 1;




                            // Thêm khoảng cách trước bảng Phiếu Thu
                            document.Add(new Paragraph("\n"));

                            // Thêm bảng Phiếu Thu tiền phòng
                            var phieuThuTable = new Table(7); // Bảng có 7 cột
                            phieuThuTable.SetWidth(UnitValue.CreatePercentValue(100)); // Đặt độ rộng bảng là 100% chiều rộng trang

                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("STT").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Khoản").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Chỉ số đầu").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Chỉ số cuối").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Số lượng").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Đơn giá").SetFont(font)));
                            phieuThuTable.AddHeaderCell(new Cell().Add(new Paragraph("Thành Tiền").SetFont(font)));

                            stt = 1;

                            // Thêm các dịch vụ cố định như Tiền Phòng
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph(stt.ToString()).SetFont(font)));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph("Tiền Phòng").SetFont(font)));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph("-")).SetFont(font));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph("-")).SetFont(font));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph("1").SetFont(font)));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.TienNha.ToString("#,0")} ").SetFont(font)));
                            phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.TienNha.ToString("#,0")} ").SetFont(font)));
                            stt++;


                            foreach (DataRow dichVu in dichVuList.Rows)
                            {
                                // Kiểm tra nếu là dịch vụ điện hoặc nước
                                if (dichVu["TenDichVu"].ToString() == "Dịch vụ điện")
                                {
                                    // Cập nhật dòng Tiền Điện
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph(stt.ToString()).SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph("Tiền Điện").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.DienCu.ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.DienMoi.ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{(phieuThu.DienMoi - phieuThu.DienCu).ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{(dichVu["DonGia"] != DBNull.Value ? Convert.ToDecimal(dichVu["DonGia"]).ToString("#,0") : "-")}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.TienDien?.ToString("#,0")} ").SetFont(font))); // Thành tiền
                                    stt++;
                                }
                                else if (dichVu["TenDichVu"].ToString() == "Dịch vụ nước")
                                {
                                    // Cập nhật dòng Tiền Nước
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph(stt.ToString()).SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph("Tiền Nước").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.NuocCu.ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.NuocMoi.ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{(phieuThu.NuocMoi - phieuThu.NuocCu).ToString()}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{(dichVu["DonGia"] != DBNull.Value ? Convert.ToDecimal(dichVu["DonGia"]).ToString("#,0") : "-")}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{phieuThu.TienNuoc?.ToString("#,0")} ").SetFont(font))); // Thành tiền
                                    stt++;
                                }
                                else
                                {
                                    // Các dịch vụ còn lại hiển thị bình thường
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph(stt.ToString()).SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph(dichVu["TenDichVu"].ToString() ?? "-").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph("-").SetFont(font))); // Chỉ số đầu
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph("-").SetFont(font))); // Chỉ số cuối
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph(dichVu["SoLuong"].ToString()).SetFont(font))); // Số lượng
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{(dichVu["DonGia"] != DBNull.Value ? Convert.ToDecimal(dichVu["DonGia"]).ToString("#,0") : "-")}").SetFont(font)));
                                    phieuThuTable.AddCell(new Cell().Add(new Paragraph($"{((dichVu["ThanhTien"] is DBNull || dichVu["ThanhTien"] == null ? 0 : Convert.ToDecimal(dichVu["ThanhTien"])) * 1).ToString("#,0")}")));
                                    stt++;
                                }
                            }
                            document.Add(new Paragraph("PHIẾU THU TIỀN PHÒNG")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFont(font)
                            .SetFontSize(12));

                            document.Add(phieuThuTable);

                            document.Add(new Paragraph("\n"));

                            // Tổng tiền và kết thúc
                            document.Add(new Paragraph($"Tổng Tiền: {phieuThu.TongTien?.ToString("#,0")} VND")
                                .SetTextAlignment(TextAlignment.RIGHT).SetFont(font).SetFontSize(12));





                            document.Add(new Paragraph("\n"));

                            // Thêm dòng yêu cầu và thông tin liên hệ, căn giữa
                            document.Add(new Paragraph("* Yêu cầu các phòng thanh toán trước thời gian quy định")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(font)
                                .SetFontColor(ColorConstants.RED)
                                .SetFontSize(12));

                            document.Add(new Paragraph("Mọi thông tin liên hệ quản lý theo SĐT: "+admin.Phone)
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(font)
                                .SetFontSize(12));

                            document.Add(new Paragraph("Chân thành cảm ơn !")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(font)
                                .SetFontSize(12));

                            // Thêm khoảng cách giữa logo và tiêu đề
                            document.Add(new Paragraph("\n"));
                            // Thêm logo vào đầu trang, căn giữa
                            logoImage = new Image(ImageDataFactory.Create(_logoPath));
                            logoImage.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                            logoImage.ScaleToFit(30, 30); // Điều chỉnh kích thước logo nếu cần
                            document.Add(logoImage);

                            // Kết thúc tài liệu
                            document.Close();
                        }
                    }

                    // Trả về mảng byte của PDF đã tạo
                    //MessageBox.Show($"Đã lưu PDF vào: {tempPdfPath}");
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ
                    Console.WriteLine($"Có lỗi xảy ra: {ex.Message}");
                    throw;
                }
            }
        }



    }
}
