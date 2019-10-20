using System;
using System.Collections.Generic;
using static ASCIIGame.World.Map;
using ASCIIGame.World;

namespace ASCIIGame.World
{
    class Room
    {
        #region Parameters

        public enum RoomType
        {
            Empty,
            Monister,
            Combat,
            Corridor,
            Max
        }

        /// <summary>
        /// Children nodes
        /// </summary>
        public List<Room> Childrens = new List<Room>();

        /// <summary>
        /// parent of this room
        /// </summary>
        public Room Parent { get; set; }
        public Tile StartCoordinate { get; set; }
        public Tile EndTile { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// What is the type of the room
        /// </summary>
        public RoomType TypeOfRoom { get; set; }
        /// <summary>
        /// Which direction the room is placed to?
        /// </summary>
        public Direction RoomDirection { get; set; }

        public bool[] IsDirectionTaken { get; set; }
        #endregion

        #region Constructor and initialization
        /// <summary>
        /// Defualt constructor (for parent)
        /// </summary>
        public Room()
        {
            Initialize();
            // set the parent room coordinates
            Tile StartCoord = new Tile(Constants.START_X, Constants.START_Y);
            StartBuildingRooms(StartCoord);
        }

        /// <summary>
        /// Full room constructor
        /// </summary>
        public Room(Room parent, Tile StartPoint, Tile EndPoint, int height, int width, RoomType typeOfRoom, Direction roomDirection)
        {
            Initialize();

            // Set the local parameters
            StartCoordinate = StartPoint;
            Height = height;
            Width = width;
            TypeOfRoom = typeOfRoom;
            RoomDirection = roomDirection;
            Generate(this, StartPoint, height, width);
        }
        private void Initialize()
        {
            IsDirectionTaken = new bool[(int)Direction.Max];
            Childrens = new List<Room>();
        }

        #endregion

        public void StartBuildingRooms(Tile StartTile)
        {
            // 1 -> Get the new room size and direction (primeraly -> parent's direction.
            int height = GetRandomRoomHeight();
            int width = GetRandomRoomWidth();
            Direction newDirection = Direction.East;

            // build the first room
            Tile endPoint = FindEndPoint(ref StartTile, width, height, newDirection);

            while(IsOverlapping(StartTile,endPoint))
            {
                height = GetRandomRoomHeight();
                width = GetRandomRoomWidth();
                endPoint = FindEndPoint(ref StartTile, width, height, newDirection);
            }

            Map.PlaceRoom(StartTile, endPoint, RoomType.Empty);

            // build the rest of the tree (rooms)
            Generate(null, StartTile, height,width);

        }

        private void Generate(Room parent, Tile StartTile, int height, int widht)
        {
            // run until all directions are taken
            while(AmountOfDirectionsAvailable() > 0)
            {
                // find the new direction (randomly)
                Direction newDirection = GetRandomRoomDirection();

                // if direction is not taken proceed
                if(!IsDirectionTaken[(int)newDirection])
                {
                    //check that there is enough space for room and corridor as well
                    if (EnoughSpaceToBuild(StartTile, height, widht, newDirection))
                    {
                        BuildCorridor(ref StartTile, height, widht, newDirection);

                        //hold the reference for the start point, in case the direction is overlapping
                        Tile statTileReference = new Tile(StartTile.x, StartTile.y);

                        // find the end tile
                        Tile endTile = FindEndPoint(ref StartTile, widht, height, newDirection);

                        //get the new room type
                        RoomType newType = RoomType.Empty;

                        // add the children for this room,  and build children childrens recursively...
                        this.Childrens.Add(new Room(this, StartTile, endTile, height, widht, newType, newDirection));
                    }
                    else
                    {
                        // unavailable to process the room, therefore mark direction as unreachable
                        IsDirectionTaken[(int)newDirection] = true;
                    }
                }
            }
        }
        private void BuildCorridor(ref Tile StartTile, int Height, int Widht, Direction Direction)
        {
            // need to check if available space is higher then constants
            // and if it is not -> just get the avialable space
            // otherwise -> create a random instances
            // check spcae available
            while(true)
            {
                int corridorHeight = GetRandomCorridorHeight(Direction);
                int corridorWidth = GetRandomCorridorWidth(Direction);
            }
        }
        private bool EnoughSpaceToBuild(Tile StartPoint, int Height, int Width, Direction Direction)
        {
            Tile startPoint = StartPoint;
            int height = Height;
            int width = Width;
            Direction direction = Direction;
            GetRoomAndCorridorSizes(ref height, ref width, Direction);
            Tile endPoint = FindEndPoint(ref startPoint, width, height, direction);
            return !IsOverlapping(StartPoint, endPoint);
        }

        private void GetRoomAndCorridorSizes(ref int Height, ref int Width, Direction Direction)
        {
            switch (Direction)
            {
                case Direction.North:
                case Direction.South:
                    {
                        Height += Constants.MIN_CORRIDOR_HEIGHT_VERTICAL;
                        Width += Constants.MIN_CORRIDOR_WIDTH_VERTICAL;
                        break;
                    }
                case Direction.East:
                case Direction.West:
                    {
                        Height += Constants.MIN_CORRIDOR_HEIGHT_HORIZONTAL;
                        Width += Constants.MIN_CORRIDOR_WIDTH_HORIZONTAL;
                        break;
                    }
            }
        }

        /// <summary>
        /// Amount of directions available for this room 
        /// </summary>
        /// <returns></returns>
        private int AmountOfDirectionsAvailable()
        {
            int available = 0;
            foreach(bool directionTaken in IsDirectionTaken)
            {
                available = (directionTaken == false ? available+=1 : available);
            }
            return available;
        }

        /// <summary>
        /// True if room is overlapping with other rooms
        /// </summary>
        /// <param name="StartTile"></param>
        /// <param name="EndTile"></param>
        /// <returns></returns>
        public bool IsOverlapping(Tile StartTile, Tile EndTile)
        {
            bool isOverlapping = false;
            for(int i = StartTile.y; i < EndTile.y; i++)
            {
                for(int j = StartTile.x; j < EndTile.x; j++)
                {
                    Tile current = ASCIIMap[i, j];
                    // if wall exists within
                    if (current.Equals(current.TileTypeToString(Tile.TileType.Wall)))
                    {
                        isOverlapping = true;
                        break;
                    }
                }
            }
            return isOverlapping;
        }
        /// <summary>
        /// Finds the end point and changes the referenced start point to the new directions.
        /// </summary>
        /// <param name="StartPoint"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="Direction"></param>
        /// <returns></returns>
        public Tile FindEndPoint(ref Tile StartPoint, int Width, int Height, Direction Direction)
        {
            int endX = 0, endY = 0;
            switch (Direction)
            {
                case Direction.North:
                    {
                        endX = StartPoint.x + Width; // go to right
                        endY = StartPoint.y;
                        StartPoint.y = StartPoint.y - Height;
                        break;
                    }
                case Direction.East:
                    {
                        endX = StartPoint.x + Width;
                        endY = StartPoint.y + Height;
                        break;
                    }
                case Direction.South:
                    {
                        endX = StartPoint.x;
                        StartPoint.x = StartPoint.x - Width;
                        endY = StartPoint.y;
                        StartPoint.y = StartPoint.y + Height;
                        break;
                    }
                case Direction.West:
                    {
                        endX = StartPoint.x;
                        endY = StartPoint.y;
                        StartPoint.x = StartPoint.x - Width;
                        StartPoint.y = StartPoint.y = Height;
                        break;
                    }
            }
            return new Tile(endX, endY);
        }

        /// <summary>
        /// Get the random width in the range stored within constants
        /// </summary>
        private int GetRandomCorridorWidth(Direction Direction)
        {
            int newWidth = -1;
            switch (Direction)
            {
                case Direction.North:
                case Direction.South:
                    {
                        newWidth = Constants.MIN_CORRIDOR_WIDTH_HORIZONTAL;
                        break;
                    }
                case Direction.East:
                case Direction.West:
                    {
                        newWidth = Map.Rand.Next(Constants.MIN_CORRIDOR_WIDTH_HORIZONTAL, Constants.MAX_CORRIDOR_WIDTH_HORIZONTAL);
                        break;
                    }
            }
            return newWidth;
        }

        /// <summary>
        /// Get the random height in the range stored within constants
        /// </summary>
        /// <returns></returns>
        private int GetRandomCorridorHeight(Direction Direction)
        {
            int newHeight = -1;
            switch (Direction)
            {
                case Direction.North:
                case Direction.South:
                    {
                        newHeight = Map.Rand.Next(Constants.MIN_CORRIDOR_HEIGHT_VERTICAL, Constants.MAX_CORRIDOR_HEIGHT_VERTICAL);
                        break;
                    }
                case Direction.East:
                case Direction.West:
                    {
                        newHeight = Constants.MIN_CORRIDOR_HEIGHT_HORIZONTAL;
                        break;
                    }
            }
            return newHeight;
        }

        /// <summary>
        /// Get the random width in the range stored within constants
        /// </summary>
        private int GetRandomRoomWidth()
        {
            return Map.Rand.Next(Constants.MIN_ROOM_WIDTH, Constants.MAX_ROOM_WIDTH);
        }

        /// <summary>
        /// Get the random height in the range stored within constants
        /// </summary>
        /// <returns></returns>
        private int GetRandomRoomHeight()
        {
            return Map.Rand.Next(Constants.MIN_ROOM_HEIGHT, Constants.MAX_ROOM_HEIGHT);
        }

        /// <summary>
        /// Gets random direction
        /// </summary>
        /// <returns></returns>
        private Direction GetRandomRoomDirection()
        {
            int newDirectionAsInt = Map.Rand.Next(0, (int)Direction.Max); // returns 0 - 4;
            return IntToDirection(newDirectionAsInt);
        }

        /// <summary>
        /// Returns Direction object at required index
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Direction IntToDirection(int position)
        {
            return (Direction)Enum.GetValues(typeof(Direction)).GetValue(position);
        }
    }
}
