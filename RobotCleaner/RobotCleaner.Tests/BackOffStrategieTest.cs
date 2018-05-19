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
            var backOffStrategies = new BackOffStrategies(worksParameters.PositionState);
            var walkingStrategie = new WalkingStrategie(worksParameters, backOffStrategies);
            var currentCommand = commands.First;
            while (currentCommand != null )
            {
                walkingStrategie.TryRunCommand();
            }
        }
    }
}
