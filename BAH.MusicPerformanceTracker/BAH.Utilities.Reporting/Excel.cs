using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace BAH.Utilities.Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
            try
            {
                Application xlApp;
                Workbook xlWB;
                Worksheet xlWS;

                xlApp = new Application();
                xlWB = xlApp.Workbooks.Add(System.Reflection.Missing.Value);
                xlWS = (Worksheet)xlWB.Sheets["Sheet1"];

                for (int iRow = 0; iRow < data.GetLength(0); iRow++)
                {
                    for (int iCol = 0; iCol < data.GetLength(1); iCol++)
                    {
                        // Put the data in the cell
                        xlWS.Cells[iRow + 1, iCol + 1] = data[iRow, iCol];
                    }
                }

                //Increase the column size as needed
                xlWS.Columns.AutoFit();

                // Add grid lines
                xlWS.UsedRange.Borders.LineStyle = XlLineStyle.xlContinuous;

                // Disable alerts
                xlApp.DisplayAlerts = false;

                // Save the file
                xlWS.SaveAs(filename);

                // Save as a pdf also
                xlWB.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF,
                                         filename.Substring(0, filename.Length - 5));

                xlWB.Close();
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWS);
                Marshal.ReleaseComObject(xlWB);
                Marshal.ReleaseComObject(xlApp);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}