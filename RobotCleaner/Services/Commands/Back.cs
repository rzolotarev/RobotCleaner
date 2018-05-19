﻿using Contracts.Commands;
using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Commands
{
    public class Back : ICommand
    {                 
        public bool ExecuteCommand(PositionState positionState)
        {
            //TODO: вынести в конфиг и доставать оттуда
            if (positionState.GoBack())
                return positionState.ConsumeBattery(3);
            
            return false;
        }
    }
}
