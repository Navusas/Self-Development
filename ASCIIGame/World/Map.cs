using System;
using System.Collections.Generic;
using System.Text;

namespace ASCIIGame.World
{
    class Map
    {
        #region Custom Enum types
        public enum Direction
        {
            North,
            East,
            South,
            West,
            Max
        }

        public enum Placement
        {
            Vertical,
            Horizontal,
            Max
        }
        #endregion

        #region Parameters
        public static Random Rand = new Random();
        /// <summary>
        /// The actual map in ASCII characters
        /// </summary>
        public static Tile[,] ASCIIMap { get; set; }

        /// <summary>
        /// Store the height and the width of the map
        /// </summary>
        private int _width;
        private int _height;

        #endregion
        #region Constructor and Initialization
        public Map()
        {

        }
        /// <summary>
        /// Create the map and initialize it
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Initialize ()
        {
            _width = Constants.MAP_WIDTH;
            _height = Constants.MAP_HEIGHT;
            ASCIIMap = new Tile[Constants.MAP_HEIGHT, Constants.MAP_WIDTH];
            SetBorders(new Tile(0,0), new Tile(Constants.MAP_WIDTH, Constants.MAP_HEIGHT));

            GenerateRooms();
        }
        #endregion

        #region Methods
        public void GenerateRooms()
        {
            Room _room = new Room();
        }
        public static void PlaceRoom(Tile StartPoint, Tile EndPoint, Room.RoomType typeOfroom)
        {
            SetBorders(StartPoint, EndPoint);
        }

        /// <summary>
        /// Sets the square shape walls around the map
        /// </summary>
        public static void SetBorders(Tile StartPoint, Tile EndPoint)
        {
            for(int i = StartPoint.y; i < EndPoint.y; i++)
            {
                for (int j = StartPoint.x; j < EndPoint.x; j++)
                {
                    if(i == StartPoint.y || j == StartPoint.x || i+1 == EndPoint.y || j+1 == EndPoint.x)
                    {
                        ASCIIMap[i, j] = new Tile(i, j, Tile.TileType.Wall);
                    }
                    else
                    {
                        ASCIIMap[i, j] = new Tile(i, j, Tile.TileType.Empty);
                    }
                }
            }
        }
        /// <summary>
        /// Display the map into the console
        /// </summary>
        public void ShowMap()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Tile current = ASCIIMap[i, j];
                    Console.Write(current.TileTypeToString(current.type));
                }
                Console.WriteLine();
            }
        }

        #endregion
    }
}
