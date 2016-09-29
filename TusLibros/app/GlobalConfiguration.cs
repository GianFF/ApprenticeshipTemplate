using System;
using TusLibros.clocks;
using TusLibros.model.entities;

namespace TusLibros.app
{
    public static class GlobalConfiguration
    {
        public const string ServerDataBaseGlobal = "localhost";
        public const string DataBaseGlobal = "tuslibros";
        public const string UserDataBaseGlobal = "root";
        public const string PasswordDataBaseGlobal = "root";

        public static string ConnectionDataBaseString =
            "Server=" + ServerDataBaseGlobal + ";" +
            "Database=" + DataBaseGlobal + ";" +
            "User ID=" + UserDataBaseGlobal + ";" +
            "Password=" + PasswordDataBaseGlobal + ";";

        public static MerchantProcessor MerchantProcessor= new MerchantProcessor();
    }
}
