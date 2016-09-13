using NHibernate.Cfg;
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
