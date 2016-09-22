using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.app
{
    public static class GlobalConfiguration
    {
        public const string Environment = GlobalProductionEnvironment;
        public const string GlobalDevelopmentEnvironment = "Development";
        public const string GlobalProductionEnvironment = "Production";

        public static IClock GlobalClock = new DevelopmentClock();
        public static MerchantProcessor MerchantProcessor = new MerchantProcessor();
    }
}
