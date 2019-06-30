using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Domain;
using ArchivoUH.Contexts;
using ArchivoUH.Models;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin: false)]
    public class AdministrativesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Administratives
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Serial", "Nombre", "Clave" };
            var rows = (from adm in ctx.Administratives.ToList()
                        select new
                        {
                            Key = adm.AdministrativeId,
                            Serial = $"{adm.Serial1}-{adm.Serial2}-{adm.SerialType}",
                            Nombre = adm.AdministrativeName,
                            Clave = adm.KeyWord.Name
                        }) ;

            var model = new AdministrativeViewModel()
            {
                IndexTable = new TableViewModel("Administrativos", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.KeyWords = new SelectList(ctx.KeyWords, "KeyWordId", "Name");
            ViewBag.ShowValidation = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministrativeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.KeyWords = new SelectList(ctx.KeyWords, "KeyWordId", "Name");
                ViewBag.ShowValidation = true;
                return View(model);
            }

            var adminRecord = new Administrative()
            {
                Serial1 = model.Serial1,
                Serial2 = model.Serial2,
                AdministrativeName = model.AdministrativeName,
                Description = model.Description,
                KeyWordId = model.KeyWordId,
            };

            ctx.Administratives.Add(adminRecord);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse(id as string);
            var adm = ctx.Administratives.Find(key);
            ViewBag.KeyWords = new SelectList(ctx.KeyWords, "KeyWordId", "Name", adm.KeyWord.Name);
            ViewBag.ShowValidation = false;
            return View(new AdministrativeViewModel(adm));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AdministrativeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.KeyWords = new SelectList(ctx.KeyWords, "KeyWordId", "Name", model.CurrentKW);
                ViewBag.ShowValidation = true;
                return View(model);
            }

            var adm = ctx.Administratives.Find(model.AdministrativeId);
            adm.AdministrativeName = model.AdministrativeName;
            adm.KeyWordId = model.KeyWordId;
            adm.Serial1 = model.Serial1;
            adm.Serial2 = model.Serial2;
            adm.Description = model.Description;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse(id as string);
            var adm = ctx.Administratives.Find(key);
            return View(new AdministrativeViewModel(adm));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse(id as string);
            var adm = ctx.Administratives.Find(key);
            return View(new AdministrativeViewModel(adm));
        }

        [HttpPost] 
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(object id)
        {
            int key = int.Parse(id as string);
            var adm = ctx.Administratives.Find(key);
            ctx.Administratives.Remove(adm);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}