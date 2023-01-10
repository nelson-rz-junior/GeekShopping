using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllAsync();
        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.CreateAsync(viewModel);
            if (product != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product != null)
        {
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.UpdateAsync(viewModel);
            if (product != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product != null)
        {
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ProductViewModel viewModel)
    {
        var response = await _productService.DeleteByIdAsync(viewModel.Id);

        return response ? RedirectToAction(nameof(Index)) : View(viewModel);
    }
}
