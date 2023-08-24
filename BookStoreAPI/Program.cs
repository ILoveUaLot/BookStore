
using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Data.Repository;
using BookStoreAPI.Data.Repository.Interfaces;
using BookStoreAPI.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            //Configure db connection
            builder.Services.AddDbContext<BookStoreContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Configure AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppMappingProfile>();
            });

            var mapper = mapperConfig.CreateMapper();

            //Configure DI
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddSingleton(mapper);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandlingMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}