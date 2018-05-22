using Contracts.Extensions;
using Contracts.Map;
using Contracts.Robot;
using Contracts.WalkingStrategies;
using Moq;
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
    public class RobotTest
    {        
        [Test]
        public void CleaningWithoutEmptySpace()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(1, 1, Facing.N, 100, newMap);
            var tracker = new Tracker(positionState.X, positionState.Y);
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var moq = new Mock<IBackOffStrategiesExecutor>();
            var walkingStrategie = new InstructionExecutor(positionState, tracker, moq.Object);            
            var robot = new Robot(walkingStrategie, commands, positionState);
            var result = robot.StartClean();
            Assert.AreEqual(87, result.FinalState.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("W", result.FinalState.Facing);
        }

        [Test]
        public void Test1WithEmptySpace()
        {            
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(3, 0, Facing.N, 80, newMap);
            var tracker = new Tracker(positionState.X, positionState.Y);
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
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, tracker, backOffInitializer);
            var walkingStrategie = new InstructionExecutor(positionState, tracker, backOffStrategies);

            
            var robot = new Robot(walkingStrategie,commands, positionState);
            var result = robot.StartClean();
            Assert.AreEqual(54, result.FinalState.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(0, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void Test2WithEmptySpace()
        {
 
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(3, 1, Facing.S, 1094, newMap);
            var tracker = new Tracker(positionState.X, positionState.Y);
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
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, tracker, backOffInitializer);
            var walkingStrategie = new InstructionExecutor(positionState, tracker, backOffStrategies);
            var robot = new Robot(walkingStrategie, commands, positionState);
            var result = robot.StartClean();
            Assert.AreEqual(1040, result.FinalState.Battery);
            Assert.AreEqual(3, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void BatteryIsRunOut()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(1, 1, Facing.N, 10, newMap);
            var tracker = new Tracker(positionState.X, positionState.Y);
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var moq = new Mock<IBackOffStrategiesExecutor>();
            var walkingStrategie = new InstructionExecutor(positionState, tracker, moq.Object);
            
            var robot = new Robot(walkingStrategie, commands, positionState);
            var result = robot.StartClean();
            Assert.AreEqual(4, result.FinalState.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void BatteryIsRunOutWithZero()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionState(1, 1, Facing.N, 12, newMap);
            var tracker = new Tracker(positionState.X, positionState.Y);
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var moq = new Mock<IBackOffStrategiesExecutor>();
            var walkingStrategie = new InstructionExecutor(positionState, tracker, moq.Object);            
            var robot = new Robot(walkingStrategie, commands, positionState);
            var result = robot.StartClean();
            Assert.AreEqual(0, result.FinalState.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("N", result.FinalState.Facing);
        }
    }
}
