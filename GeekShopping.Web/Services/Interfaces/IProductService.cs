using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllAsync();

    Task<ProductViewModel?> GetByIdAsync(long id);

    Task<ProductViewModel?> CreateAsync(ProductViewModel viewModel);

    Task<ProductViewModel?> UpdateAsync(ProductViewModel viewModel);

    Task<bool> DeleteByIdAsync(long id);
}
