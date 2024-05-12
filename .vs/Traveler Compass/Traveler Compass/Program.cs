using Microsoft.EntityFrameworkCore;
using Traveler_Compass.Data;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Traveler_Compass.Helper;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //validates the server generates the tokens
              ValidateAudience = true, //Validates the recipet of the token
            ValidateLifetime = true, //checks if the token isn't expired 
            ValidIssuer = builder.Configuration["Jwt:Issue"], //
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]))
        };  //Validated the signature of the token
      }); 
    
     
    
builder.Services.AddControllers();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Adds the Authentication feature to swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

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
builder.Services.AddScoped<IRegisterRepository,  RegisterRepository>();   

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
//enable authentication for your API:
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
