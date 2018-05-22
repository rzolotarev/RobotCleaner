using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robots
{
    public class FinalResult
    {
        public int Battery { get; set; }

        public FinalState FinalState { get; set; }
        public List<Coordinate> Visited { get; set; }
        public List<Coordinate> Cleaned { get; set; }
    }
}
