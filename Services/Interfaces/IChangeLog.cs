using BookingSystemLibrary;
using Projekt_API__Avancerad.NET.DTO;

namespace Projekt_API__Avancerad.NET.Services.Interfaces
{
    public interface IChangeLog
    {
        Task<IEnumerable<ChangeLogDto>> GetAll();
        
    }
}
