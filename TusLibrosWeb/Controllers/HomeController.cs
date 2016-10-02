using System.Web.Mvc;
using TusLibros.app.environment;
using TusLibros.app;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Controllers
{
    public class HomeController : Controller
    {
        private static DevelopmentEnvironment Environment = new DevelopmentEnvironment(new PersitentDataBaseStrategy());
        private static IYourBooksApplication Application = Environment.GetApplication();

        public ActionResult Index()
        {
            return View();
        }
    }
}