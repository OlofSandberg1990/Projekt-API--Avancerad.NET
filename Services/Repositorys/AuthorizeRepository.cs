using BookingSystemLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Projekt_API__Avancerad.NET.Data;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using System.Threading.Tasks;

namespace Projekt_API__Avancerad.NET.Services.Repositorys
{
    public class AuthorizeRepository : IAuthorize
    {
        private readonly DataContext _context;

        public AuthorizeRepository(DataContext context)
        {
            _context = context;
        }


        //Metoder för att se om det finns email och password som matchar användarens i databasen.
        public async Task<Admin> AuthenticateAdmin(string email, string password)
        {
            return await _context.Admins.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Company> AuthenticateCompany(string email, string password)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        public async Task<Customer> AuthenticateCustomer(string email, string password)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
        }

        //Hämtar userRole från sessionen för att se om det är Admin, Company eller Customer
        public bool IsAdmin(HttpContext context)
        {
            var userRole = context.Session.GetString("UserRole");
            return userRole == "Admin";
        }

        public bool IsCompany(HttpContext context)
        {
            var userRole = context.Session.GetString("UserRole");
            return userRole == "Company";
        }

        public bool IsCustomer(HttpContext context)
        {
            var userRole = context.Session.GetString("UserRole");
            return userRole == "Customer";
        }
    }
}
