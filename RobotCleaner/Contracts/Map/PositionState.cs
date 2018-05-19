using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class PositionState
    {    
        private Coordinate coordinate { get; set; }
        private Facing facing { get; set; }
        private int batteryUnit { get; set; }
        private List<Coordinate> visited { get; set; }
        private List<Coordinate> cleaned { get; set; }
        private readonly PlaceStatus[,] _map;
        private readonly int _rowCount;
        private readonly int _colCount;

        public PositionState(int x, int y, Facing facing, int batteryUnit, PlaceStatus[,] map)
        {
            visited = new List<Coordinate>() { new Coordinate(x, y) };
            cleaned = new List<Coordinate>();

            coordinate = new Coordinate(x, y);
            this.facing = facing;
            this.batteryUnit = batteryUnit;
            _map = map;
            _rowCount = _map.GetLength(0);
            _colCount = _map.GetLength(1);
        }

        public int BatteryUnit => batteryUnit;
        public Facing Facing => facing;
        public Coordinate Coordinate => new Coordinate(coordinate.X, coordinate.Y);

        //TODO - лучше сделать через Dictionary
        public List<Coordinate> Visited => visited.ToList();
        public List<Coordinate> Cleaned => cleaned.ToList();

        public void AddCleaned()
        {
            cleaned.Add(new Coordinate(coordinate.X, coordinate.Y));
        }

        public bool TurnRight()
        {
            if (ConsumeBattery())
            {
                facing = (Facing)((int)(++facing) % (Enum.GetValues(typeof(Facing)).Length));
                return true;
            }

            return false;
        }

        public bool TurnLeft()
        {
            if (ConsumeBattery())
            {
                facing = --facing < 0 ? (Facing)Enum.GetValues(typeof(Facing)).Length - 1 : facing;
                return true;
            }

            return false;
        }

        private bool XCoordinateInBorder()
        {
            return coordinate.X >= 0 && coordinate.X < _colCount;
        }

        private bool YCoordinateInBorder()
        {
            return coordinate.Y >= 0 && coordinate.Y < _rowCount;
        }

        private bool PlaceIsAvailable()
        {
            return _map[coordinate.Y, coordinate.X] == PlaceStatus.S;
        }

        //TODO: Go and Back - отрефакторить
        public bool Go(int stepCount = 1)
        {
            var previousCoordinateX = coordinate.X;
            var previousCoordinateY = coordinate.Y;

            if (!ConsumeBattery(2))
                return false;

            if (facing == Facing.N)
                coordinate.Y -= stepCount;
            if (facing == Facing.E)
                coordinate.X += stepCount;
            if (facing == Facing.S)
                coordinate.Y += stepCount;
            if (facing == Facing.W)
                coordinate.X -= stepCount;

            //TODO-отрефакторить
            if (XCoordinateInBorder() && YCoordinateInBorder())
                if (PlaceIsAvailable())
                {
                    visited.Add(new Coordinate(coordinate.X, coordinate.Y));
                    return true;
                }

            coordinate.X = previousCoordinateX;
            coordinate.Y = previousCoordinateY;
            return false;
        }

        public bool GoBack(int stepCount = 1)
        {
            var previousCoordinateX = coordinate.X;
            var previousCoordinateY = coordinate.Y;

            if (!ConsumeBattery(3))
                return false;

            if (facing == Facing.N)
                coordinate.Y += stepCount;
            if (facing == Facing.E)
                coordinate.X -= stepCount;
            if (facing == Facing.S)
                coordinate.Y -= stepCount;
            if (facing == Facing.W)
                coordinate.X += stepCount;

            if (XCoordinateInBorder() && YCoordinateInBorder())
                if (PlaceIsAvailable())
                {
                    visited.Add(new Coordinate(coordinate.X, coordinate.Y));
                    return true;
                }
            coordinate.X = previousCoordinateX;
            coordinate.Y = previousCoordinateY;
            return false;
        }

        public bool ConsumeBattery(int units = 1)
        {
            if (batteryUnit - units < 0)
                return false;

            batteryUnit -= units;
            return true;
        }
    }
}
