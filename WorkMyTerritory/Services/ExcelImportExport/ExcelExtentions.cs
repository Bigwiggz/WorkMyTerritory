/*
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace WorkMyTerritory.Services.ExcelImportExport
{
    /// <summary>
    /// This class is used to open, close and manipulate excel files
    /// </summary>
    public class ExcelExtentions
    {
        string path = "";
        _Application excel = new Excel.Application();
        Workbook wb;
        Worksheet ws;
        /// <summary>
        /// Overloaded Excel method for creating a new file
        /// </summary>
        public void Excel()
        {
            // Overloaded Method
        }

        /// <summary>
        /// This opens an excel file and sets focus on the first indexed sheet
        /// </summary>
        /// <param name="path">This is the full path to the excel file you want to open</param>
        /// <param name="Sheet">This is the sheet index of the specific worksheet you want to focus on</param>
        public void Excel(string path, int Sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = (Worksheet)wb.Worksheets[Sheet];
        }
        /// <summary>
        /// Reads the value of a single cell from excel
        /// </summary>
        /// <param name="i">This is the row index starting at zero</param>
        /// <param name="j">This is the column index starting at zero</param>
        /// <returns></returns>
        public string ReadCell(int i, int j)
        {
            i++;
            j++;
            string cellValue = ((Excel.Range)ws.Cells[i, j]).Value2.ToString();
            if (cellValue != null)
            {
                return cellValue;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Writes value to a cell
        /// </summary>
        /// <param name="i">This is the row index starting at zero</param>
        /// <param name="j">This is the column index starting at zero</param>
        /// <param name="s">This is the value you want to write to the cell</param>
        public void WriteToCell(int i, int j, string s)
        {
            i++;
            j++;
            ((Excel.Range)ws.Cells[i, j]).Value2 = s;
        }
        /// <summary>
        /// Saves Workbook
        /// </summary>
        public void Save()
        {
            wb.Save();
        }
        /// <summary>
        /// SaveAs workbook provided a path to do so
        /// </summary>
        /// <param name="fileName">This is the full name of the new saved file</param>
        public void SaveAs(string fileName)
        {
            wb.SaveAs2(fileName);
        }
        /// <summary>
        /// This is to close the workbook
        /// </summary>
        public void Close()
        {
            wb.Close();
        }
        /// <summary>
        /// This creates a new excel file
        /// </summary>
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            this.ws = (Worksheet)wb.Worksheets[1];
        }
        /// <summary>
        /// Method to create a new worksheet
        /// </summary>
        public void CreateNewSheet()
        {
            Worksheet temptsheet = (Worksheet)wb.Worksheets.Add(After: ws);
        }
        /// <summary>
        /// Select a worsheet and place focus on it
        /// </summary>
        /// <param name="SheetNumber">Sheet Number index</param>
        public void SelectWorksheet(int SheetNumber)
        {
            this.ws = (Worksheet)wb.Worksheets[SheetNumber];
        }

        /// <summary>
        /// Delete Worksheet
        /// </summary>
        /// <param name="SheetNumber">Selected index number of the worsheet to delete</param>
        public void DeleteWorksheet(int SheetNumber)
        {
            this.ws = (Worksheet)wb.Worksheets[SheetNumber];
            if (this.ws==null)
            {
                //do something here
            }
            else
            {
                ((Worksheet)wb.Worksheets[SheetNumber]).Delete();
            }
        }

        /// <summary>
        /// Protect Worksheet
        /// </summary>
        public void ProtectSheet()
        {
            ws.Protect();
        }

        /// <summary>
        /// Password Protect Worksheet
        /// </summary>
        /// <param name="Password">Password</param>
        public void ProtectSheet(string Password)
        {
            ws.Protect(Password);
        }

        /// <summary>
        /// Unprotect Worksheet
        /// </summary>
        /// <param name="Password"></param>
        public void UnprotectSheet(string Password)
        {
            ws.Unprotect(Password);
        }

        /// <summary>
        /// Read a range of values into an array
        /// </summary>
        /// <param name="starti">The starting row cell index</param>
        /// <param name="starty">The starting column cell index</param>
        /// <param name="endi">The ending row cell index</param>
        /// <param name="endy">The ending column cell index</param>
        /// <returns></returns>
        public string[,] ReadRange(int starti, int starty, int endi, int endy)
        {
            var range = (Excel.Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
            object[,] holder = (object[,])range.Value2;
            string[,] returnstring = new string[endi - starti, endy - starty];
            for(int q=1; q<=endi-starti; q++)
            {
                for (int p = 1; p <= endy - starty; p++)
                {
                    returnstring[p - 1, q - 1] = holder[p, q].ToString();
                }
            }
            return returnstring;
        }
        /// <summary>
        /// Method to write array of data to an excel file
        /// </summary>
        /// <param name="starti">The starting row cell index</param>
        /// <param name="starty">The starting column cell index</param>
        /// <param name="endi">The ending row cell index</param>
        /// <param name="endy">The ending column cell index</param>
        /// <param name="writestring">array of values you want to past</param>
        public void WriteRange(int starti, int starty, int endi, int endy, string[,] writestring)
        {
            var range = (Excel.Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
            range.Value2 = writestring;
        }
        /// <summary>
        /// Converts text values in excel document to a number
        /// </summary>
        /// /// <param name="starti">The starting row cell index</param>
        /// <param name="starty">The starting column cell index</param>
        /// <param name="endi">The ending row cell index</param>
        /// <param name="endy">The ending column cell index</param>
        /// <param name="convertTo">Specify the excel datatype you want to convert the string to: Number, Dollars, DateTime</param>
        public void ConvertRangeFromTextToNumber(int starti, int starty, int endi, int endy, string convertTo)
        {
            var range = (Excel.Range)ws.Range[ws.Cells[starti, starty], ws.Cells[endi, endy]];
            //need to finish

            switch(convertTo)
            {
                case "Number":
                    range.NumberFormat = "0.0"; // change number of decimal places as needed
                    break;

                case "Dollars":
                    range.Style = "Currency";
                    break;
                
                case "DateTime":
                    range.Style = "Currency";
                    break;

                default:
                    break;
            }
        }
    }
}
*/