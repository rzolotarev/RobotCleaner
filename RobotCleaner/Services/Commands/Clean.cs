﻿using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class Clean : Command
    {
        public Clean(int consumeBattery): base(consumeBattery)
        {
        }

        public override CommandResult ExecuteCommand(PositionState positionState, Tracker tracker)
        {
            if (TryToConsumeBattery(positionState))
            {
                tracker.TryAddingToCleaned(positionState.X, positionState.Y);
                return new CommandResult(true);
            }

            return new CommandResult(false, true);   
        }  
    }
}
