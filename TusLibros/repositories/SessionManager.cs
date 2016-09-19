using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

using TusLibros.model.Entitys;

namespace TusLibros.repositories
{
    public static class SessionManager
    {
      
        public static ISession OpenSession()
        {
            return null;//ISessionFactory.OpenSession();
        }
        
        public static ISessionFactory BuildSessionFactory()
        {
            var coneccion = "Server=localhost;Database=tuslibros;User ID=root;Password=root;";

            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(coneccion))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Cart>())
                //.ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }
   
        private static void BuildSchema(Configuration config)
        {            
            new SchemaExport(config).Execute(false, true, false);
        }

    }
}