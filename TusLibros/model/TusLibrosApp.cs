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
                    auto.AddItem("abc");
                    auto.AddItem("dfg");

                    var otroAuto = new Cart();
                    otroAuto.AddItem("3454");
                    otroAuto.AddItem("678");
                    
                    session.SaveOrUpdate(auto);
                    session.SaveOrUpdate(otroAuto);

                    transaction.Commit();
                    session.Close();
                }
            }
        }
    }
}
