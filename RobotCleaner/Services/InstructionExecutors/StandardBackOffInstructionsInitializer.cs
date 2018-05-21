using Contracts.Commands;
using Contracts.InstructionExecutors;
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
        const string BackOffStrategies = "BackOffStrategies";

        public LinkedList<LinkedList<Instructions>> GetInstructions()
        {
            var instructions = new LinkedList<LinkedList<Instructions>>();
            var strategies = AppSettings.SafeGet<string>(BackOffStrategies);
            strategies.Split(';').ToList().ForEach(strategie =>
            {
                var currentStrategie = new LinkedList<Instructions>();

                strategie.Split(',').ToList().ForEach(command =>
                {
                    Instructions currentInstruction;

                    if (Enum.TryParse(command, out currentInstruction))
                        currentStrategie.AddLast(currentInstruction);
                    else                                
                        throw new Exception($"Error parsing back off strategie: {command}");                    
                });

                instructions.AddLast(currentStrategie);
            });            
            //var firstBackOff = 
            //firstBackOff.AddLast(Instructions.TR);
            //firstBackOff.AddLast(Instructions.A);
            //instructions.AddLast(firstBackOff);
            //var secondBackOff = new LinkedList<Instructions>();
            //secondBackOff.AddLast(Instructions.TL);
            //secondBackOff.AddLast(Instructions.B);
            //secondBackOff.AddLast(Instructions.TR);
            //secondBackOff.AddLast(Instructions.A);
            //instructions.AddLast(secondBackOff);

            //var thirdBackOff = new LinkedList<Instructions>();
            //thirdBackOff.AddLast(Instructions.TL);
            //thirdBackOff.AddLast(Instructions.TL);
            //thirdBackOff.AddLast(Instructions.A);
            //instructions.AddLast(thirdBackOff);

            //var fourthBackOff = new LinkedList<Instructions>();
            //fourthBackOff.AddLast(Instructions.TR);
            //fourthBackOff.AddLast(Instructions.B);
            //fourthBackOff.AddLast(Instructions.TR);
            //fourthBackOff.AddLast(Instructions.A);
            //instructions.AddLast(fourthBackOff);

            //var fifthBackOff = new LinkedList<Instructions>();
            //fifthBackOff.AddLast(Instructions.TL);
            //fifthBackOff.AddLast(Instructions.TL);
            //fifthBackOff.AddLast(Instructions.A);
            //instructions.AddLast(fifthBackOff);
            return instructions;
        }
    }
}
