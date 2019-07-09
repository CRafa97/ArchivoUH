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
    [CustomAuthorize(admin: true)]
    public class FacultiesController : BaseController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Nombre" };
            var rows = (from fac in ctx.Faculties.ToList()
                        select new
                        {
                            Key = fac.FacultyId,
                            Nombre = fac.FacultyName,
                        });

            var model = new FacultyViewModel()
            {
                IndexTable = new TableViewModel("Facultades", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create(FacultyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var headers = new string[] { "Key", "Nombre", "Tomo", "Folio", "Numero" };
                var rows = (from fac in ctx.Faculties.ToList()
                            select new
                            {
                                Key = fac.FacultyId,
                                Nombre = fac.FacultyName,
                            });
                model.IndexTable = new TableViewModel("Facultades", headers, rows);
                return View("Index", model);
            }

            var faculty = new Faculty()
            {
                FacultyName = model.FacultyName
            };

            ctx.Faculties.Add(faculty);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var faculty = ctx.Faculties.Find(key);
            return View(new FacultyViewModel(faculty));
        }

        [HttpPost]
        public ActionResult Edit(FacultyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var faculty = ctx.Faculties.Find(model.FacultyId);

            faculty.FacultyName = model.FacultyName;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var faculty = ctx.Faculties.Find(key);
            return View(new FacultyViewModel(faculty));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var faculty = ctx.Faculties.Find(key);
            return View(new FacultyViewModel(faculty));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var faculty = ctx.Faculties.Find(key);

            if(faculty.Graduates.Count != 0 || faculty.Leaves.Count != 0)
            {
                ModelState.AddModelError("", "Existen entidades que dependen de esta facultad");
                return View(new FacultyViewModel(faculty));
            }

            ctx.Faculties.Remove(faculty);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}