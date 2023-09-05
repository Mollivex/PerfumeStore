﻿using System.Web.Mvc;
using PerfumeStore.WebUI.Infrastructure.Abstract;
using PerfumeStore.WebUI.Models;

namespace PerfumeStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController (IAuthProvider auth)
        {
            this.authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }

                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}