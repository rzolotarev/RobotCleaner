using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class Check
    {
        public static void That(bool condition, string message)
        {
            if (!condition)
                throw new CheckException(message);
        }

        private class CheckException : Exception
        {
            public CheckException(string message) : base(message)
            {

            }
        }
    }
}
