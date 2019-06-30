using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Contexts;
using ArchivoUH.Domain;
using ArchivoUH.Models;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin: true)]
    public class KeyWordsController : BaseController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Faculties
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Nombre" };
            var rows = (from kw in ctx.KeyWords.ToList()
                        select new
                        {
                            Key = kw.KeyWordId,
                            Nombre = kw.Name
                        });

            var model = new KeyWordViewModel()
            {
                IndexTable = new TableViewModel("Palabras Clave", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create(KeyWordViewModel model)
        {
            var kw = new KeyWord()
            {
                Name = model.Name
            };

            ctx.KeyWords.Add(kw);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var kw = ctx.KeyWords.Find(key);
            return View(new KeyWordViewModel(kw));
        }

        [HttpPost]
        public ActionResult Edit(KeyWordViewModel model)
        {
            var kw = ctx.KeyWords.Find(model.KeyWordId);

            kw.Name = model.Name;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var kw = ctx.KeyWords.Find(key);
            return View(new KeyWordViewModel(kw));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var kw = ctx.KeyWords.Find(key);
            return View(new KeyWordViewModel(kw));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var kw = ctx.KeyWords.Find(key);
            ctx.KeyWords.Remove(kw);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}