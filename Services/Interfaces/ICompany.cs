using Projekt_API__Avancerad.NET.DTO;

namespace Projekt_API__Avancerad.NET.Services.Interfaces
{
    public interface ICompany
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsWithFilters(string sortOrder, string searchString, int? customerId, int? companyId);
        Task LogChange(int appointmentId, int customerId, int companyId, string action);
    }
}
