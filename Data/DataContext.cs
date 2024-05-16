using BookingSystemLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Projekt_API__Avancerad.NET.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        

            //Seed data
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Gekås", Email = "kontakt@gekas.se", Password = "Test123" },
                new Company { Id = 2, Name = "ICA", Email = "kontakt@ica.se", Password = "Test123" },
                new Company { Id = 3, Name = "Coop", Email = "kontakt@coop.se", Password = "Test123" },
                new Company { Id = 4, Name = "Campus", Email = "kontakt@campus.se", Password = "Test123" },
                new Company { Id = 5, Name = "Elgiganten", Email = "kontakt@elgiganten.se", Password = "Test123" },
                new Company { Id = 6, Name = "Net On Net", Email = "kontakt@netonnet.se", Password = "Test123" },
                new Company { Id = 7, Name = "Campino", Email = "kontakt@campino.se", Password = "Test123" },
                new Company { Id = 8, Name = "Dressman", Email = "kontakt@dressman.se", Password = "Test123" },
                new Company { Id = 9, Name = "Lindex", Email = "kontakt@lindex.se", Password = "Test123" },
                new Company { Id = 10, Name = "H&M", Email = "kontakt@hm.se", Password = "Test123" },
                new Company { Id = 11, Name = "Volvo", Email = "kontakt@volvo.se", Password = "Test123" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Olof Sandberg", Email = "olof@test.com", Password = "Test123", Phone = "+46701234567" },
                new Customer { Id = 2, Name = "Jonatan Nordin", Email = "jonatan@test.com", Password = "Test123", Phone = "+46706543210" },
                new Customer { Id = 3, Name = "Christian Rapp", Email = "christian@test.com", Password = "Test123", Phone = "+46709876543" },
                new Customer { Id = 4, Name = "Anna Söderberg", Email = "anna@test.com", Password = "Test123", Phone = "+46702345678" },
                new Customer { Id = 5, Name = "Nina Lindberg Nilsson", Email = "nina@test.com", Password = "Test123", Phone = "+46707654321" },
                new Customer { Id = 6, Name = "Pär Sandberg", Email = "par@test.com", Password = "Test123", Phone = "+46701123456" },
                new Customer { Id = 7, Name = "Tobias Qlok", Email = "tobias@test.com", Password = "Test123", Phone = "+46702234567" },
                new Customer { Id = 8, Name = "Reidar Qlok", Email = "reidar@test.com", Password = "Test123", Phone = "+46703345678" },
                new Customer { Id = 9, Name = "Pepsi Sandberg", Email = "pepsi@test.com", Password = "Test123", Phone = "+46704456789" },
                new Customer { Id = 10, Name = "Miki Vidacic", Email = "miki@test.com", Password = "Test123", Phone = "+46705567890" }
            );

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, StartTime = new DateTime(2024, 5, 6, 10, 0, 0), DurationInMinutes = 90, Title = "Support Session", Description = "Technical support session", CustomerId = 1, CompanyId = 2 },
                new Appointment { Id = 2, StartTime = new DateTime(2024, 5, 7, 14, 30, 0), DurationInMinutes = 120, Title = "Data Backup", Description = "Scheduled data backup", CustomerId = 2, CompanyId = 2 },
                new Appointment { Id = 3, StartTime = new DateTime(2024, 5, 8, 11, 0, 0), DurationInMinutes = 60, Title = "Website Consultation", Description = "Consultation for website development", CustomerId = 3, CompanyId = 6 },
                new Appointment { Id = 4, StartTime = new DateTime(2024, 5, 9, 15, 30, 0), DurationInMinutes = 90, Title = "Network Upgrade", Description = "Upgrade network infrastructure", CustomerId = 4, CompanyId = 1 },
                new Appointment { Id = 5, StartTime = new DateTime(2024, 5, 10, 9, 0, 0), DurationInMinutes = 120, Title = "Software Training", Description = "Training session for new software", CustomerId = 5, CompanyId = 5 },
                new Appointment { Id = 6, StartTime = new DateTime(2024, 5, 11, 13, 0, 0), DurationInMinutes = 90, Title = "IT Consultation", Description = "Consultation for IT infrastructure", CustomerId = 6, CompanyId = 9 },
                new Appointment { Id = 7, StartTime = new DateTime(2024, 5, 12, 14, 0, 0), DurationInMinutes = 120, Title = "System Upgrade", Description = "Upgrading system components", CustomerId = 7, CompanyId = 7 },
                new Appointment { Id = 8, StartTime = new DateTime(2024, 5, 13, 10, 30, 0), DurationInMinutes = 60, Title = "Database Optimization", Description = "Optimizing database performance", CustomerId = 8, CompanyId = 8 },
                new Appointment { Id = 9, StartTime = new DateTime(2024, 5, 14, 11, 0, 0), DurationInMinutes = 90, Title = "Security Audit", Description = "Comprehensive security audit", CustomerId = 9, CompanyId = 9 },
                new Appointment { Id = 10, StartTime = new DateTime(2024, 5, 15, 15, 0, 0), DurationInMinutes = 120, Title = "Software Installation", Description = "Installing new software", CustomerId = 10, CompanyId = 10 },
                new Appointment { Id = 11, StartTime = new DateTime(2024, 5, 16, 10, 0, 0), DurationInMinutes = 90, Title = "Technical Meeting", Description = "Technical meeting for project planning", CustomerId = 2, CompanyId = 3 },
                new Appointment { Id = 12, StartTime = new DateTime(2024, 5, 17, 14, 30, 0), DurationInMinutes = 120, Title = "Product Demo", Description = "Demonstration of new products", CustomerId = 4, CompanyId = 4 },
                new Appointment { Id = 13, StartTime = new DateTime(2024, 5, 18, 11, 0, 0), DurationInMinutes = 60, Title = "Maintenance Check", Description = "Routine maintenance check", CustomerId = 6, CompanyId = 6 },
                new Appointment { Id = 14, StartTime = new DateTime(2024, 5, 19, 15, 30, 0), DurationInMinutes = 90, Title = "Project Kickoff", Description = "Kickoff meeting for new project", CustomerId = 7, CompanyId = 7 },
                new Appointment { Id = 15, StartTime = new DateTime(2024, 5, 20, 9, 0, 0), DurationInMinutes = 120, Title = "Training Session", Description = "Training session for staff", CustomerId = 9, CompanyId = 8 },
                new Appointment { Id = 16, StartTime = new DateTime(2024, 5, 21, 13, 0, 0), DurationInMinutes = 90, Title = "Client Meeting", Description = "Meeting with potential client", CustomerId = 10, CompanyId = 9 },
                new Appointment { Id = 17, StartTime = new DateTime(2024, 5, 22, 14, 0, 0), DurationInMinutes = 120, Title = "System Review", Description = "Review of current systems", CustomerId = 1, CompanyId = 10 },
                new Appointment { Id = 18, StartTime = new DateTime(2024, 5, 23, 10, 30, 0), DurationInMinutes = 60, Title = "Consultation", Description = "Consultation for future projects", CustomerId = 3, CompanyId = 1 },
                new Appointment { Id = 19, StartTime = new DateTime(2024, 5, 24, 11, 0, 0), DurationInMinutes = 90, Title = "Feedback Session", Description = "Session to gather client feedback", CustomerId = 5, CompanyId = 2 },
                new Appointment { Id = 20, StartTime = new DateTime(2024, 5, 25, 15, 0, 0), DurationInMinutes = 120, Title = "Strategy Meeting", Description = "Meeting to discuss company strategy", CustomerId = 8, CompanyId = 3 },
                new Appointment { Id = 21, StartTime = new DateTime(2024, 6, 1, 10, 0, 0), DurationInMinutes = 90, Title = "Planning Session", Description = "Planning session for upcoming projects", CustomerId = 7, CompanyId = 4 },
                new Appointment { Id = 22, StartTime = new DateTime(2024, 6, 3, 14, 30, 0), DurationInMinutes = 120, Title = "Budget Review", Description = "Review of company budget", CustomerId = 6, CompanyId = 5 },
                new Appointment { Id = 23, StartTime = new DateTime(2024, 6, 5, 11, 0, 0), DurationInMinutes = 60, Title = "Performance Review", Description = "Review of company performance", CustomerId = 4, CompanyId = 6 },
                new Appointment { Id = 24, StartTime = new DateTime(2024, 6, 7, 15, 30, 0), DurationInMinutes = 90, Title = "Project Meeting", Description = "Meeting to discuss project details", CustomerId = 3, CompanyId = 7 },
                new Appointment { Id = 25, StartTime = new DateTime(2024, 6, 9, 9, 0, 0), DurationInMinutes = 120, Title = "Client Presentation", Description = "Presentation to potential clients", CustomerId = 2, CompanyId = 8 },
                new Appointment { Id = 26, StartTime = new DateTime(2024, 6, 11, 13, 0, 0), DurationInMinutes = 90, Title = "Team Meeting", Description = "Meeting with project team", CustomerId = 1, CompanyId = 9 },
                new Appointment { Id = 27, StartTime = new DateTime(2024, 6, 13, 14, 0, 0), DurationInMinutes = 120, Title = "Technical Review", Description = "Review of technical aspects", CustomerId = 10, CompanyId = 10 },
                new Appointment { Id = 28, StartTime = new DateTime(2024, 6, 15, 10, 30, 0), DurationInMinutes = 60, Title = "Progress Meeting", Description = "Meeting to discuss project progress", CustomerId = 9, CompanyId = 1 },
                new Appointment { Id = 29, StartTime = new DateTime(2024, 6, 17, 11, 0, 0), DurationInMinutes = 90, Title = "Client Update", Description = "Update meeting with client", CustomerId = 8, CompanyId = 2 },
                new Appointment { Id = 30, StartTime = new DateTime(2024, 6, 19, 15, 0, 0), DurationInMinutes = 120, Title = "Staff Training", Description = "Training session for staff", CustomerId = 7, CompanyId = 3 }
            };

            modelBuilder.Entity<Appointment>().HasData(appointments);

            modelBuilder.Entity<Admin>().HasData(
                new Admin { Id = 1, Email = "admin@example.com", Password = "Admin123" }
            );
        }
    }
}
