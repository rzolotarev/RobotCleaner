using Contracts.Extensions;
using Contracts.Map;
using NUnit.Framework;
using Services.Container;
using Unity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.InstructionExecutors;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class Result1Json
    {
        [Test]
        public void TestPosition()
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
    }
}
