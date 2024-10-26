using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace QuanLyPhongTro
{
    public class WordExport
    {
        private Word.Application _app;
        Word.Document _doc;
        private object _pathFile;
        public WordExport(string vPath, bool vCreateApp)
        {
            _pathFile = vPath;
            _app = new Word.Application();
            _app.Visible = vCreateApp;
            object ob = System.Reflection.Missing.Value;
            _doc = _app.Documents.Add(ref _pathFile, ref ob, ref ob, ref ob);
        }
        public void WriteFields(Dictionary<string, string> vValues)
        {
            foreach (Word.Field field in _doc.Fields)
            {
                string fieldName = field.Code.Text.Substring(11, field.Code.Text.IndexOf("\\") - 12).Trim();
                if (vValues.ContainsKey(fieldName))
                {
                    field.Select();
                    _app.Selection.TypeText(vValues[fieldName]);
                }
            }
        }
        public void WriteTable(System.Data.DataTable vDataTable, int vIndexTable)
        {
            Word.Table tbl = _doc.Tables[vIndexTable];
            int lenrow = vDataTable.Rows.Count;
            int lencol = vDataTable.Columns.Count;
            for (int i = 0; i < lenrow; ++i)
            {
                object ob = System.Reflection.Missing.Value;
                tbl.Rows.Add(ref ob);
                for (int j = 0; j < lencol; ++j)
                {
                    tbl.Cell(i + 2, j + 1).Range.Text = vDataTable.Rows[i][j].ToString();
                }
            }
        }
        public void WriteToTable(List<string> numbers, int indexTable)
        {
            // Lấy bảng theo chỉ số được cung cấp
            Table table = _doc.Tables[indexTable];

            // Kiểm tra xem bảng có ít nhất một dòng không
            if (table.Rows.Count > 0)
            {
                // Thêm từng số vào các ô bắt đầu từ ô thứ hai
                for (int i = 0; i < numbers.Count; i++)
                {
                    // Bắt đầu từ ô thứ 2 (cột 2)
                    if (i + 1 < table.Columns.Count) // Kiểm tra xem có đủ cột trong bảng không
                    {
                        table.Cell(1, i + 2).Range.Text = numbers[i]; // Thêm số vào ô thứ 2 trở đi
                    }
                    else
                    {
                        // Nếu không đủ cột, bạn có thể thông báo hoặc không làm gì
                        break; // Dừng thêm nếu đã đầy cột
                    }
                }
            }
        }
        public void WriteDataTableToWordTable(System.Data.DataTable dt, int indexTable)
        {
            // Giả sử bạn đang làm việc với tài liệu Word đã mở
            Table table = _doc.Tables[indexTable]; // Lấy bảng thứ indexTable từ tài liệu

            // Xóa tất cả các dòng trong bảng (nếu cần) trước khi thêm dữ liệu mới
            while (table.Rows.Count > 1) // Giữ lại dòng tiêu đề nếu có
            {
                table.Rows[2].Delete(); // Xóa dòng thứ 2 trở đi
            }

            // Thêm tiêu đề cho bảng (nếu cần)
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                table.Cell(1, i + 1).Range.Text = dt.Columns[i].ColumnName; // Thêm tiêu đề
            }

            // Thêm dữ liệu từ DataTable vào bảng
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Thêm một dòng mới
                Row newRow = table.Rows.Add();

                // Điền dữ liệu vào từng ô trong hàng mới
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    newRow.Cells[j + 1].Range.Text = dt.Rows[i][j].ToString();
                }
            }
        }


        public void SaveAs(string newFilePath)
        {
            _doc.SaveAs2(newFilePath);
        }
        public void Close()
        {
            _doc.Close(false); // false để không lưu lại khi đóng
            _app.Quit();
        }

        public void ReplaceFieldWithImage(string fieldName, Bitmap image)
        {
            // Kiểm tra xem trường có tồn tại không
            foreach (Word.Field field in _doc.Fields)
            {
                // Lấy tên trường từ mã trường
                string currentFieldName = field.Code.Text.Substring(11, field.Code.Text.IndexOf("\\") - 12).Trim();

                // Nếu tên trường khớp, thay thế nó bằng hình ảnh
                if (currentFieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    // Chọn trường để thay thế
                    field.Select();

                    // Xóa nội dung của trường
                    _app.Selection.Delete();

                    // Lưu hình ảnh Bitmap tạm thời vào file
                    string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                    image.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);

                    // Chèn hình ảnh vào vị trí của trường
                    var range = _app.Selection.Range;
                    var inlineShape = range.InlineShapes.AddPicture(tempImagePath, LinkToFile: false, SaveWithDocument: true);

                    inlineShape.Width = 40f;   // Chiều rộng ảnh (tính bằng points, 1 inch = 72 points)
                    inlineShape.Height = 50f;

                    // Xóa file tạm sau khi chèn
                    File.Delete(tempImagePath);
                    break; // Ngừng vòng lặp khi đã thay thế xong trường
                }
            }
        }
        public void InsertImageAndTextInTableCell(int tableIndex, int row, int column, string text, Bitmap image)
        {
            // Get the specified table from the document
            if (tableIndex <= _doc.Tables.Count)
            {
                Word.Table table = _doc.Tables[tableIndex];

                // Check if the specified column exists in the table
                if (column <= table.Columns.Count)
                {
                    // Check if row is greater than the current row count
                    while (row > table.Rows.Count)
                    {
                        // Add a new row
                        table.Rows.Add();
                    }

                    // Get the range of the specified cell (after ensuring the row exists)
                    Word.Range cellRange = table.Cell(row, column).Range;

                    // Add the image
                    string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                    image.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);
                    InlineShape picture = cellRange.InlineShapes.AddPicture(tempImagePath, LinkToFile: false, SaveWithDocument: true);

                    // Set image size
                    picture.Width = 40f; // Width in points
                    picture.Height = 50f; // Height in points

                    // Check if there is existing text in the cell
                    if (cellRange.Text.Length > 0)
                    {
                        // If cell is not empty, we will insert the text after the image
                        cellRange.InsertAfter("\n" + text);
                    }
                    else
                    {
                        // If cell is empty, just add the text after the image
                        cellRange.InsertAfter("\n" + text);
                    }

                    // Delete temporary image file after use
                    File.Delete(tempImagePath);
                }
                else
                {
                    Console.WriteLine("Specified column does not exist in the table.");
                }
            }
            else
            {
                Console.WriteLine("Specified table index does not exist.");
            }
        }





    }
}
