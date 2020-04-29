using Aliuna_Kundenmanagement.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.IO;

namespace Aliuna_Kundenmanagement.Controller
{
    static class ExcelController
    {
        private static FileInfo fiA = new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Data\excel\Vorlage_Angebot.xlsx"));
        private static FileInfo fiL = new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Data\excel\Vorlage_Lieferschein.xlsx"));
        private static FileInfo fiR = new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Data\excel\Vorlage_Rechnung.xlsx"));
        public static void CreateOffer(Customer customer)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(fiA))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];

                WriteInformation(firstWorksheet, customer);

                //Save your file
                SaveFile(excelPackage);
            }
        }
        public static void CreateInvoice(Customer customer)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(fiR))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];

                WriteInformation(firstWorksheet, customer);

                //Save your file
                SaveFile(excelPackage);
            }
        }
        public static void CreateDelivery(Customer customer)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(fiL))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];

                WriteInformation(firstWorksheet, customer);

                //Save your file
                SaveFile(excelPackage);
            }
        }

        private static void WriteInformation(ExcelWorksheet ws, Customer customer)
        {
            if (customer.company.Equals(string.Empty))
            {
                ws.Cells["A8"].Value = $"{customer.firstName} {customer.lastName}";
                ws.Cells["A9"].Value = $"{customer.street} {customer.housenumber}";
                ws.Cells["A10"].Value = $"{customer.postcode} {customer.city}";
            }
            else
            {
                ws.Cells["A8"].Value = $"{customer.company}";
                if (customer.firstName.Equals(String.Empty))
                {
                    ws.Cells["A9"].Value = $"{customer.street} {customer.housenumber}";
                    ws.Cells["A10"].Value = $"{customer.postcode} {customer.city}";
                }
                else
                {
                    ws.Cells["A9"].Value = $"{customer.firstName} {customer.lastName}";
                    ws.Cells["A10"].Value = $"{customer.street} {customer.housenumber}";
                    ws.Cells["A11"].Value = $"{customer.postcode} {customer.city}";
                }
            }
            ws.Cells["F17"].Value = $"{customer.ID}";
            ws.Cells["I17"].Value = $"{DateTime.Now.ToString("dd.MM.yy")}";
        }
        private static void SaveFile(ExcelPackage excelPackage)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".xlsx"; // Default file extension
            dlg.Filter = "Excel File |*.xlsx"; // Filter files by extension

            // Process save file dialog box results
            if (dlg.ShowDialog() == true)
            {
                excelPackage.SaveAs(
                    new FileInfo(dlg.FileName));
            }
        }
    }

}