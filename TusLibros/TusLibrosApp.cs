using System;
using TusLibros.model;
using TusLibros.repositories;

namespace TusLibros
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

                    var otroAuto = new Cart();
                    otroAuto.AddItem("3454");

                    //session.SaveOrUpdate(auto);
                    session.SaveOrUpdate(otroAuto);
                    
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

                Console.ReadKey();
            }

            
        }
    }
}
