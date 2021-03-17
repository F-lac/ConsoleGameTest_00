using System;
using System.Threading;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public enum GameState
    {
        SplashScreen,
        MainMenu,
        Loading,
        Game,
        GameMenu
    }

    public static class GameLogic
    {
        public static GameState State;
        private static int _turn;

        public static void NextTurn()
        {

        }
    }
}