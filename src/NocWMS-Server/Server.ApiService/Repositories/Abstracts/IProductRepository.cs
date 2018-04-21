using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
        void UpdatProduct(Product product, ProductFormViewModel productForm);
        Task<Product> AddProductAsync(ProductFormViewModel productForm);
        Task<ValueTuple<bool, Product>> TryAddProductAsync(ProductFormViewModel productForm);
        Task<ValueTuple<IList<Product>,int>> GetProductListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Product>, int>> GetProductListAsync(string query, int page, int pageSize);
        Task<bool> DeleteProductAsync(int id);
    }
}
