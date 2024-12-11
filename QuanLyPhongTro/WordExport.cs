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
            Table table = _doc.Tables[indexTable];

            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    if (i + 1 < table.Columns.Count) 
                    {
                        table.Cell(1, i + 2).Range.Text = numbers[i]; 
                    }
                    else
                    {
                        break; 
                    }
                }
            }
        }
        public void WriteDataTableToWordTable(System.Data.DataTable dt, int indexTable)
        {
            Table table = _doc.Tables[indexTable]; 

            while (table.Rows.Count > 1) 
            {
                table.Rows[2].Delete(); 
            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                table.Cell(1, i + 1).Range.Text = dt.Columns[i].ColumnName;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Row newRow = table.Rows.Add();

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
            _doc.Close(false); 
            _app.Quit();
        }

        public void ReplaceFieldWithImage(string fieldName, Bitmap image)
        {
            foreach (Word.Field field in _doc.Fields)
            {
                string currentFieldName = field.Code.Text.Substring(11, field.Code.Text.IndexOf("\\") - 12).Trim();

                if (currentFieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    field.Select();

                    _app.Selection.Delete();

                    string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                    image.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);

                    var range = _app.Selection.Range;
                    var inlineShape = range.InlineShapes.AddPicture(tempImagePath, LinkToFile: false, SaveWithDocument: true);

                    inlineShape.Width = 40f;   
                    inlineShape.Height = 50f;

                    File.Delete(tempImagePath);
                    break; 
                }
            }
        }
        public void InsertImageAndTextInTableCell(int tableIndex, int row, int column, string text, Bitmap image)
        {
            if (tableIndex <= _doc.Tables.Count)
            {
                Word.Table table = _doc.Tables[tableIndex];

                if (column <= table.Columns.Count)
                {
                    while (row > table.Rows.Count)
                    {
                        table.Rows.Add();
                    }

                    Word.Range cellRange = table.Cell(row, column).Range;

                    string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                    image.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);
                    InlineShape picture = cellRange.InlineShapes.AddPicture(tempImagePath, LinkToFile: false, SaveWithDocument: true);

                    picture.Width = 40f; 
                    picture.Height = 50f; 

                    if (cellRange.Text.Length > 0)
                    {
                        cellRange.InsertAfter("\n" + text);
                    }
                    else
                    {
                        cellRange.InsertAfter("\n" + text);
                    }

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
