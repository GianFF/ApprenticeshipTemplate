using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using TusLibros.lib;
using TusLibros.lib.repositories;
using TusLibros.tests.support;

namespace TusLibros.tests.persistance
{
    [TestClass]
    public class TestCartRepository
    {
        private ISessionFactory sessionFactory;
        private Configuration configuration;

        protected Cart cart;
        private string aBook;
        private string otherBook;
        private ObjectProvider objectProvider;
        private CartRepository cartRepository;

        [TestInitialize]
        public void SetUp()
        {
            //configuration = dataBase.Configuration();
            configuration.AddAssembly(typeof(Cart).Assembly);
            sessionFactory = configuration.BuildSessionFactory();

            new SchemaExport(configuration).Execute(false, true, false);

            cartRepository = new CartRepository();
            objectProvider = new ObjectProvider();

            cart = objectProvider.EmptyCart();
            aBook = objectProvider.ABook();
            otherBook = objectProvider.OtherBook();
        }

        [TestMethod]
        public void Test01CanAddAnEmptyCart()
        {
            cartRepository.Add(cart);
        }
    }
}



