using System;

namespace forLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] courses = { "Matematik", "c#", "Html", "Css", "Javascript","sql" };
            for(int i = 0; i < courses.Length; i++)
            {
                Console.WriteLine(courses[i]);
            }

            int[] ages = { 42, 29, 26, 29, 29, 23 };
            int ageTotal = 0;
            for(int i = 0;ages.Length > 0; i++)
            {
                ageTotal += ages[i];
            }
            int average = ageTotal / ages.Length;

            Console.WriteLine($"Yaş ortalaması:{average}");

            int[] numbers = { 36, 12, 26, 9, -4, 22, 8 };
        }
    }
}
