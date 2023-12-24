using NLayer.Core.DTOs;
using NLayer.Core.Repositories;

namespace NLayer.Core.Services;

public interface IProductServiceWithDto:IServiceWithDto<Product,ProductDto>
{
    Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory();
    
    //Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
}