using Exercise.Database;
using Exercise.Domain;
using Microsoft.EntityFrameworkCore;
using static Exercise.Database.ExerciseContext;

namespace Exercise.Repositories
{
    public interface IProductRepository
    {
        Task<Guid> AddAsync(Product product);
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ExerciseContext _context;

        public ProductRepository(ExerciseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Select(product => MapProduct(product))
                                          .AsNoTracking()
                                          .ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return MapProduct(await GetProductWithoutTrackingAsync(id));
        }

        public async Task<Guid> AddAsync(Product product)
        {
            var productToAdd = new DbProduct
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            await _context.Products.AddAsync(productToAdd);
            await _context.SaveChangesAsync();
            return productToAdd.Id;
        }

        public async Task UpdateAsync(Guid id, Product product)
        {
            var existingProduct = await GetProduct(id);
            existingProduct.Price = product.Price;
            existingProduct.Name = product.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var productToRemove = new DbProduct { Id = id };
            _context.Products.Attach(productToRemove);
            _context.Products.Remove(productToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync()
        {
            //method syntax
            return await _context.Products.Where(product => product.Price > 100)
                                          .Select(product => MapProduct(product))
                                          .AsNoTracking()
                                          .ToListAsync();

            //query syntax
            //return await (from product in _context.Products
            //              where product.Price > 100
            //              select MapProduct(product)).AsNoTracking()
            //                                         .ToListAsync();
        }

        private async Task<DbProduct> GetProduct(Guid id)
            => await _context.Products.FindAsync(id) ?? throw new Exception("Product not found");

        private async Task<DbProduct> GetProductWithoutTrackingAsync(Guid id)
            => await _context.Products.AsNoTracking().SingleOrDefaultAsync(product => product.Id == id) ?? throw new Exception("Product not found");

        private static Product MapProduct(DbProduct product)
            => new(product.Id, product.Name, product.Price);
    }
}
