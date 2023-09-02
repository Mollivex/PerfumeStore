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
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        private readonly IPerfumeRepository repository;
        public CartController(IPerfumeRepository repo)
        {
            this.repository = repo;
        }

        public RedirectToRouteResult AddToCart(int perfumeId, string returnUrl)
        {
            Perfume perfume = repository.Perfumes
                .FirstOrDefault(g => g.PerfumeId == perfumeId);

            if( perfume != null)
            {
                GetCart().AddItem(perfume, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int perfumeId, string returnUrl)
        {
            Perfume perfume = repository.Perfumes
                .FirstOrDefault(g => g.PerfumeId == perfumeId);

            if (perfume != null)
            {
                GetCart().RemoveLines(perfume);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}