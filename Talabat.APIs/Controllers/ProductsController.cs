using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _ProductRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo
                                 , IMapper mapper
                                 , IGenericRepository<ProductType> typeRepo
                                 , IGenericRepository<ProductBrand> brandsRepo)
        {
            _ProductRepo = ProductRepo;
            _mapper = mapper;
            _typeRepo = typeRepo;
            _brandsRepo = brandsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery]ProductSpecParams Params)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(Params);
            var Products = await _ProductRepo.GetAllWithSpecAsync(spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var SpecCount = new ProductWithFilterationWithCountAsync(Params);
            var Count = await _ProductRepo.GetCountWithSpecAsync(SpecCount);
            return Ok(new Pagination<ProductToReturnDto>(Params.PageIndex, Params.PageSize, MappedProducts, Count));
        }
  
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _ProductRepo.GetByIdWithSpecAsync(spec);
            if (Product is null) return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _brandsRepo.GetAllAsync(); 
            return Ok(Brands); 
        }
    }
}
