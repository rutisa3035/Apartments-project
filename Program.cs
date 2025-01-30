using Apartments;
using Apartments.Core;
using Apartments.Core.Repositories;
using Apartments.Core.Services;
using Apartments.Data;
using Apartments.Data.Repositories;
using Apartmrnts.Service;
using System.Text.Json.Serialization;

namespace Apartments.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IApartmentService, ApartmentsService>();
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();


            builder.Services.AddScoped<IBrokerService, BrokerService>();
            builder.Services.AddScoped<IBrokerRepository, BrokerRepository>();

            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();

            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(MappingPostModel));
            var app = builder.Build();

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














