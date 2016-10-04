using System;
using TusLibros.app;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using TusLibrosWeb.Controllers;
using TusLibros.app.environment;

namespace TusLibrosWeb
{
    public class TusLibrosControllerFactory : IControllerFactory
    {
        private DevelopmentEnvironment Environment;
        private IYourBooksApplication Application;

        public TusLibrosControllerFactory()
        {
            Environment = new DevelopmentEnvironment(new TransientDataBaseStrategy());
            Application = Environment.GetApplication();
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            Controller controller;

            if (controllerName == "Home")
                controller = new HomeController(Application);
            else
                controller = new AccountController(Application);

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}