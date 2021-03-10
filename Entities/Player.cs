using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public class Player : Entity
    {
        public int MaxHealth;
        public int Health;

        public Player(Console par, int maxHealth)
        {
            this.Parent = par;
            this.Name = "Player";
            this.PositionOffset = new Point(5,2);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            console.Print(0, 0, "@");
        }
    }
}