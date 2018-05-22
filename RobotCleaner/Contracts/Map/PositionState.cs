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

        private PlaceStatus[,] map;
        private int rowCount;
        private int colCount;
        
        public PositionState()
        {

        }

        public PositionState(int x, int y, Facing facing, 
                            int batteryCharge, PlaceStatus[,] map)
        {
            X = x;
            Y = y;
            Facing = facing;
            LeftBattery = batteryCharge;
            this.map = map;
            rowCount = map.GetLength(0);
            colCount = map.GetLength(1);
        }

        public void SetMap(PlaceStatus[,] map)
        {
            this.map = map;
            rowCount = map.GetLength(0);
            colCount = map.GetLength(1);
        }

        public bool XCoordinateInBorder()
        {
            return X >= 0 && X < colCount;
        }

        public bool YCoordinateInBorder()
        {
            return Y >= 0 && Y < rowCount;
        }

        public bool PlaceIsAvailable()
        {
            return map[Y, X] == PlaceStatus.S;
        }
    }
}
