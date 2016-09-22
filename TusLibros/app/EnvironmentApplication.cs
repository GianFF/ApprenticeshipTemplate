using System;
using System.Collections.Generic;
using System.Linq;
using TusLibros.clocks;

namespace TusLibros.app
{
     public abstract class EnvironmentApplication
    {
        public static IYourBooksApplication GetEnvironment()
        {
            
            var yourBooksApplications = new List<IYourBooksApplication>();
            yourBooksApplications.Add(new PersistentYourBooksApplication(GlobalConfiguration.GlobalClock));
            yourBooksApplications.Add(new TransientYourBooksApplication(GlobalConfiguration.GlobalClock));

            return yourBooksApplications.First(aClass => aClass.CanHandle(GlobalConfiguration.Environment));

        }
    }
}
