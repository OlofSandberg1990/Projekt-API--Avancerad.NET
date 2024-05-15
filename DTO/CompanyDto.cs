namespace Projekt_API__Avancerad.NET.DTO
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<AppointmentDto> Appointments { get; set; }
    }
}
