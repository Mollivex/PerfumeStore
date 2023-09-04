using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfumeStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        readonly IPerfumeRepository repository;

        public AdminController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Perfumes);
        }

        public ViewResult Edit(int perfumeId)
        {
            Perfume perfume = repository.Perfumes
                .FirstOrDefault(g => g.PerfumeId == perfumeId);
            return View(perfume);
        }

        // Overloaded Edit() method version for saving changes
        [HttpPost]
        public ActionResult Edit(Perfume perfume)
        {
            if (ModelState.IsValid)
            {
                repository.SavePerfume(perfume);
                TempData["message"] = string.Format("Changes in perfume \"{0} {1}\" was saved", perfume.HouseName, perfume.PerfumeName);
                return RedirectToAction("Index");
            }
            else
            {
                // Something wrong with data values
                return View(perfume);
            }
        }
    }
}