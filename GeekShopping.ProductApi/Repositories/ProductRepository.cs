using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Models;
using GeekShopping.ProductApi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVO>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<IEnumerable<ProductVO>>(products);
    }

    public async Task<ProductVO> GetByIdAsync(long id)
    {
        var product = await _context.Products.FindAsync(id);
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductVO productVO)
    {
        Product product = _mapper.Map<Product>(productVO);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> UpdateAsync(ProductVO productVO)
    {
        Product product = _mapper.Map<Product>(productVO);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);

        return await _context.SaveChangesAsync() == 1;
    }
}
