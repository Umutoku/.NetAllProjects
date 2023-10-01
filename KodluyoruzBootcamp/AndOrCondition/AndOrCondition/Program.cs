using System;

namespace AndOrCondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            DateTime today = new DateTime(2022, 3, 21);
            Console.WriteLine(today ==DateTime.Now? "İyi ki doğdun!":$"Doğum gününüze: {(today - DateTime.Now)} gün var.");
        }
    }
}
