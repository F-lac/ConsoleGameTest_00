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
        public Entity Entity;

        public Enemy(Entity entity, int maxHP, int HP, int dam, int def)
        {
            Entity = entity;
            HealthHandler = new HealthHandler(maxHP, HP);
            CombatHandler = new CombatHandler(dam, def);
        }

        public void UpdateActions()
        {
            
        }

        public void UpdateReactions()
        {
            
        }
    }
}