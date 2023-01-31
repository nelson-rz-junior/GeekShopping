using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Repositories;
using GeekShopping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

    [HttpGet]
	public async Task<ActionResult<IEnumerable<ProductVO>>> GetAll()
	{
		var products = await _productRepository.GetAllAsync();

        return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ProductVO>> GetById(long id)
	{
		var product = await _productRepository.GetByIdAsync(id);
		if (product == null)
		{
			return BadRequest();
		}

		return Ok(product);
	}

	[HttpPost]
	public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVO)
	{
		if (productVO == null)
		{
			return BadRequest();
		}

		var product = await _productRepository.CreateAsync(productVO);

		return CreatedAtRoute(new { product.Id }, product);
	}

    [HttpPut]
    public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
    {
        if (productVO == null)
        {
            return BadRequest();
        }

        var product = await _productRepository.UpdateAsync(productVO);
        return Ok(product);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("{id}")]
	public async Task<ActionResult<bool>> Delete(long id)
	{
		var result = await _productRepository.DeleteByIdAsync(id);
		return result ? Ok(result) : BadRequest();
	}
}
