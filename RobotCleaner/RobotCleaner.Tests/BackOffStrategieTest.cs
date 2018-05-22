using Contracts.Extensions;
using Contracts.Map;
using NUnit.Framework;
using Services.InstructionExecutors;
using Services.WalkingStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class BackOffStrategieTest
    {
        [Test]
        public void Test1BackOffStrategie()
        {
            
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(3, 0, Facing.N, 80, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);

            var backOffInitializer = new StandardBackOffInstructionsInitializer();
            var tracker = new Tracker(positionState.X, positionState.Y);
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, tracker, backOffInitializer);
            
            var walkingStrategie = new InstructionExecutor(positionState, tracker, backOffStrategies);
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = walkingStrategie.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;

                currentCommand = currentCommand.Next;
            }

            var visited = tracker.Visited;
            var cleaned = tracker.Cleaned;
            Assert.AreEqual(54, positionState.LeftBattery);
            Assert.AreEqual(2, positionState.X);
            Assert.AreEqual(0, positionState.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }

        [Test]
        public void Test2BackOffStrategie()
        {
            
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(3, 1, Facing.S, 1094, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);

            var backOffInitializer = new StandardBackOffInstructionsInitializer();
            var tracker = new Tracker(positionState.X, positionState.Y);
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, tracker, backOffInitializer);
            var instructionExecutor = new InstructionExecutor(positionState, tracker, backOffStrategies);
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = instructionExecutor.TryToExecuteInstruction(currentCommand.Value);
                if (!isSuccessful)
                    break;                

                currentCommand = currentCommand.Next;
            }

            var actulaVisited = tracker.Visited;
            var expectedVisited = new List<Coordinate>()
            {
                new Coordinate(2,2),
                new Coordinate(3,0),
                new Coordinate(3,1),
                new Coordinate(3,2)
            };
            Assert.AreEqual(expectedVisited.Count, actulaVisited.Count);
            actulaVisited.ForEach(a => Assert.That(expectedVisited.Contains(a)));

            var expectedCleaned = new List<Coordinate>()
            {
                new Coordinate(2,2),
                new Coordinate(3,0),                
                new Coordinate(3,2)
            };
            var actualCleaned = tracker.Cleaned;
            Assert.AreEqual(expectedCleaned.Count, actualCleaned.Count);
            actualCleaned.ForEach(a => Assert.That(expectedCleaned.Contains(a)));

            Assert.AreEqual(1040, positionState.LeftBattery);
            Assert.AreEqual(3, positionState.X);
            Assert.AreEqual(2, positionState.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }  
    }
}
