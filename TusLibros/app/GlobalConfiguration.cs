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


        public const string ServerDataBaseGlobal = "localhost";
        public const string DataBaseGlobal = "tuslibros";
        public const string UserDataBaseGlobal = "root";
        public const string PasswordDataBaseGlobal = "root";

        public const string ConnectionDataBaseString =
            "Server=" + ServerDataBaseGlobal + ";" +
            "Database=" + DataBaseGlobal + ";" +
            "User ID=" + UserDataBaseGlobal + ";" +
            "Password=" + PasswordDataBaseGlobal + ";";
    }
}
