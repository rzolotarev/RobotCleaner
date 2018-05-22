using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class TurnLeft : Command
    {
        public TurnLeft(int consumeBattery) : base(consumeBattery)
        {
        }

        public override CommandResult ExecuteCommand(PositionState positionState, Tracker tracker)
        {
            if (!TryToConsumeBattery(positionState))
                return new CommandResult(false, true);

            positionState.Facing = --positionState.Facing < 0 ?
                                    (Facing)Enum.GetValues(typeof(Facing)).Length - 1
                                    : positionState.Facing;
            return new CommandResult(true);
        }
    }
}
