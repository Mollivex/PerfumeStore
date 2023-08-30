using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using PerfumeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfumeStore.WebUI.Controllers
{
    public class PerfumeController : Controller
    {
        private readonly IPerfumeRepository repository;
        public int pageSize = 4;
        public PerfumeController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult List(int page = 1)
        {
            PerfumeListViewModel model = new PerfumeListViewModel
            {
                Perfumes = repository.Perfumes
                    .OrderBy(perfume => perfume.PerfumeId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Perfumes.Count()
                }
            };
            return View(model);
        }
    }
}