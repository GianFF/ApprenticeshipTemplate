using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.app;
using TusLibros.app.environment;
using TusLibrosWeb.Controllers;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private DevelopmentEnvironment Environment;
        private IYourBooksApplication Application;
        private AccountController Controller;

        [TestInitialize]
        public void SetUp()
        {
            Environment = new DevelopmentEnvironment(new TransientDataBaseStrategy());
            Application = Environment.GetApplication();
            Controller = new AccountController(Application);
        }

        [TestMethod]
        public void ReturnLoginView()
        {
            ViewResult result = Controller.Login() as ViewResult;

//            Assert.AreEqual("Login",result.ViewName);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginReturnsARedirectToIndexHome()
        {
            UserViewModel userView = new UserViewModel {Password = "123", UserName = "gian pepe"};
            Controller.Register(userView, "");
            
            var result = Controller.Login(userView, "") as RedirectToRouteResult;
            
            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
            Assert.IsTrue(result.RouteValues.ContainsValue("Home"));
        }

        [TestMethod]
        public void ReturnRegisterView()
        {
            ViewResult result = Controller.Register() as ViewResult;

            //Assert.AreEqual("Register", result.ViewName);
            Assert.IsNotNull(result);
        }

        /*//Get register page
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
        }*/
    }
}
