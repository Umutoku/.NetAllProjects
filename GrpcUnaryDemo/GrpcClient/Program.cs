using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcService.Protos;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5220"); // bu kod ile servis adresi belirlenir
            var client = new Sample.SampleClient(channel); // bu kod ile servis clienti oluşturulur

            var response = await client.GetFullNameAsync(new SampleRequest { FirstName = "John", LastName = "Doe" }); // bu kod ile servis çağrılır
            Console.WriteLine(response.FullName); // bu kod ile servis cevabı yazdırılır

            var productClient = new Product.ProductClient(channel); // bu kod ile servis clienti oluşturulur

            var productResponse = await productClient.SaveProductAsync(new ProductModel { Name = "Samsung S21", Price = 10000, Code = "S21", CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc)) }); // bu kod ile servis çağrılır

            Console.WriteLine(productResponse.Success); // bu kod ile servis cevabı yazdırılır

            var productListResponse = await productClient.GetProductsAsync(new Empty()); // bu kod ile servis çağrılır

            foreach (var product in productListResponse.Products)
            {
                var createdDate = product.CreatedDate.ToDateTime(); // bu kod ile tarih dönüştürülür çünkü servisten gelen tarih protobuf formatındadır
                Console.WriteLine($"Product Name: {product.Name} - Price: {product.Price} - Code: {product.Code}"); // bu kod ile servis cevabı yazdırılır
            }

            await channel.ShutdownAsync(); // bu kod ile servis kanalı kapatılır

            Console.Read();
        }
    }
}