using BookStoreTask.Data;
using BookStoreTask.Data.Repositories;
using BookStoreTask.Mapping;
using BookStoreTask.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace BookStoreTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookDbContext>(option =>
              option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Repositories
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

            // Services
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AutherService>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            // Validators
            //builder.Services.AddScoped<IValidator<BookCreateDto>, BookCreateValidator>();
            //builder.Services.AddScoped<IValidator<BookUpdateDto>, BookUpdateValidator>();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //// Add FluentValidation
            builder.Services.AddFluentValidationAutoValidation();


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
