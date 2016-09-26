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

        /*********************************************************************/
        //TODO: mover a donde corresponda. Porqe no sabemos donde puede ir.
        public static void RepeatAction(int repeatCount, Action action)
        {
            for (int i = 0; i < repeatCount; i++)
                action();
        }
    }
}
