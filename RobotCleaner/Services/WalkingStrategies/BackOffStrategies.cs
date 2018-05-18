using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WalkingStrategies
{
    public class BackOffStrategies
    {
        private PositionState _positionState;
        private LinkedList<LinkedList<Instructions>> instructions = new LinkedList<LinkedList<Instructions>>();
        LinkedListNode<LinkedList<Instructions>> currentBackStageStrategie;

        public BackOffStrategies(PositionState positionState)
        {
            _positionState = positionState;
            var firstBackOff = new LinkedList<Instructions>();
            firstBackOff.AddLast(Instructions.TR);
            firstBackOff.AddLast(Instructions.A);
            instructions.AddLast(firstBackOff);
            currentBackStageStrategie = instructions.First;
        }

        public bool ExecuteCommand()
        {
            //var noObstacle = false;

            //while(currentBackStageStrategie != null || noObstacle)
            //{

            //}

            return true;
        }
    }
}
