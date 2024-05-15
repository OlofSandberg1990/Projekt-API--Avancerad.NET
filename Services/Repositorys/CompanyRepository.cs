using AutoMapper;
using BookingSystemLibrary;
using Microsoft.EntityFrameworkCore;
using Projekt_API__Avancerad.NET.Data;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;

namespace Projekt_API__Avancerad.NET.Services.Repositorys
{
    public class CompanyRepository : IRepository<AppointmentDto>, ICompany
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CompanyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Lägga till ny bokning
        public async Task<AppointmentDto> Add(AppointmentDto entity)
        {
            var appointment = _mapper.Map<Appointment>(entity);
            var result = await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            await LogChange(result.Entity.Id, result.Entity.CustomerId, result.Entity.CompanyId, "Appointment Created");
            return _mapper.Map<AppointmentDto>(result.Entity);
        }

        //Ta bort en bokning
        public async Task<AppointmentDto> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return null;
            }

            await LogChange(appointment.Id, appointment.CustomerId, appointment.CompanyId, "Appointment Deleted");
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return _mapper.Map<AppointmentDto>(appointment);
        }

        //Hämta alla bokningar
        public async Task<IEnumerable<AppointmentDto>> GetAll()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        //Hämta bokning via Id
        public async Task<AppointmentDto> GetById(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        //Updatera en bokning
        public async Task<AppointmentDto> Update(AppointmentDto entity)
        {
            var appointment = await _context.Appointments.FindAsync(entity.Id);
            if (appointment == null)
            {
                return null;
            }

            appointment.StartTime = entity.StartTime;
            appointment.DurationInMinutes = entity.DurationInMinutes;
            appointment.Title = entity.Title;
            appointment.CustomerId = entity.CustomerId;
            appointment.CompanyId = entity.CompanyId;

            await _context.SaveChangesAsync();
            await LogChange(appointment.Id, appointment.CustomerId, appointment.CompanyId, "Appointment Updated");
            return _mapper.Map<AppointmentDto>(appointment);
        }

        //Hämta bokningar med filter
        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsWithFilters(string sortOrder, string searchString, int? customerId, int? companyId)
        {
            IQueryable<Appointment> query = _context.Appointments.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Title.Contains(searchString));
            }

            if (customerId.HasValue)
            {
                query = query.Where(a => a.CustomerId == customerId.Value);
            }

            if (companyId.HasValue)
            {
                query = query.Where(a => a.CompanyId == companyId.Value);
            }

            //Sorterar baserat på vad som skickas med i sortOrder.
            switch (sortOrder)
            {
                case "date_desc":
                    query = query.OrderByDescending(a => a.StartTime);
                    break;

                case "date_asc":
                    query = query.OrderBy(a => a.StartTime);
                    break;

                default:
                    query = query.OrderBy(a => a.Id);
                    break;
            }

            var appointments = await query.ToListAsync();
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        
        //Logga ändrigar. 
        public async Task LogChange(int appointmentId, int customerId, int companyId, string action)
        {
            var changeLog = new ChangeLog
            {
                AppointmentId = appointmentId,
                CustomerId = customerId,
                CompanyId = companyId,
                ChangedAt = DateTime.Now,
                ChangeDetails = $"{action.ToUpper()} - AppointmentId: {appointmentId}, CustomerId: {customerId}, CompanyId: {companyId}, LogTime: {DateTime.Now}"
            };
            await _context.ChangeLogs.AddAsync(changeLog);
            await _context.SaveChangesAsync();
        }
    }
}
