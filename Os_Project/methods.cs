using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class methods
    {
        internal static void getToFunction(List<string> inputTokens)
        {
            if (inputTokens[0]=="cls")
            {
                Shell.command.clear(inputTokens);
            }
            if (inputTokens[0]=="help")
            {
                Shell.command.help(inputTokens);
            }
            if (inputTokens[0]=="quit")
            {
                Shell.command.exit(inputTokens);
            }

        }
        public static List<string> tokenize(string input)
        {
            
            List<string> tokens = new List<string>();
            tokens = input.Split().ToList();
            return tokens;
        }
    }
}
