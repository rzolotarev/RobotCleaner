using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Map
{
    public interface IMapProvider
    {
        WorksParameters Read(string source);
    }
}
