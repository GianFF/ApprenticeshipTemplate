using System;
using System.Net.Mime;
using TusLibros.app;
using TusLibros.app.environment;
using TusLibrosWeb.Controllers;

namespace TusLibrosWeb.Tests
{
    class TestObjectProvider
    {
        public TestObjectProvider() { }

        public IYourBooksApplication ProvideTransientAplpliApplication()
        {
            DevelopmentEnvironment environment = new DevelopmentEnvironment(new TransientDataBaseStrategy());

            return environment.GetApplication();
        }
    }
}
