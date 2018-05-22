using Common.Outputs;
using Services.Container;
using Services.FileReaders;
using Services.InputValidations;
using Services.Robots;
using System;
using Unity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Robots;
using Contracts.FileReaders;
using Contracts.FileWriters;
using Services.FileWriters;
using Contracts.Map;
using Contracts.Extensions;
using Common.Exceptions;
using Unity.Resolution;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = Container.BuildContainer();

                InputValidation.CheckInputParameters(args);
                var inputReader = container.Resolve<IWorkParametersProvider>();

                var instructions = inputReader.Read(args[0]);
                var positionState = container.Resolve<PositionState>();
                positionState.Facing = instructions.PositionState.Facing;
                positionState.X = instructions.PositionState.X;
                positionState.Y = instructions.PositionState.Y;
                positionState.LeftBattery = instructions.PositionState.LeftBattery;
                var parsedMap = instructions.Map.ToPlaceStatuses();
                Check.That(parsedMap.IsSucceed, "Map has not been parsed correctly");
                positionState.SetMap(parsedMap.Matrix);

                var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", instructions.Commands));
                var result = robot.StartClean();
                var fileSaver = new FileWriter(result);
                fileSaver.Save(args[1]);
                ConsoleLogger.WriteSuccessInfo($"Result of program is located in {args[1]}");
            }
            catch (Exception ex)
            {
                ConsoleLogger.WriteError("Program is ended with error");
                Console.ReadKey();
            }
        }
    }
}
