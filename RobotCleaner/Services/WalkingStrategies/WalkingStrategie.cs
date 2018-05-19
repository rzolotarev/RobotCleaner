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
    public class WalkingStrategie : BaseStrategies, IWalkingStrategie
    {        
        private readonly PositionState _positionState;        

        public WalkingStrategie(PositionState positionState)
        {
            _positionState = positionState;            
        }

        public CleaningResult GetResult()
        {
            //TODO: для final создать отдельную вьюшку
            return new CleaningResult() {  final = new PositionState(_positionState.Coordinate.X, 
                _positionState.Coordinate.Y, _positionState.Facing, _positionState.BatteryUnit, null) };
        }

        public bool RunCommand(Instructions currentInstruction)
        {
            var command = CommandMapping[currentInstruction];
            return command.ExecuteCommand(_positionState);                                         
        }
    }
}
