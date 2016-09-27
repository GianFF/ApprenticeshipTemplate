using TusLibros.model.entities;

namespace TusLibros.app.environment
{
    public interface IPersistentStrategy
    {
        IYourBooksApplication GetApplication(DevelopmentEnvironment developmentEnvironment);
    }
}