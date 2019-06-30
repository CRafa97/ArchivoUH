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
    public class CoursesController : BaseController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Nombre" };
            var rows = (from course in ctx.Courses.ToList()
                        select new
                        {
                            Key = course.CourseId,
                            Nombre = course.CourseName
                        });

            var model = new CourseViewModel()
            {
                IndexTable = new TableViewModel("Carreras", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var headers = new string[] { "Key", "Nombre" };
                var rows = (from c in ctx.Courses.ToList()
                            select new
                            {
                                Key = c.CourseId,
                                Nombre = c.CourseName
                            });
                model.IndexTable = new TableViewModel("Carreras", headers, rows);
                return View("Index", model);
            }

            var course = new Course()
            {
                CourseName = model.CourseName
            };

            ctx.Courses.Add(course);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var course = ctx.Courses.Find(key);
            return View(new CourseViewModel(course));
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel model)
        {
            var course = ctx.Courses.Find(model.CourseId);

            course.CourseName = model.CourseName;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var course = ctx.Courses.Find(key);
            return View(new CourseViewModel(course));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var course = ctx.Courses.Find(key);
            return View(new CourseViewModel(course));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var course = ctx.Courses.Find(key);

            if (course.Graduates.Count != 0 || course.Leaves.Count != 0)
            {
                ModelState.AddModelError("", "Existen entidades que dependen de esta carrera");
                return View(new CourseViewModel(course));
            }

            ctx.Courses.Remove(course);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}