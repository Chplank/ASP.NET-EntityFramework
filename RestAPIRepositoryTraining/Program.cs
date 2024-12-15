
using EntityFramework;
using EntityFramework.Enities;
using EntityFramework.Interfaces;
using EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace RestAPIRepositoryTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<Context>();
            builder.Services.AddScoped<IRepository<Author>, DbRepository<Author>>();
            builder.Services.AddScoped<IRepository<Book>, DbRepository<Book>>();
            builder.Services.AddScoped<IRepository<BookLoan>, DbRepository<BookLoan>>();
            builder.Services.AddScoped<IRepository<LibraryMember>, DbRepository<LibraryMember>>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Apply migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
