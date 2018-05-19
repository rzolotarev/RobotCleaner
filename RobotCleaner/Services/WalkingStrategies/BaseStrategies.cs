using Contracts.Commands;
using Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WalkingStrategies
{
    public class BaseStrategies
    {
        protected Dictionary<Instructions, ICommand> CommandMapping = new Dictionary<Instructions, ICommand>()
        {
            { Instructions.A, new Advance() },
            { Instructions.B, new Back() },
            { Instructions.C, new Clean() },
            { Instructions.TL, new TurnLeft() },
            { Instructions.TR, new TurnRight() },
        };
    }
}
