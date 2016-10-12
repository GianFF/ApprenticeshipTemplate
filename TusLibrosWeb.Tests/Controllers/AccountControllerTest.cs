using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using TusLibrosWeb.Controllers;
using TusLibrosWeb.Models;
using Westwind.Web.Mvc;

namespace TusLibrosWeb.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private TestObjectProvider TestObjectProvider;
        private AccountController controller;

        [TestInitialize]
        public void SetUp()
        {
            TestObjectProvider = new TestObjectProvider();
            controller = TestObjectProvider.GetAccountController();
        }

        [TestMethod]
        public void AnUserCanBeRegisteredWithAnUserNameAndAPasswordIfThereIsNotAnotherUserWithThatUserName()
        {
            var response = controller.Register(RegistrationFormForPepe(), "");

            response.AssertActionRedirect().ToAction("Login");
        }

        private static UserViewModel RegistrationFormForPepe()
        {
            UserViewModel userView = new UserViewModel {Password = "123", UserName = "pepe"};
            return userView;
        }

        [TestMethod]
        public void AnUserCanNotBeRegisteredWithAnUserNameAndAPasswordIfThereIsAnotherUserWithThatName()
        {
            var formForPepe = RegistrationFormForPepe();
            controller.Register(formForPepe, "");

            var response = controller.Register(formForPepe, "");

            Assert.AreEqual("User already registered", controller.UserAlreadyRegisteredMessage());
            response.AssertViewRendered().ForView("Register");
        }

        [TestMethod]
        public void AnUserCanLoginWithAUserNameAndAPasswordIfItIsRegistered()
        {
            UserViewModel userView = new UserViewModel {Password = "123", UserName = "pepe"};//TODO: usar RegistrationFormForPepe
            controller.Register(userView, "");

            var result = controller.Login(userView, "") as RedirectToRouteResult;

            Assert.IsTrue(controller.IsLogged(userView.UserName));
            result.AssertActionRedirect().ToAction("Index");
        }

        [TestMethod]
        public void AnUserCanNotLoginWithAUserNameAndAPasswordIfItIsNotRegistered()
        {
            UserViewModel userView = new UserViewModel { Password = "123", UserName = "pepe" };

            var result = controller.Login(userView, "");

            Assert.IsFalse(controller.IsLogged(userView.UserName));
            Assert.AreEqual("Invalid user or password", controller.TempData["ErrorMessage"]);
            result.AssertViewRendered().ForView("Login");
        }

        [TestMethod]
        public void Xxxxx()
        {
            UserViewModel userView = new UserViewModel { Password = "123", UserName = "pepe" };
            controller.Register(userView, "");

            controller.Login(userView, "");

            Assert.IsFalse(controller.IsLogged("cacho"));
        }
    }
}