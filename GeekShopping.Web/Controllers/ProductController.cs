using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Interfaces;
using GeekShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var products = await _productService.GetAllAsync(token);
        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.CreateAsync(viewModel, token);
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
        var token = await HttpContext.GetTokenAsync("access_token");

        var product = await _productService.GetByIdAsync(id, token);
        if (product != null)
        {
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.UpdateAsync(viewModel, token);
            if (product != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        return View(viewModel);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var product = await _productService.GetByIdAsync(id, token);
        if (product != null)
        {
            return View(product);
        }

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpPost]
    public async Task<IActionResult> Delete(ProductViewModel viewModel)
    {
        var token = await HttpContext.GetTokenAsync("access_token");

        var response = await _productService.DeleteByIdAsync(viewModel.Id, token);

        return response ? RedirectToAction(nameof(Index)) : View(viewModel);
    }
}
