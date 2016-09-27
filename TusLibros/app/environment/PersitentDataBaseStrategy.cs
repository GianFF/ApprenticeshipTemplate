using TusLibros.model.entities;

namespace TusLibros.app.environment
{
    public class PersitentDataBaseStrategy : IPersistentStrategy
    {
        public IYourBooksApplication GetApplication(DevelopmentEnvironment enviroment)
        {
            return new PersistentYourBooksApplication(enviroment.GlobalClock, enviroment.MerchantProcessor);
        }
    }
}