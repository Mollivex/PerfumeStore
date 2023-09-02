using PerfumeStore.Domain.Abstract;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace PerfumeStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IPerfumeRepository repository;
        public NavController(IPerfumeRepository repo)
        {
            this.repository = repo;    
        }
        public PartialViewResult MenuConcentration(string concentration = null)
        {
            ViewBag.SelectedCategory = concentration;

            IEnumerable<string> categories = repository.Perfumes
                .Select(perfume => perfume.Concentration)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }           
        public PartialViewResult MenuGender(string gender = null)
        {
            ViewBag.SelectedCategory = gender;

            IEnumerable<string> categories = repository.Perfumes
                .Select(perfume => perfume.Gender)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }            
        public PartialViewResult MenuCategory(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Perfumes
                .Select(perfume => perfume.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }      
        public PartialViewResult MenuCountry(string country = null)
        {
            ViewBag.SelectedCategory = country;

            IEnumerable<string> categories = repository.Perfumes
                .Select(perfume => perfume.Country)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }        
    }
}