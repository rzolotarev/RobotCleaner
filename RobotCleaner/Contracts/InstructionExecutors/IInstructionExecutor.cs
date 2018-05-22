using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InstructionExecutors
{
    public interface IInstructionExecutor
    {
        bool TryToExecuteInstruction(Instructions instruction);                     
        List<Coordinate> GetVisited();
        List<Coordinate> GetCleaned();
    }
}
