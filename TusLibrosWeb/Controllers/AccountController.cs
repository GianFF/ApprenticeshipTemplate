using System.Web.Mvc;
using TusLibros.app;
using TusLibros.model.entities;
using TusLibros.model.exceptions;
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

            try //TODO: ver si no hay una mejor practica para cachear excepciones del servidor. (Que se quieran mostrar en la vista)
            {
                client = Application.Login(model.UserName, model.Password);
            }
            catch (LoginException exception)
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
            catch (RegisterException exception)
            {
                TempData["ErrorMessage"] = exception.Message;

                return View("Register");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}