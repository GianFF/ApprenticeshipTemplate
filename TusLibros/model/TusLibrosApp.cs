using System;
using TusLibros.model.Entitys;
using TusLibros.repositories;

namespace TusLibros.model

{
    class TusLibrosApp
    {
        public static void Main(string[] args)
        {
            var sessionFactory = SessionManager.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var auto = new Cart();
                    auto.AddItem("abc");
                    auto.AddItem("dfg");

                    var otroAuto = new Cart();
                    otroAuto.AddItem("3454");
                    otroAuto.AddItem("678");

                    var merchant = new MerchantProcessor();
                    var cashier = new Cashier(merchant);
                    var creditC = new CreditCard(DateTime.Now);

                    cashier.CheckoutFor(creditC,auto);
                    cashier.CheckoutFor(creditC, otroAuto);

                    session.SaveOrUpdate(auto);
                    session.SaveOrUpdate(cashier);
                    //session.SaveOrUpdate(otroAuto);
                    
                    transaction.Commit();

                }

                using (session.BeginTransaction())
                {

                    //Get post with id == 2
                    int id_cart = 2;
                    Cart p1 = session.Get<Cart>(id_cart);
                    Console.WriteLine(p1.Id);
                    foreach (var libro in p1.Items)
                    {
                        Console.WriteLine("  ,  " + libro);
                    }
                }

                
            }

            
        }
    }
}
