using TusLibros.db;
using TusLibros.model.entities;

namespace TusLibros.model

{
    class TusLibrosApp
    {
        public static void Main(string[] args)
        {
            using (var session = SessionManager.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var auto = new Cart();
                    auto.AddItemSomeTimes("abc",1);
                    auto.AddItemSomeTimes("dfg",1);

                    var otroAuto = new Cart();
                    otroAuto.AddItemSomeTimes("3454",1);
                    otroAuto.AddItemSomeTimes("678",1);
                    
                    session.SaveOrUpdate(auto);
                    session.SaveOrUpdate(otroAuto);

                    transaction.Commit();
                    session.Close();
                }
            }
        }
    }
}
