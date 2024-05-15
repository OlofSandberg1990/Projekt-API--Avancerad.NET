using BookingSystemLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_API__Avancerad.NET.Data;
using Projekt_API__Avancerad.NET.DTO;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using Projekt_API__Avancerad.NET.Services.Repositorys;
using System.Text.Json.Serialization;

namespace Projekt_API__Avancerad.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Register repositories and services
            builder.Services.AddScoped<IAuthorize, AuthorizeRepository>();
            builder.Services.AddScoped<IRepository<CustomerDto>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<AppointmentDto>, CompanyRepository>();
            builder.Services.AddScoped<ICustomer, CustomerRepository>();
            builder.Services.AddScoped<ICompany, CompanyRepository>();
            builder.Services.AddScoped<IChangeLog, ChangeLogRepository>();

            // Configure JSON options
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Configure EF Core with SQL Server
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            // Configure MemoryCache
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
