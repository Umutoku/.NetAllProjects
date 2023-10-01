using System;
using System.Collections.Generic;

namespace forEachLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> products = new List<string>();
            products.Add("Samsung A51");
            products.Add("Xiaomi");
            products.Add("Oppo");
            products.Add("IPhone");

            List<string> storage = new List<string>();

            foreach(string product in products)
            {
                if (product.StartsWith("I")) ;
                storage.Add(product);
            }

        }
    }
}
