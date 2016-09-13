using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using TusLibros.mappers;

namespace TusLibros
{
    class TusLibrosApp
    {
        public static void Main(string []args)
        {
            NHibernateConfiguration  nHibernateConfiguration = new NHibernateConfiguration();
            Configuration configuration = nHibernateConfiguration.Configuration();

            nHibernateConfiguration.Execute(configuration);
        }
    }
}
