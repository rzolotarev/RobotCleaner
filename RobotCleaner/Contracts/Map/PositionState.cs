using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class PositionState
    {
        private int x;
        private int y;
        private Facing facing;
        private int batteryUnit;

        public PositionState(int x, int y, Facing facing, int batteryUnit)
        {
            this.x = x;
            this.y = y;
            this.facing = facing;
            this.batteryUnit = batteryUnit;
        }

        public int BatteryUnit => batteryUnit;

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
                y -= stepCount;
            if (facing == Facing.E)
                x += stepCount;
            if (facing == Facing.S)
                y += stepCount;
            if (facing == Facing.W)
                x -= stepCount;
        }

        public void GoBack(int stepCount = 1)
        {
            if (facing == Facing.N)
                y += stepCount;
            if (facing == Facing.E)
                x -= stepCount;
            if (facing == Facing.S)
                y -= stepCount;
            if (facing == Facing.W)
                x += stepCount;
        }

        public void ConsumeBattery(int units = 1)
        {
            this.batteryUnit -= units;
        }
    }
}
