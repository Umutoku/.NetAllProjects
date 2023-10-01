using FluentValidationWeb.App.Models;
using System.Collections.Generic;
using System;

namespace FluentValidationWeb.App.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Posta { get; set; }
        public int Age { get; set; }
        public string FullName { get; set; }
    }
}
