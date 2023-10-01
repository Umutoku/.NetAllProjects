// See https://aka.ms/new-console-template for more information

using Solid.App.DIPGoodAndBad;

Console.WriteLine("Hello, World!");

var ProductService = new ProductService(new ProductRepositoryFromOracleServer());

ProductService.GetAll().ForEach(x => Console.WriteLine(x));