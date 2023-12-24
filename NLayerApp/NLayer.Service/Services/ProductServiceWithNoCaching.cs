﻿using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class ProductServiceWithNoCaching : Service<Product>,IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductServiceWithNoCaching(IGenericRepository<Product> genericRepository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(genericRepository, unitOfWork)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var product = await _productRepository.GetProductsWithCategory();
        var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productsDto);
    }
}