using Contracts.Extensions;
using Contracts.Map;
using Contracts.Robot;
using Contracts.WalkingStrategies;
using Moq;
using NUnit.Framework;
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
        public void TestWithoutEmptySpace()
        {            
            var worksParameters = new WorksParameters() { PositionState = new PositionState(1, 1, Facing.N, 100) };            

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            worksParameters.Commands = commands;

            var moqBackOffStrategies = new Mock<IBackOffStrategies>();
            var walkingStrategie = new WalkingStrategie(worksParameters, moqBackOffStrategies.Object);

            var robot = new Robot(walkingStrategie);
            var result = robot.StartClean();
            Assert.AreEqual(87, result.final.BatteryUnit);
            Assert.AreEqual(2, result.final.Coordinate.X);
            Assert.AreEqual(2, result.final.Coordinate.Y);
            Assert.AreEqual(Facing.W, result.final.Facing);
        }

        [Test]
        public void TestWithEmptySpace()
        {
            var worksParameters = new WorksParameters() { PositionState = new PositionState(3, 0, Facing.N, 80) };
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            worksParameters.map = map.ToPlaceStatuses().Matrix;

            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            worksParameters.Commands = commands;
            var moqBackOffStrategies = new Mock<IBackOffStrategies>();
            var walkingStrategie = new WalkingStrategie(worksParameters, moqBackOffStrategies.Object);

            var robot = new Robot(walkingStrategie);
            var result = robot.StartClean();
            Assert.AreEqual(54, result.final.BatteryUnit);
            Assert.AreEqual(2, result.final.Coordinate.X);
            Assert.AreEqual(0, result.final.Coordinate.Y);
            Assert.AreEqual(Facing.E, result.final.Facing);
        }        
    }
}
