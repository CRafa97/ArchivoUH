using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Contexts;
using ArchivoUH.Models;
using ArchivoUH.Domain;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{

    [CustomAuthorize(admin: false)]
    public class GraduatesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Graduates
        public ActionResult Index()
        {
            var headers = new[] { "Key", "Expedición", "Apellidos", "Nombre", "Nacionalidad", "Finalización",
                                    "Carrera", "Facultad"};

            var rows = (from grd in ctx.Graduates.ToList()
                       select new
                       {
                           Key = grd.GraduatedId,
                           Expedición = grd.ExpeditionTime,
                           Apellidos = grd.LastName,
                           Nombre = grd.FirstName,
                           Nacionalidad = grd.Locality.Province.Country.CountryName,
                           Finalización = grd.FinishTime,
                           Carrera = grd.Course.CourseName,
                           Facultad = grd.Faculty.FacultyName
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
                var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId.ToString() == model.CountrySelected) : Enumerable.Empty<Province>()).ToList();
                var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId.ToString() == (model.ProvinceSelected?? provs[0].ProvinceId.ToString())) : Enumerable.Empty<Locality>()).ToList();
                ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", model.CountrySelected);
                ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", model.ProvinceSelected?? provs[0].ProvinceId.ToString());
                ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", ((model.LocalityId as int?)?? locals[0].LocalityId).ToString());
                return View(model);
            }

            var graduated = new Graduated()
            {
                Serial1 = model.Serial1,
                Serial2 = model.Serial2,
                SerialType = model.SerialType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CourseId = model.CourseId,
                FacultyId = model.FacultyId,
                FolioUH = model.FolioUH,
                TomeUH = model.TomeUH,
                NumberUH = model.NumberUH,
                LocalityId = model.LocalityId,
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
            var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == grad.Locality.Province.CountryId) : Enumerable.Empty<Province>()).ToList();
            var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == grad.Locality.ProvinceId) : Enumerable.Empty<Locality>()).ToList();

            ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", grad.Locality.Province.CountryId.ToString());
            ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", grad.Locality.Province.ProvinceId.ToString());
            ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", grad.LocalityId.ToString());

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
                var provs = (cs.Count != 0 ? ctx.Provinces.ToList().Where(x => x.CountryId == cs[0].CountryId) : Enumerable.Empty<Province>()).ToList();
                var locals = (provs.Count != 0 ? ctx.Localities.ToList().Where(x => x.ProvinceId == provs[0].ProvinceId) : Enumerable.Empty<Locality>()).ToList();
                ViewBag.Countries = new SelectList(cs, "CountryId", "CountryName", model.CountrySelected);
                ViewBag.Provinces = new SelectList(provs, "ProvinceId", "ProvinceName", model.ProvinceSelected ?? provs[0].ProvinceId.ToString());
                ViewBag.Localities = new SelectList(locals, "LocalityId", "LocalityName", (model.LocalityId as int?) ?? locals[0].LocalityId);
                return View(model);
            }

            var grad = ctx.Graduates.Find(model.GraduatedId);

            grad.Serial1 = model.Serial1;
            grad.Serial2 = model.Serial2;
            grad.SerialType = model.SerialType;
            grad.FirstName = model.FirstName;
            grad.LastName = model.LastName;
            grad.CourseId = model.CourseId;
            grad.FacultyId = model.FacultyId;
            grad.FolioUH = model.FolioUH;
            grad.TomeUH = model.TomeUH;
            grad.NumberUH = model.NumberUH;
            grad.LocalityId = model.LocalityId;
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
}