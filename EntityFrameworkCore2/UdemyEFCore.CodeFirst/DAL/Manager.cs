using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    public class Manager :BasePerson
    {
        //owned typle ile miras işlemi // owned typeda base tablo tanımlanmaz
        //public BasePerson Person { get; set; }
        public int Grade { get; set; }
    }
}
