using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Settings
{
    public class BackOffSettings
    {
        public string BackOffStrategiesTag = "BackOffStrategies";

        public string Strategies { get; set; }
    }
}
