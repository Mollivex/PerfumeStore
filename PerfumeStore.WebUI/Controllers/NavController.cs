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
    }
}