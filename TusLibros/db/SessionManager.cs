using NHibernate;

namespace TusLibros.db
{
    public static class SessionManager
    {
        private static ISessionFactory _sessionFactory;

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        
        private static ISessionFactory CreateSessionFactory()
        {
            var configurationDbMappingAndSchema = ConfigurationMappingDataBase.ConfigureDbMappingAndSchema();

            return configurationDbMappingAndSchema.BuildSessionFactory();
        }

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }
    }
}