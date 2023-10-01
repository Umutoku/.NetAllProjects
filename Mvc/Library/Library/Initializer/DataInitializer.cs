using Library.Enums;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Initializer
{
    public static class DataInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            string password1 = BCrypt.Net.BCrypt.HashPassword("123");
            string password2 = BCrypt.Net.BCrypt.HashPassword("1234");
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser() { ID = 1, UserName = "administrator", Password = password1, Role = Role.admin },
                new AppUser() { ID = 2, UserName = "Umut", Password = password2, Role = Role.user }
                );
            modelBuilder.Entity<Student>().HasData(
               new Student() { ID = 1, FirstName = "Talha", LastName = "Sağdan", Gender = Gender.Erkek },
               new Student() { ID = 2, FirstName = "Turna", LastName = "Yurtsever", Gender = Gender.Kadın },
               new Student() { ID = 3, FirstName = "Ümit", LastName = "Fidan", Gender = Gender.Erkek },
               new Student() { ID = 4, FirstName = "Pelin", LastName = "Toş", Gender = Gender.Kadın }
               );

            modelBuilder.Entity<StudentDetail>().HasData(
                new StudentDetail() { ID = 1, StudentID = 1, SchoolNumber = "100", BirthDay = new DateTime(1997, 10, 13) },
                new StudentDetail() { ID = 2, StudentID = 2, SchoolNumber = "101", BirthDay = new DateTime(1994, 10, 18) },
                new StudentDetail() { ID = 3, StudentID = 3, SchoolNumber = "102", BirthDay = new DateTime(1992, 12, 13) },
                new StudentDetail() { ID = 4, StudentID = 4, SchoolNumber = "103", BirthDay = new DateTime(1995, 11, 13) }


                );
        }
    }
}
