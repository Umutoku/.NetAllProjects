using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageLibrary.Models
{
    class Product : TableEntity
    {
        public Product()
        {
        }

        public string Name { get; set; }
}
}
