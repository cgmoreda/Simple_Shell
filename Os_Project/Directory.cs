using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class Directory:directoryEntry
    {
        List<Directory> directories;
        Directory parent;
        public Directory(){}
        public Directory(string name, int size, int firstCluster, int attribute, Directory parent):base(name, (byte)attribute, size, firstCluster,parent)
        {
            directories = new List<Directory>();
        }
        public Directory(byte[] data, Directory parent):base(data)
        {
            this.parent = parent;
            directories = new List<Directory>();
        }
        public void ReadDirectory()
        {
            byte[] data = virtualDisk.readBlock(firstCluster);
            directoryEntry x =byteToData(data);
            this.name = x.name;
            this.size = x.size;
            this.emptyData = x.emptyData;
            this.firstCluster = x.firstCluster;
            this.attribute = x.attribute;
            var fc = firstCluster;
            while(fc!=-1)
            {
                fc = fatTable.getValue(fc);
                data = virtualDisk.readBlock(fc);
                x = byteToData(data);
                Directory d = new Directory(x.name, x.size, x.firstCluster, x.attribute, this);
                directories.Add(d);
            }

        }
        public void writeDirectory()
        {
            byte[] data = dataToByte();
            virtualDisk.writeBlock(data, firstCluster);

            int fc;
            int nc = firstCluster;
            foreach (var d in directories)
            {
             
                fc = fatTable.getAvailableBlock();
                fatTable.setValue(nc, fc);
                fatTable.setValue(fc,-1);
                nc = fc;
                data = d.dataToByte();
                virtualDisk.writeBlock(data, fc);
            }
        }
        public void addDirectory(Directory d)
        {
            directories.Add(d);
        }
        public void removeDirectory(Directory d)
        {
            d.deleteDirectory();
            directories.Remove(d);
        }
        public void deleteDirectory()
        {
            fatTable.clearFatAt(firstCluster);
            foreach (var d in directories)
                d.deleteDirectory();
            directories.Clear();
        }
        public Directory getDirectory(string name)
        {
            return directories.Find(x => x.name == name);
        }


    }
}
