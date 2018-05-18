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
        private PositionState currentPositionState;
        private readonly IWalkingStrategie _walkingStrategie;

        public Robot(PositionState positionState, IWalkingStrategie walkingStrategie)
        {            
            currentPositionState = positionState;
            _walkingStrategie = walkingStrategie;
        }

        public CleaningResult StartClean()
        {
            //TODO - что посетил, что помыл
            _walkingStrategie.TryRunCommand();
            return _walkingStrategie.GetResult();
        }
    }
}
