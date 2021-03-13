using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public class CombatHandler
    {
        public int Damage;
        public int Defense;

        public CombatHandler(int dam, int def)
        {
            Damage = dam;
            Defense = def;
        }
    }
}