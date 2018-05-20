using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class TurnLeft : ICommand
    {  
        public bool ExecuteCommand(PositionState positionState)
        {
            //TODO: вынести в конфиг и доставать оттуда                
            return positionState.TryToTurnLeft();            
        }
    }
}
