using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfumeStore.WebUI.Controllers
{
    public class PerfumeController : Controller
    {
        private  IPerfumeRepository repository;
        public PerfumeController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult List()
        {
            return View(repository.Perfumes);
        }
    }
}