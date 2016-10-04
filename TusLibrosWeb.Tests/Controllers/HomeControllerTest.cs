using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TusLibros.app;
using TusLibros.app.environment;
using TusLibrosWeb.Controllers;

namespace TusLibrosWeb.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private DevelopmentEnvironment Environment;
        private IYourBooksApplication Application;
        private HomeController Controller;

        [TestInitialize]
        public void SetUp()
        {
            Environment = new DevelopmentEnvironment(new TransientDataBaseStrategy());
            Application = Environment.GetApplication();
            Controller = new HomeController(Application);
        }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = Controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
