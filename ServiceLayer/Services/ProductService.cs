using ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using ServiceLayer.ParameterObjects;

namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        private Product CheckProductExists(Product? product)
        {
            if (product == null)
            {
                throw new ArgumentException("Product Doesn't Exist");
            }
            return product;
        }

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            Product? product = await _context.Products
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            product = CheckProductExists(product);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts(PaginationParams paginationParams)
        {
            int pageNumber = paginationParams.PageNumber;
            int pageSize = paginationParams.PageSize;
            return await _context.Products.AsNoTracking()
                                          .Skip((pageNumber -1)*pageSize).Take(pageSize)
                                          .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            Product? product = await _context.Products.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
            product = CheckProductExists(product);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

    }
}
