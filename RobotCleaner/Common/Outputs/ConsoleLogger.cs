using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Outputs
{
    public static class ConsoleLogger
    {
        public static void OutputMethod(string message)
        {            
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteDiagnosticInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            OutputMethod(message);
        }

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            OutputMethod(message);
        }

        public static void WriteInstructions(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            OutputMethod(message);
        }

        public static void WriteSuccessInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            OutputMethod(message);
        }
    }
}

