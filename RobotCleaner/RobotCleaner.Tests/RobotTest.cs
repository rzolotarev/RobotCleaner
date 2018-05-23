using Contracts.Extensions;
using Contracts.InstructionExecutors;
using Contracts.Map;
using Contracts.Robots;
using NUnit.Framework;
using Services.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Resolution;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class RobotTest
    {        
        [Test]
        public void CleaningWithoutEmptySpace()
        {
            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.X = 1;
            positionState.Y = 1;
            positionState.Facing = Facing.N;
            positionState.LeftBattery = 100;
            positionState.SetMap(newMap);            
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

          
            var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", commands));         
            var result = robot.StartClean();
            Assert.AreEqual(87, result.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("W", result.FinalState.Facing);
        }

        [Test]
        public void Test1WithEmptySpace()
        {
            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.X = 3;
            positionState.Y = 0;
            positionState.Facing = Facing.N;
            positionState.LeftBattery = 80;
            positionState.SetMap(newMap);
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
            var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", commands));
            var result = robot.StartClean();
            Assert.AreEqual(54, result.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(0, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void Test2WithEmptySpace()
        {
            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "C", "S" },
                                         { "S", "S", "S", "S" }, {"S", "Null", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.X = 3;
            positionState.Y = 1;
            positionState.Facing = Facing.S;
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

            
            var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", commands));
            var result = robot.StartClean();
            Assert.AreEqual(1040, result.Battery);
            Assert.AreEqual(3, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void BatteryIsRunOut()
        {
            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;
            var positionState = container.Resolve<PositionState>();
            positionState.X = 1;
            positionState.Y = 1;
            positionState.Facing = Facing.N;
            positionState.LeftBattery = 10;
            positionState.SetMap(newMap);
            
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);            
     
            var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", commands));
            var result = robot.StartClean();
            Assert.AreEqual(4, result.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("E", result.FinalState.Facing);
        }

        [Test]
        public void BatteryIsRunOutWithZero()
        {

            var container = Container.BuildContainer();
            var map = new string[4, 4] { { "S", "S", "S", "S" },{ "S", "S", "S", "S" },
                                         { "S", "S", "S", "S" }, {"S", "S", "S", "S"} };
            var newMap = map.ToPlaceStatuses().Matrix;

            var positionState = container.Resolve<PositionState>();
            positionState.X = 1;
            positionState.Y = 1;
            positionState.Facing = Facing.N;
            positionState.LeftBattery = 12;
            positionState.SetMap(newMap);
                        
            var commands = new LinkedList<Contracts.Commands.Instructions>();
            commands.AddLast(Contracts.Commands.Instructions.B);
            commands.AddLast(Contracts.Commands.Instructions.TR);
            commands.AddLast(Contracts.Commands.Instructions.A);
            commands.AddLast(Contracts.Commands.Instructions.C);
            commands.AddLast(Contracts.Commands.Instructions.TL);
            commands.AddLast(Contracts.Commands.Instructions.TL);

            var robot = container.Resolve<IMachineCleaner>(new ParameterOverride("instructions", commands));
            var result = robot.StartClean();
            Assert.AreEqual(0, result.Battery);
            Assert.AreEqual(2, result.FinalState.X);
            Assert.AreEqual(2, result.FinalState.Y);
            Assert.AreEqual("N", result.FinalState.Facing);
        }
    }
}
