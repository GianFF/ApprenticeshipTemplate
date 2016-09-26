using System;

namespace TusLibros.model

{
    class TusLibrosApp
    {
        public static void Main(string[] args)
        {
        }

        public static void RepeatAction(int repeatCount, Action action)
        {
            for (int i = 0; i < repeatCount; i++)
                action();
        }
    }
}
