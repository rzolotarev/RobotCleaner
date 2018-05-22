using Contracts.Commands;
using Contracts.Map;
using Contracts.WalkingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robot
{
    public class Robot : IMachineCleaner
    {                
        private readonly IInstructionExecutor _instructionExecutor;
        private readonly LinkedList<Instructions> _instructions;        
        private readonly PositionState _positionState;        

        public Robot(IInstructionExecutor instructionExecutor, 
                     LinkedList<Instructions> instructions,                     
                     PositionState positionState)
        {
            _instructions = instructions;
            _instructionExecutor = instructionExecutor;            
            _positionState = positionState;
        }

        public FinalResult StartClean()
        {                                               
            //TODO: запись в файл
            var currentInstruction = _instructions.First;
            while (currentInstruction != null)
            {
                var isSuccessful = _instructionExecutor.TryToExecuteInstruction(currentInstruction.Value);
                if (!isSuccessful)                
                    return GetResult();

                currentInstruction = currentInstruction.Next;                                
            }
            return GetResult();
        }

        private FinalResult GetResult()
        {
            var finalResult = new FinalResult();
            finalResult.FinalState = new FinalState(_positionState.X, _positionState.Y, _positionState.Facing.ToString(), _positionState.LeftBattery);
            finalResult.Visited = _instructionExecutor.GetVisited();
            finalResult.Cleaned = _instructionExecutor.GetCleaned();
            return finalResult;
        }
    }
}
