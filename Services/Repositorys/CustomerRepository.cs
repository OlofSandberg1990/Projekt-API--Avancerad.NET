using AutoMapper;
using BookingSystemLibrary;
using Microsoft.EntityFrameworkCore;
using Projekt_API__Avancerad.NET.Data;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using System.Globalization;

namespace Projekt_API__Avancerad.NET.Services.Repositorys
{
    public class CustomerRepository : IRepository<CustomerDto>, ICustomer
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Lägga till ny kund
        public async Task<CustomerDto> Add(CustomerDto entity)
        {
            var customer = _mapper.Map<Customer>(entity);
            var result = await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(result.Entity);
        }

        //Ta bort kund
        public async Task<CustomerDto> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        //Hämta lista med alla kunder
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        //Hämta CustomerDto via Id
        public async Task<CustomerDto> GetById(int id)
        {
            var customer = await _context.Customers                
                .FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CustomerDto>(customer);
        }

        //Hämta Customer via id
        public async Task<Customer> GetCustomerWithAppointmentsById(int id)
        {
            return await _context.Customers
                .Include(c => c.Appointments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Uppdatera kund
        public async Task<CustomerDto> Update(CustomerDto entity)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (customer == null)
            {
                return null;
            }

            customer.Name = entity.Name;
            customer.Phone = entity.Phone;
            customer.Email = entity.Email;

            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        //Hämta aktuellt veckonummer
        private int GetCurrentWeek(DateTime date)
        {
            Calendar cal = new CultureInfo("sv-SE").Calendar;
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        //Hämta aktuell månad
        private int GetCurrentMonth(DateTime date)
        {
            Calendar cal = new CultureInfo("sv-SE").Calendar;
            return cal.GetMonth(date);
        }

        //Hämta kunder och deras möten denna vecka
        public async Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisWeek()
        {

            int currentWeek = GetCurrentWeek(DateTime.Now);
            var customers = await _context.Customers
                .Include(c => c.Appointments)
                .ToListAsync();

            //Hämtar kunder vars mötens StartTime matchar currentWeek
            return customers.Where(c => c.Appointments.Any(a => GetCurrentWeek(a.StartTime) == currentWeek))
                            .Select(c => new Customer
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Email = c.Email,
                                Phone = c.Phone,
                                Appointments = c.Appointments.Where(a => GetCurrentWeek(a.StartTime) == currentWeek).ToList()
                            }).ToList();
        }

        //Hämta kunder och deras möten denna månad
        public async Task<IEnumerable<Customer>> GetCustomersWithAppointmentsThisMonth()
        {
            int currentMonth = GetCurrentMonth(DateTime.Now);
            var customers = await _context.Customers
                .Include(c => c.Appointments)
                .ToListAsync();

            return customers.Where(c => c.Appointments.Any(a => GetCurrentMonth(a.StartTime) == currentMonth))
                            .Select(c => new Customer
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Email = c.Email,
                                Phone = c.Phone,
                                Appointments = c.Appointments.Where(a => GetCurrentMonth(a.StartTime) == currentMonth).ToList()
                            }).ToList();
        }


        // Hämta totalt antal minuter en kund har bokat för en specifik vecka
        public async Task<decimal> GetTotalMinutesForCustomerPerWeek(int weekNumber, int customerId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.CustomerId == customerId)
                .ToListAsync();

            //Hämtar de möte som matchar inmatade veckonummret.
            var appointmentsDuringWeek = appointments.Where(a => GetCurrentWeek(a.StartTime) == weekNumber).ToList();

            return appointmentsDuringWeek.Sum(a => a.DurationInMinutes);
        }

        // Hämta alla kunder med filter
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersWithFilters(string sortOrder, string searchString)
        {
            //Starta en fråga som representerar alla kunder
            IQueryable<Customer> query = _context.Customers.AsQueryable();


            //Filtrerar baserat på searchstring
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.Name.Contains(searchString));
            }

            //Sorterar baserat på vad som skickas med i sortOrder.
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(c => c.Name);
                    break;
                case "name_asc":
                    query = query.OrderBy(c => c.Name);
                    break;
                case "id_desc":
                    query = query.OrderByDescending(c => c.Id);
                    break;
                case "id_asc":
                    query = query.OrderBy(c => c.Id);
                    break;
                default:

                    //Om inget skickas med så sortera på Id
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            //Sparar frågan som en lista och retunerar
            var customers = await query.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
        
    }
}
