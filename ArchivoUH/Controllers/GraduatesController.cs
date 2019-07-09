using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Contexts;
using ArchivoUH.Models;
using ArchivoUH.Domain;
using ArchivoUH.Validations;
using System.Data.OleDb;
using System.Data;
using System.Data.Entity.Validation;
using LinqToExcel;

namespace ArchivoUH.Controllers
{

    [CustomAuthorize(admin: false)]
    public class GraduatesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Graduates
        public ActionResult Index()
        {
            var headers = new[] { "Key", "Serial", "Expedición", "Apellidos", "Nombre", "Finalización",
                                    "Carrera", "SerialUH"};

            var rows = (from grd in ctx.Graduates.ToList()
                       select new
                       {
                           Key = grd.GraduatedId,
                           Serial = $"{grd.Serial1??"000"}-{grd.Serial2??"000"}-{grd.SerialType}",
                           Expedición = grd.ExpeditionTime,
                           Apellidos = grd.LastName,
                           Nombre = grd.FirstName,
                           Finalización = grd.FinishTime,
                           Carrera = grd.Course.CourseName,
                           SerialUH = $"{grd.TomeUH}-{grd.FolioUH}-{grd.NumberUH}"
                       });

            var model = new GraduatedViewModel()
            {
                IndexTable = new TableViewModel("Graduados", headers, rows)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ShowValidation = false;
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
            ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName");
            ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName");
            var cs = ctx.Countries.ToList();
            var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == cs[0].CountryId) : Enumerable.Empty<Province>()).ToList();
            var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == provs[0].ProvinceId) : Enumerable.Empty<Locality>()).ToList();
            ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName");
            ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName");
            ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(GraduatedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowValidation = true;
                ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
                ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", model.FacultyId);
                ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", model.CourseId);
                var cs = ctx.Countries.ToList();
                var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == model.CountryId) : Enumerable.Empty<Province>()).ToList();
                var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == (model.ProvinceId?? provs[0].ProvinceId)) : Enumerable.Empty<Locality>()).ToList();
                ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", model.CountryId);
                ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", model.ProvinceId?? provs[0].ProvinceId);
                ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", model.LocalityId?? locals[0].LocalityId);
                return View(model);
            }

            var graduated = new Graduated()
            {
                Serial1 = model.Serial1 ?? "000",
                Serial2 = model.Serial2 ?? "000",
                SerialType = model.SerialType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CourseId = model.CourseId,
                FacultyId = model.FacultyId,
                FolioUH = model.FolioUH,
                TomeUH = model.TomeUH,
                NumberUH = model.NumberUH,
                FacultyTome = model.FacultyTome,
                FacultyFolio = model.FacultyFolio,
                FacultyNumber = model.FacultyNumber,
                ProvinceId = model.ProvinceId,
                LocalityId = model.LocalityId,
                CountryId = model.CountryId.Value,
                Observations = model.Observations,
                GoldTitle = model.GoldTitle,
                ScientistCredit = model.ScientistCredit,
                ExpeditionCauses = model.ExpeditionCauses,
                ExpeditionTime = model.ExpeditionTime,
                FinishTime = model.FinishTime
            };

            ctx.Graduates.Add(graduated);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse(id as string);
            var leaved = ctx.Graduates.Find(key);
            return View(new GraduatedViewModel(leaved));
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse(id as string);
            var grad = ctx.Graduates.Find(key);
            ViewBag.ShowValidation = false;
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
            ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", grad.FacultyId);
            ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", grad.CourseId);

            var cs = ctx.Countries.ToList();
            var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == grad.CountryId) : Enumerable.Empty<Province>()).ToList();
            var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == grad.ProvinceId) : Enumerable.Empty<Locality>()).ToList();

            ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", grad.CountryId);
            ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", grad.ProvinceId);
            ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", grad.LocalityId);

            return View(new GraduatedViewModel(grad));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GraduatedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowValidation = true;
                ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
                ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", model.FacultyId);
                ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", model.CourseId);
                var cs = ctx.Countries.ToList();
                var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == model.CountryId) : Enumerable.Empty<Province>()).ToList();
                var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == model.ProvinceId) : Enumerable.Empty<Locality>()).ToList();
                ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", model.CountryId);
                ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", model.ProvinceId);
                ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", model.LocalityId);
                return View(model);
            }

            var grad = ctx.Graduates.Find(model.GraduatedId);

            grad.Serial1 = model.Serial1?? "000";
            grad.Serial2 = model.Serial2?? "000";
            grad.SerialType = model.SerialType;
            grad.FirstName = model.FirstName;
            grad.LastName = model.LastName;
            grad.CourseId = model.CourseId;
            grad.FacultyId = model.FacultyId;
            grad.FolioUH = model.FolioUH;
            grad.TomeUH = model.TomeUH;
            grad.NumberUH = model.NumberUH;
            grad.LocalityId = model.LocalityId;
            grad.CountryId = model.CountryId.Value;
            grad.ProvinceId = model.ProvinceId;
            grad.Observations = model.Observations;
            grad.GoldTitle = model.GoldTitle;
            grad.ScientistCredit = model.ScientistCredit;
            grad.ExpeditionCauses = model.ExpeditionCauses;
            grad.ExpeditionTime = model.ExpeditionTime;
            grad.FinishTime = model.FinishTime;
            
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse(id as string);
            var grad = ctx.Graduates.Find(key);
            return View(new GraduatedViewModel(grad));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse(id as string);
            var grad = ctx.Graduates.Find(key);
            ctx.Graduates.Remove(grad);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public sealed override ActionResult ExportExcel()
        {
            var headers = new[] { "Expedición", "Apellidos", "Nombre", "Nacionalidad", "Finalización",
                                    "Carrera", "Facultad"};
            Func<Graduated, object[]> map = (g) => new object[] {g.ExpeditionTime, g.LastName, g.FirstName, g.Locality.Province.Country.CountryName,
                                                                    g.FinishTime, g.Course.CourseName, g.Faculty.FacultyName};
            var items = from grad in ctx.Graduates
                        select grad;

            ExportToExcel(items, headers, map);

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult UploadExcel(Graduated users, HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {

                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var graduatedrecords = from a in excelFile.Worksheet<ImportTemplate>(sheetName) select a;

                    foreach (var a in graduatedrecords)
                    {
                        try
                        {
                            Graduated TU = new Graduated();
                            TU.ExpeditionTime = a.FechaExpedicion;
                            TU.FirstName = a.Nombre;
                            TU.LastName = a.Apellidos;
                            TU.CountryId = ctx.Countries.Where(x => x.CountryName.Equals(a.Pais)).Select(x => x.CountryId).ToList()[0];
                            TU.FinishTime = a.FechaTerminacion;
                            TU.FacultyId = ctx.Faculties.Where(x => x.FacultyName.Equals(a.Facultad)).Select(x => x.FacultyId).ToList()[0];
                            TU.CourseId = ctx.Courses.Where(x => x.CourseName.Equals(a.Carrera)).Select(x => x.CourseId).ToList()[0];
                            TU.TomeUH = a.TomoUH;
                            TU.FolioUH = a.FolioUH;
                            TU.NumberUH = a.NumeroUH;
                            TU.FacultyTome = a.TomoFac;
                            TU.FacultyFolio = a.FolioFac;
                            TU.FacultyNumber = a.NumeroFac;

                            ctx.Graduates.Add(TU);
                            ctx.SaveChanges();
                        }

                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    //alert message for invalid file format  
                    //data.Add("<ul>");
                    //data.Add("<li>Only Excel file format is allowed</li>");
                    //data.Add("</ul>");
                    //data.ToArray();
                    //return Json(data, JsonRequestBehavior.AllowGet);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                //    data.Add("<ul>");
                //    if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                //    data.Add("</ul>");
                //    data.ToArray();
                //    return Json(data, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult GetProvinces(int? id)
        {
            var provs = new SelectList((from p in ctx.Provinces
                                       where p.CountryId == (id?? p.CountryId)
                                       select new SelectListItem
                                       {
                                           Value = p.ProvinceId.ToString(),
                                           Text = p.ProvinceName
                                       }).ToList(), "Value", "Text");

            return Json(provs, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLocalities(int? id)
        {
            var locs = new SelectList((from l in ctx.Localities
                                       where l.ProvinceId == (id ?? l.ProvinceId)
                                       select new SelectListItem
                                       {
                                           Value = l.LocalityId.ToString(),
                                           Text = l.LocalityName
                                       }).ToList(), "Value", "Text");

            return Json(locs, JsonRequestBehavior.AllowGet);
        }
    }
    class ImportTemplate
    {
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Pais { get; set; }

        public string Facultad { get; set; }

        public string Carrera { get; set; }

        public int TomoUH { get; set; }

        public int FolioUH { get; set; }

        public int NumeroUH { get; set; }

        public DateTime FechaTerminacion { get; set; }

        public DateTime FechaExpedicion { get; set; }

        public int TomoFac { get; set; }
        public int FolioFac { get; set; }
        public int NumeroFac { get; set; }

    }
}