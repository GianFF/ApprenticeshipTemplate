using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TusLibros.app;
using TusLibros.app.environment;
using TusLibros.model.entities;

namespace TusLibros.db
{
    public static class ConfigurationMappingDataBase
    {
        public static string DataBaseConeccionString()
        {
            return GlobalConfiguration.ConnectionDataBaseString;
        }

        public static FluentConfiguration ConfigureDbMappingAndSchema()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(DataBaseConeccionString()))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Cart>())
                .ExposeConfiguration(BuildSchema);
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true); //TODO: Ver de usar execute con el ultimo parametro true para que dropee todo.
        }
    }
}
