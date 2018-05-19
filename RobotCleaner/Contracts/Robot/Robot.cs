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
        private readonly IWalkingStrategie _walkingStrategie;
        private readonly LinkedList<Instructions> _instructions;
        private readonly IBackOffStrategies _backOffStrategies;

        public Robot(IWalkingStrategie walkingStrategie, LinkedList<Instructions> instructions, IBackOffStrategies backOffStrategies)
        {                        
            _walkingStrategie = walkingStrategie;
            _instructions = instructions;
            _backOffStrategies = backOffStrategies;
        }

        public CleaningResult StartClean()
        {
            //TODO - что посетил, что помыл
            //TODO - может сделать так, что из робота передавать следующую команду
            var currentInstruction = _instructions.First;
            while (currentInstruction != null)
            {
                var isSuccessful = _walkingStrategie.RunCommand(currentInstruction.Value);
                if (!isSuccessful)
                {
                    var backOffSuccessful = _backOffStrategies.RunCommands();
                }
                else
                    currentInstruction = currentInstruction.Next;
            }       
            return _walkingStrategie.GetResult();
        }
    }
}
