using Common.Outputs;
using Contracts.Commands;
using Contracts.InstructionExecutors;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InstructionExecutors
{
    public class BackOffStrategiesExecutor : BaseInstructionExecutor, IBackOffStrategiesExecutor
    {
        private PositionState _positionState;
        private LinkedList<LinkedList<Instructions>> instructions = new LinkedList<LinkedList<Instructions>>();        

        public BackOffStrategiesExecutor(PositionState positionState, Tracker tracker,
                                        IBackOffInstructionsInitializer backOffInstructionInitializer) 
                                        : base(tracker)
        {
            _positionState = positionState;
            instructions = backOffInstructionInitializer.GetInstructions();
        }

        public bool RunBackOffCommands()
        {
            var hitOnObstacle = false;
            var currentBackStageStrategy = instructions.First;      

            while (currentBackStageStrategy != null)
            {
                var currentInstruction = currentBackStageStrategy.Value.First;
                while (currentInstruction != null && !hitOnObstacle)
                {
                    Command command = null;
                    if (!CommandMapping.TryGetValue(currentInstruction.Value, out command))
                    {
                        ConsoleLogger.WriteError($"There is no matching back off command to instruction: {currentInstruction.Value}");
                        return false;
                    }

                    var result = command.ExecuteCommand(_positionState, Tracker);
                    if (result.Terminate)
                        return false;

                    if(!result.IsSuccesful)
                        hitOnObstacle = true;                                        

                    currentInstruction = currentInstruction.Next;
                }

                if (!hitOnObstacle)
                    return true;

                hitOnObstacle = false;
                currentBackStageStrategy = currentBackStageStrategy.Next;                
            }

            return false;
        }
    }
}
