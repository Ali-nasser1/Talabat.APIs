using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _ProductRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo)
        {
            _ProductRepo = ProductRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var spec = new ProductWithBrandAndTypeSpecifications();
            var Products = await _ProductRepo.GetAllWithSpecAsync(spec);
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product =await _ProductRepo.GetByIdWithSpecAsync(spec);
            return Ok(Product);
        }
    }
}
