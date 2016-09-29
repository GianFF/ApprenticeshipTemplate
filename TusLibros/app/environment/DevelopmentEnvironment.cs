using TusLibros.clocks;

namespace TusLibros.app.environment
{
    public class DevelopmentEnvironment
    {
        public IPersistentStrategy PersistentStrategy;
        public IClock Clock;

        public DevelopmentEnvironment(IPersistentStrategy aPersistentStrategy)
        {
            PersistentStrategy = aPersistentStrategy;
            Clock = new DevelopmentClock();
        }

        public IYourBooksApplication GetApplication()
        {
            return PersistentStrategy.GetApplication(this);
        }
    }
}
