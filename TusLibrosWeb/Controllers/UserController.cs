using System.Web.Mvc;
using TusLibros.app.environment;
using TusLibros.app;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Controllers
{
    public class UserController : Controller
    {
        private static DevelopmentEnvironment Environment = new DevelopmentEnvironment(new PersitentDataBaseStrategy());
        private static IYourBooksApplication Application = Environment.GetApplication();

        //Get login page
        public ActionResult Login()
        {
            return View();
        }

        // Post: login user
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            Application.RegisterClient(model.UserName, model.Password);
            return View();
        }
    }
}