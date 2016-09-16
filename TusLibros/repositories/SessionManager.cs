using NHibernate;
using NHibernate.Cfg;
using TusLibros.app;
using TusLibros.lib;

namespace TusLibros.repositories
{
    public static class SessionManager
    {
        private static ISessionFactory sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddAssembly(typeof(Cart).Assembly);
                    configuration.AddAssembly(typeof(UsersSession).Assembly);
                    sessionFactory = configuration.BuildSessionFactory();

                }
                return sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}