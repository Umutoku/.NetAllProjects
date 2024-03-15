using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Protos;

namespace GrpcService.Services
{
    public class ProductService : Product.ProductBase
    {
        public override async Task<ProductSaveResponse> SaveProduct(ProductModel request, ServerCallContext context)
        {
            //var requestProduct = new ProductModel
            //{
            //    Name = "Samsung S21",
            //    Price = 10000,
            //    Code = "S21"
            //};

            if (request is not null)
            { 
                var response = new ProductSaveResponse
                {
                    Success = true,
                    Message = "Product saved successfully."
                };
                Console.WriteLine($"Product saved: {request.Name} {request.Price} ");
                return await Task.FromResult(response);
            }
            else
            {
                var response = new ProductSaveResponse
                {
                    Success = false,
                    Message = "Product could not be saved."
                };
                return await Task.FromResult(response);
            }
        }

        public override Task<ProductList> GetProducts(Empty request, ServerCallContext context)
        {
            var product1 = new ProductModel
            {
                Name = "Samsung S21",
                Price = 10000,
                Code = "S21",
                CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
                
            };

            var product2 = new ProductModel
            {
                Name = "Samsung S20",
                Price = 9000,
                Code = "S20",
                CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
            };

            var product3 = new ProductModel
            {
                Name = "Samsung S10",
                Price = 8000,
                Code = "S10",
                CreatedDate = Timestamp.FromDateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc))
            };

            var productList = new ProductList();
            productList.Products.Add(product1);
            productList.Products.Add(product2);
            productList.Products.Add(product3);


            return Task.FromResult(productList);
        }
    }
}