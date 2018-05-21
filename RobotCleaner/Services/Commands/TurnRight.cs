using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class TurnRight : Command
    {
        public TurnRight(int consumeBattery): base(consumeBattery)
        {
        }

        public override bool ExecuteCommand(PositionState positionState, Tracker tracker)
        {
            if (TryToConsumeBattery(positionState))
            {
                positionState.Facing = (Facing)((int)(++positionState.Facing) % 
                                        (Enum.GetValues(typeof(Facing)).Length));
                return true;
            }

            return false;            
        }
    }
}
