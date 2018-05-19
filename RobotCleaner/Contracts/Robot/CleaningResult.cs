﻿using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robot
{
    public class CleaningResult
    {
        public PositionState final { get; set; }
        public List<Coordinate> Visited { get; set; }
        public List<Coordinate> Cleaned { get; set; }
    }
}
