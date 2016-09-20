using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TusLibros.model.entities;

namespace TusLibros.db
{
    public static class ConfigurationMappingDataBase
    {
        public static string DataBaseConeccionString()
        {
            return "Server=localhost;Database=tuslibros;User ID=root;Password=root;";
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
            new SchemaExport(config).Create(false, true);
        }
    }
}
