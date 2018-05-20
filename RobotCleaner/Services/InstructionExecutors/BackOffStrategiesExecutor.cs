using Contracts.Commands;
using Contracts.InstructionExecutors;
using Contracts.Map;
using Contracts.WalkingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WalkingStrategies
{
    public class BackOffStrategiesExecutor : BaseInstructionExecutor, IBackOffStrategiesExecutor
    {
        private PositionStateManager _positionState;
        private LinkedList<LinkedList<Instructions>> instructions = new LinkedList<LinkedList<Instructions>>();        

        public BackOffStrategiesExecutor(PositionStateManager positionState, IBackOffInstructionsInitializer backOffInstructionInitializer)
        {
            _positionState = positionState;
            instructions = backOffInstructionInitializer.GetInstructions();
        }

        public bool RunBackOffCommands()
        {
            var hitOnObstacle = false;
            var currentBackStageStrategie = instructions.First;
            var sourcePosisionX = _positionState.Coordinate.X;
            var sourcePosisionY = _positionState.Coordinate.Y;

            while (currentBackStageStrategie != null)
            {
                var currentCommand = currentBackStageStrategie.Value.First;
                while(currentCommand != null && !hitOnObstacle)
                {
                    var command = CommandMapping[currentCommand.Value];
                    var isSucceed = command.ExecuteCommand(_positionState);
                    if (!isSucceed)                    
                        hitOnObstacle = true;                    
                    else
                        currentCommand = currentCommand.Next;                    
                }

                if (!hitOnObstacle)
                    return true;

                currentBackStageStrategie = currentBackStageStrategie.Next;
                hitOnObstacle = false;
            }

            return false;
        }
    }
}
