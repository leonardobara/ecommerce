using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductCatalog.Api.Core.Entities;
using Services.ProductCatalog.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoRepository<Product> _productRepository;
        public ProductController(IMongoRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpPost("")]
        public async Task InsertProduct([FromBody] Product product)
        {
           await _productRepository.InsertAsync(product);
        }

        [HttpGet("")]
        public IEnumerable<Product> GetAllActiveProducts()
        {
            var products = _productRepository.GetAll();
            return products;
        }

        [HttpPost("pagination")]
        public async Task<ActionResult<PaginationEntity<Product>>> Pagination(PaginationEntity<Product> pagination) 
        {
            var results = await _productRepository.PaginationBy(x => x.Name.ToLower() == pagination.Filter,
                                                   pagination
                );

            return Ok(results);
        }
    }
}
