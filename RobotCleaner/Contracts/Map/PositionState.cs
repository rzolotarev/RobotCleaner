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

        public PositionState(int x, int y, Facing facing, int batteryUnit)
        {
            coordinate = new Coordinate(x, y);            
            this.facing = facing;
            this.batteryUnit = batteryUnit;
        }

        public int BatteryUnit => batteryUnit;
        public Facing Facing => facing;
        public Coordinate Coordinate => new Coordinate(coordinate.X, coordinate.Y);

        public void TurnRight()
        {
            facing = (Facing)((int)(++facing) % (Enum.GetValues(typeof(Facing)).Length));            
        }

        public void TurnLeft()
        {
            facing = --facing < 0 ? (Facing)Enum.GetValues(typeof(Facing)).Length - 1 : facing;            
        }

        public void Go(int stepCount = 1)
        {
            if(facing == Facing.N)
                coordinate.Y -= stepCount;
            if (facing == Facing.E)
                coordinate.X += stepCount;
            if (facing == Facing.S)
                coordinate.Y += stepCount;
            if (facing == Facing.W)
                coordinate.X -= stepCount;
        }

        public void GoBack(int stepCount = 1)
        {
            if (facing == Facing.N)
                coordinate.Y += stepCount;
            if (facing == Facing.E)
                coordinate.X -= stepCount;
            if (facing == Facing.S)
                coordinate.Y -= stepCount;
            if (facing == Facing.W)
                coordinate.X += stepCount;
        }

        public void ConsumeBattery(int units = 1)
        {
            this.batteryUnit -= units;
        }
    }
}
