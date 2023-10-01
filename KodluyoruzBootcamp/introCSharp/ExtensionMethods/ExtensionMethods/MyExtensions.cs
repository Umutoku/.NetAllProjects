using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static double GetSquare(this int value)
        {
            return Math.Pow(value,2);
        }
    }
}
