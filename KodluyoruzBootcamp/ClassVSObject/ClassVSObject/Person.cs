using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassVSObject
{
    internal class Person
    {
        public string Name;
        public string LastName;
        public bool isActive;
        public double Salary;
        public string Address;
        public DateTime BirthDate;
    }

    class Cooker:Person
    {
        public string kitchenStyle; 
    }
}
