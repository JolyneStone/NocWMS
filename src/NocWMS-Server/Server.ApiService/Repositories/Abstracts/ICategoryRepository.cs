using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface ICategoryRepository: IRepository<Category, int>
    {
        void UpdatCategory(Category category, CategoryFormViewModel categoryForm);
        Task<Category> AddCategoryAsync(CategoryFormViewModel categoryForm);
        Task<ValueTuple<bool, Category>> TryAddCategoryAsync(CategoryFormViewModel categoryForm);
        Task<ValueTuple<IList<Category>, int>> GetCategoryListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Category>, int>> GetCategoryListAsync(string query, int page, int pageSize);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
