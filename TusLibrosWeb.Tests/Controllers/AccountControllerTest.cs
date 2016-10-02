using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibrosWeb.Controllers;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void GetLogin()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.Equals("Login",result.ViewName);
        }

        [TestMethod]
        public void PostLogin()
//                    public void ReturnsTempData()

        {
            // Arrange
            AccountController controller = new AccountController();
            UserViewModel userView = new UserViewModel {Password = "123", UserName = "gian pepe"};
            controller.Register(userView, "");

            // Act
            var result = controller.Login(userView, "") as ViewResult;

            Assert.AreEqual("ClientID", result.TempData["ClientId"]); 
            /*HomeController controllerUnderTest = new HomeController();
            var result = controllerUnderTest.Details("a1") as ViewResult;
            Assert.AreEqual("foo", result.TempData["Name"]);*/



            //ViewBag.ClientId = client.Id
            // Assert
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.IsNull(result.RouteValues["controller"]); // means we redirected to the same controller
        }

        /*

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
        */
    }
}
