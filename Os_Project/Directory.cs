using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class Directory : directoryEntry
    {
        List<directoryEntry> ?directories;

        public Directory()
        {
            base.parent = null;
            directories = new List<directoryEntry>();
        }
        public Directory(string name, int size, int firstCluster, int attribute, Directory parent) : base(name, (byte)attribute, size, firstCluster, parent)
        {
            directories = new List<directoryEntry>();
        }
        public Directory(byte[] data, Directory parent) : base(data, parent)
        {
            directories = new List<directoryEntry>();
        }
        public Directory(directoryEntry dir)
        {
            this.parent = dir.parent;
            this.name = dir.name;
            this.size = dir.size;
            this.emptyData = dir.emptyData;
            this.firstCluster = dir.firstCluster;
            this.attribute = dir.attribute;
            directories = new List<directoryEntry>();

        }
        public void readDirectory()
        {
            int maxEntriesPerBlock = 1024 / 32;

            var currentCluster = firstCluster;
            directories.Clear();
            while (currentCluster != -1)
            {
                byte[] blockData = virtualDisk.readBlock(currentCluster);

                for (int i = 0; i < maxEntriesPerBlock; i++)
                {
                    byte[] entryData = new byte[32];
                    Array.Copy(blockData, i * 32, entryData, 0, 32);


                    if (entryData[0]=='#')
                    {
                        break;
                    }
                    directoryEntry entry = new directoryEntry(entryData, this);
                    directories.Add(entry);
                }
                currentCluster = fatTable.getValue(currentCluster);
            }

        }


        public void writeDirectory()
        {
            int maxEntriesPerBlock = 1024 / 32;

            byte[] blockData = new byte[1024];
            for (int i = 0; i < 1024; i++)
            {
                blockData[i] = (byte)'#';
            }
            int entryIndex = 0; // Track the index of the current directory entry in the block
            var currentCluster = firstCluster;
            var PrevCluster = -1;
            fatTable.clearFatAt(firstCluster);
            fatTable.setValue(firstCluster, -1);
            // Write directory entries to the block
            foreach (var directory in directories)
            {
                byte[] entryData = directory.dataToByte();
                int offset = entryIndex * 32;
                Array.Copy(entryData, 0, blockData, offset, 32);
                entryIndex++;

                if (entryIndex >= maxEntriesPerBlock)
                {
                    if (PrevCluster!=-1)
                    {
                        fatTable.setValue(PrevCluster, currentCluster);
                        fatTable.setValue(currentCluster, -1);
                    }
                    virtualDisk.writeBlock(blockData, currentCluster);
                    blockData = new byte[1024];
                    for (int i = 0; i < 1024; i++)
                    {
                        blockData[i] = (byte)'#';
                    }
                    entryIndex = 0;
                    PrevCluster = currentCluster;
                    currentCluster = fatTable.getAvailableBlock();

                }
            }

            if (entryIndex > 0)
            {
                virtualDisk.writeBlock(blockData, currentCluster);
                fatTable.setValue(currentCluster, -1);
            }

        }


        public void addDirectory(directoryEntry d)
        {
            directories.Add(d);
            writeDirectory();
        }

        public void addDirectory(string name)
        {
            Directory temp = new Directory(name, 0, 0, 0, this);
            directories.Add(temp);
            writeDirectory();
        }
        public void removeDirectory(Directory d)
        {
            d.deleteDirectory();
            directories.Remove(d);
            writeDirectory();
        }
        internal void removeDirectory(string name)
        {
            directoryEntry d = directories.Find(x => x.name == name);
            Directory D = new Directory(d);
            D.deleteDirectory();
            directories.Remove(d);

            writeDirectory();
        }
        public void deleteDirectory()
        {
            fatTable.clearFatAt(firstCluster);
            foreach (var d in directories)
            {
                if (d.attribute==0)
                {
                    Directory x = (Directory)d;
                    x.deleteDirectory();
                }
            }
            directories.Clear();
            parent.writeDirectory();
        }
        public directoryEntry getDirectory(string name)
        {
            return directories.Find(x => x.name == name);
        }
        public bool addFile(string name, string content)
        {
            if (DirectoryExists(name))
                return false;

            directories.Add(new FFile(name, content.Length, fatTable.getAvailableBlock(), 1, this, content));
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
            if (parent!=null)
                return parent.getFullPath()+"/"+name;
            return name+":";
        }

        internal List<string> getEntriesNames()
        {
            List<string> ret = new List<string>();
            foreach (var d in directories)
            {
                ret.Add(d.name);
            }
            return ret;
        }

        internal List<directoryEntry> getEntries()
        {
            return directories;
        }

        internal int Search(string fileName)
        {
            for (int i = 0; i < directories.Count; i++)
            {
                if (directories[i].name == fileName)
                    return i;
            }
            return -1;
        }
    }
}
