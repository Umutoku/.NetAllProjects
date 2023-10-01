using System;
using System.Collections.Generic;

namespace usingDelegate
{
     class Program
    {
        static List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        static void Main(string[] args)
        {
            /*
             *yukarıdaki numbers isimli array içindeki sayıları programcının dilediği gibi filtrelesin istiyoruz. 
             * 
             * 
             */
        }

        static List<int> filter(List<int>values)
        {
            List<int> filtered = new List<int>();
            foreach(var item in values)
            {
                if(isEven(item))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
        static bool isEven(int item)
        {
            return item%2==0;
        }
    }
}
