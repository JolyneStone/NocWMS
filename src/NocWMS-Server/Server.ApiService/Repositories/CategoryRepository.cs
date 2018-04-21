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
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdatCategory(Category category, CategoryFormViewModel categoryForm)
        {
            if (category == null || categoryForm == null)
            {
                return;
            }

            category.CategoryName = categoryForm.CategoryName;
            category.Remarks = categoryForm.Remarks;

            this.Update(category);
        }

        public async Task<Category> AddCategoryAsync(CategoryFormViewModel categoryForm)
        {
            if (categoryForm == null)
            {
                throw new ArgumentNullException("param categoryForm is NULL in CategoryRepository.AddCategoryAnsync");
            }

            var category = new Category()
            {
                CategoryName = categoryForm.CategoryName,
                Creator = categoryForm.Creator,
                Remarks = categoryForm.Remarks
            };

            await this.InsertAsync(category);
            return category;
        }

        public async Task<ValueTuple<bool, Category>> TryAddCategoryAsync(CategoryFormViewModel categoryForm)
        {
            if (categoryForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddCategoryAsync(categoryForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Category>, int>> GetCategoryListAsync(int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync()).AsNoTracking();
            return (await list
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<ValueTuple<IList<Category>, int>> GetCategoryListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.CategoryName, $"%{query}%")))
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (category != null)
            {
                this.Delete(category);
                return true;
            }

            return false;
        }
    }
}
