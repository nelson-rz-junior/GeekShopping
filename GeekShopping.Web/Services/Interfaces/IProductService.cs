using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllAsync(string token);

    Task<ProductViewModel?> GetByIdAsync(long id, string token);

    Task<ProductViewModel?> CreateAsync(ProductViewModel viewModel, string token);

    Task<ProductViewModel?> UpdateAsync(ProductViewModel viewModel, string token);

    Task<bool> DeleteByIdAsync(long id, string token);
}
