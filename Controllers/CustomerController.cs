using BookingSystemLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Projekt_API__Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<CustomerDto> _customerDtoRepo;
        private readonly ICustomer _customerRepo;
        private readonly IRepository<AppointmentDto> _appointmentDtoRepo;
        private readonly ICompany _companyRepo;

        public CustomerController(IRepository<CustomerDto> customerDtoRepo, ICustomer customerRepo, IRepository<AppointmentDto> appointmentDtoRepo, ICompany companyRepo)
        {
            _customerDtoRepo = customerDtoRepo;
            _customerRepo = customerRepo;
            _appointmentDtoRepo = appointmentDtoRepo;
            _companyRepo = companyRepo;
        }

        //Kontrollerar först om användaren är en Admin, Customer eller Company
        private bool IsAdmin()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Admin";
        }

        
        private bool IsCustomer()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Customer";
        }

        private bool IsAdminOrCustomer()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Admin" || userRole == "Customer";
        }

        //Hämta info om en kund via Id (både Admin och Customers. Ändra till IsAdmin istället för att begränsa åtkomsten)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            //Om metoden retunerar false så retuneras Unauthorized
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admins and customers have access");
            }

            try
            {
                var customer = await _customerDtoRepo.GetById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        //Hämta alla kunder (endast Admin)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!IsAdmin())
            {
                return Unauthorized("Only admins have access");
            }

            try
            {
                return Ok(await _customerDtoRepo.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        // Hämta alla kunder som har bokningar denna vecka (admin och customer)
        [HttpGet("week")]
        public async Task<IActionResult> GetCustomersWithAppointmentsThisWeek()
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admins and customers have access");

            }

            try
            {
                var customers = await _customerRepo.GetCustomersWithAppointmentsThisWeek();
                if (customers == null)
                {
                    return NotFound("No appointments found this week.");
                }

                var result = customers.Select(c => new
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Appointments = c.Appointments.Select(a => new
                    {                        
                        a.Id,
                        a.StartTime,
                        a.DurationInMinutes,
                        a.Title,
                        a.CompanyId,
                    })
                });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }       
        
        // Hämta alla kunder som har bokningar denna månad (admin och customer)
        [HttpGet("month")]
        public async Task<IActionResult> GetCustomersWithAppointmentsThisMonth()
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admins and customers have access");

            }

            try
            {
                var customers = await _customerRepo.GetCustomersWithAppointmentsThisMonth();
                if (customers == null)
                {
                    return NotFound("No appointments found this month.");
                }

                var result = customers.Select(c => new
                {
                    CustomerId = c.Id,
                    CustomerName = c.Name,
                    Appointments = c.Appointments.Select(a => new
                    {
                        a.Id,
                        a.StartTime,
                        a.DurationInMinutes,
                        a.Title,
                        a.CompanyId,
                    })
                });

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        //Hämta antalet bokningar en specifik kund har en specifik vecka (admin och customer)
        [HttpGet("{customerId:int}/week/{weekNumber:int}")]
        public async Task<IActionResult> GetTotalHoursByCustomerAndWeek(int customerId, int weekNumber)
        {
            if (!IsAdminOrCustomer())
                return Unauthorized("Only admins and customers have access");

            try
            {
                decimal totalMinutes = await _customerRepo.GetTotalMinutesForCustomerPerWeek(weekNumber, customerId);
                if (totalMinutes == 0)
                {
                    return NotFound($"Customer with ID {customerId} has no appointments this week.");
                }
                return Ok(new { CustomerId = customerId, WeekNumber = weekNumber, TotalHours = totalMinutes / 60 });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        // Hämta alla bokningar för en specifik kund (admin och customer)
        [HttpGet("{customerId:int}/appointments")]
        public async Task<IActionResult> GetAppointmentsByCustomerId(int customerId)
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admins and customers have access");

            }

            try
            {
                var customer = await _customerRepo.GetCustomerWithAppointmentsById(customerId);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {customerId} not found");
                }

                var appointments = customer.Appointments.Select(a => new
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.Name,
                    CustomerEmail = customer.Email,
                    CustomerPhone = customer.Phone,                    
                    Appointments = customer.Appointments.Select(a => new
                    {
                        a.Id,
                        a.StartTime,
                        a.DurationInMinutes,
                        a.Title,
                        a.Description,
                        a.CompanyId
                    }).ToList()

                });

                return Ok(appointments);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        // Skapa en ny bokning (admin och customer)
        [HttpPost("appointments")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admin and customers have access");

            }

            try
            {
                if (appointment == null)
                {
                    return BadRequest("Invalid appointment data");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newAppointment = await _appointmentDtoRepo.Add(appointment);
                return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, newAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to create new appointment");
            }
        }

        // Uppdatera en bokning (admin och customer)
        [HttpPut("appointments/{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointment)
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admin and customers have access");

            }

            try
            {
                if (id != appointment.Id)
                {
                    return BadRequest("The ID does not match");
                }

                if (appointment == null)
                {
                    return BadRequest("Invalid appointment data");
                }

                var updatedAppointment = await _appointmentDtoRepo.Update(appointment);
                return Ok(updatedAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update appointment");
            }
        }

        // Ta bort en bokning (admin och customer)
        [HttpDelete("appointments/{id:int}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admin and customers have access");

            }

            try
            {
                var appointmentToDelete = await _appointmentDtoRepo.Delete(id);
                if (appointmentToDelete == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }
                return Ok(appointmentToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to delete appointment");
            }
        }

        // Stödja sortering och filtrering av bokningar (admin och customer)
        [HttpGet("appointmentsWithFilters")]
        public async Task<IActionResult> GetAppointmentsWithFilters(string sortOrder, string searchAfterAppointmentTitle, int? customerId, int? companyId)
        {
            if (!IsAdminOrCustomer())
            {
                return Unauthorized("Only admin and customers have access");
            }                

            try
            {
                return Ok(await _companyRepo.GetAllAppointmentsWithFilters(sortOrder, searchAfterAppointmentTitle, customerId, companyId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }
    }
}
