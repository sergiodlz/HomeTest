using HomeTest.Data.Entities;
using HomeTest.Services.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsClientController : ControllerBase
    {
        private readonly IEntityService<Product> _productService;

        public ProductsClientController(IEntityService<Product> productService)
        {
            _productService = productService;
        }

        // GET: api/ProductsClient
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.FindByConditionAsync(p => p.Enable);
        }
    }
}