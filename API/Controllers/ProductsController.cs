using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> productRepo,
                              IGenericRepository<ProductBrand> productBrandRepo,
                              IGenericRepository<ProductType> productTypeRepo,
                              IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo = productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo = productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo = productTypeRepo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var products = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>
                    (productParams.PageIndex, 
                    productParams.PageSize,
                    totalItems, data));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if(product == null) return NotFound(new ApiResponse(404));
            
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.GetAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.GetAllAsync());
        }
    }
}