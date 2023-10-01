using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.App.OCBad
{


    public class SalaryCalculator
    {
        public decimal Calculate(decimal salary, SalaryTpe salaryTpe)
        {
            decimal newSalary = 0;

            switch (salaryTpe)
            {
                case SalaryTpe.Low:
                    newSalary = salary*2;
                    break;
                case SalaryTpe.High:
                    newSalary = salary*6;
                    break;
                case SalaryTpe.Middle:
                    newSalary = salary*4;
                    break;
                default:
                    break;
            }
            return newSalary;
        }
    }
    public enum SalaryTpe
    {
        Low,
        Middle,
        High
    }

}
