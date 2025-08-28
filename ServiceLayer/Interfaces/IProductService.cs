
using DataLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>>  GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }

}
