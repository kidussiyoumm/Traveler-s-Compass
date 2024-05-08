using Microsoft.EntityFrameworkCore;
using Traveler_Compass.Data;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();




//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//This is debendancy injection. We use builder services to connect the Db connection string from appsetting 
builder.Services.AddDbContext<CompassDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompassDbConnectionString"));
});

//Injecting a Service inisde the program.cs file
//If we want an implementation class coming from the Repository 
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IItineraryRepository, ItineraryRepository>();

//Adding the mapper 
builder.Services.AddAutoMapper(typeof(Program).Assembly);


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
