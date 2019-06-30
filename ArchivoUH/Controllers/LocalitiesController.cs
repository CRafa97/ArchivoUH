using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Contexts;
using ArchivoUH.Domain;
using ArchivoUH.Models;
using System.Data;
using System.Data.Entity;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin: true)]
    public class LocalitiesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Localities
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Localidad", "Provincia", "País" };
            var rows = (from locality in ctx.Localities.ToList()
                        select new
                        {
                            Key = locality.LocalityId,
                            Localidad = locality.LocalityName,
                            Provincia = locality.Province.ProvinceName,
                            País = locality.Province.Country.CountryName
                        });

            var model = new LocalityViewModel()
            {
                IndexTable = new TableViewModel("Localidades", headers, rows)
            };

            ViewBag.Provincies = new SelectList(ctx.Provinces, "ProvinceId", "ProvinceName");
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LocalityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var headers = new string[] { "Key", "Localidad", "Provincia", "País" };
                var rows = (from l in ctx.Localities.ToList()
                            select new
                            {
                                Key = l.LocalityId,
                                Localidad = l.LocalityName,
                                Provincia = l.Province.ProvinceName,
                                País = l.Province.Country.CountryName
                            });
                model.IndexTable = new TableViewModel("Localidades", headers, rows);
                ViewBag.Provincies = new SelectList(ctx.Provinces, "ProvinceId", "ProvinceName", model.ProvinceId);
                return View("Index", model);
            }

            var locality = new Locality()
            {
                LocalityName = model.LocalityName,
                ProvinceId = model.ProvinceId
            };

            ctx.Localities.Add(locality);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var locality = ctx.Localities.Find(key);
            ViewBag.Provinces = new SelectList(ctx.Provinces, "ProvinceId", "ProvinceName", locality.ProvinceId);
            return View(new LocalityViewModel(locality));
        }

        [HttpPost]
        public ActionResult Edit(LocalityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Provinces = new SelectList(ctx.Provinces, "ProvinceId", "ProvinceName", model.ProvinceId);
                return View(model);
            }

            var locality = ctx.Localities.Find(model.LocalityId);
            locality.LocalityName = model.LocalityName;
            locality.ProvinceId = model.ProvinceId;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var locality = ctx.Localities.Find(key);
            return View(new LocalityViewModel(locality));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var locality = ctx.Localities.Find(key);
            return View(new LocalityViewModel(locality));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var locality = ctx.Localities.Find(key);
            ctx.Localities.Remove(locality);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}