using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingFuncApp
{
    internal class Product : TableEntity
    {
        public Product(string category, string id)
        {
            PartitionKey = category;
            RowKey = id;
        }

        public Product()
        {

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
