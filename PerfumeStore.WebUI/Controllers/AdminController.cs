using PerfumeStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfumeStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IPerfumeRepository repository;

        public AdminController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Perfumes);
        }
    }
}