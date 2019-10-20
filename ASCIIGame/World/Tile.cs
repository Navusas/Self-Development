using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGame.World
{
    class Tile
    {
        #region Parameters
        /// <summary>
        /// Types of available tiles types
        /// </summary>
        public enum TileType
        {
            Wall,
            Empty,
            Player,
            Max
        }
        public int x { get; set; }
        public int y { get; set; }
        public TileType type { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates empty tile at given coordinates
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        public Tile(int _x, int _y)
        {
            x = _x;
            y = _y;
            type = TileType.Empty;
        }

        /// <summary>
        /// Creates tile at coordinates of specified type
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Type"></param>
        public Tile(int X, int Y, TileType Type)
        {
            x = X;
            y = Y;
            type = Type;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Converts given tile type into string character
        /// </summary>
        /// <param name="tileType"></param>
        /// <returns></returns>
        public string TileTypeToString(TileType tileType)
        {
            string toReturn = string.Empty;
            switch (tileType)
            {
                case TileType.Wall:
                    {
                        toReturn = "#";
                        break;
                    }
                case TileType.Empty:
                    {
                        toReturn = ".";
                        break;
                    }
                case TileType.Player:
                    {
                        toReturn = "@";
                        break;
                    }
                default:
                    {
                        toReturn = ".";
                        break;
                    }
            }
            return toReturn;
        }
        #endregion
    }
}
