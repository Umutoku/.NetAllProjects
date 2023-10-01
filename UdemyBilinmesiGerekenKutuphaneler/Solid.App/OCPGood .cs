using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.App.OCGood2
{


    public class SalaryCalculator
    {


        public interface ISalaryCalculate
        {
            decimal Calculate(decimal salary);
        }

        public class LowSalaryCalculate
        {
            public decimal Calculate(decimal salary) { return salary * 2; }
        }

        public class MiddleSalaryCalculate 
        {
            public decimal Calculate(decimal salary) { return salary * 4; }
        }

        public class HighSalaryCalculate
        {
            public decimal Calculate(decimal salary) { return salary * 6; }
        }

        public class ManagerSalaryCalculate
        {
            public decimal Calculate(decimal salary) { return salary * 7; }
        }


        //Action => void
        //Predicate => bool
        //Function 


        public decimal Calculate(decimal salary, Func<decimal,decimal> calculateDelegate)
        {
           
            return calculateDelegate(salary);
        }
    }
    public enum SalaryTpe
    {
        Low,
        Middle,
        High
    }

}
