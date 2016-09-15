using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using TusLibros.lib;
using TusLibros.repositories;
using TusLibros.tests.support;

namespace TusLibros.tests.persistance
{
    [TestFixture]
    public class TestCartRepository
    {
        private ObjectProvider objectProvider;
        private Configuration configuration;
        private CartRepository cartRepository;
        private Cart cart;

        [OneTimeSetUp]
        public void SetUp()
        {
            configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Cart).Assembly);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
        }

        [SetUp]
        public void SetupContext()
        {
            objectProvider = new ObjectProvider();

            cart = objectProvider.EmptyCart();
            cartRepository = new CartRepository();

            new SchemaExport(configuration).Execute(false, true, false);
        }

        [Test]
        public void Test001CanAddNewCart()
        {
            cartRepository.Add(cart);
        }
    }
}