using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project.Shell
{
    static internal class command
    {

        
        internal static void clear(List<string> args)
        {
            Console.Clear();
        }
        internal static void help(List<string> args)
        {


            string command;
            if (args.Count < 2)
            {
                command = "defualt";
            }
            else
                command = args[1];
            switch (command)
            {
                case "cd":
                    Console.WriteLine("cd - Change the current default directory to <directory>.");
                    Console.WriteLine("    If the directory is not present, an appropriate error should be reported.");
                    break;
                case "cls":
                    Console.WriteLine("cls - Clear the screen.");
                    break;
                case "dir":
                    Console.WriteLine("dir - List the contents of <directory>.");
                    break;
                case "quit":
                    Console.WriteLine("quit - Quit the shell.");
                    break;
                case "copy":
                    Console.WriteLine("copy - Copies one or more files to another location.");
                    break;
                case "del":
                    Console.WriteLine("del - Deletes one or more files.");
                    break;
                case "help":
                    Console.WriteLine("help - Provides Help information for commands.");
                    break;
                case "md":
                    Console.WriteLine("md - Creates a directory.");
                    break;
                case "rd":
                    Console.WriteLine("rd - Removes a directory.");
                    break;
                case "rename":
                    Console.WriteLine("rename - Renames a file.");
                    break;
                case "type":
                    Console.WriteLine("type - Displays the contents of a text file.");
                    break;
                default:
                    Console.WriteLine("cd - Change the current default directory to <directory>.");
                    Console.WriteLine("    If the directory is not present, an appropriate error should be reported.");
                    Console.WriteLine("cls - Clear the screen.");
                    Console.WriteLine("dir - List the contents of <directory>.");
                    Console.WriteLine("quit - Quit the shell.");
                    Console.WriteLine("copy - Copies one or more files to another location.");
                    Console.WriteLine("del - Deletes one or more files.");
                    Console.WriteLine("help - Provides Help information for commands.");
                    Console.WriteLine("md - Creates a directory.");
                    Console.WriteLine("rd - Removes a directory.");
                    Console.WriteLine("rename - Renames a file.");
                    Console.WriteLine("type - Displays the contents of a text file.");
                    break;
            }
        }
        internal static void exit(List<string> args)
        {
            Environment.Exit(0);
        }

    }
}
