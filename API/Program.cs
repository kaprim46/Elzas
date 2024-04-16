using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => 
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultCon")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
