using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class Back : Command
    {
        public Back(int consumeBattery) : base(consumeBattery)
        {
        }

        public override bool ExecuteCommand(PositionState positionState, Tracker tracker)
        {
            if (!TryToConsumeBattery(positionState))
                return false;

            var previousCoordinateX = positionState.X;
            var previousCoordinateY = positionState.Y;

            var facing = positionState.Facing;

            if (facing == Facing.N)
                positionState.Y += StepCount;
            if (facing == Facing.E)
                positionState.X -= StepCount;
            if (facing == Facing.S)
                positionState.Y -= StepCount;
            if (facing == Facing.W)
                positionState.X += StepCount;

            if (positionState.XCoordinateInBorder() && positionState.YCoordinateInBorder())
                if (positionState.PlaceIsAvailable())
                {
                    tracker.TryAddingToVisited(positionState.X, positionState.Y);
                    return true;
                }

            positionState.X = previousCoordinateX;
            positionState.Y = previousCoordinateY;

            return false;
        }
    }
}
