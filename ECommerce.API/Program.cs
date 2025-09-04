using ECommerce.API.Middlewares;
using ECommerce.Core;
using ECommerce.Infrastructure;
using System.Text.Json.Serialization;
using ECommerce.Core.MappingProfiles;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAutoMapper(cfg =>
{
    // You can add global configuration here if needed
    cfg.AllowNullCollections = true;
    cfg.AllowNullDestinationValues = true;

}, typeof(UserMapProfiles).Assembly);

//fluent validation
//Build the web application
builder.Services.AddFluentValidationAutoValidation();

// Add Api Explorer 
builder.Services.AddEndpointsApiExplorer();
// Add Swagger 
builder.Services.AddSwaggerGen();
builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(y =>
    {
        y.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();
//Routing
app.UseRouting();
app.UseSwagger(); // Adds swagger
app.UseSwaggerUI();

app.UseCors();
//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();
app.Run();
