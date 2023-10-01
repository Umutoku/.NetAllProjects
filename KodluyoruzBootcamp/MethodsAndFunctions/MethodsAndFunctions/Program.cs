using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodsAndFunctions
{
    class Program
    {
        static void Main(string[] args)
        {
            showOutput("Kodluyoruz bootcamp çok iyi");
            int[] numbers = { 1, 2, 3, 4, 5 };
            int average = getAverage(numbers.ToList());
            showOutput(average.ToString());
            Console.WriteLine(isPrime(int.Parse(Console.ReadLine())));

            List<int > first6numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> small1 = new List<int> { 1, 2, 3 };
            List<int> small2 = new List<int> { 6, 7, 8 };
            bool result1 = isSubCollection(first6numbers,small1 );
            bool result2 = isSubCollection(first6numbers, small2);
        }

        static void showOutput(string message)
        {
            Console.WriteLine(message);
        }

        static int getAverage(List<int> numbers)
        {
            int total = 0;
            foreach(var number in numbers)
            {
                total += number;
            }
            return total/numbers.Count;
        }

        static bool isPrime(int number)
        {
            bool isPrimeValue = true;
            for(int i = 2; i < number;i++)
            {
                if(number % i == 0)
                    isPrimeValue= false;
            }
            
            return isPrimeValue;
        }
        static void Main()
        {
            Console.WriteLine("Girdiğiniz sayı: "+isPrime(int.Parse(Console.ReadLine())));
        }

        static void primeNumbersOnValue(int value)
        {
            for(int i=0; i<value;i++)
                if(isPrime(i))
                {
                    Console.WriteLine(i.ToString());
                }
        }

        static int IndexOf(string word, char letter)
        {
            for(int index =0; index < word.Length;index++)
            {
                if(word[index] == letter)
                    return index;
            }
            return -1;

        }

        static int IndexOf(string word,string anotherword)
        {
            return 0;
        }

        static bool isSubCollection(List<int> main,List<int> small)
        {
            //main 2,4,6,8
            //small 6,7,8
            bool isOk = true;
            foreach(var number in small)
            {
                if(!main.Contains(number))
                {
                    isOk = false;
                    break;
                }
            }
            return isOk;
        }
    }
}
