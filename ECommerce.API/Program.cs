using ECommerce.API.Middlewares;
using ECommerce.Core;
using ECommerce.Infrastructure;
using System.Text.Json.Serialization;
using ECommerce.Core.MappingProfiles;

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
//Build the web application
var app = builder.Build();
app.UseExceptionHandlingMiddleware();
//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();
app.Run();
