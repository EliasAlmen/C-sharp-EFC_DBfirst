using EC05_C_sharp_EFC_DBfirst.Contexts;
using EC05_C_sharp_EFC_DBfirst.Models;
using EC05_C_sharp_EFC_DBfirst.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC05_C_sharp_EFC_DBfirst.Services
{
    internal class CustomerService
    {
        private static DataContext _context = new();


        public static async Task SaveAsync(CustomerModel model)
        {
            var _customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var _address = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode && x.City == model.City);
            if (_address != null)
            {
                _customer.AddressId = _address.Id;
            }
            else
                _customer.Address = new Address
                {
                    StreetName = model.StreetName,
                    PostalCode = model.PostalCode,
                    City = model.City,
                };
            _context.Add(_customer);
            await _context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var _customers = new List<CustomerModel>();
            foreach (var _customer in await _context.Customers.Include(x => x.Address).ToListAsync())
                _customers.Add(new CustomerModel
                {
                    Id = _customer.Id,
                    FirstName = _customer.FirstName,
                    LastName = _customer.LastName,
                    Email = _customer.Email,
                    PhoneNumber = _customer.PhoneNumber,
                    StreetName = _customer.Address.StreetName,
                    PostalCode = _customer.Address.PostalCode,
                    City = _customer.Address.City,

                });
            return _customers;
        }

        public static async Task<Customer> GetAsync(string email)
        {
            var _customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            if (_customer != null)
            {
                return new Customer
                {
                    Id = _customer.Id,
                    FirstName = _customer.FirstName,
                    LastName = _customer.LastName,
                    Email = _customer.Email,
                    PhoneNumber = _customer.PhoneNumber,
                    StreetName = _customer.Address.StreetName,
                    City = _customer.Address.City,
                    PostalCode = _customer.Address.PostalCode,
                };

            }
            else
                return null!;
        }

        public static async Task UpdateAsync(Customer customer)
        {


            var _customerEntity = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == customer.Id);
            if (_customerEntity != null)
            {
                if (!string.IsNullOrEmpty(customer.FirstName))
                    _customerEntity.FirstName = customer.FirstName;
                if (!string.IsNullOrEmpty(customer.LastName))
                    _customerEntity.LastName = customer.LastName;
                if (!string.IsNullOrEmpty(customer.Email))
                    _customerEntity.Email = customer.Email;
                if (!string.IsNullOrEmpty(customer.PhoneNumber))
                    _customerEntity.PhoneNumber = customer.PhoneNumber;

                if (!string.IsNullOrEmpty(customer.FirstName) || !string.IsNullOrEmpty(customer.PostalCode) || !string.IsNullOrEmpty(customer.City))
                {
                    var _addressEntity = _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == customer.StreetName && x.PostalCode == customer.PostalCode && x.City == customer.City);
                    if (_addressEntity != null)
                    {
                        _customerEntity.AddressId = _addressEntity.Id;

                    }
                    else
                        _customerEntity.Address = new AddressEntity
                        {
                            StreetName = customer.StreetName,
                            City = customer.City,
                            PostalCode = customer.PostalCode,
                        };
                }
                _context.Update(_customerEntity);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task DeleteAsync(string email)
        {
            var customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            if (customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
