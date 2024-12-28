using EasyKart.Shared.Models;

namespace EasyKart.Products.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(string productId, string categoryId);
        Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds);
    }
}
