using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class PositionState
    {
        private int _colCount1;

        private Coordinate coordinate { get; set; }
        private Facing facing { get; set; }
        private int batteryUnit { get; set; }
        private readonly PlaceStatus[,] _map;
        private readonly int _rowCount;
        private readonly int _colCount;

        public PositionState(int x, int y, Facing facing, int batteryUnit, PlaceStatus[,] map)
        {
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
            return coordinate.X > 0 && coordinate.X < _rowCount;
        }

        private bool YCoordinateInBorder()
        {
            return coordinate.Y > 0 && coordinate.Y < _colCount;
        }

        private bool PlaceIsAvailable()
        {
            return _map[coordinate.X, coordinate.Y] == PlaceStatus.S;
        }

        //TODO: Go and Back - отрефакторить
        public bool Go(int stepCount = 1)
        {
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
                    return true;
            coordinate.X = previousCoordinateX;
            coordinate.Y = previousCoordinateY;
            return false;
        }

        public bool GoBack(int stepCount = 1)
        {
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
                    return true;
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
