using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TusLibros.lib;

namespace TusLibros.tests.persistance
{
    [TestClass]
    class TestSchemaGeneration
    {
        [TestMethod] 
        public void Test001CanGenerateSchema()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Cart).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}