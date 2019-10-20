using ASCIIGame.World;
using System;

namespace ASCIIGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Map myWorld = new Map();
            myWorld.Initialize();
            myWorld.ShowMap();
        }
    }
}
