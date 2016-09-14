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
        //TODO: esto no anda. no se porque no se puede hacer correr el test para ver si se genera bien es schema.
        public void Aaaaaa()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Cart).Assembly);

            new SchemaExport(cfg).Execute(false, true, false);
        }
    }
}