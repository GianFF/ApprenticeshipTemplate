using TusLibros.model.entities;

namespace TusLibros.app.environment
{
    public class PersitentDataBaseStrategy : IPersistentStrategy
    {
        public const string ServerDataBaseGlobal = "localhost";
        public const string DataBaseGlobal = "tuslibros";
        public const string UserDataBaseGlobal = "root";
        public const string PasswordDataBaseGlobal = "root";
        public string ConnectionDataBaseString { get; set; }

        public PersitentDataBaseStrategy()
        {
            ConnectionDataBaseString =
                "Server=" + ServerDataBaseGlobal + ";" +
                "Database=" + DataBaseGlobal + ";" +
                "User ID=" + UserDataBaseGlobal + ";" +
                "Password=" + PasswordDataBaseGlobal + ";";
        }

        public IYourBooksApplication GetApplication(DevelopmentEnvironment enviroment)
        {
            return new PersistentYourBooksApplication(enviroment.GlobalClock, enviroment.MerchantProcessor);
        }
    }
}