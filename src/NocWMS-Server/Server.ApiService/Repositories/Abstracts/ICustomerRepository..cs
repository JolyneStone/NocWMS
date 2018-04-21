using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KiraNet.Camellia.Infrastructure.DomainModel.Data;
using Server.ApiService.Models;
using Server.ApiService.Models.ViewModels;

namespace Server.ApiService.Repositories.Abstracts
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        void UpdatCustomer(Customer customer, CustomerFormViewModel customerForm);
        Task<Customer> AddCustomerAsync(CustomerFormViewModel customerForm);
        Task<ValueTuple<bool, Customer>> TryAddCustomerAsync(CustomerFormViewModel customerForm);
        Task<ValueTuple<IList<Customer>, int>> GetCustomerListAsync(int page, int pageSize);
        Task<ValueTuple<IList<Customer>, int>> GetCustomerListAsync(string query, int page, int pageSize);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
