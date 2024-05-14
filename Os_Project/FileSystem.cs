using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal static class FileSystem
    {
        public static Directory currentDirectory;
        private static Directory root;
        public static void Init()
        {
            root = new Directory("root", 0, 5, 0, null);
            if (fatTable.getValue(5) == 0)
            {
                fatTable.setValue(5, -1);
            }
            root.readDirectory();
            currentDirectory = root;
        }
        public static void CreateFile(string name, string content, bool forceWrite = false)
        {

            if (currentDirectory.addFile(name, content))
            {
                Console.WriteLine("File Created Successfully");
            }
            else if (forceWrite)
            {
                currentDirectory.removeFile(name);
                currentDirectory.addFile(name, content);
                Console.WriteLine("File Replaced Successfully");
            }
            else
                Console.WriteLine("File already exists");
        }
        public static void DeleteFile(string name)
        {
            currentDirectory.removeFile(name);
        }
        public static void CreateDirectory(string name)
        {
            currentDirectory.addDirectory(name);
        }
        public static void DeleteDirectory(string name)
        {
            currentDirectory.removeDirectory(name);
        }
        public static string GetCurrentPath()
        {
            return currentDirectory.getFullPath();
        }
        public static void ChangeDirectory(string path)
        {
            if (path.Length>=5&&path.Substring(0, 5)=="root:")
            {
                currentDirectory = root;
            }
            else
            {
                directoryEntry dirTo = currentDirectory.getDirectory(path);
                if(dirTo == null)
                {
                    Console.WriteLine("Does Not Exist");
                }
                else if(dirTo.attribute==1)
                {
                       Console.WriteLine("Not a Directory");
                }
                else
                {
                    Directory DIR = new Directory(dirTo);
                    currentDirectory = DIR;
                }
            }
        }
    }
}
