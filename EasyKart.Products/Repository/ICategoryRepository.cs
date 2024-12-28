using EasyKart.Shared.Models;

namespace EasyKart.Products.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
    }
}
