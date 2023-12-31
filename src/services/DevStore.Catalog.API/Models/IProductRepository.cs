using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevStore.Core.Data;

namespace DevStore.Catalog.API.Models;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string query = null);
    Task<Product> GetById(Guid id);
    Task<List<Product>> GetProductsById(string ids);

    void Add(Product product);
    void Update(Product product);
}