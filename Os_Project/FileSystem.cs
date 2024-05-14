﻿using System;
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
        static Directory root;
        public static void init()
        {
            if (fatTable.getValue(5) == 0)
            {
                root = new Directory("root", 0, 0, 5, null);
                fatTable.setValue(5, -1);
            }
            else
            {
                root= new Directory("root", 0, 0, 5, null);
                root.ReadDirectory();
            }
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
            if (path.Substring(0, 5)=="root:")
            {
                currentDirectory = root;
                
            }
            else
            {
                Directory d = (Directory)currentDirectory.getDirectory(path);
                if (d != null)
                    currentDirectory = d;
                else
                    Console.WriteLine("Directory not found");
            }
        }
    }
}
