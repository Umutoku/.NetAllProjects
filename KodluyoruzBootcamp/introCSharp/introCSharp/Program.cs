using System;

namespace introCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Sayısal veri tipler
            byte age = 42;
            sbyte negativeNumber = -4;
            short length = -32768;
            ushort unsignedShort = 65535;
            int totalYear = 1000000000;
            long watchCount = 1000000000000;

            double pi = 3.14;
            float discount = 0.15f;
            decimal formulaConstant = 0.0000000000001M;

            //Sözel
            char karakter = '%';
            string kelime = "kelimeler";

            //mantıksal
            bool isClosed = true;

            //Amaç: kullanıcının boy ve kilosuna göre bmi hesaplamak
            try
            {
                int weight = 103;
                double height = 1.83;

                double bmi = weight / Math.Pow(height, height);
                Console.WriteLine("Your bmi value is: " + bmi);
                if(bmi>=30)
                {
                    Console.WriteLine("You are obese");
                }
                else if(bmi<25&&bmi>29.9)
                {
                    Console.WriteLine("You are overweight");
                }
                else if (bmi < 18.5 && bmi > 24.9)
                {
                    Console.WriteLine("You are normal");
                }
                else if (bmi < 18.5)
                {
                    Console.WriteLine("You are underweight");
                }
                else
                {
                    Console.WriteLine("You are not obese");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Please give values with true format");
            }
            
        }
    }
}
