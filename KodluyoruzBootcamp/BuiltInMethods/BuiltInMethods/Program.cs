using System;
using System.Collections.Generic;

namespace BuiltInMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string word = Console.ReadLine();
            //int index = word.IndexOf('i',5);
            int startindex = 0;
            Console.WriteLine("Aradığınız Harf:");
            string ch = Console.ReadLine();

            Console.Write($"{ch} harfinin index değeri: ");

            while (word.IndexOf(ch, startindex) != -1)
            {
                int findingindex = word.IndexOf(ch, startindex);
                Console.WriteLine($"{findingindex}, ");
                startindex = findingindex + 1;
            }


            List<string> companymail = new List<string>();


            List<string> list = new List<string>()
            {
                "turkay.urkmez@dinamikzihin.com",
                "kirikkalp72@gmail.com",
                "nur.bilge@microsoft.com",
                "babyboy@mynet.com"
            };

            List<string> publicDomain = new List<string>()
            {
                "yahoo.com",
                "gmail.com",
                "hotmail.com",
                "mynet"
            };
            /*
            for(int i = 0; i < list.Count; i++)
            {
                string domain = list[i].Split('@')[1];
                bool isExists = publicDomain.Contains(domain);
                if(!isExists)
                {
                    companymail.Add(list);
                }
            }
            */
            foreach (var mail in list)
            {
                string[] mailParts = mail.Split('@');
                if (mailParts.Length > 1)
                {
                    string domain = mailParts[1];
                    if (!publicDomain.Contains(domain))
                    {
                        companymail.Add(mail);
                    }
                }
                Console.WriteLine("Şirket e-posta adresleri: ");
                foreach (var mailAdress in companymail)
                {
                    Console.WriteLine(mailAdress);
                }

                List<string> cities = new List<string>() { "Ankara", "Ankara", "Sinop", "İstanbul", "İstanbul", "Eskişehir" };
                foreach (var city in cities)
                {
                    
                }
            }
        }
    }
}