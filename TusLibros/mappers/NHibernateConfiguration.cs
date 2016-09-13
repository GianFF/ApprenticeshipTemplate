using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using TusLibros.mappers;

namespace TusLibros.mappers
{
    class NHibernateConfiguration
    {
        private string connectionString;

        public NHibernateConfiguration()
        {
            connectionString = "Server = localhost; Database = tuslibros; User Id = root;Password = root;";
        }

        public Configuration Configuration()
        {
            var configuration = new Configuration();

            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionProvider<DriverConnectionProvider>();
                db.Dialect<MySQLDialect>();
                db.Driver<MySqlDataDriver>();
                db.ConnectionString = "Server = localhost; Database = tuslibros; User Id = root;Password = root;";
                db.BatchSize = 30;
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.Timeout = 10;
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
            });

            var mapper = new ConventionModelMapper();
            var mappings = new[]
                {
                    typeof(CartMap)
                };

            mapper.AddMappings(mappings);

            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            return configuration; 
        }

        public void Execute(Configuration configuration)
        {
            new SchemaExport(configuration).Execute(false, true, false);
        }

    }
}
