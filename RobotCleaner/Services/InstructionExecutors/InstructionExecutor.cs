using Contracts.Commands;
using Contracts.Map;
using Contracts.Robot;
using Contracts.WalkingStrategies;
using Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WalkingStrategies
{
    public class InstructionExecutor : BaseInstructionExecutor, IInstructionExecutor
    {        
        private readonly PositionState _positionState;      

        public InstructionExecutor(PositionState positionState, Tracker tracker) : base(tracker) 
        {
            _positionState = positionState;
        }

        public CleaningResult GetResult()
        {            
            return new CleaningResult() {  Final = new FinalStateView(_positionState.X, 
                _positionState.Y, _positionState.Facing.ToString(), _positionState.LeftBattery) };
        }

        public bool TryToExecuteInstruction(Instructions currentInstruction)
        {
            Command command = null;
            if (CommandMapping.TryGetValue(currentInstruction, out command))
                return command.ExecuteCommand(_positionState, Tracker);                
            //TODO-logging
            return false;
        }
    }
}
