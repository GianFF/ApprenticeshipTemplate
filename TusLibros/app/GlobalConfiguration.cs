using System;

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

        public static Guid GeneratorId()
        {
            throw new NotImplementedException();
        }
    }
}
