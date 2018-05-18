using Contracts.Map;
using Contracts.Robot;
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
            var worksParameters = new WorksParameters();
            worksParameters.PositionState = new PositionState(1, 1, Facing.N, 100);
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            worksParameters.Commands = commands;
            var walkingStrategie = new WalkingStrategie(worksParameters);
            var robot = new Robot(worksParameters.PositionState, walkingStrategie);
            var result = robot.StartClean();
            Assert.AreEqual(88, result.batteryUnit);
        }
    }
}
