using BookingSystemLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Globalization;

namespace Projekt_API__Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<AppointmentDto> _appointmentRepo;
        private readonly ICompany _companyRepo;

        public CompanyController(IRepository<AppointmentDto> appointmentRepo, ICompany companyRepo)
        {
            _appointmentRepo = appointmentRepo;
            _companyRepo = companyRepo;
        }

        //Kontrollerar först om användaren är en Admin, Customer eller Company
        private bool IsAdmin()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Admin";
        }        
        private bool IsCompany()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Company";
        }
        private bool IsCustomer()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return userRole == "Customer";
        }

        // Hämta alla bokningar för en specifik månad
        [HttpGet("appointments/month/{month:int}")]
        public async Task<IActionResult> GetAppointmentsByMonth(int month)
        {
            if (!IsAdmin() && !IsCompany())
            {
                return Unauthorized("Only admins and companies have access");

            }

            try
            {
                var appointments = await _appointmentRepo.GetAll();
                var filteredAppointments = appointments.Where(a => a.StartTime.Month == month).ToList();
                return Ok(filteredAppointments);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        // Hämta alla bokningar för en specifik vecka
        [HttpGet("appointments/week/{week:int}")]
        public async Task<IActionResult> GetAppointmentsByWeek(int week)
        {
            if (!IsAdmin() && !IsCompany())
            {
                return Unauthorized("Only admins and companies have access");
            }

            try
            {
                var appointments = await _appointmentRepo.GetAll();
                var filteredAppointments = appointments.Where(a => ISOWeek.GetWeekOfYear(a.StartTime) == week).ToList();
                return Ok(filteredAppointments);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }

        // Skapa en ny bokning
        [HttpPost("appointments")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
        {
            if (!IsAdmin() && !IsCompany() && !IsCustomer())
            {
                return Unauthorized("Only admins, customers and companies have access");

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



                var newAppointment = await _appointmentRepo.Add(appointment);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, newAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to create new appointment");
            }
        }

        // Uppdatera en bokning
        [HttpPut("appointments/{id:int}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentDto appointment)
        {
            if (!IsAdmin() && !IsCompany() && !IsCustomer())
            {
                return Unauthorized("Only admins, customers and companies have access");

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

                var updatedAppointment = await _appointmentRepo.Update(appointment);
                return Ok(updatedAppointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to update appointment");
            }
        }

        // Ta bort en bokning
        [HttpDelete("appointments/{id:int}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (!IsAdmin() && !IsCompany() && !IsCustomer())
            {
                return Unauthorized("Only admins, customers and companies have access");

            }

            try
            {
                var appointmentToDelete = await _appointmentRepo.Delete(id);
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

        // Stödja sortering och filtrering av bokningar
        [HttpGet("appointments/filters")]
        public async Task<IActionResult> GetAppointmentsWithFilters(string sortOrder, string searchAfterAppointmentTitle, int? customerId, int? companyId)
        {
            if (!IsAdmin() && !IsCompany())
            {
                return Unauthorized("Only admins and companies have access");

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

        // Hämta en specifik bokning efter ID
        [HttpGet("appointments/{id:int}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            if (!IsAdmin() && !IsCompany())
                return Unauthorized("Only admins and companies have access");

            try
            {
                var appointment = await _appointmentRepo.GetById(id);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                return Ok(appointment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error to retrieve data from database");
            }
        }
    }
}
