using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robot
{
    public class FinalResult
    {
        public FinalState FinalState { get; set; }
        public List<Coordinate> Visited { get; set; }
        public List<Coordinate> Cleaned { get; set; }
    }
}
