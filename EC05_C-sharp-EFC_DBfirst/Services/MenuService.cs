using EC05_C_sharp_EFC_DBfirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC05_C_sharp_EFC_DBfirst.Services
{
    internal class MenuService
    {
        public async Task CreateNewContactAsync()
        {
            var customer = new CustomerModel();

            Console.WriteLine("Förnamn: ");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.WriteLine("Efernamn: ");
            customer.LastName = Console.ReadLine() ?? "";

            Console.WriteLine("E-postadress: ");
            customer.Email = Console.ReadLine() ?? "";

            Console.WriteLine("Telefonnummer: ");
            customer.PhoneNumber = Console.ReadLine() ?? "";

            Console.WriteLine("Gatuadress: ");
            customer.StreetName = Console.ReadLine() ?? "";

            Console.WriteLine("Postnummer: ");
            customer.PostalCode = Console.ReadLine() ?? "";

            Console.WriteLine("Stad: ");
            customer.City = Console.ReadLine() ?? "";

            //save customer to database
            await CustomerService.SaveAsync(customer);

        }

        public async Task ListAllContactsAsync()
        {
            //get all customers+address from database
            var customers = await CustomerService.GetAllAsync();

            if (customers.Any())
            {
                foreach (CustomerModel customer in customers)
                {
                    Console.WriteLine($"Kundnummer: {customer.Id}");
                    Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefonnummer: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.StreetName}, {customer.PostalCode} {customer.City}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Inga kunder i databasen...");
                Console.WriteLine("");
            }
        }

        public async Task ListSpecificContactAsync()
        {
            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                //get specific customer+address from database
                var customer = await CustomerService.GetAsync(email);

                if (customer != null)
                {
                    Console.WriteLine($"Kundnummer: {customer.Id}");
                    Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"E-postadress: {customer.Email}");
                    Console.WriteLine($"Telefonnummer: {customer.PhoneNumber}");
                    Console.WriteLine($"Adress: {customer.StreetName}, {customer.PostalCode} {customer.City}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen kund med den angivna e-postadressen {email} hittad...");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }

        public async Task DeleteSpecificContactAsync()
        {
            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                //delete specific customer from database
                await CustomerService.DeleteAsync(email);
            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }

        public async Task UpdateSpecificContactAsync()
        {
            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                var customer = await CustomerService.GetAsync(email);
                if (customer != null)
                {
                    Console.Write("Skriv in information på de fält du vill uppdatera. \n");

                    Console.WriteLine("Förnamn: ");
                    customer.FirstName = Console.ReadLine() ?? null!;

                    Console.WriteLine("Efernamn: ");
                    customer.LastName = Console.ReadLine() ?? null!;

                    Console.WriteLine("E-postadress: ");
                    customer.Email = Console.ReadLine() ?? null!;

                    Console.WriteLine("Telefonnummer: ");
                    customer.PhoneNumber = Console.ReadLine() ?? null!;

                    Console.WriteLine("Gatuadress: ");
                    customer.StreetName = Console.ReadLine() ?? null!;

                    Console.WriteLine("Postnummer: ");
                    customer.PostalCode = Console.ReadLine() ?? null!;

                    Console.WriteLine("Stad: ");
                    customer.City = Console.ReadLine() ?? null!;


                    //update specific customer from database
                    await CustomerService.UpdateAsync(customer);
                }
                else
                {
                    Console.WriteLine($"Hittade inte någon kund med den angivan e-postadressen.");
                    Console.WriteLine("");
                }




            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }
    }
}
