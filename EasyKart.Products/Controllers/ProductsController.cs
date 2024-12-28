using EasyKart.Shared.Models;
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
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                List<Product> products = await _productRepository.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{productId}/{categoryId}")]
        public async Task<ActionResult<Product>> Get(string productId, string categoryId)
        {
            try
            {
                Product product = await _productRepository.GetProductAsync(productId, categoryId);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("byGuids")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByGuids([FromBody] List<Guid> guids)
        {
            try
            {
                // Assuming you have a method in your repository to get products by a list of GUIDs
                List<Product> products = await _productRepository.GetProductsByIdsAsync(guids);
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
