using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1Week
{
    internal class PrimeNumber
    {
        static void Main(string[] args)
        {       
            // Girilen sayının asal olup olmadığını gösteren program
            bool prime = true;
            Console.Write("Sayı Giriniz: ");
            int sayi = int.Parse(Console.ReadLine());
            for (int i = 2; i < sayi; i++) //2den başlayarak kendi sahil olmayacak şekilde bölümünü ölçmek için döngü
            {
                if (sayi % i == 0) // eğer bu sayılardan birine bölünüyor ise asal değildir.
                {
                    prime = false;
                    break;
                }
            }
                Console.WriteLine(prime == true ? "Girilen Sayı Asal": "Girilen Sayı Asal Değil");
            Console.ReadLine();
        }
    }
}
