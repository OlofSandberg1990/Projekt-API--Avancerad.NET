using BookingSystemLibrary;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Projekt_API__Avancerad.NET.Services.Interfaces
{
    public interface IAuthorize
    {
        //Interface för autentisering och aukorisering.

        //Metoder för att auktorisera användarens roll baserat på email och password. 
        Task<Customer> AuthenticateCustomer(string email, string password);
        Task<Company> AuthenticateCompany(string email, string password);
        Task<Admin> AuthenticateAdmin(string email, string password);


        //Kontrollerar om användaren är admin, company eller customer
        bool IsAdmin(HttpContext context);
        bool IsCompany(HttpContext context);
        bool IsCustomer(HttpContext context);
    }
}
