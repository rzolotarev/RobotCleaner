using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class Advance : ICommand
    {
        public bool ExecuteCommand(PositionStateManager positionState)
        {            
            return positionState.TryToGo();                 
        }
    }
}
