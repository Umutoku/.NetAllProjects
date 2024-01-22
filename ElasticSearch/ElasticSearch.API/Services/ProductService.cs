﻿using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Net;

namespace ElasticSearch.API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
        {
           var response = await _productRepository.SaveAsync(request.CreateProduct());

            if (response == null)
                return ResponseDto<ProductDto>.Fail(new List<string> { "An error occurred while adding the product" }, HttpStatusCode.InternalServerError);

            return ResponseDto<ProductDto>.Success(response.CreateDto(), HttpStatusCode.Created);
        }


    }
}
