using EasyKart.Shared.Models;

namespace EasyKart.Products.Repository
{
    public interface IProductDetailsRepository
    {
        Task<ProductDetails> GetProductDetailsAsync(string productId);
    }
}
