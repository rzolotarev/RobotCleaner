using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.FileReaders.JsonView
{
    public class Input
    {
        [JsonProperty("map")]
        public string[,] Map { get; set; }
        [JsonProperty("start")]
        public Start Start { get; set; }
        [JsonProperty("commands")]
        public string[] Commands { get; set; }
        [JsonProperty("battery")]
        public int Battery { get; set; }
    }
}
