using HostelListing.Configurations;
using HostelListing.Data;
using HostelListing.IRepository;
using HostelListing.Repository;
using HostelListing.Services;
using HostelListing.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace HostelListing
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: "c:\\hotellistings\\logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();

            try
            {
                Log.Information("Appliction Is Starting");
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConntection"))
                );

                builder.Services.AddAuthentication();
                builder.Services.ConfigureIdentity();
                builder.Services.ConfigureJWT(builder.Configuration);

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // logging
                builder.Host.UseSerilog();

                // CORS (Allow All)
                builder.Services.AddCors(o =>
                {
                    o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
                });

                // Auto-Mapper
                builder.Services.AddAutoMapper(typeof(MapperInitializer));

                builder.Services.AddControllers().AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                // Dependency Injection
                builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
                builder.Services.AddScoped<IAuthManager, AuthManager>();

                // Configure the app

                var app = builder.Build();

                // cors
                app.UseCors("AllowAll");

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
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application Falied to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}