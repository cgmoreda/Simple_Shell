using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class Directory:directoryEntry
    {
        List<directoryEntry> directories;
        Directory parent;
        public Directory(){}
        public Directory(string name, int size, int firstCluster, int attribute, Directory parent):base(name, (byte)attribute, size, firstCluster,parent)
        {
            directories = new List<directoryEntry>();
        }
        public Directory(byte[] data, Directory parent):base(data)
        {
            this.parent = parent;
            directories = new List<directoryEntry>();
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

        public void addDirectory(string name)
        {
            Directory temp = new Directory(name, 32, fatTable.getAvailableBlock(), 0, this);
            directories.Add(temp);
        }
        public void removeDirectory(Directory d)
        {
            d.deleteDirectory();
            directories.Remove(d);
        }
        internal void removeDirectory(string name)
        {
            Directory d = (Directory)directories.Find(x => x.name == name);
            d.deleteDirectory();
            directories.Remove(d);
        }
        public void deleteDirectory()
        {
            fatTable.clearFatAt(firstCluster);
            foreach (var d in directories){
                if(d.attribute==0)
                {
                    Directory x = (Directory)d;
                    x.deleteDirectory();
                }
            }
            directories.Clear();
        }
        public directoryEntry getDirectory(string name)
        {
            return directories.Find(x => x.name == name);
        }
        public bool addFile(string name, string content)
        {
            if(DirectoryExists(name))
                return false;

            directories.Add(new File(name, content.Length, fatTable.getAvailableBlock(), 1, this, content));
            return true;
        }
        public void removeFile(string name)
        {
            directories.Remove(directories.Find(x => x.name == name));
        }
        public bool DirectoryExists(string name)
        {
            return directories.Exists(x => x.name == name);
        }

        internal string getFullPath()
        {
            return string.Join(parent.getFullPath(),"/", name);
        }

        internal List<string> getEntries()
        {
            throw new NotImplementedException();
        }
    }
}
