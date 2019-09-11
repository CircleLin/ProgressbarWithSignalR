using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using ProgressbarWithSignalR.Models;
using NPOI.XSSF.UserModel;
using System.Threading;

namespace ProgressbarWithSignalR.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string errMsg = null;

            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var uploadPath = Path.Combine(Server.MapPath("~/Content/Upload"), file.FileName);

                    if (System.IO.File.Exists(uploadPath))
                    {
                        System.IO.File.Delete(uploadPath);
                    }
                    file.SaveAs(uploadPath);

                    XSSFWorkbook workbook = null;
                    XSSFSheet sheet = null;

                    workbook = new XSSFWorkbook(new FileStream(uploadPath, FileMode.Open));
                    sheet = (XSSFSheet)workbook.GetSheetAt(0);

                    var rowCount = sheet.LastRowNum - sheet.FirstRowNum;
                    HttpContext.Cache.Insert("rowCount", rowCount, null, DateTime.MaxValue, TimeSpan.FromMinutes(3));

                    for (int j = (sheet.FirstRowNum + 1); j <= sheet.LastRowNum; j++)
                    {
                        var row = sheet.GetRow(j);

                        //Mapping到Hospital class
                        Hospital hospital = DataMapper(row);

                        //模擬寫入到資料庫的情況，每筆randon 0~10毫秒
                        Random random = new Random();
                        Thread.Sleep(random.Next(10));

                        //將目前處理到的筆數寫入Cache暫存，顯示進度用
                        HttpContext.Cache.Insert("insertedRowNum", j, null, DateTime.MaxValue, TimeSpan.FromMinutes(3));
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }

            return View();
        }

        private Hospital DataMapper(IRow row)
        {
            Hospital hospital = new Hospital()
            {
                title = row.GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString(),
                address_for_display = row.GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString(),
                telephone = row.GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString(),
            };

            return hospital;
        }
    }
}