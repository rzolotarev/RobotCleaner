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
            var positionState = new PositionStateManager(1, 1, Facing.N, 100, newMap);            

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);            
            
            var walkingStrategie = new InstructionExecutor(positionState);
            var moq = new Mock<IBackOffStrategiesExecutor>();
            var robot = new Robot(walkingStrategie, commands, moq.Object);
            var result = robot.StartClean();
            Assert.AreEqual(87, result.Final.Battery);
            Assert.AreEqual(2, result.Final.X);
            Assert.AreEqual(2, result.Final.Y);
            Assert.AreEqual("W", result.Final.Facing);
        }

        [Test]
        public void Test1WithEmptySpace()
        {            
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionStateManager(3, 0, Facing.N, 80, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);            
            var walkingStrategie = new InstructionExecutor(positionState);

            var backOffInitializer = new StandardBackOffInstructionsInitializer();
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, backOffInitializer);
            var robot = new Robot(walkingStrategie,commands, backOffStrategies);
            var result = robot.StartClean();
            Assert.AreEqual(54, result.Final.Battery);
            Assert.AreEqual(2, result.Final.X);
            Assert.AreEqual(0, result.Final.Y);
            Assert.AreEqual("E", result.Final.Facing);
        }

        [Test]
        public void Test2WithEmptySpace()
        {
 
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionStateManager(3, 1, Facing.S, 1094, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            var walkingStrategie = new InstructionExecutor(positionState);
            var backOffInitializer = new StandardBackOffInstructionsInitializer();
            var backOffStrategies = new BackOffStrategiesExecutor(positionState, backOffInitializer);
            var robot = new Robot(walkingStrategie, commands, backOffStrategies);
            var result = robot.StartClean();
            Assert.AreEqual(1040, result.Final.Battery);
            Assert.AreEqual(3, result.Final.X);
            Assert.AreEqual(2, result.Final.Y);
            Assert.AreEqual("E", result.Final.Facing);
        }

        [Test]
        public void BatteryIsRunOut()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionStateManager(1, 1, Facing.N, 10, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var walkingStrategie = new InstructionExecutor(positionState);
            var moq = new Mock<IBackOffStrategiesExecutor>();
            var robot = new Robot(walkingStrategie, commands, moq.Object);
            var result = robot.StartClean();
            Assert.AreEqual(4, result.Final.Battery);
            Assert.AreEqual(2, result.Final.X);
            Assert.AreEqual(2, result.Final.Y);
            Assert.AreEqual("E", result.Final.Facing);
        }

        [Test]
        public void BatteryIsRunOutWithZero()
        {
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = new PositionStateManager(1, 1, Facing.N, 12, newMap);

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var walkingStrategie = new InstructionExecutor(positionState);
            var moq = new Mock<IBackOffStrategiesExecutor>();
            var robot = new Robot(walkingStrategie, commands, moq.Object);
            var result = robot.StartClean();
            Assert.AreEqual(0, result.Final.Battery);
            Assert.AreEqual(2, result.Final.X);
            Assert.AreEqual(2, result.Final.Y);
            Assert.AreEqual("N", result.Final.Facing);
        }
    }
}
