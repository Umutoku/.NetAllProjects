namespace ODataExtensionsApp.Console
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var serviceRoot = new Uri("https://localhost:5001/odata/");

            var context = new Default.Container(serviceRoot); // bu kod ile odata servisine bağlanıyoruz

            var product = context.Products.ExecuteAsync().Result; // bu kod ile odata servisinden veri çekiyoruz. Odata extension ile veri çekme işlemi gerçekleşiyor.

            var products2 = context.Products.AddQueryOption("$filter", "Price gt 100").ExecuteAsync().Result; // bu kod ile odata servisinden veri çekiyoruz. AddQueryOption ile filtreleme işlemi gerçekleşiyor.

            var products3 = context.Products.AddQueryOption("$orderby", "Price desc").ExecuteAsync().Result; // bu kod ile odata servisinden veri çekiyoruz. AddQueryOption ile sıralama işlemi gerçekleşiyor.

            var products4 = context.Products.Expand(p => p.Category).ExecuteAsync().Result; // bu kod ile odata servisinden veri çekiyoruz. Expand ile ilişkili tabloların verileri çekiliyor.

            product.ToList().ForEach(p => Console.WriteLine(p.Name));


        }
    }
}