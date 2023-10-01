using System;

namespace HomeWork1Week
{
    internal class PercentCalculator
    {
        static void Main(string[] args)
        {
            /*
             * Ödev-1
                Console üzerinden girilen sayının yüzdesini bulmayı sağlayan program
                    1. Sayı = 100
                    2. Yüzde = %20
                    Sonuç = 20
             */
            int num, percent;
            
             Console.WriteLine("Lütfen bir sayı giriniz.");
            num = int.Parse(Console.ReadLine());
            Console.WriteLine("Lütfen bir yüzde giriniz.");
            percent = int.Parse(Console.ReadLine());
            double sonuc = ((num * percent) / 100); //Sayının yüzde ile çarpımı ve 100'e bölümü
            Console.WriteLine(sonuc);
        }
    }
}
