using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class PositionStateManager
    {    
        private Coordinate coordinate { get; set; }
        private Facing facing { get; set; }
        private int batteryUnit { get; set; }
        private Dictionary<Coordinate, int> visited { get; set; }
        private Dictionary<Coordinate, int> cleaned { get; set; }
        private readonly PlaceStatus[,] _map;
        private readonly int _rowCount;
        private readonly int _colCount;

        public PositionStateManager(int x, int y, Facing facing, int batteryUnit, PlaceStatus[,] map)
        {            
            this.facing = facing;
            this.batteryUnit = batteryUnit;
            _map = map;
            _rowCount = _map.GetLength(0);
            _colCount = _map.GetLength(1);

            visited = new Dictionary<Coordinate, int>(new CoordinateComparer()) { { new Coordinate(x, y), 0 } };
            cleaned = new Dictionary<Coordinate, int>(new CoordinateComparer());
            coordinate = new Coordinate(x, y);
        }

        public int BatteryUnit => batteryUnit;
        public Facing Facing => facing;
        public Coordinate Coordinate => new Coordinate(coordinate.X, coordinate.Y);
        
        public List<Coordinate> Visited => visited.OrderBy(v => v.Value).Select(v => v.Key).ToList();
        public List<Coordinate> Cleaned => cleaned.OrderBy(c => c.Value).Select(c => c.Key).ToList();     

        public bool TryToTurnRight()
        {
            if (TryToConsumeBattery())
            {
                facing = (Facing)((int)(++facing) % (Enum.GetValues(typeof(Facing)).Length));
                return true;
            }

            return false;
        }

        public bool TryToTurnLeft()
        {
            if (TryToConsumeBattery())
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
        
        public bool TryToGo(int stepCount = 1)
        {
            if (!TryToConsumeBattery(2))
                return false;

            var previousCoordinateX = coordinate.X;
            var previousCoordinateY = coordinate.Y;           

            if (facing == Facing.N)
                coordinate.Y -= stepCount;
            if (facing == Facing.E)
                coordinate.X += stepCount;
            if (facing == Facing.S)
                coordinate.Y += stepCount;
            if (facing == Facing.W)
                coordinate.X -= stepCount;
            
            if (XCoordinateInBorder() && YCoordinateInBorder())
                if (PlaceIsAvailable())
                {
                    TryAddingToVisited();
                    return true;
                }

            coordinate.X = previousCoordinateX;
            coordinate.Y = previousCoordinateY;
            return false;
        }

        public bool TryToGoBack(int stepCount = 1)
        {
            if (!TryToConsumeBattery(3))
                return false;

            var previousCoordinateX = coordinate.X;
            var previousCoordinateY = coordinate.Y;

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
                    TryAddingToVisited();
                    return true;
                }

            coordinate.X = previousCoordinateX;
            coordinate.Y = previousCoordinateY;

            return false;
        }

        private bool TryToConsumeBattery(int units = 1)
        {
            if (batteryUnit - units < 0)
                return false;

            batteryUnit -= units;
            return true;
        }

        public bool TryToClean()
        {
            if (TryToConsumeBattery(5))
            {
                TryAddingToCleaned();
                return true;
            }

            return false;
        }

        private void TryAddingToVisited()
        {
            try
            {
                var length = visited.Count;
                visited.Add(new Coordinate(coordinate.X, coordinate.Y), length);
            }
            catch (Exception ex) { }
        }

        private void TryAddingToCleaned()
        {
            try
            {
                var length = cleaned.Count;
                cleaned.Add(new Coordinate(coordinate.X, coordinate.Y), length);
            }
            catch (Exception ex) { }
        }
    }
}
