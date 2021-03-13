using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public class HealthHandler
    {
        public int MaxHealth;
        public int Health;
        public bool isDead;

        public HealthHandler(int maxHP, int HP)
        {
            MaxHealth = maxHP;
            Health = HP;
            isDead = true;
            if(HP > 0) isDead = false;
        }

        public void Damage(int dmg)
        {
            if(Health >= dmg)
            {
                Health -= dmg;
            } else {
                Health = 0;
                isDead = true;
            }
        }
    }
}