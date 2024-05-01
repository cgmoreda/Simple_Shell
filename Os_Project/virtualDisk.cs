using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using file reading and writing

namespace Os_Project
{
    internal class virtualDisk
    {
        static string virtualDiskName = "virtualDisk.txt";

        public static void initialize()
        {
            if (File.Exists(virtualDiskName))
            {
                fatTable.readFatTable();
            }
            else if (!File.Exists(virtualDiskName))
            {

                FileStream fs = File.Create(virtualDiskName);

                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < 1024; i++)
                        writer.Write('0');
                    
                    for (int i = 0; i < 1024 * 4; i++)
                        writer.Write('.');
                
                    for (int i = 0; i < 1024 * 1024 - (1024 * 5); i++)
                        writer.Write('#');
                }
                fatTable.initialize();
                fs.Close();
            }

        }
        internal static void writeBlock(byte[] data, int idx)
        {
            using (FileStream fs = File.Open(virtualDiskName, FileMode.Open))
            {
                fs.Seek(idx * 1024, SeekOrigin.Begin);
                fs.Write(data, 0, 1024);
            }
        }
        internal static void writeBlocks(byte[] data, int idx,int numberOfBlocks)
        {
            using (FileStream fs = File.Open(virtualDiskName, FileMode.Open))
            {
                fs.Seek(idx * 1024, SeekOrigin.Begin);
                fs.Write(data, 0, 1024*numberOfBlocks);
            }
        }
        internal static byte[] readBlock(int idx)
        {
            byte[] data = new byte[1024];
            using (FileStream fs = File.Open(virtualDiskName, FileMode.Open))
            {
                fs.Seek(idx * 1024, SeekOrigin.Begin);
                fs.Read(data, 0, 1024);
            }
            return data;
        }

        internal static byte[] readBlocks(int idx,int numberOfBlocks)
        { 
            byte[] data = new byte[1024*numberOfBlocks];
            using (FileStream fs = File.Open(virtualDiskName, FileMode.Open))
            {
                fs.Seek(idx * 1024, SeekOrigin.Begin);
                fs.Read(data, 0, 1024*numberOfBlocks);
            }
            return data;
        }
    }
}
