using Contracts.Extensions;
using Contracts.InstructionExecutors;
using Contracts.Map;
using NUnit.Framework;
using Services.Container;
using Unity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class BackOffStrategyTest
    {
        [Test]
        public void Test1BackOffStrategy()
        {
            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;            

            var positionState = container.Resolve<PositionState>();
            positionState.Facing = Facing.N;
            positionState.X = 3;
            positionState.Y = 0;
            positionState.LeftBattery = 80;
            positionState.SetMap(newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
                                    
            
            var walkingStrategy = container.Resolve<IInstructionExecutor>();
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = walkingStrategy.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;

                currentCommand = currentCommand.Next;
            }

            Assert.AreEqual(54, positionState.LeftBattery);
            Assert.AreEqual(2, positionState.X);
            Assert.AreEqual(0, positionState.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }

        [Test]
        public void Test2BackOffStrategy_Position()
        {
            var container = Container.BuildContainer();

            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;            
            var positionState = container.Resolve<PositionState>();
            positionState.Facing = Facing.S;
            positionState.X = 3;
            positionState.Y = 1;
            positionState.LeftBattery = 1094;
            positionState.SetMap(newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);

            var instructionExecutor = container.Resolve<IInstructionExecutor>();            
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = instructionExecutor.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;                

                currentCommand = currentCommand.Next;
            }

            Assert.AreEqual(1040, positionState.LeftBattery);
            Assert.AreEqual(3, positionState.X);
            Assert.AreEqual(2, positionState.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }

        [Test]
        public void Test2BackOffStrategy_VisitedPoints()
        {
            var container = Container.BuildContainer();

            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.Facing = Facing.S;
            positionState.X = 3;
            positionState.Y = 1;
            positionState.LeftBattery = 1094;
            positionState.SetMap(newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);

            var instructionExecutor = container.Resolve<IInstructionExecutor>();
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = instructionExecutor.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;

                currentCommand = currentCommand.Next;
            }

            var actulaVisited = container.Resolve<Tracker>().Visited;
            var expectedVisited = new List<Coordinate>()
            {
                new Coordinate(2,2),
                new Coordinate(3,0),
                new Coordinate(3,1),
                new Coordinate(3,2)
            };
            Assert.AreEqual(expectedVisited.Count, actulaVisited.Count);
            actulaVisited.ForEach(a => Assert.That(expectedVisited.Contains(a)));
        }

        [Test]
        public void Test2BackOffStrategy_CleanedPoints()
        {
            var container = Container.BuildContainer();

            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.Facing = Facing.S;
            positionState.X = 3;
            positionState.Y = 1;
            positionState.LeftBattery = 1094;
            positionState.SetMap(newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);

            var instructionExecutor = container.Resolve<IInstructionExecutor>();
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = instructionExecutor.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;

                currentCommand = currentCommand.Next;
            }

            var expectedCleaned = new List<Coordinate>()
            {
                new Coordinate(2,2),
                new Coordinate(3,0),
                new Coordinate(3,2)
            };
            var actualCleaned = container.Resolve<Tracker>().Cleaned;
            Assert.AreEqual(expectedCleaned.Count, actualCleaned.Count);
            actualCleaned.ForEach(a => Assert.That(expectedCleaned.Contains(a)));
        }
    }
}
