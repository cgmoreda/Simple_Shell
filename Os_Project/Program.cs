using System.Xml;
using Os_Project.Shell;

namespace Os_Project
{
    internal class Program
    {
        static string path = "root";
        static void Main(string[] args)
        {
            Console.Clear();

            virtualDisk.initialize();
            fatTable.printFatTable();
            FileSystem.Init();
            path = FileSystem.GetCurrentPath();
            while (true)
            {
                Console.Write(path+">");
                string input=Console.ReadLine();
                List<string> inputTokens= methods.tokenize(input);
              
            }
        }
       
    }
}
