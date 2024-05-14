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
            if (inputTokens[0]=="cd")
            {
                Shell.command.cd(inputTokens);
            }
            if (inputTokens[0]=="ls")
            {
                Shell.command.ls(inputTokens);
            }
            if (inputTokens[0]=="create")
            {
                Shell.command.create(inputTokens);
            }   
            if (inputTokens[0]=="dir")
            {
                Shell.command.dir(inputTokens);
            }
            if (inputTokens[0]=="copy")
            {
                Shell.command.copy(inputTokens);
            }
            if (inputTokens[0]=="del")
            {
                Shell.command.del(inputTokens);
            }
            if (inputTokens[0]=="md")
            {
                Shell.command.md(inputTokens);
            }
            if (inputTokens[0]=="rd")
            {
                Shell.command.rd(inputTokens);
            }
            if (inputTokens[0]=="rename")
            {
                Shell.command.rename(inputTokens);
            }
            if (inputTokens[0]=="type")
            { 
                Shell.command.type(inputTokens);
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
