using Common.Exceptions;
using Contracts.Commands;
using Contracts.InstructionExecutors;
using Contracts.Settings;
using Settings.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InstructionExecutors
{
    public class StandardBackOffInstructionsInitializer : IBackOffInstructionsInitializer
    {
        private readonly BackOffSettings _backOffSettings;

        public StandardBackOffInstructionsInitializer(BackOffSettings backOffSettings)
        {
            _backOffSettings = backOffSettings;
        }

        public LinkedList<LinkedList<Instructions>> GetInstructions()
        {
            var instructions = new LinkedList<LinkedList<Instructions>>();
            var strategies = _backOffSettings.Strategies;
            strategies.Split(';').ToList().ForEach(strategie =>
            {
                var currentStrategy = new LinkedList<Instructions>();

                strategie.Split(',').ToList().ForEach(command =>
                {
                    Instructions currentInstruction;

                    Check.That(Enum.TryParse(command, out currentInstruction), $"Error parsing back off strategie: {command}");

                    currentStrategy.AddLast(currentInstruction);                    
                });

                instructions.AddLast(currentStrategy);
            });                     
            return instructions;
        }
    }
}
