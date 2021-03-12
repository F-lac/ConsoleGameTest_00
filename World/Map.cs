using System;
using System.Collections.Generic;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;

namespace ConsoleGameTest_00
{
    public struct TerrainInfo
    {
        public int Height;
        public bool Obstacle;
        public bool PassableNorth;
        public bool PassableWest;
        public bool PassableSouth;
        public bool PassableEast;
    }

    public class Map
    {
        public int SizeX {get; private set;}
        public int SizeY {get; private set;}

        public TerrainInfo[,] TerrainInfoArray;

        public Console MapConsole;

        public Map(int sx, int sy)
        {
            SizeX = sx;
            SizeY = sy;
            TerrainInfoArray = new TerrainInfo[sx, sy];
            MapConsole = new Console(sx, sy);
        }

        public TerrainInfo this[int i, int j]
        {
            get { return TerrainInfoArray[i,j]; }
            set { TerrainInfoArray[i,j] = value; }
        }

        public TerrainInfo this[Point p]
        {
            get { return TerrainInfoArray[p.X,p.Y]; }
        }

        public void Refresh()
        {
            for(int i = 0; i < SizeX; i++)
            {
                for(int j = 0; j < SizeY; j++)
                {
                    if(TerrainInfoArray[i, j].Obstacle)
                        MapConsole.Print(i, j, "#", Color.White, Color.Black);
                }
            }
        }
    }
}