using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Os_Project
{
    internal class methods
    {
        public static List<string> tokenize(string input)
        {
            
            List<string> tokens = new List<string>();
            tokens = input.Split().ToList();
            return tokens;
        }
    }
}
