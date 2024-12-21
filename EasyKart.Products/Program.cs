
using EasyKart.Products.Repository;

namespace EasyKart.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var allowedOriginsString = builder.Configuration.GetSection("CorsSettings:AllowedOrigins").ToString();
            var allowedOrigins = allowedOriginsString?.Split(',');

            foreach (var item in allowedOriginsString)
            {
                Console.WriteLine("CORS");
                Console.WriteLine(item);
            }
           

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.WithOrigins("http://localhost:4200","http://20.235.211.32")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            // Add services to the container.
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();

            builder.Services.AddControllers();
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
            app.UseCors("AllowCors");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
