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
        private readonly IInstructionExecutor _instructionExecutor;
        private readonly LinkedList<Instructions> _instructions;
        private readonly IBackOffStrategiesExecutor _backOffStrategies;
        private PositionState position;
        private int batteryUnit;

        public Robot(IInstructionExecutor instructionExecutor, 
                     LinkedList<Instructions> instructions, 
                     IBackOffStrategiesExecutor backOffStrategies)
        {
            _instructions = instructions;
            _instructionExecutor = instructionExecutor;
            _backOffStrategies = backOffStrategies;            
        }

        public CleaningResult StartClean()
        {                       
            //TODO - сделать логирование на консоль
            //TODO - чтение файла
            // Чтение параметров о затратах из конфига            
            var currentInstruction = _instructions.First;
            while (currentInstruction != null)
            {
                var isSuccessful = _instructionExecutor.TryToExecuteInstruction(currentInstruction.Value);
                if (!isSuccessful)
                {
                    var backOffSuccessful = _backOffStrategies.RunBackOffCommands();
                    if (!backOffSuccessful)
                    {
                        //TODO: logging
                        return _instructionExecutor.GetResult();
                    }
                }                

                currentInstruction = currentInstruction.Next;
            }  
            return _instructionExecutor.GetResult();
        }
    }
}
