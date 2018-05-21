using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class PositionState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Facing Facing { get; set; }
        public int LeftBattery { get; set; }

        private readonly PlaceStatus[,] _map;
        private readonly int _rowCount;
        private readonly int _colCount;

        public PositionState(int x, int y, Facing facing, 
                            int batteryCharge, PlaceStatus[,] map)
        {
            X = x;
            Y = y;
            Facing = facing;
            LeftBattery = batteryCharge;
            _map = map;
            _rowCount = _map.GetLength(0);
            _colCount = _map.GetLength(1);
        }

        public bool XCoordinateInBorder()
        {
            return X >= 0 && X < _colCount;
        }

        public bool YCoordinateInBorder()
        {
            return Y >= 0 && Y < _rowCount;
        }

        public bool PlaceIsAvailable()
        {
            return _map[Y, X] == PlaceStatus.S;
        }
    }
}
