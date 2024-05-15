using BookingSystemLibrary;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Projekt_API__Avancerad.NET.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        
        public DateTime StartTime { get; set; }

        public int DurationInMinutes { get; set; }
        public string Title { get; set; }      
        
        public int CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore]
        public Company Company { get; set; }
    }
}
