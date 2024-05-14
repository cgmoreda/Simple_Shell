using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class directoryEntry
    {
        public string name { get; set; }// 11 byte [0,11]
        public int size { get; set; }// 4 byte =>[13,16]
        public byte[] emptyData { get; set; }//12 byte[21,32]
        public int firstCluster { get; set; }//4 byte [17,20]
        public byte attribute { get; set; }// 1 byte =>[12,12]  0=>folder 1=>file
        Directory parent;
        public directoryEntry() { }
        public directoryEntry(string Name, byte Attribute, int Size, int FirstCluster, Directory parent)
        {
            name = Name;
            attribute = Attribute;
            size = Size;
            emptyData = new byte[12];
            firstCluster = FirstCluster;
            this.parent=parent;
        }
        public directoryEntry(string Name, byte Attribute, int Size, int FirstCluster)
        {
            name = Name;
            attribute = Attribute;
            size = Size;
            firstCluster = FirstCluster;
        }

        public directoryEntry(byte[] data)
        {
            directoryEntry x = byteToData(data);
            name = x.name;
            size = x.size;
            emptyData = x.emptyData;
            firstCluster = x.firstCluster;
            attribute = x.attribute;
        }
        public byte[] dataToByte()
        {
            byte[] ret = new byte[32];

            string Name = reformat(name);

            Encoding.ASCII.GetBytes(Name).CopyTo(ret,0);
            ret[12] = attribute;
            BitConverter.GetBytes(size).CopyTo(ret,13);
            BitConverter.GetBytes(firstCluster).CopyTo(ret, 17);
            if (emptyData != null && emptyData.Length <= ret.Length - 21)
            {
                emptyData.CopyTo(ret, 21);
            }
            return ret;
        }

        public directoryEntry byteToData(byte[] data)
        {
            return new directoryEntry
            {

                name = Encoding.ASCII.GetString(data, 0, 11),
                attribute = data[12],
                size = BitConverter.ToInt32(data, 13),
                firstCluster = BitConverter.ToInt32(data, 17),
            };
        }


        public string reformat(string filename)
        {
            const int targetLength = 11;

            if (filename.Length == targetLength)
            {
                return filename;
            }
            else if (filename.Length < targetLength)
            {
                return filename.PadRight(targetLength);
            }
            else
            {
                
                int dotIndex = filename.LastIndexOf('.');
                if (dotIndex == -1 )
                {
                    return filename.Substring(0, targetLength);
                }
                else
                {

                    string nameWithoutExtension = filename.Substring(0, dotIndex);
                    string extension = filename.Substring(dotIndex + 1);
                    int charactersToTake = targetLength - extension.Length - 1;
                    if (extension.Length>=10)
                    {
                        return "Invalid Name";
                    }
                    return nameWithoutExtension.Substring(nameWithoutExtension.Length - charactersToTake) + "." + extension;
                   
                }
            }
        }

    }
}
