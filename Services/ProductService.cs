using Exercise.Domain;
using Exercise.Dtos;
using Exercise.Repositories;

namespace Exercise.Services
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(ProductDto product);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetAsync(Guid id);
        Task UpdateAsync(Guid id, ProductDto product);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductDto(product.Id, product.Name, product.Price));
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return new(id, product.Name, product.Price);
        }

        public async Task<Guid> CreateAsync(ProductDto product)
        {
            var newProductId = Guid.NewGuid();
            var mappedProduct = new Product(newProductId, product.Name, product.Price);
            await _productRepository.AddAsync(mappedProduct);
            return newProductId;
        }

        public async Task UpdateAsync(Guid id, ProductDto product)
        {
            var mappedProduct = new Product(id, product.Name, product.Price);
            await _productRepository.UpdateAsync(id, mappedProduct);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
