using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Commands
{
    public interface ICommand
    {
        bool ExecuteCommand(PositionState positionState);        
    }
}
