using System.Web.Mvc;
using PerfumeStore.Domain.Entities;

namespace PerfumeStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            // Get a Cart object from the session
            Cart cart = null;
            if (controllerContext.HttpContext != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            // Create a Cart object, if it isn't found in the session
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            // Return a Cart object
            return cart;
        }
    }
}