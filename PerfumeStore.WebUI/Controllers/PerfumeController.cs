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

        public ViewResult List(string houseName,
                               string category, 
                               string gender, 
                               string concentration, 
                               string country, 
                               string volume, 
                               int page = 1)
        {
            PerfumeListViewModel model = new PerfumeListViewModel
            {
                Perfumes = repository.Perfumes
                    .Where(h => houseName == null || h.HouseName == houseName)
                    .Where(p => category == null || p.Category == category)
                    .Where(g => gender == null || g.Gender == gender)
                    .Where(c => concentration == null || c.Concentration == concentration)
                    .Where(ct => country == null || ct.Country == country)
                    .Where(v => volume == null || v.Volume == volume)
                    .OrderBy(perfume => perfume.PerfumeId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,

                    TotalItems = concentration == null ?
                                //(houseName == null || category == null || gender == null || concentration == null || country == null || volume == null) ?
                    repository.Perfumes.Count() :
                    repository.Perfumes
                                       //.Where(a => a.HouseName == houseName)
                                       //.Where(b => b.Category == category)
                                       //.Where(c => c.Gender == gender)
                                       .Where(d => d.Concentration == concentration)
                                       //.Where(f => f.Country == country)
                                       //.Where(g => g.Volume == volume)
                                       .Count()

                },
                CurrentHouseName = houseName,
                CurrentCategory = category,
                CurrentGender = gender,
                CurrentConcentration = concentration,
                CurrentCountry = country,
                CurrentVolume = volume
            };
            return View(model);
        }
    }
}