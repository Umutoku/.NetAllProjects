using System;

namespace UdemyIOC.console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BL bl = new BL(new DAL());

            bl.GetProducts().ForEach(x =>
            {
                Console.WriteLine($"{x.ID}-{x.Name}");
            }
            );
        }
    }
}
