using BookingSystemLibrary;
using Projekt_API__Avancerad.NET.DTO;

namespace Projekt_API__Avancerad.NET.Services.Interfaces
{
    public interface ICustomer
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersWithFilters(string sortOrder, string searchString);
        Task<Customer> GetCustomerWithAppointmentsById(int id);

        Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisWeek();
        Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisMonth();

        Task<decimal> GetTotalMinutesForCustomerPerWeek(int weekNumber, int customerId);
    }
}
