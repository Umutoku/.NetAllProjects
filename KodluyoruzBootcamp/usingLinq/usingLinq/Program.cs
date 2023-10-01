using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usingLinq
{

    
    public class Program
    {
        private static List<Student> students;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            students = new List<Student>
            {
                new Student{Id=1, Name="Yusuf", LastName="Yaþar", Age=2022, AverageScore= 100},
                new Student{Id=2, Name="Emre", LastName="Soylu", Age=29, AverageScore=44},
                new Student{Id=3, Name="Umut", LastName="Oku", Age=28, AverageScore= 70},
                new Student{Id=4, Name="Sinem", LastName="Gülmez", Age=30, AverageScore=56}
            };
            basicLinq();
        }

        private static void basicLinq()
        {
            var scorebigThan70 = from student in students
                                 where student.AverageScore >= 70
                                 select student;

            var alternativeBigThanFive = students.Where(students => students.AverageScore >= 70)
                .OrderBy(students =>students.AverageScore).ToList();
            alternativeBigThanFive.ForEach(student => Console.WriteLine(student.Name));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
