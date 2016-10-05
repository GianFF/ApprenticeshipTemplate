using System;
using System.Net;
using System.Web.Mvc;
using TusLibros.app;
using TusLibros.model.entities;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Controllers
{
    public class AccountController : Controller
    {
        private IYourBooksApplication Application;

        public AccountController(IYourBooksApplication anApplication)
        {
            Application = anApplication;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel model, string returnUrl)
        {
            Client client; //TODO: LOGIN NO DEBERIA DEVOLVER UN CLIENT, SINO UN GUID

            try
            {
                client = Application.Login(model.UserName, model.Password);
            }
            catch (ArgumentException exception)
            {
                TempData["ErrorMessage"] = exception.Message;

                return View("Login");
            }

            TempData["ClientId"] = client.Id;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model, string returnUrl)
        {
            try
            {
                Application.RegisterClient(model.UserName, model.Password);
            }
            catch (ArgumentException exception)
            {
                TempData["ErrorMessage"] = exception.Message;

                return View("Register");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}