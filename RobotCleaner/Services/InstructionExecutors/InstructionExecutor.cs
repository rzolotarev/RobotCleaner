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
        private readonly PositionStateManager _positionState;        

        public InstructionExecutor(PositionStateManager positionState)
        {
            _positionState = positionState;            
        }        

        public CleaningResult GetResult()
        {            
            return new CleaningResult() {  Final = new FinalStateView(_positionState.Coordinate.X, 
                _positionState.Coordinate.Y, _positionState.Facing.ToString(), _positionState.BatteryUnit) };
        }

        public bool TryToExecuteInstruction(Instructions currentInstruction)
        {
            ICommand command = null;
            if(CommandMapping.TryGetValue(currentInstruction, out command))
                return command.ExecuteCommand(_positionState);
            //TODO-logging
            return false;
        }
    }
}
