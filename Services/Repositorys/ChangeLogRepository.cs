using AutoMapper;
using BookingSystemLibrary;
using Microsoft.EntityFrameworkCore;
using Projekt_API__Avancerad.NET.Data;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;

namespace Projekt_API__Avancerad.NET.Services.Repositorys
{
    public class ChangeLogRepository : IChangeLog
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ChangeLogRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChangeLogDto>> GetAll()
        {
            var changeLogs = await _context.ChangeLogs.ToListAsync();

            return _mapper.Map<IEnumerable<ChangeLogDto>>(changeLogs);
            
        }
    }
}
