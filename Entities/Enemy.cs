using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public class Enemy : IActiveObject
    {
        public static HealthHandler HealthHandler;
        public static CombatHandler CombatHandler;
        public static MovementHandler MovementHandler;
        public Entity Entity;

        public Enemy(Entity entity, int maxHP, int HP, int dam, int def, Point initPos, int tps)
        {
            Entity = entity;
            HealthHandler = new HealthHandler(maxHP, HP);
            CombatHandler = new CombatHandler(dam, def);
            MovementHandler = new MovementHandler(initPos, tps);
        }

        public void UpdateActions()
        {
            
        }

        public void UpdateReactions()
        {
            
        }
    }
}