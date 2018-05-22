using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.FileWriters
{
    public interface IFileWriter
    {
        void Save(string destPathToFile);
    }
}
