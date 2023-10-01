using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor
{
    public class Report
    {
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public DateTime CreatedDate { get; set; }



        public Report()
        {
            CreatedDate = DateTime.Now;
            Format = "PDF";
            Type = "Monthly Sales Report";
        }


        public Report(string owner):this()
        {
            Owner = owner;
        }
    }

   
}
