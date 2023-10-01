using System;

namespace OperatorsAndCheck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte bignumber = 254;
            byte smallnumber = 2;

            try
            {
                checked
                {
                    byte total =(byte)(bignumber+smallnumber);
                    Console.WriteLine(total);
                }
            }
            catch (System.OverflowException ex)
            {

                Console.WriteLine("Total value cannot cast to byte data ype");
            }
            

            try
            {
                Console.WriteLine("Number 1 is:");
                int number1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Number 2 is:");
                int number2 = Convert.ToInt32(Console.ReadLine());

                int division = number1 / number2;
                Console.WriteLine($"Result: {division}");
            }
            catch (FormatException)
            {

                Console.WriteLine("Please just give digit");
            }
            catch(DivideByZeroException)
            {
                Console.WriteLine("Number 2 cannot be 0");
            }
        }
    }
}
