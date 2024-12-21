using EasyKart.Products.Models;
using EasyKart.Products.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyKart.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
      
        [HttpGet]
        public async Task< IEnumerable<Product>> Get()
        {
            List<Product> products = await _productRepository.GetProductsAsync();

            return products.Take(2);
        }
       
    }
}
