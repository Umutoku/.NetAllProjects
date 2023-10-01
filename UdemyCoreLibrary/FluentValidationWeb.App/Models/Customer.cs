using System;
using System.Collections;
using System.Collections.Generic;

namespace FluentValidationWeb.App.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }
        public IList<Address> Address { get; set; } //adresi index olarak kullanabilmek için list yapıldı //Customer.Adress[1].Id gibi
        public Gender Gender { get; set; }

        public string GetFullName()
        {
            return $"{Name}-{EMail}-{Age}";
        }
    
    }
}
