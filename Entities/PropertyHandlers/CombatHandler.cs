using System;

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