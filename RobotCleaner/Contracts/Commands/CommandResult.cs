using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Commands
{
    public class CommandResult
    {
        public bool IsSuccesful { get; set; }
        public bool Terminate { get; set; }

        public CommandResult(bool isSuccesful, bool terminate = false)
        {
            IsSuccesful = isSuccesful;
            Terminate = terminate;
        }
    }
}
