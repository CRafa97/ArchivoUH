using ArchivoUH.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using ArchivoUH.Models;
using ArchivoUH.Domain;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin: true)]
    public class ProvincesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Provinces
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Provincia", "País" };

            var rows = (from p in ctx.Provinces.Include(p => p.Country).ToList()
                        select new
                        {
                            Key = p.ProvinceId,
                            Provincia = p.ProvinceName,
                            País = p.Country.CountryName
                        });

            var model = new ProvinceViewModel()
            {
                IndexTable = new TableViewModel("Provincias", headers, rows)
            };

            ViewBag.Countries = new SelectList(ctx.Countries, "CountryId", "CountryName");
            return View(model);
        }

        public ActionResult Create(ProvinceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var headers = new string[] { "Key", "Provincia", "País" };

                var rows = (from p in ctx.Provinces.Include(p => p.Country).ToList()
                            select new
                            {
                                Key = p.ProvinceId,
                                Provincia = p.ProvinceName,
                                País = p.Country.CountryName
                            });

                model.IndexTable = new TableViewModel("Provincias", headers, rows);
                ViewBag.Countries = new SelectList(ctx.Countries, "CountryId", "CountryName", model.CountryId);
                return View("Index", model);
            }

            var province = new Province()
            {
                ProvinceName = model.ProvinceName,
                CountryId = model.CountryId
            };

            ctx.Provinces.Add(province);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var provincie = ctx.Provinces.Find(key);
            ViewBag.Countries = new SelectList(ctx.Countries, "CountryId", "CountryName", provincie.CountryId);
            return View(new ProvinceViewModel(provincie));
        }

        [HttpPost]
        public ActionResult Edit(ProvinceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Countries = new SelectList(ctx.Countries, "CountryId", "CountryName", model.CountryId);
                return View(model);
            }

            var province = ctx.Provinces.Find(model.ProvinceId);
            province.ProvinceName = model.ProvinceName;
            province.CountryId = model.CountryId;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var provincie = ctx.Provinces.Find(key);
            ViewBag.LocalitiesProvince = provincie.Localities.Select(x => x.LocalityName);
            return View(new ProvinceViewModel(provincie));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var provincie = ctx.Provinces.Find(key);
            return View(new ProvinceViewModel(provincie));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var provincie = ctx.Provinces.Find(key);

            if(provincie.Localities.Count != 0)
            {
                ModelState.AddModelError("", "Existen localidades que dependen de esta provincia, consulte los detalles para más información");
                return View(new ProvinceViewModel(provincie));
            }

            ctx.Provinces.Remove(provincie); 
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}