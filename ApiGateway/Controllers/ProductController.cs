
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            product = await _productService.AddProduct(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            product = await _productService.UpdateProduct(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }
    }
}
