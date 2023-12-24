using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Caching;

public class ProductServiceWithCaching : IProductService
{
    private const string CacheProductKey = "productCache";
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _memoryCache = memoryCache;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());
        }
    }

    public Task<Product> GetByIdAsync(int id)
    {
        return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x=>x.Id== id));
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _productRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _productRepository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entities;
    }

    public async Task UpdateAsync(Product entity)
    {
        _productRepository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveAsync(Product entity)
    {
        _productRepository.Remove(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _productRepository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
    {
        var products = await _productRepository.GetProductsWithCategory();
        var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
        return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productsWithCategoryDto);
    }

    private async Task CacheAllProductsAsync()
    {
        _memoryCache.Set(CacheProductKey, _productRepository.GetAll().ToList());
    }
}