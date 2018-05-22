using Common.Outputs;
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
        private readonly IBackOffStrategiesExecutor _backOffStrategies;

        public InstructionExecutor(PositionState positionState, Tracker tracker, IBackOffStrategiesExecutor backOffStrategies) : base(tracker) 
        {
            _positionState = positionState;
            _backOffStrategies = backOffStrategies;
        }

        public List<Coordinate> GetCleaned() => Tracker.Cleaned;
        public List<Coordinate> GetVisited() => Tracker.Visited;   

        public bool TryToExecuteInstruction(Instructions currentInstruction)
        {
            Command command = null;
            if (!CommandMapping.TryGetValue(currentInstruction, out command))
            {
                ConsoleLogger.WriteError($"There is no matching command to instruction: {currentInstruction}");
                return false;
            }

            if (!command.ExecuteCommand(_positionState, Tracker))
            {
                var backOffSuccessful = _backOffStrategies.RunBackOffCommands();
                if (!backOffSuccessful)
                {
                    ConsoleLogger.WriteDiagnosticInfo("Program is finished, as all back off strategies have been completed.");                    
                    return false;
                }
            }

            return true;
        }
    }
}
