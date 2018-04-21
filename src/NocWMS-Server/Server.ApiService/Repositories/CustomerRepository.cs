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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(NocDbContext dbContext) : base(dbContext)
        {
        }

        public void UpdatCustomer(Customer customer, CustomerFormViewModel customerForm)
        {
            if (customer == null || customerForm == null)
            {
                return;
            }

            customer.CustomerName = customerForm.CustomerName;
            customer.Telephone = customerForm.Telephone;
            customer.PostCode = customerForm.PostCode;
            customer.Email = customer.Email;
            customer.Fax = customer.Fax;
            customer.Contact = customer.Contact;
            customer.Duty = customerForm.Duty;
            customer.Remarks = customerForm.Remarks;
            customer.Gender = customerForm.Gender;

            this.Update(customer);
        }

        public async Task<Customer> AddCustomerAsync(CustomerFormViewModel customerForm)
        {
            if (customerForm == null)
            {
                throw new ArgumentNullException("param customerForm is NULL in CustomerRepository.AddCustomerAnsync");
            }

            var customer = new Customer()
            {
                Id = GenerageNumber.GetCustomerNumber(),
                CustomerName = customerForm.CustomerName,
                Duty = customerForm.Duty,
                Email = customerForm.Email,
                Gender = customerForm.Gender,
                Address = customerForm.Address,
                Contact = customerForm.Contact,
                Fax = customerForm.Fax,
                PostCode = customerForm.PostCode,
                Remarks = customerForm.Remarks,
                Telephone = customerForm.Telephone
            };

            await this.InsertAsync(customer);
            return customer;
        }

        public async Task<ValueTuple<bool, Customer>> TryAddCustomerAsync(CustomerFormViewModel customerForm)
        {
            if (customerForm == null)
            {
                return (false, null);
            }

            try
            {
                return (true, await this.AddCustomerAsync(customerForm));
            }
            catch
            {
                return (false, null);
            }
        }

        public async Task<ValueTuple<IList<Customer>, int>> GetCustomerListAsync(int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync())
                .AsNoTracking();
            return (await list
                .OrderBy(x => x.CreateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<ValueTuple<IList<Customer>,int>> GetCustomerListAsync(string query, int page, int pageSize)
        {
            page = page > 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 5;
            var list = (await this.GetAllAsync(x => EF.Functions.Like(x.CustomerName, $"%{query}%")))
                .AsNoTracking()
                .OrderBy(x => x.CreateTime);
            return (await list.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(), list.Count());
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await this.GetFirstOrDefaultAsync(x => x.Id == id);
            if (customer != null)
            {
                this.Delete(customer);
                return true;
            }

            return false;
        }
    }
}
