using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class fatTable
    {
        private static int[] fatTableArray = new int[1024];
        internal static void initialize()
        {

            Array.Fill(fatTableArray, 0, 5, 1019);
            fatTableArray[0] = -1;
            fatTableArray[1] = 2;
            fatTableArray[2] = 3;
            fatTableArray[3] = 4;
            fatTableArray[4] = -1;

            writeFatTable();
        }

        internal static void readFatTable()
        {
            for (int i = 1; i<=4; i++)
            {
                byte[] data = virtualDisk.readBlock(1);
                Buffer.BlockCopy(data, 0, fatTableArray, 1024*(i-1), 1024);
            }
        }
        internal static void writeFatTable()
        {
            for (int i = 1; i<=4; i++)
            {
                Byte[] fatTableBytes = new Byte[1024*4];
                Buffer.BlockCopy(fatTableArray, 1024*(i-1), fatTableBytes, 0, 1024);
                virtualDisk.writeBlock(fatTableBytes, i);
            }
        }
        internal static void printFatTable(int l = 0, int r = 10)
        {
            for (int i = l; i <= r; i++)
                Console.WriteLine($"Fat[{i}] => {fatTableArray[i]}");

        }
        internal static List<int> getChain(int idx)
        {
            List<int> chain = new List<int> { idx };

            while (fatTableArray[idx] != -1)
            {
                idx = fatTableArray[idx];
                chain.Add(idx);
            }
            return chain;
        }

        internal static int getAvailableBlock()
        {
            return Array.FindIndex(fatTableArray, x => x == 0);
        }
        internal static int getValue(int idx)
        {
            return fatTableArray[idx];
        }
        internal static void setValue(int idx, int val)
        {
            fatTableArray[idx] = val;
            writeFatTable();
        }
        internal static int getNumberOfFreeBlocks()
        {
            return fatTableArray.Count(x => x == 0);
        }
        public static int getFreeSpace()
        {
            return getNumberOfFreeBlocks() * 1024;
        }
        public static void clearFatAt(int idx)
        {
            List<int> chain = getChain(idx);
            foreach (var x in chain)
            {
                fatTableArray[x] = 0;
            }
            writeFatTable();
        }
    }
}
