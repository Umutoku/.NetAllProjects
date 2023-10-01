using System;

namespace ArraysAndCollections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] shoppingList = new string[] { "Elma", "Salatalık", "Domates" };
            Console.WriteLine($"Array içindeki toplam eleman {shoppingList.Length}");
            Console.WriteLine($"Array içindeki son eleman {shoppingList[shoppingList.Length-1]}");

            //Elemanların kaç adet olacağını biliyorsam:
            string[] participants = new string[38];
            participants[0] = "Abdullah Halit";
            participants[37] = "Umut Oku";

            //42
            //Kırk İki

            string[] birler = { "", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz" };
            string[] onlar = { "", "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };

            Console.WriteLine("1 ile 100 arasında bir sayı giriniz");
            int number = int.Parse(Console.ReadLine());
            int onlarbasamagi = number / 10;
            int birlerbasamagi=number % 10;

            Console.WriteLine($"{onlar[onlarbasamagi]}{birler[birlerbasamagi]}");
        }
    }
}
