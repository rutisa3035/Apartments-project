using Apartments;
using Apartments.Core.Repositories;
using Apartments.Core.Services;
using Apartments.Data.Repositories;
using Apartmrnts.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApartmentService, ApartmentsService>();
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();


builder.Services.AddScoped<IBrokerService, BrokerService>();
builder.Services.AddScoped<IBrokerRepository, BrokerRepository>();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

builder.Services.AddSingleton<DataContext>();

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
