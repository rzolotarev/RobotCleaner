using Contracts.Extensions;
using Contracts.Map;
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
    public class BackOffStrategieTest
    {
        [Test]
        public void TestBackOffStrategie()
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
            
            var backOffStrategies = new BackOffStrategies(positionState);
            var walkingStrategie = new WalkingStrategie(positionState);
            var currentCommand = commands.First;
            while (currentCommand != null )
            {
                var isSuccessful = walkingStrategie.RunCommand(currentCommand.Value);
                if (!isSuccessful)
                {
                    var backOffSuccessful = backOffStrategies.RunCommands();
                }
                else
                    currentCommand = currentCommand.Next;
            }

            Assert.AreEqual(54, positionState.BatteryUnit);
            Assert.AreEqual(2, positionState.Coordinate.X);
            Assert.AreEqual(0, positionState.Coordinate.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }
    }
}
