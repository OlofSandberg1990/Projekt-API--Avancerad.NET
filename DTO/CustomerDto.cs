using System.Text.Json.Serialization;

namespace Projekt_API__Avancerad.NET.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string Phone { get; set; }

        [JsonIgnore]
        public ICollection<AppointmentDto> Appointments { get; set; }
    }
}
