using System.Web.Mvc;
using System.Web.Routing;

namespace TusLibrosWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterTusLibrosControllerFactory();
        }

        // Register TusLibrosControllerFactory() to MVC framework.
        private void RegisterTusLibrosControllerFactory()
        {
            IControllerFactory factory = new TusLibrosControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}
