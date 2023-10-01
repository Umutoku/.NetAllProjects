using System;
using System.Collections.Generic;
namespace asalSayi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int AsalSayac = 1;
            bool AsalDegil = false;
            while (AsalSayac < 10000) 
            {
                AsalSayac++;
                for (int i = 2; i < AsalSayac; i++)  
                {
                    if (AsalSayac % i == 0)  
                    {
                        AsalDegil = true;        
                         
                    }
                }
                if (AsalDegil == false)   
                {
                    Console.WriteLine(AsalSayac);   
                }
                AsalDegil = false;
            }
        }
    }
}
