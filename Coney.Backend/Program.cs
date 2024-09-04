using Coney.Backend.Data;
using Coney.Backend.Data.Repositories.Users;
using Coney.Backend.Services.Users;
using Coney.Backend.Filters; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Configuration  Kestrel When you run an ASP.NET Core application in a Docker container, 
// If not configured correctly, the web server will only listen on localhost inside the container 
// and you will not be able to access the application from outside.
var port = Environment.GetEnvironmentVariable("ASPNETCORE_PORT") ?? "5293";
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<ConeyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container and create filter for error handling.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// Register Repositories and Services
// AddScoped: creates an instance for each HTTP request
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

// Required to use swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Builds the WebApplication instance using the configured services and settings of "builder"
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set the global prefix "api" for all routes
app.UsePathBase("/api");

// Redirects all HTTP requests to HTTPS
app.UseHttpsRedirection(); 

app.UseAuthorization();

// assigns the attributes to the controller [HttpGet], [HttpPost]...etc
app.MapControllers();

//Run application 
app.Run();
