using Contracts.Commands;
using Contracts.Map;
using Contracts.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WalkingStrategies
{
    public interface IInstructionExecutor
    {
        bool TryToExecuteInstruction(Instructions instruction);                     
        List<Coordinate> GetVisited();
        List<Coordinate> GetCleaned();
    }
}
