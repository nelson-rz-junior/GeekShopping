using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllAsync();

    Task<ProductViewModel?> GetByIdAsync(long id);

    Task<ProductViewModel?> Create(ProductViewModel viewModel);

    Task<ProductViewModel?> Update(ProductViewModel viewModel);

    Task<bool> DeleteByIdAsync(long id);
}
