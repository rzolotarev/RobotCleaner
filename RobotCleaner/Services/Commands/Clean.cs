using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class Clean : ICommand
    {
        public bool ExecuteCommand(PositionState positionState)
        {
            positionState.ConsumeBattery(5);
            return true;
        }
    }
}
