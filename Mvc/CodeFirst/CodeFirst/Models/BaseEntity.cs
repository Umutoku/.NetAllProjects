using CodeFirst.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            Status=DataStatus.Inserted;
        }
        //[Key]
        public int ID { get; set; }
        public DataStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedTime { get; set; }

    }
}
