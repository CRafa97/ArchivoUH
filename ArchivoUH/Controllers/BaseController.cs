using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace ArchivoUH.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        //Provides build table using context information
        private DataTable GetTable<T>(IEnumerable<T> elements, string[] headers, Func<T, object[]> Map)
        {
            DataTable dt = new DataTable("Table");

            dt.Columns.AddRange((from h in headers select (new DataColumn(h))).ToArray());

            foreach (var item in elements)
                dt.Rows.Add(Map(item));

            return dt;
        }

        //generic export to excel
        public void ExportToExcel<T>(IEnumerable<T> elements, string[] headers, Func<T, object[]> Map)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(GetTable(elements, headers, Map));

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Table.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public virtual ActionResult ExportExcel()
        {
            return new EmptyResult();
        } 
    }
}