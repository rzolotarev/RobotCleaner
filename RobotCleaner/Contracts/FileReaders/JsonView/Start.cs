using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.FileReaders.JsonView
{
    public class Start
    {        
        public int X { get; set; }
        public int Y { get; set; }
        [JsonProperty("facing")]
        public string Facing { get; set; }
    }
}
