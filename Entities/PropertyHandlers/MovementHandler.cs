using System;
using Microsoft.Xna.Framework;
using SadConsole;

namespace ConsoleGameTest_00
{
    public class MovementHandler
    {
        public Point Position;
        public Directions.DirectionEnum Direction;

        public int TurnsPerStep;
        public int CurrentTurn;

        public bool Moving;

        public MovementHandler(Point initPos, int tps)
        {
            Position = initPos;
            Direction = Directions.DirectionEnum.South;
            TurnsPerStep = tps;
            CurrentTurn = 0;
            Moving = false;
        }

        public void Move()
        {
            Moving = true;
            if(CurrentTurn < TurnsPerStep)
            {
                CurrentTurn++;
            } else {
                Position += Directions.Points[(int)Direction];
                CurrentTurn = 0;
                Moving = false;
            }
        }

        public void Stop()
        {
            Moving = false;
            CurrentTurn = 0;
        }

        public void ChangeDirection(Directions.DirectionEnum dir)
        {
            Stop();
            Direction = dir;
        }


    }
}