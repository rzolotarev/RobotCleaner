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
        private readonly IWalkingStrategie _walkingStrategie;
        
        public Robot(IWalkingStrategie walkingStrategie)
        {                        
            _walkingStrategie = walkingStrategie;
        }

        public CleaningResult StartClean()
        {
            //TODO - что посетил, что помыл
            //TODO - может сделать так, что из робота передавать следующую команду
            _walkingStrategie.TryRunCommand();
            return _walkingStrategie.GetResult();
        }
    }
}
