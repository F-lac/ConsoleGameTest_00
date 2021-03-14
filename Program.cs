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
            var fontMaster = SadConsole.Global.LoadFont("Fonts/font-sample_extended.font");
            SadConsole.Global.FontDefault = fontMaster.GetFont(Font.FontSizes.One);

            var console = new Console(80, 25);
            console.Fill(Color.White, Color.Black, 0);

            Cell cell = new Cell(Color.White, Color.Black, 10);
            CellDecorator cellDec1 = new CellDecorator(Color.White, 256, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            CellDecorator cellDec2 = new CellDecorator(Color.White, 257, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            CellDecorator cellDec3 = new CellDecorator(Color.White, 258, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            CellDecorator cellDec4 = new CellDecorator(Color.White, 259, Microsoft.Xna.Framework.Graphics.SpriteEffects.None);
            CellDecorator[] cDArray = new CellDecorator[4];
            cDArray[0] = cellDec1;
            cDArray[1] = cellDec2;
            cDArray[2] = cellDec3;
            cDArray[3] = cellDec4;
            CellState cellState = new CellState(Color.White, Color.Black, 10, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, true, cDArray);
            cell.RestoreState(ref cellState);

            console.IsFocused = true;
            console.Components.Add(new MyKeyboardComponent());

            Player.Entity = new SadConsole.Entities.Entity(Color.White, Color.Black, 64);
            Player.Entity.Parent = console;
            Player.Entity.Position = new Point(5,2);
            Player.MovementHandler = new MovementHandler(new Point(5,2),1);

            console.Print(7,7,"A",Color.White, Color.Black);
            console.SetDecorator(7,7,1,cDArray);

            Map maptest = new Map(80,25);
            World.ActualMap = maptest;
            Random rnd = new Random();
            for(int i = 0; i < 80; i++)
            {
                for(int j = 0; j < 25; j++)
                {
                    TerrainInfo ti = new TerrainInfo();
                    ti.Height = 0;
                    ti.PassableNorth = true;
                    ti.PassableWest = true;
                    ti.PassableSouth = true;
                    ti.PassableEast = true;
                    ti.Obstacle = rnd.NextDouble() > 0.7;
                    if(j==0||j==24||i==0||i==79)
                        ti.Obstacle = true;
                    maptest[i,j] = ti;
                }
            }
            maptest.Refresh();

            maptest.MapConsole.Parent = console;
            maptest.MapConsole.Position = new Point(0,0);

            SadConsole.Global.CurrentScreen = console;
        }
    }

    public class MyKeyboardComponent : KeyboardConsoleComponent
    {
        public override void ProcessKeyboard(SadConsole.Console console, Keyboard info, out bool handled)
        {
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                Player.UpdateActions();
                Player.UpdateReactions();
            }

            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                Player.MovementHandler.ChangeDirection(Directions.DirectionEnum.North);
                Player.Move = true;
            }
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                Player.MovementHandler.ChangeDirection(Directions.DirectionEnum.South);
                Player.Move = true;
            }
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                Player.MovementHandler.ChangeDirection(Directions.DirectionEnum.West);
                Player.Move = true;
            }
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                Player.MovementHandler.ChangeDirection(Directions.DirectionEnum.East);
                Player.Move = true;
            }

            handled = true;
        }
    }
}