using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1Week
{
     class PrimeNumberTenThousand
    {
        static void Main(string[] args)
        {
            int counter = 0;
            for (int i = 2; i < 10000; i++)// 1 ve kendisi dışında bir sayıya bölünmemesi lazım.
            {
                bool prime = true;
                for (int j = 2; j < i; j++)// Bu demek oluyor ki döngü asla bu iki sayıya ulaşmamalı
                {
                    if (i % j == 0)
                    {
                        prime = false;
                        break;
                    }
                }
                if (prime)
                {
                    counter++;
                    //Console.WriteLine(i); //Eğer asal sayıları yazdırmak istersek
                }
            }
            Console.WriteLine($"2 ile 10000 arasında {counter} adet asal sayı vardır.");
            Console.ReadLine();
        }
    }
}
