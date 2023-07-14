﻿
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
           _productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> Getproducts()
        {
            var response = await _productService.GetProductsAsync();
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> Getproduct(int Id)
        {
            var response = await _productService.GetProductAsync(Id);
            return Ok(response);
        }

        [HttpGet("getByCategory/{CategoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetproductsByCategory(string CategoryUrl)
        {
            var response = await _productService.GetProductbyCategory(CategoryUrl);
            return Ok(response);
        }

        [HttpGet("search/{SearchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> SearchProducts(string SearchText)
        {
            var response = await _productService.SearchProducts(SearchText);
            return Ok(response);
        }

        [HttpGet("searchSuggestions/{SearchText}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsSuggestions(string SearchText)
        {
            var response = await _productService.GetProductSearchSuggestions(SearchText);
            return Ok(response);
        }
    }
}
