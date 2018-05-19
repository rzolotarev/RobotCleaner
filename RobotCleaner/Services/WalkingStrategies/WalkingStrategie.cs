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
        private readonly WorksParameters _worksParameters;
        private readonly IBackOffStrategies _backOffStrategies;        

        public WalkingStrategie(WorksParameters workParameters, IBackOffStrategies backoffStrategies)
        {
            _worksParameters = workParameters;
            _backOffStrategies = backoffStrategies;
        }

        public CleaningResult GetResult()
        {
            return new CleaningResult() {  final = _worksParameters.PositionState };
        }

        public bool TryRunCommand()
        {
            var currentCommand = _worksParameters.Commands.First;
            while (currentCommand != null)
            {
                var command = CommandMapping[currentCommand.Value];
                if (!command.ExecuteCommand(_worksParameters.PositionState))
                {
                    if (!_backOffStrategies.ExecuteCommand())
                        return false;
                }

                currentCommand = currentCommand.Next;
            }

            return true;
        }
    }
}
