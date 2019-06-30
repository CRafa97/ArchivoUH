using ArchivoUH.Contexts;
using ArchivoUH.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArchivoUH.Models;
using System.Web.Mvc;
using ArchivoUH.Validations;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin:true)]
    public class CountriesController : BaseController
    {
        ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Countries
        public ActionResult Index()
        {
            var headers = new string[] { "Key", "Nombre" };

            var rows = (from country in ctx.Countries.ToList()
                        select new
                        {
                            Key = country.CountryId,
                            Nombre = country.CountryName
                        });

            CountryViewModel model = new CountryViewModel()
            {
                IndexTable = new TableViewModel("Paises", headers, rows)
            };

            return View(model);
        }

        public ActionResult Create(CountryViewModel model)
        {
            if (ctx.Countries.Select(x => x.CountryName).Contains(model.CountryName))
            {
                ModelState.AddModelError("", "Este pais ya existe en el sistema");
            }

            if (!ModelState.IsValid)
            {
                var headers = new string[] { "Key", "Nombre" };

                var rows = (from c in ctx.Countries.ToList()
                            select new
                            {
                                Key = c.CountryId,
                                Nombre = c.CountryName
                            });

                model.IndexTable = new TableViewModel("Paises", headers, rows);

                return View("Index", model);
            }

            var country = new Country()
            {
                CountryName = model.CountryName
            };

            ctx.Countries.Add(country);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(object id)
        {
            int key = int.Parse((string)id);
            var country = ctx.Countries.Find(key);
            return View(new CountryViewModel(country));
        }

        [HttpPost]
        public ActionResult Edit(CountryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var country = ctx.Countries.Find(model.CountryId);
            country.CountryName = model.CountryName;
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(object id)
        {
            int key = int.Parse((string)id);
            var country = ctx.Countries.Find(key);
            ViewBag.CountryProvincies = country.Provinces.Select(x => x.ProvinceName);
            return View(new CountryViewModel(country));
        }

        public ActionResult Delete(object id)
        {
            int key = int.Parse((string)id);
            var country = ctx.Countries.Find(key);
            return View(new CountryViewModel(country));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(object id)
        {
            int key = int.Parse((string)id);
            var country = ctx.Countries.Find(key);
            
            if (country.Provinces.Count != 0)
            {
                ModelState.AddModelError("", "Existen provincias que dependen de este pais, consulte los detalles del mismo para mas informacion");
                return View(new CountryViewModel(country));
            }

            ctx.Countries.Remove(country);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}