﻿using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Robots
{
    public interface IMachineCleaner
    {
        FinalResult StartClean();
    }
}
