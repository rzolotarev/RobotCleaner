﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InstructionExecutors
{
    public interface IBackOffStrategiesExecutor
    {
        bool RunBackOffCommands();
    }
}
