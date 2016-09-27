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
        public ActionResult Index()
        {
            return View();
        }

        // Post: login user
        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            Application.RegisterClient(model.UserName, model.Password);
            return View();
        }
    }
}