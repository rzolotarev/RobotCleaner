using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.FileReaders
{
    public class WorksParameters
    {
        public string[,] Map { get; set; }
        public PositionState PositionState { get; set; }
        public LinkedList<Instructions> Commands { get; set; }
    }
}
