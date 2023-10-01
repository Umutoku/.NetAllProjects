using System;

namespace Constructor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Report report=new Report();
            //report.CreatedDate = DateTime.Now;
            Console.WriteLine($"Tarih: {report.CreatedDate}, format:{report.Format}, ve türü: {report.Type}");

            Report performanceReport = new Report("Türkay ");
        }
    }
}
