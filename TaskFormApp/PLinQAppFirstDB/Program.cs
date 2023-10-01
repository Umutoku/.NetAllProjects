// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using PLinQAppFirstDB.Models;
using System.Linq;
using System.Threading.Channels;

static void WriteLog(Product p)
{
    Console.WriteLine(p.Name +" log kaydedildi");
}


AdventureWorks2017Context context = new AdventureWorks2017Context();

var products = context.Products.Take(100).ToArray();

products[3].Name = "##";

var query = products.AsParallel().Where(p => p.Name[4] == 'a');

try
{
    query.ForAll(x =>
    {
        Console.WriteLine($"{x.Name}");
    });
}
catch (AggregateException ex) //birden fazla exception için
{

    foreach (var item in ex.InnerExceptions.ToList()) //InnerExceptions bir list
    {
        Console.WriteLine(item);
    }
}

//context.Products.Take(10).ToList().ForEach(p =>
//Console.WriteLine(p.Name));



static void Sorgular()
{

    AdventureWorks2017Context context = new AdventureWorks2017Context();

    var product = (from p in context.Products.AsParallel()
               .WithExecutionMode(ParallelExecutionMode.Default) //kesin parallel çalışsın istersek
               .WithDegreeOfParallelism(2) //kaç adet işlemcide çalışacağını söyler
                   where p.ListPrice > 10M
                   select p).Take(10); //parallel Queryable olduğu için lazyloading

    product.ForAll(x =>
    {
        WriteLog(x);
    });

    var product2 = context.Products
        .AsParallel()
        .AsOrdered() //sıralamayı koru, parallellerde normalde veri sıra garantsi yok
        .Where(x => x.ListPrice > 10M).Take(10);
}