using Common.Exceptions;
using Contracts.Commands;
using Contracts.Extensions;
using Contracts.FileReaders;
using Contracts.FileReaders.JsonView;
using Contracts.Map;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileReaders
{
    public class ReadingInput : IWorkParametersProvider
    {        
        public ReadingInput()
        {                     
        }

        public WorksParameters Read(string pathToFile)
        {                        
            try
            {                
                var json = File.ReadAllText(pathToFile);
                var info = JsonConvert.DeserializeObject<Input>(json);
                var workParameters = new WorksParameters();
                LinkedList<Instructions> instructions = new LinkedList<Instructions>();

                info.Commands.ToList().ForEach(c => {
                    Instructions currentCommand;
                    Check.That(Enum.TryParse(c, out currentCommand), $"Error parsing instruction {c}");
                    instructions.AddLast(currentCommand);                                              
                });

                workParameters.Commands = instructions;

                Facing facing;
                Check.That(Enum.TryParse(info.Start.Facing, out facing), $"Error parsing Facing {info.Start.Facing}");                                    

                var map = info.Map.ToPlaceStatuses();
                Check.That(map.IsSucceed, $"Error parsing matrix {info.Start.Facing}");                                    

                workParameters.PositionState = new PositionState(info.Start.X, info.Start.Y, facing,
                                                    info.Battery, map.Matrix);

                return workParameters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
