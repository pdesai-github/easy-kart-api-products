using EasyKart.Products.Repository;
using EasyKart.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyKart.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailsRepository _productDetailsRepository;
        public ProductDetailsController(IProductDetailsRepository productDetailsRepository)
        {
            _productDetailsRepository = productDetailsRepository;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDetails>> Get(string productId)
        {
            try
            {
                ProductDetails productDetails = await _productDetailsRepository.GetProductDetailsAsync(productId);
                if (productDetails == null)
                {
                    return NotFound();
                }
                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
