using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => 
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultCon")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
   await context.Database.MigrateAsync();
   await AppDbContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
   logger.LogError(ex, "An error occured during migration");
}

app.Run();
