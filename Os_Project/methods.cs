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
            else if (inputTokens[0]=="help")
            {
                Shell.command.help(inputTokens);
            }
            else if (inputTokens[0]=="quit")
            {
                Shell.command.exit(inputTokens);
            }
            else if (inputTokens[0]=="cd")
            {
                Shell.command.cd(inputTokens);
            }
            else if (inputTokens[0]=="dir")
            {
                Shell.command.dir(inputTokens);
            }
            else if (inputTokens[0]=="copy")
            {
                Shell.command.copy(inputTokens);
            }
            else if (inputTokens[0]=="del")
            {
                Shell.command.del(inputTokens);
            }
            else if (inputTokens[0]=="md")
            {
                Shell.command.md(inputTokens);
            }
            else if (inputTokens[0]=="rd")
            {
                Shell.command.rd(inputTokens);
            }
            else if (inputTokens[0] =="pwd")
            {
                Shell.command.pwd(inputTokens);
            }
            else if (inputTokens[0] == "rename")
            {
                Shell.command.rename(inputTokens);
            }
            else if (inputTokens[0]=="import")
            {
                Shell.command.Import(inputTokens);
            }
            else if (inputTokens[0]=="export")
            {
                Shell.command.Export(inputTokens);
            }
            else if (inputTokens[0] == "type")
            {
                Shell.command.type(inputTokens);
            }
            else
            {
                Console.WriteLine("Unkown command, use help to know supported commands");
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
