using Contracts.Commands;
using Contracts.Map;
using Contracts.WalkingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WalkingStrategies
{
    public class BackOffStrategies : BaseStrategies, IBackOffStrategies
    {
        private PositionState _positionState;
        private LinkedList<LinkedList<Instructions>> instructions = new LinkedList<LinkedList<Instructions>>();        

        public BackOffStrategies(PositionState positionState)
        {
            _positionState = positionState;
            InitializeStrategies();                                    
        }

        //TODO - вынести, чтобы можно было проще переопределить
        public void InitializeStrategies()
        {
            var firstBackOff = new LinkedList<Instructions>();
            firstBackOff.AddLast(Instructions.TR);
            firstBackOff.AddLast(Instructions.A);
            instructions.AddLast(firstBackOff);
            var secondBackOff = new LinkedList<Instructions>();           
            secondBackOff.AddLast(Instructions.TL);
            secondBackOff.AddLast(Instructions.B);
            secondBackOff.AddLast(Instructions.TR);
            secondBackOff.AddLast(Instructions.A);
            instructions.AddLast(secondBackOff);
            
            var thirdBackOff = new LinkedList<Instructions>();
            thirdBackOff.AddLast(Instructions.TL);
            thirdBackOff.AddLast(Instructions.TL);
            thirdBackOff.AddLast(Instructions.A);           
            instructions.AddLast(thirdBackOff);
            
            var fourthBackOff = new LinkedList<Instructions>();
            fourthBackOff.AddLast(Instructions.TR);
            fourthBackOff.AddLast(Instructions.B);
            fourthBackOff.AddLast(Instructions.TR);
            fourthBackOff.AddLast(Instructions.A);
            instructions.AddLast(fourthBackOff);

            var fifthBackOff = new LinkedList<Instructions>();
            fifthBackOff.AddLast(Instructions.TL);
            fifthBackOff.AddLast(Instructions.TL);
            fifthBackOff.AddLast(Instructions.A);
            instructions.AddLast(fifthBackOff);            
        }

        public bool ExecuteCommand()
        {
            var hitOnObstacle = false;
            var currentBackStageStrategie = instructions.First;

            while(currentBackStageStrategie != null)
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
            }

            return false;
        }
    }
}
