using TusLibros.model.entities;

namespace TusLibros.app.environment
{
    public class TransientDataBaseStrategy : IPersistentStrategy
    {
        public IYourBooksApplication GetApplication(DevelopmentEnvironment developmentEnvironment)
        {
            return new TransientYourBooksApplication(developmentEnvironment.GlobalClock, developmentEnvironment.MerchantProcessor);
        }
    }
}