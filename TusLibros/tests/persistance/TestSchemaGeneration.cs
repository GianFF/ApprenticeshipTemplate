using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TusLibros.lib;
using NUnit.Framework;

namespace TusLibros.tests.persistance
{
    [TestFixture]
    class TestSchemaGeneration
    {
        [Test] 
        public void Test001CanGenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Cart).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}