using Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public class WorksParameters
    {
        public PlaceStatus[,] map;
        public PositionStateManager PositionState;
        public LinkedList<Instructions> Commands;        
    }
}
