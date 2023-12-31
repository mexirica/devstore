using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Catalog.API.Models;
using DevStore.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.Catalog.API.Controllers;

[Route("catalog")]
public class CatalogController : MainController
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("products")]
    public async Task<PagedResult<Product>> Index([FromQuery] int ps = 8, [FromQuery] int page = 1,
        [FromQuery] string q = null)
    {
        return await _productRepository.GetAll(ps, page, q);
    }

    [HttpGet("products/{id}")]
    public async Task<Product> Details(Guid id)
    {
        return await _productRepository.GetById(id);
    }

    [HttpGet("products/list/{ids}")]
    public async Task<IEnumerable<Product>> GetManyById(string ids)
    {
        return await _productRepository.GetProductsById(ids);
    }
}