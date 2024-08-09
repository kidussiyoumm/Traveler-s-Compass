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
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//Allows us to fetch from app.setting and other configurations
var configuration = builder.Configuration;
var SecurityKey = configuration.GetSection("Jwt:key").Value;

var key = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(SecurityKey));

// Add JwtBearer support for Authentications
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//constant AAuthentication Scheme
    .AddJwtBearer(options => 
    //AddJwtBearer is an extension method
    //options to validate Tokens
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {   //ValidateLifetime = true, //checks if the token isn't expired 
              //ValidateIssuer = false, //validates the server generates the tokens
              ValidateIssuerSigningKey = true,
              ValidateAudience = false, //Validates the recipet of the token
              ValidateIssuer = false, 
              IssuerSigningKey = key
                
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompassAppDbConnectionString"));
});

// Add CORS services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyOrigin()
                  .AllowAnyMethod();
        });
});

//Injecting a Service inisde the program.cs file
//If we want an implementation class coming from the Repository 
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IItineraryRepository, ItineraryRepository>();
builder.Services.AddScoped<IRegisterRepository,  RegisterRepository>();   
builder.Services.AddScoped<ICreateJWT, JwtTokenUtility>();

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

//app.UseRouting();

app.UseCors("AllowAll");

//enable authentication for your API:
app.UseAuthentication();//This middleware validate the tokem

app.UseAuthorization();//This middleware will allow the bearer(Access of a protected method
                       //when and only it gets a vaild token on request
app.MapControllers();

app.Run();
