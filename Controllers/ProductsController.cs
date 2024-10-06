using Exercise.Dtos;
using Exercise.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Exercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetProductById()
        {
            return await _productService.GetAllAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ProductDto> GetProductById([Required] Guid id)
        {
            return await _productService.GetAsync(id);
        }

        [HttpPost]
        public async Task<Guid> CreateProduct([Required] ProductDto product)
        {
            return await _productService.CreateAsync(product);
        }

        [HttpPatch("{id:guid}")]
        public async Task UpdateProduct([Required] Guid id, [Required] ProductDto product)
        {
            await _productService.UpdateAsync(id, product);
        }

        [HttpDelete("{id:guid}")]
        public async Task DeleteProduct([Required] Guid id)
        {
            await _productService.DeleteAsync(id);
        }
    }
}
