using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public static class Player
    {
        public static HealthHandler HealthHandler;
        public static CombatHandler CombatHandler;
        public static MovementHandler MovementHandler;
        public static Entity Entity;
        public static bool Move;

        public static void UpdateActions()
        {
            if(Move)
            {
                MovementHandler.Move();
            }
        }

        public static void UpdateReactions()
        {
            if(Move)
            {
                if(World.ActualMap[Entity.Position + Directions.Points[(int)MovementHandler.Direction]].Obstacle)
                {
                    MovementHandler.Stop();
                    Move = false;
                } else {
                    Move = true;
                    if(Entity.Position != MovementHandler.Position)
                    {
                        Entity.Position = MovementHandler.Position;
                    }
                }
            }
        }
    }
}