using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Commands
{
    public abstract class Command
    {
        protected const int StepCount = 1;
        protected int ConsumeBattery { get; set; }        

        public Command(int consumeBattery)
        {
            ConsumeBattery = consumeBattery;
        }

        protected bool TryToConsumeBattery(PositionState positionState)
        {
            if (positionState.LeftBattery - ConsumeBattery < 0)
                return false;

            positionState.LeftBattery -= ConsumeBattery;
            return true;
        }

        public abstract bool ExecuteCommand(PositionState positionState, Tracker tracker);        
    }
}
