using Contracts.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.WalkingStrategies
{
    public interface IWalkingStrategie
    {
        bool TryRunCommand();
        CleaningResult GetResult();
    }
}
