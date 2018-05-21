using Contracts.FileReaders.JsonView;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCleaner.Tests
{
    [TestFixture]
    public class ReadFileTest
    {
        [Test]
        public void ReadFile()
        {
            var path = "test1.json";
            var json = File.ReadAllText(Path.Combine("C:",path));
            var info = JsonConvert.DeserializeObject<Input>(json);
            Assert.AreEqual(info.Battery,80);
        }
    }
}
