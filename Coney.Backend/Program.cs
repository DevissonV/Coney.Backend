using Coney.Backend.Data;
using Coney.Backend.Data.Repositories;
using Coney.Backend.Services;
using Coney.Backend.Filters; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<ConeyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// Register Repositories and Services
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar el prefijo global "api" para todas las rutas
app.UsePathBase("/api");

app.UseHttpsRedirection(); // Redirects all HTTP requests to HTTPS

app.UseAuthorization();

app.MapControllers();

app.Run();
