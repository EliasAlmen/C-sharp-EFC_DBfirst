﻿using EC05_C_sharp_EFC_DBfirst.Contexts;
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
                Id = Guid.NewGuid(),
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

        public static async Task<CustomerModel> GetAsync(string email)
        {
            var _customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
            if (_customer != null)
            {
                return new CustomerModel
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

        public static async Task UpdateAsync(CustomerModel model)
        {
            var _customer = await _context.Customers.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (_customer != null)
            {
                if (!string.IsNullOrEmpty(model.FirstName) || !string.IsNullOrEmpty(model.PostalCode) || !string.IsNullOrEmpty(model.City))
                {
                    var _address = _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == model.StreetName && x.PostalCode == model.PostalCode && x.City == model.City);
                    if (_address != null)
                    {
                        _customer.AddressId = _address.Id;

                    }
                    else
                    {
                        var address_new = new Address
                        {
                            StreetName = model.StreetName,
                            PostalCode = model.PostalCode,
                            City = model.City,
                        };
                        _context.Add(address_new);
                        await _context.SaveChangesAsync();

                        _customer.AddressId = address_new!.Id;
                    }
                        
                }
                if (!string.IsNullOrEmpty(model.FirstName))
                    _customer.FirstName = model.FirstName;
                if (!string.IsNullOrEmpty(model.LastName))
                    _customer.LastName = model.LastName;
                if (!string.IsNullOrEmpty(model.Email))
                    _customer.Email = model.Email;
                if (!string.IsNullOrEmpty(model.PhoneNumber))
                    _customer.PhoneNumber = model.PhoneNumber;

                
                _context.Update(_customer);
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
