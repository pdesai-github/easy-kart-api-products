using EasyKart.Products.Models;

namespace EasyKart.Products.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
    }
}
