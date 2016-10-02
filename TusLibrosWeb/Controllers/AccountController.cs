using System.Web.Mvc;
using TusLibros.app;
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
        //Get login page
        public ActionResult Login()
        {
            return View();
        }

        // Post: login user
        [HttpPost]
        public ActionResult Login(UserViewModel model, string returnUrl)
        {
            var client = Application.Login(model.UserName, model.Password);

            //ViewBag.ClientId = client.Id; //TODO usar ViewBag o usar UserViewModel?
            TempData["ClientId"] = client.Id;

            return RedirectToAction("Index", "Home");
        }

        //Get register page
        public ActionResult Register()
        {
            return View();
        }

        // Post: register user
        [HttpPost]
        public ActionResult Register(UserViewModel model, string returnUrl)
        {
            Application.RegisterClient(model.UserName, model.Password);

            return RedirectToAction("Login", "Account");
        }
    }
}