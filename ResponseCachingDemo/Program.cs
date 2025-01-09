using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResponseCachingDemo.Models;
namespace ResponseCachingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure DbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Response Caching services
            builder.Services.AddResponseCaching();

            builder.Services.AddControllers(options =>
            {
                //Creating Custom Cache Profiles
                options.CacheProfiles.Add("Default60", new CacheProfile()
                {
                    Duration = 60,
                    Location = ResponseCacheLocation.Any
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Enable Response Caching Middleware
            app.UseResponseCaching();

            app.MapControllers();

            app.Run();
        }
    }
}