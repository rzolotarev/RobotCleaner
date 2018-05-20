using Contracts.Commands;
using Contracts.InstructionExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InstructionExecutors
{
    public class StandardBackOffInstructionsInitializer : IBackOffInstructionsInitializer
    {
        public LinkedList<LinkedList<Instructions>> GetInstructions()
        {
            var instructions = new LinkedList<LinkedList<Instructions>>();
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
            return instructions;
        }
    }
}
