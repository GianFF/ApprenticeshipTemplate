using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using TusLibrosWeb.Controllers;
using TusLibrosWeb.Models;

namespace TusLibrosWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        //TODO: refactorear los tests para no romper encapsulamiento ni hacer test implementativos.

        private TestObjectProvider TestObjectProvider;

        [TestInitialize]
        public void SetUp()
        {
            TestObjectProvider = new TestObjectProvider();
        }

        [TestMethod]
        public void AnUserCanBeRegisteredWithAnUserNameAndAPasswordIfThereIsNotAnotherUserWithThatName()
        {
            AccountController controller = TestObjectProvider.GetAccountController();
            UserViewModel userView = new UserViewModel { Password = "123", UserName = "pepe" };


            var response = controller.Register(userView, "");

            response.AssertActionRedirect().ToAction("Login");

            controller.Login(userView, "");
            Assert.IsTrue(controller.TempData.ContainsKey("ClientId"));
        }

        [TestMethod]
        public void AnUserCanNotBeRegisteredWithAnUserNameAndAPasswordIfThereIsAnotherUserWithThatName()
        {
            AccountController controller = TestObjectProvider.GetAccountController();
            UserViewModel userView = new UserViewModel { Password = "123", UserName = "pepe" };

            controller.Register(userView, "");
            var response = controller.Register(userView, "");

            Assert.AreEqual("User already registered", controller.TempData["ErrorMessage"]);
            response.AssertViewRendered().ForView("Register");
        }

        [TestMethod]
        public void AnUserCanLoginWithAUserNameAndAPasswordIfItIsRegistered()
        {
            AccountController controller = TestObjectProvider.GetAccountController();
            UserViewModel userView = new UserViewModel {Password = "123", UserName = "pepe"};

            controller.Register(userView, "");
            var result = controller.Login(userView, "") as RedirectToRouteResult;

            Assert.IsTrue(controller.TempData.ContainsKey("ClientId"));
            result.AssertActionRedirect().ToAction("Index");
        }

        [TestMethod]
        public void AnUserCanNotLoginWithAUserNameAndAPasswordIfItIsNotRegistered()
        {
            AccountController controller = TestObjectProvider.GetAccountController();
            UserViewModel userView = new UserViewModel { Password = "123", UserName = "pepe" };

            var result = controller.Login(userView, "");

            Assert.AreEqual("Invalid user or password", controller.TempData["ErrorMessage"]);
            result.AssertViewRendered().ForView("Login");
        }
    }
}
