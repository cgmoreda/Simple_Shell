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
            fatTableArray[0] = -1;
            fatTableArray[1] = 2;
            fatTableArray[2] = 3;
            fatTableArray[3] = 4;
            fatTableArray[4] = -1;

            Array.Fill(fatTableArray, 0, 5, 1019);
            writeFatTable();
        }

        internal static void readFatTable()
        {
            byte[] data = virtualDisk.readBlocks(1, 4);
            Buffer.BlockCopy(data, 0, fatTableArray, 0, 1024*4);
        }
        internal static void writeFatTable()
        {
            Byte[] fatTableBytes = new Byte[1024*4];
            Buffer.BlockCopy(fatTableArray, 0, fatTableBytes, 0, fatTableBytes.Length);
            virtualDisk.writeBlocks(fatTableBytes, 1, 4);
        }
        internal static void printFatTable()
        {
            for (int i = 0; i < 1024; i++)
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
