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
        public int MaxHealth;
        public int Health;
        public Entity Entity;

        public Enemy(Entity entity, int maxHP, int HP)
        {
            Entity = entity;
            MaxHealth = maxHP;
            Health = HP;
        }

        public void UpdateActions()
        {
            
        }

        public void UpdateReactions()
        {
            
        }
    }
}