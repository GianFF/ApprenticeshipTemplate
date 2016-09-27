using System;
using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.app.environment
{
    public class DevelopmentEnvironment
    {
        public IPersistentStrategy PersistentStrategy;
        public IClock GlobalClock;
        public MerchantProcessor MerchantProcessor;

        public DevelopmentEnvironment(IPersistentStrategy aPersistentStrategy)
        {
            GlobalClock = new DevelopmentClock();
            MerchantProcessor = new MerchantProcessor();
            PersistentStrategy = aPersistentStrategy;
        }

        public IYourBooksApplication GetApplication()
        {
            return PersistentStrategy.GetApplication(this);
        }
    }
}
