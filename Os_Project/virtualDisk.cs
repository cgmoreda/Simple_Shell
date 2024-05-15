using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // make data2 = data padded with '#' till 1024
            byte[] data2 = new byte[1024];
            for (int i = 0; i < 1024; i++)
            {
                if (i < data.Length)
                    data2[i] = data[i];
                else
                    data2[i] = (byte)'#';
            }

            using (FileStream fs = File.Open(virtualDiskName, FileMode.Open))
            {
                fs.Seek(idx * 1024, SeekOrigin.Begin);
                fs.Write(data2, 0, 1024);
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

    }       
}
