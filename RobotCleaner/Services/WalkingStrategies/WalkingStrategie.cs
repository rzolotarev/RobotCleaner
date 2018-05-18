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
    public class WalkingStrategie : IWalkingStrategie
    {        
        private readonly WorksParameters _worksParameters;
        private readonly BackOffStrategies _backOffStrategies;

        private Dictionary<Instructions, ICommand> commandMapping = new Dictionary<Instructions, ICommand>()
        {
            { Instructions.A, new Advance() },
            { Instructions.B, new Back() },
            { Instructions.C, new Clean() },
            { Instructions.TL, new TurnLeft() },
            { Instructions.TR, new TurnRight() },
        };

        public WalkingStrategie(WorksParameters workParameters)
        {
            _worksParameters = workParameters;
            _backOffStrategies = new BackOffStrategies(workParameters.PositionState);
        }

        public CleaningResult GetResult()
        {
            return new CleaningResult() { batteryUnit = _worksParameters.PositionState.BatteryUnit };
        }

        public bool TryRunCommand()
        {
            var currentCommand = _worksParameters.Commands.First;
            while (currentCommand != null)
            {
                var command = commandMapping[currentCommand.Value];
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
