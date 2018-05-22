using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robots
{
    public class FinalState
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Facing { get; set; }
        
        public FinalState(int x, int y, string facing)
        {
            X = x;
            Y = y;
            Facing = facing;            
        }
    }
}
