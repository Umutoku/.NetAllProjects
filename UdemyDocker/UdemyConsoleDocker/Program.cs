using System;
using System.Threading;

namespace UdemyConsoleDocker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            while(i<10000)
            {
                Console.WriteLine("Hello World!" +i);
                i++;
                Thread.Sleep(1000);
            }
            
        }
    }
}
