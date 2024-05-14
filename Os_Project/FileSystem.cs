using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal static class FileSystem
    {
        static string currentPath;
        static Directory currentDirectory;
        public static void init()
        {
            if (fatTable.getValue(5) == 0)
            {

                currentDirectory = new Directory("root", 0, 0, 5, null);
                fatTable.setValue(5, -1);
            }
            else
            {
                currentDirectory = new Directory("root", 0, 0, 5, null);
                currentDirectory.ReadDirectory();
            }

        }
        public static void CreateFile(string name,string content)
        {

        }
        public static void DeleteFile(string name)
        {

        }
        public static void CreateDirectory(string name)
        {
            // Create a new directory
        }
        public static void DeleteDirectory(string name)
        {

        }
        public static string GetCurrentPath()
        {
            return "root";
        }
    }
}
