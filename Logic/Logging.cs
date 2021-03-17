using System;
using SadConsole;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public static class Logging
    {
        public static Console LogConsole = new Console(80,25);
        public static bool Open = false;

        public static void Refresh()
        {
            LogConsole.Print(1,1,"Log:");
        }
    }
}