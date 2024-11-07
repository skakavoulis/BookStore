using BookStore.Data;
using BookStore.Middlewares;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IBooksService, BooksService>();
            // builder.Services.AddSingleton<IBooksService, BooksService>(); // Singleton solution
            // builder.Services.AddTransient<IBooksService, BooksService>(); // Instantiate every time

            builder.Services.AddScoped<IAuthorsService, AuthorsService>();
            builder.Services.AddScoped<IMembersService, MembersService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();
            builder.Services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                // Ensure that the database is created
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<Data.BookStoreDbContext>();
                    dbContext.Database.EnsureCreated();
                }
            }

            app.Run();
        }
    }
}