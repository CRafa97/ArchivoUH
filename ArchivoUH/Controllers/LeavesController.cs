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
    public class LeavesController : BaseController
    {
        public ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Leaves
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Serial", "Nombre", "Apellidos", "Facultad", "Carrera", "Fecha" };
            var rows = (from leaved in ctx.Leaves.ToList()
                        select new
                        {
                            Key = leaved.LeavedId,
                            Serial = $"{leaved.Serial1}-{leaved.Serial2}-{leaved.SerialType}",
                            Nombre = leaved.FirstName,
                            Apellidos = leaved.LastName,
                            Facultad = leaved.Faculty.FacultyName,
                            Carrera = leaved.Course.CourseName,
                            Fecha = leaved.LeavedDate
                        });

            var model = new LeavedViewModel()
            {
                IndexTable = new TableViewModel("Bajas", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
            ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName");
            ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName");
            ViewBag.ShowValidation = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeavedViewModel model)
        { 
            if(!ModelState.IsValid)
            {
                ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>().Where(x => x != SerialType.ADM));
                ViewBag.ShowValidation = true;
                ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", model.FacultyId);
                ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", model.CourseId);
                return View(model);
            }

            var leaved = new Leaved()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FacultyId = model.FacultyId,
                CourseId = model.CourseId,
                LeavedDate = model.LeavedDate,
                Serial1 = model.Serial1,
                Serial2 = model.Serial2,
                SerialType = model.SerialType,
                Causes = model.Causes
            };

            ctx.Leaves.Add(leaved);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse(id as string);
            var leaved = ctx.Leaves.Find(key);
            ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>(), leaved.SerialType);
            ViewBag.ShowValidation = false;
            ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", leaved.FacultyId);
            ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", leaved.CourseId);
            return View(new LeavedViewModel(leaved));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeavedViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = new SelectList(Enum.GetValues(typeof(SerialType)).OfType<SerialType>(), model.SerialType);
                ViewBag.ShowValidation = true;
                ViewBag.Faculties = new SelectList(ctx.Faculties, "FacultyId", "FacultyName", model.CurrentFaculty);
                ViewBag.Courses = new SelectList(ctx.Courses, "CourseId", "CourseName", model.CurrentCourse);
                return View(model);
            }

            var leaved = ctx.Leaves.Find(model.LeavedId);
            leaved.Serial1 = model.Serial1;
            leaved.Serial2 = model.Serial2;
            leaved.SerialType = model.SerialType;
            leaved.FirstName = model.FirstName;
            leaved.LastName = model.LastName;
            leaved.FacultyId = model.FacultyId;
            leaved.CourseId = model.CourseId;
            leaved.LeavedDate = model.LeavedDate;
            leaved.Causes = model.Causes;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse(id as string);
            var leaved = ctx.Leaves.Find(key);
            return View(new LeavedViewModel(leaved));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse(id as string);
            var leaved = ctx.Leaves.Find(key);
            return View(new LeavedViewModel(leaved));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse(id as string);
            var leaved = ctx.Leaves.Find(key);
            ctx.Leaves.Remove(leaved);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public sealed override ActionResult ExportExcel()
        {
            var headers = new [] { "Serial", "Nombre", "Apellidos", "Facultad", "Carrera", "Fecha" };
            var items = from leave in ctx.Leaves
                        select leave;
            Func<Leaved, object[]> map = (l) => new object[] { $"{l.Serial1}-{l.Serial2}-{l.SerialType}", l.FirstName,
                                                                    l.LastName, l.Faculty.FacultyName, l.Course.CourseName, l.LeavedDate };

            ExportToExcel(items, headers, map);

            return new EmptyResult();
        }
    }
}