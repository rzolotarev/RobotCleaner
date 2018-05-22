using Contracts.FileWriters;
using Contracts.Robots;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FileWriters
{
    public class FileWriter : IFileWriter
    {
        private readonly FinalResult _finalResult;

        public FileWriter(FinalResult finalResult)
        {
            _finalResult = finalResult;
        }

        public void Save(string destPathToFile)
        {                               
            using (StreamWriter file = File.CreateText(destPathToFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _finalResult);
            }
        }
    }
}
