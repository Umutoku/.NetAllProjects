using System;
using System.Collections.Generic;
using System.Linq;

namespace usingLinq
{
    internal class Program
    {
        private static List<Student> students;
        static void Main(string[] args)
        {
            //Language Integrated Query
            students = new List<Student>
            {
                new Student{Id = 1, Name ="Umut", LastName="Oku", Age =25, AvarageScore=90, Info="Çalışkan"},
                new Student{Id = 2, Name ="Türkay", LastName="ürkmez", Age =42, AvarageScore=85, Info="He was a meh student"},
                new Student{Id = 3, Name ="Nur", LastName="Kul", Age =29, AvarageScore=80, Info="She was a genius student"},
                new Student{Id = 4, Name ="Marwan", LastName="Kaseer", Age =24, AvarageScore=54}
            };

            basicLinq();
            getAverageScore();
        }

        private static void getAverageScore()
        {
            var average = students.Average(student => student.Age);
            Console.WriteLine($"Ortalama değer: {average}");
        }

        private static void basicLinq()
        {
           var scorebigThan70 = from student in students where student.AvarageScore >= 70 select student;
            var alternativeBigThanFive = students.Where(s => s.AvarageScore >= 70).OrderBy(x=>x.LastName).ToList();
            alternativeBigThanFive.ForEach(stu => Console.WriteLine($"{stu.Name} {stu.LastName} {stu.Age}"));

        }
    }
}
