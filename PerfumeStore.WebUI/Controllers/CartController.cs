using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using PerfumeStore.WebUI.Models;

namespace PerfumeStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IPerfumeRepository repository;
        public CartController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int perfumeId, string returnUrl)
        {
            Perfume perfume = repository.Perfumes
                .FirstOrDefault(g => g.PerfumeId == perfumeId);

             if( perfume != null)
            {
                cart.AddItem(perfume, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int perfumeId, string returnUrl)
        {
            Perfume perfume = repository.Perfumes
                .FirstOrDefault(g => g.PerfumeId == perfumeId);

            if (perfume != null)
            {
                cart.RemoveLines(perfume);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
    }
}