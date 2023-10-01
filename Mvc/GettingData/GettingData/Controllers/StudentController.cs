using GettingData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GettingData.Controllers
{
    public class StudentController : Controller
    {
        List<Student> students = new List<Student>()
        {
            new Student(){FirstName ="Talha", LastName ="Sağdan"},
            new Student(){FirstName ="Veysi", LastName ="Soldan"},
            new Student(){FirstName ="Umut", LastName ="Oku"},
            new Student(){FirstName ="Turna", LastName ="Telsiz"},
        };
        public IActionResult GetStudentList()
        {
            return View(students);
        }
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            students.Add(student);

            return View("GetStudentList",students);
        }
    }
}
