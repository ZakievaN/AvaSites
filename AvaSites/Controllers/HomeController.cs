using AvailabilitySites.Models;
using AvailabilitySites.Services.Interfaces;
using AvailabilitySites.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using AvailabilitySites.Data;
using AvailabilitySites.Models.Interfaces;

namespace AvailabilitySites.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISitesDataModel _sitesDataModel;

        public HomeController(ISitesDataModel model)
        {
            _sitesDataModel = model;
        }

        public IActionResult Refresh()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = _sitesDataModel.Get();

            List<SiteAvailableViewModel> sites = new();

            foreach (var item in list)
            {
                sites.Add(new SiteAvailableViewModel()
                {
                    PrimaryKey = item.Id,
                    Name = item.Name,
                    Url = item.Url,
                    Interval = item.Interval,
                    IsAvailable = item.IsAvailable
                }); 
            }

            return View(sites);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Site site = _sitesDataModel.Get(id);

            if (site == null)
            {
                return NotFound();
            }

            return View(new SiteViewModel()
            {
                PrimaryKey = site.Id,
                Name = site.Name,
                Url = site.Url,
                Interval = site.Interval
            });
        }

        [HttpPost]
        public IActionResult Edit(SiteViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            Site site = new Site
            {
                Id = model.PrimaryKey,
                Name = model.Name,
                Url = model.Url,
                Interval = model.Interval
            };

            _sitesDataModel.Update(site);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            Site site = _sitesDataModel.Get(id);

            if (site == null)
            {
                return NotFound();
            }

            return View(new SiteViewModel()
            {
                PrimaryKey = site.Id,
                Name = site.Name,
                Url = site.Url,
                Interval = site.Interval
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var site = _sitesDataModel.Get(id);

            if (site is null)
            {
                return NotFound();
            }

            return View(new SiteViewModel()
            {
                PrimaryKey = site.Id,
                Name = site.Name,
                Url = site.Url,
                Interval = site.Interval
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _sitesDataModel.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new SiteViewModel());
        }

        [HttpPost]
        public IActionResult Create(SiteViewModel model)
        {
            _sitesDataModel.Add(new Site
            {
                Id = model.PrimaryKey,
                Name = model.Name,
                Url = model.Url,
                Interval = model.Interval
            });

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}