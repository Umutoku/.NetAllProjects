using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Linq;

namespace PlinQApp
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //Scaffold - DbContext "Data Source=UMUT;Initial Catalog=AdventureWorks2017;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables "Production.ProductCategory,Production.ProductSubCategory,Production.Product"

            //    Scaffold - DbContext "Your Connection String" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables "Your-Table-Name" - ContextDir Context - Context "your context name'

            //    Scaffold - DbContext "'Server=(localdb)\mssqllocaldb;Database=AdventureWorks2017;Trusted_Connection=True;'" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables "Production.ProductCategory,Production.ProductSubCategory,Production.Product"



            //var array = Enumerable.Range(1,200).ToList();

            //var newArr = array.AsParallel().Where(x => x % 2 == 0); //birden fazla thread //query dönüyor

            //var newArr1 = array.Where(x => x % 2 == 0).ToList();

            //newArr.ToList().ForEach(x => Console.WriteLine(x));//asparallel tolist yerine forall

            //newArr.ForAll(x => Console.WriteLine(x)); //sonuçlar karışık gelecektir.
        }
    }
}
