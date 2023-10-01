using System;

namespace switchCase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose traffic light(R/G/Y)");

            string choosing = Console.ReadLine();
            if( choosing =="R")
            {
                Console.WriteLine("Dur - Stop!");
            }
            else if( choosing =="Y")
            {
                Console.WriteLine("Dikkat - Warning!");
            }
            else if(choosing =="G")
            {
                Console.WriteLine("Geç - Pass");
            }
            else
            {
                Console.WriteLine("Hatalı Seçim - Wrong choosing");
            }

            switch (choosing)
            {
                case "R":
                    Console.WriteLine("Dur - Stop!");
                    break;
                case "Y":
                    Console.WriteLine("Dikkat - Warning!");
                    break;
                case "G":
                    Console.WriteLine("Geç - Pass!");
                    break;
                default:
                    Console.WriteLine("Hatalı Seçim - Wrong choosing");
                    break;
            
            }
        }
    }
}
