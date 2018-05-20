﻿using Contracts.Extensions;
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
            
            var backOffStrategies = new BackOffStrategies(positionState);
            var walkingStrategie = new WalkingStrategie(positionState);
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = walkingStrategie.RunCommand(currentCommand.Value);
                if (!isSuccessful)
                {
                    var backOffSuccessful = backOffStrategies.RunCommands();
                    if (!backOffSuccessful)
                        break;
                }

                currentCommand = currentCommand.Next;
            }

            var visited = positionState.Visited;
            var cleaned = positionState.Cleaned;
            Assert.AreEqual(54, positionState.BatteryUnit);
            Assert.AreEqual(2, positionState.Coordinate.X);
            Assert.AreEqual(0, positionState.Coordinate.Y);
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

            var backOffStrategies = new BackOffStrategies(positionState);
            var walkingStrategie = new WalkingStrategie(positionState);
            var currentCommand = commands.First;
            while (currentCommand != null)
            {
                var isSuccessful = walkingStrategie.RunCommand(currentCommand.Value);
                if (!isSuccessful)
                {
                    var backOffSuccessful = backOffStrategies.RunCommands();
                    if (!backOffSuccessful)
                        break;
                }

                currentCommand = currentCommand.Next;
            }

            var actulaVisited = positionState.Visited;
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
            var actualCleaned = positionState.Cleaned;
            Assert.AreEqual(expectedCleaned.Count, actualCleaned.Count);
            actualCleaned.ForEach(a => Assert.That(expectedCleaned.Contains(a)));

            Assert.AreEqual(1040, positionState.BatteryUnit);
            Assert.AreEqual(3, positionState.Coordinate.X);
            Assert.AreEqual(2, positionState.Coordinate.Y);
            Assert.AreEqual(Facing.E, positionState.Facing);
        }  
    }
}
