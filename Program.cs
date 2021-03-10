using System;
using SadConsole;
using SadConsole.Components;
using SadConsole.Input;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    class Program
    {
        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(80, 25);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            var console = new Console(80, 25);
            console.Fill(Color.White, Color.Black, 0);

            console.IsFocused = true;
            console.Components.Add(new MyKeyboardComponent());

            Player.Entity = new SadConsole.Entities.Entity(Color.White, Color.Black, 1);
            Player.Entity.Parent = console;
            Player.Entity.Position = new Point(5,2);

            SadConsole.Global.CurrentScreen = console;
        }
    }

    public class MyKeyboardComponent : KeyboardConsoleComponent
    {
        public override void ProcessKeyboard(SadConsole.Console console, Keyboard info, out bool handled)
        {
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
                Player.Entity.Position += SadConsole.Directions.North;
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
                Player.Entity.Position += SadConsole.Directions.South;
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
                Player.Entity.Position += SadConsole.Directions.East;
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
                Player.Entity.Position += SadConsole.Directions.West;

            handled = true;
        }
    }
}