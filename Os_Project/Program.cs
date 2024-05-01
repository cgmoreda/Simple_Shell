using System.Xml;

namespace Os_Project
{
    internal class Program
    {
        static string path = "root";
        static void Main(string[] args)
        {
            Console.Clear();
            while(true)
            {
                virtualDisk.initialize();
                fatTable.printFatTable();
                Console.Write(path+">");
                string input=Console.ReadLine();
                List<string> inputTokens= methods.tokenize(input);
                if (inputTokens[0]=="cls")
                {
                    command.clear(inputTokens);
                }
                if (inputTokens[0]=="help")
                {
                    command.help(inputTokens);
                }
                if (inputTokens[0]=="quit")
                {
                    command.exit(inputTokens);
                }
            }
        }
       
    }
}
