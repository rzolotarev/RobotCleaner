﻿using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Extensions
{
    public class MatrixConvertionResult
    {
        public bool IsSucceed { get; set; }
        public PlaceStatus[,] Matrix { get; set; }
    }
}
