using System.Xml;

namespace Os_Project
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Clear();

            virtualDisk.initialize();
            FileSystem.Init();
            while (true)
            {
               // fatTable.printFatTable();
                Console.Write(FileSystem.GetCurrentPath()+">");
                string input=Console.ReadLine();
                List<string> inputTokens= methods.tokenize(input);
                methods.getToFunction(inputTokens);
            }
        }
       
    }
}
