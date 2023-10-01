using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewStructure.Models;

namespace ViewStructure.Controllers
{
    public class TeacherController : Controller
    {
        List<Teacher> teachers = new List<Teacher>()
        {
            new Teacher(){ID = 1,Name="Umut",Gender="man"},
            new Teacher(){ID = 2,Name="Talha",Gender="man"},
            new Teacher(){ID = 3,Name="Turna",Gender="woman"},
            new Teacher(){ID = 4,Name="Pelin",Gender="woman"},
        };

        public IActionResult GetTeacherList()
        {
            return View(teachers);
        }
        public IActionResult DeleteTeacher(int id)
        {
            Teacher teacher = teachers.Find(n=>n.ID==id);
            return View(teacher);
        }
    }
}
