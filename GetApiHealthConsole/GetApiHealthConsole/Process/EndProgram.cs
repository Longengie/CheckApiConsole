using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetApiHealthConsole.Process
{
    internal static class EndProgram
    {
        internal static void StopProgram()
        {
            Console.WriteLine("Stop Program");
            Console.ReadKey();
            Environment.Exit(1);
        }
        public static void StopProgramErrors(string processName, params string[] errors)
        {
            Console.WriteLine($"Process {processName} meet the Error when run");
            foreach (string error in errors) Console.WriteLine(error);
            Console.WriteLine("Stop Program");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
