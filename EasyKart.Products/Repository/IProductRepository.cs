using EasyKart.Shared.Models;

namespace EasyKart.Products.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
    }
}
