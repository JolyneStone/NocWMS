using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Microsoft.EntityFrameworkCore;
using Server.ApiService.Common;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;
using Server.ApiService.Repositories.Abstracts;

namespace Server.ApiService.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdatProduct(Product product, ProductFormViewModel productForm)
        {
            if (product == null || productForm == null)
            {
                return;
            }

            product.ProductName = productForm.ProductName;
            product.CategoryId = productForm.CategoryId;

            this.Update(product);
        }

        public async Task<Product> AddProductAsync(ProductFormViewModel productForm)
        {
            if (productForm == null)
            {
                throw new ArgumentNullException("param productForm is NULL in ProductRepository.AddProductAnsync");
            }

            var product = new Product()
            {
                CategoryId = productForm.CategoryId,
                ProductName = productForm.ProductName,
                SellPrice = productForm.SellPrice,
                Unit = productForm.Unit,
                Model = productForm.Model,
                Spec = productForm.Spec
            };

            await this.InsertAsync(product);
            return product;
        }

        public async Task<ValueTuple<bool, Product>> TryAddProductAsync(ProductFormViewModel productForm)
        {
            if (productForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddProductAsync(productForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Product>,int>> GetProductListAsync(int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync())
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),list.Count());
        }

        public async Task<ValueTuple<IList<Product>, int>> GetProductListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.ProductName, $"%{query}%")))
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                this.Delete(product);
                return true;
            }

            return false;
        }
    }
}
