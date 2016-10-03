using System.Web.Mvc;
using TusLibros.app.environment;
using TusLibros.app;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Controllers
{
    public class HomeController : Controller
    {
        private IYourBooksApplication Application;

        public HomeController(IYourBooksApplication anApplication)
        {
            Application = anApplication;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}