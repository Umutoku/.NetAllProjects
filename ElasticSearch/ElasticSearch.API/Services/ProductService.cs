using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using Microsoft.Extensions.Logging;

//using Nest;
using System.Collections.Immutable;
using System.Linq;
using System.Net;

namespace ElasticSearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
           var response = await _productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "An error occurred while adding the product" }, HttpStatusCode.InternalServerError);

            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);
        }

        public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
        {
            var response = await _productRepository.GetAllAsync();

            var productListDto = new List<ProductDto>();

            foreach (var item in response)
            { 
                if(item.Feature != null)
                    productListDto.Add(new ProductDto(item.Id,item.Name,item.Price,item.Stock,new ProductFeatureDto(item.Feature.Width,item.Feature.Height,item.Feature.Color)));
                else
                    productListDto.Add(new ProductDto(item.Id,item.Name,item.Price,item.Stock,null));
            }

            return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);

        }

        public async Task<ResponseDto<ProductDto>> GetAsync(string id)
        {
            var hasProduct = await _productRepository.GetAsync(id);

            if (hasProduct == null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);

            return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(), HttpStatusCode.OK);

        }

        public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var responseProduct = await _productRepository.UpdateAsync(productUpdateDto);

            if (!responseProduct)
                return ResponseDto<bool>.Fail(new List<string> { "An error occurred while updating the product" }, HttpStatusCode.InternalServerError);

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var deleteResponse = await _productRepository.DeleteAsync(id);

            if(!deleteResponse.IsValidResponse && deleteResponse.Result == Result.NotFound )
                return ResponseDto<bool>.Fail(new List<string> { "Product not found" }, HttpStatusCode.NotFound);

            if (!deleteResponse.IsValidResponse)
            {
                deleteResponse .TryGetOriginalException(out Exception? ex);
                _logger.LogError(ex?.Message, deleteResponse.ElasticsearchServerError?.Error.ToString());
                return ResponseDto<bool>.Fail(new List<string> { "An error occurred while deleting the product" }, HttpStatusCode.InternalServerError);
            }

            return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
        }


    }
}
