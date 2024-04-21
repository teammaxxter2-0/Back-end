using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfferteApp.Data;
using OfferteApp.Models;

namespace OfferteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Tijdelijk authentication uitgezet omdat het nog niet gebruikt wordt.
            // builder.Services.AddAuthorization();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:5173", "http://localhost:5018")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            });

            builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);

            builder.Services.AddControllers();


            builder.Configuration.AddEnvironmentVariables().AddJsonFile(builder.Environment.IsDevelopment()
                ? "appsettings.development.json"
                : "appsettings.json");

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            var app = builder.Build();

            app.UseHttpsRedirection(); // Add HTTPS redirection middleware here

            app.UseRouting();

            app.UseCors("AllowSpecificOrigin"); // Apply CORS policy

            app.UseAuthorization();

            app.MapControllers();

            var db = new DatabaseContext();
            var filled = db.Set<Account>().FirstOrDefault();

            if (filled == null)
            {
                DBSeeding.Seed();
            }

            // Swagger documentatie alleen zichtbaar in development.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                });
            }

            // Tijdelijk authentication uitgezet omdat het nog niet gebruikt wordt.
            app.Run();
        }
    }
}