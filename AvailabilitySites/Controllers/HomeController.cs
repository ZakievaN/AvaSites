using AvailabilitySites.Models;
using AvailabilitySites.Services.Interfaces;
using AvailabilitySites.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace AvailabilitySites.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ISitesDataModel _sitesDataModel;

        private ICheckAvailabilityInThread _checkAvailability;

        public HomeController(ILogger<HomeController> logger, ISitesDataModel model, ICheckAvailabilityInThread checkAvailability)
        {
            _logger = logger;
            _sitesDataModel = model;
            _checkAvailability = checkAvailability;
            _sitesDataModel.PropertyChanged += Chatter_SitesDataModelChanged;
            _checkAvailability.Check();
        }

        public IActionResult Refresh()
        {
            return RedirectToAction("Index");
        }

        private void Chatter_SitesDataModelChanged(object sender, PropertyChangedEventArgs e)
        {
            _checkAvailability.Check();
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
                    PrimaryKey = item.PrimaryKey,
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
                return NotFound();

            return View(new SiteViewModel()
            {
                PrimaryKey = site.PrimaryKey,
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

            Site site = new(model.PrimaryKey, model.Name, model.Url, model.Interval);

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
                PrimaryKey = site.PrimaryKey,
                Name = site.Name,
                Url = site.Url,
                Interval = site.Interval
            });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();

            var site = _sitesDataModel.Get(id);

            if (site is null)
            {
                return NotFound();
            }

            return View(new SiteViewModel()
            {
                PrimaryKey = site.PrimaryKey,
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
            _sitesDataModel.Add(new (model.PrimaryKey, model.Name, model.Url, model.Interval));
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
