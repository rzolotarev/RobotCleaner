using Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InstructionExecutors
{
    public interface IBackOffInstructionsInitializer
    {
        LinkedList<LinkedList<Instructions>> GetInstructions();
    }
}
