namespace TusLibros.app.environment
{
    public class PersitentDataBaseStrategy : IPersistentStrategy
    {
        public IYourBooksApplication GetApplication(DevelopmentEnvironment developmentEnvironment)
        {
            return new PersistentYourBooksApplication(developmentEnvironment.Clock);
        }
    }
}