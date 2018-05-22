using Contracts.Commands;
using Contracts.Map;
using Services.Commands;
using Settings.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InstructionExecutors
{
    public class BaseInstructionExecutor
    {
        protected Dictionary<Instructions, Command> CommandMapping = new Dictionary<Instructions, Command>();
        protected Tracker Tracker { get; set; }

        public BaseInstructionExecutor(Tracker tracker)
        {
            Tracker = tracker;
            InitalizeCommandMapping();
        }

        private void InitalizeCommandMapping()
        {            
            var advanceCommand = new Advance(AppSettings.SafeGet<int>(Instructions.A.ToString()));            
            CommandMapping.Add(Instructions.A, advanceCommand);
            var backCommand = new Back(AppSettings.SafeGet<int>(Instructions.B.ToString()));
            CommandMapping.Add(Instructions.B, backCommand);
            var cleanCommand = new Clean(AppSettings.SafeGet<int>(Instructions.C.ToString()));
            CommandMapping.Add(Instructions.C, cleanCommand);
            var turnLeft = new TurnLeft(AppSettings.SafeGet<int>(Instructions.TL.ToString()));
            CommandMapping.Add(Instructions.TL, turnLeft);
            var turnRight = new TurnRight(AppSettings.SafeGet<int>(Instructions.TR.ToString()));
            CommandMapping.Add(Instructions.TR, turnRight);                    
        }        
    }
}
