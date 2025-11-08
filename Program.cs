using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Jadev.Library.Managment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<Data.LibraryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Dependency Injection registrations
            builder.Services.AddScoped<Services.IAuthorService,Services.AuthorService>();
            builder.Services.AddScoped<Repositories.IAuthorRepository, Repositories.AuthorRepository>();
            builder.Services.AddScoped<Repositories.IBookRepository, Repositories.BookRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
