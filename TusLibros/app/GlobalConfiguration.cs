using TusLibros.clocks;

namespace TusLibros.app
{
    public static class GlobalConfiguration
    {
        public const string Environment = GlobalDevelopmentEnvironment;
        public const string GlobalDevelopmentEnvironment = "Development";
        public const string GlobalProductionEnvironment = "Production";

        public static IClock GlobalClock = new DevelopmentClock();
    }
}
