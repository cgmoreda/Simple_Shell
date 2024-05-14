using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class File : directoryEntry
    {
        string content { get; set; }
        public File() { }
        public File(string name, int size, int firstCluster, int attribute, Directory parent, string content) : base(name, (byte)attribute, size, firstCluster, parent)
        {
            this.content=content;
            WriteFile();
        }
        public File(byte[] data, Directory parent) : base(data)
        {
        }
        public void ReadFile()
        {
            byte[] data = virtualDisk.readBlock(firstCluster);
            directoryEntry x = byteToData(data);
            this.name = x.name;
            this.size = x.size;
            this.emptyData = x.emptyData;
            this.firstCluster = x.firstCluster;
            this.attribute = x.attribute;
            ReadFileData();
        }
        public void WriteFile()
        {
            byte[] data = dataToByte();
            virtualDisk.writeBlock(data, firstCluster);
            WriteFileData();
        }
        public void ReadFileData()
        {
            int fc = fatTable.getValue(firstCluster);
            content ="";
            while (fc != -1)
            {
                byte[] data = virtualDisk.readBlock(fc);
                content += Encoding.ASCII.GetString(data);
                fc = fatTable.getValue(fc);
            }
        }
        public void WriteFileData()
        {
            int fc;
            int nc =fatTable.getValue(firstCluster);
            int i = 0;
            while (i < content.Length)
            {
                byte[] data = new byte[1024];
                Encoding.ASCII.GetBytes(content.Substring(i, Math.Min(1024, content.Length - i))).CopyTo(data, 0);
                if (nc == -1)
                {
                    fc = fatTable.getAvailableBlock();
                    fatTable.setValue(nc, fc);
                    fatTable.setValue(fc, -1);
                    nc = fc;
                }
                else
                {
                    fc = fatTable.getValue(nc);
                }
                virtualDisk.writeBlock(data, fc);
                i += 1024;
            }
        }
        public void deleteFile()
        {
            fatTable.clearFatAt(firstCluster);
        }

    }
}
