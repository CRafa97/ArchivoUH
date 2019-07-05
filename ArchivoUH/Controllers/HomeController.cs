using ArchivoUH.Contexts;
using ArchivoUH.Models;
using ArchivoUH.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArchivoUH.Controllers
{
    [CustomAuthorize(admin:false)]
    public class HomeController : BaseController
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public ActionResult Index(string faculty)
        {
            //Collect totals
            int grad = ctx.Graduates.Count();
            int lev = ctx.Leaves.Count();
            int adms = ctx.Administratives.Count();
            int total = grad + lev + adms;

            //Collect graduates per years (6 years)
            var grad_per_years = ctx.Graduates.GroupBy(x => x.FinishTime.Year).OrderByDescending(g => g.Key).Take(6).ToList();
            var leav_per_years = ctx.Leaves.GroupBy(x => x.LeavedDate.Year).OrderByDescending(g => g.Key).Take(6).ToList();

            ViewBag.Grad = grad;
            ViewBag.Lev = lev;
            ViewBag.Adms = adms;
            ViewBag.Total = total;

            BarChart bar = new BarChart() {
                Labels = grad_per_years.Select(x => $"\"{x.Key}\"").Reverse().ToList(),
                CanvasName = "grad_per_year",
                Legend = true,
                Label1 = "Graduados",
                Data1 = grad_per_years.Select(x => x.Count()).Reverse().ToList(),
                Label2 = "Bajas",
                Data2 = leav_per_years.Select(x => x.Count()).Reverse().ToList()
            };

            if(grad_per_years.Count != 0 && leav_per_years.Count != 0)
            {
                bar.MaxTick = Math.Max(bar.Data1.Max(), bar.Data2.Max()) + 5;
                bar.MaxTick -= bar.MaxTick % 5;
            }
            else
            {
                ViewBag.ValidGraphics = false;
                ViewBag.SelectedFaculty = faculty;
                ViewBag.FacNames = ctx.Faculties.Select(x => x.FacultyName).ToList();
                return View(new IndexCharts()
                {
                    BarChart = new BarChart() { CanvasName = "canvas1" },
                    Comparison = new AreaChart() { CanvasName = "canvas2" },
                    FacChart = new PieChart() { CanvasName = "canvas3" },
                    PieChart = new PieChart() { CanvasName = "canvas4" }
                });
            }

            //6 carrers with more graduates
            var most_carrers = grad_per_years.First().GroupBy(g => g.Course.CourseName).OrderByDescending(gr => gr.Count()).Take(6).ToList();
            PieChart pie = new PieChart()
            {
                Labels = most_carrers.Select(x => $"\"{x.Key}\""),
                CanvasName = "most_carrers",
                Legend = true,
                BkColor = new[] { "#4e73df", "#1cc88a", "#36b9cc", "#2f4f4f", "#483d8b", "#e9967a" },
                HoverBkColor = new[] { "#4e73df", "#1cc88a", "#36b9cc", "#2f4f4f", "#483d8b", "#e9967a" },
                Data = most_carrers.Select(x => x.Count())
            };

            //faculties leaves
            var fac_leaves = ctx.Leaves.GroupBy(x => x.LeavedDate.Year).OrderBy(l => l.Key).ToList().Last().GroupBy(s => s.Faculty.FacultyName);
            PieChart fac_pie = new PieChart()
            {
                Labels = fac_leaves.Select(x => $"\"{x.Key}\""),
                CanvasName = "fac_leaves",
                Legend = true,
                BkColor = new[] { "#ff6347", "#4682b4", "#708090", "#008080", "#a0522d", "#fa8072" },
                HoverBkColor = new[] { "#ff6347", "#4682b4", "#708090", "#008080", "#a0522d", "#fa8072" },
                Data = fac_leaves.Select(x => x.Count())
            };

            faculty = faculty ?? ctx.Faculties.First().FacultyName;

            var model = new IndexCharts()
            {
                BarChart = bar,
                PieChart = pie,
                FacChart = fac_pie,
                Comparison = FacultyAreaGraph(faculty)
            };

            ViewBag.ValidGraphics = true;
            ViewBag.SelectedFaculty = faculty;
            ViewBag.FacNames = ctx.Faculties.Select(x => x.FacultyName).ToList();
            return View(model);
        }

        public AreaChart FacultyAreaGraph(string faculty)
        {
            var grad = ctx.Graduates.Where(x => x.Faculty.FacultyName == faculty)
                                    .GroupBy(g => g.FinishTime.Year)
                                    .OrderByDescending(f => f.Key).ToList()
                                    .Take(6).ToList();

            var leaves = ctx.Leaves.Where(x => x.Faculty.FacultyName == faculty)
                                   .GroupBy(g => g.LeavedDate.Year)
                                   .OrderByDescending(f => f.Key).ToList()
                                   .Take(6).ToList();

            var area_chart = new AreaChart()
            {
                Labels = grad.Select(x => $"\"{x.Key}\"").Reverse().ToList(),
                Data1 = grad.Select(x => x.Count()).Reverse().ToList(),
                Data2 = leaves.Select(x => x.Count()).Reverse().ToList(),
                CanvasName = "comparison",
                Legend = true
            };

            return area_chart;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}