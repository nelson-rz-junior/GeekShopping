using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using System.Net;

namespace GeekShopping.Web.Services;

public class ProductService : IProductService
{
    private const string BASE_PATH = "/api/v1/product";
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(BASE_PATH);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>>()) ?? Enumerable.Empty<ProductViewModel>();
    }

    public async Task<ProductViewModel?> GetByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"{BASE_PATH}/{id}");
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductViewModel>();
    }

    public async Task<ProductViewModel?> CreateAsync(ProductViewModel viewModel)
    {
        var response = await _httpClient.PostAsJsonAsync(BASE_PATH, viewModel);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductViewModel>();
    }

    public async Task<ProductViewModel?> UpdateAsync(ProductViewModel viewModel)
    {
        var response = await _httpClient.PutAsJsonAsync(BASE_PATH, viewModel);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ProductViewModel>();
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{BASE_PATH}/{id}");
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            return false;
        }

        response.EnsureSuccessStatusCode();

        return true;
    }
}
