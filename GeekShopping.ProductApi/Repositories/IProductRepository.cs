using GeekShopping.ProductApi.Data.ValueObjects;

namespace GeekShopping.ProductApi.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductVO>> GetAllAsync();

    Task<ProductVO> GetByIdAsync(long id);

    Task<ProductVO> CreateAsync(ProductVO productVO);

    Task<ProductVO> UpdateAsync(ProductVO productVO);

    Task<bool> DeleteByIdAsync(long id);
}
