using AutoMapper;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class ProductServiceWithDto:ServiceWithDto<Product,ProductDto>,IProductServiceWithDto
{
    private readonly IProductRepository _productRepository;
    public ProductServiceWithDto(IGenericRepository<Product> genericRepository, IProductRepository productRepository , IUnitOfWork unitOfWork, IMapper mapper) : base(genericRepository, unitOfWork, mapper)
    {
        _productRepository = productRepository;
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory()
    {
        var products = await _productRepository.GetProductsWithCategory();
        return _mapper.Map<CustomResponseDto<List<ProductWithCategoryDto>>>(products);
    }
}