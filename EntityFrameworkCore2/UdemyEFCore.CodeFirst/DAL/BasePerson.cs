using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    //[Owned] //eğer owned olarak işaretler isek base tanımı kaldırılır normalde miras alan tablolara id eklenir ve buradan kaldırılır
    public class BasePerson
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
