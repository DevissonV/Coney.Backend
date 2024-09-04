using Coney.Backend.Data;
using Coney.Backend.Data.Repositories.Users;
using Coney.Backend.Services.Users;
using Coney.Backend.Filters; 
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
DotNetEnv.Env.Load();

// Load configurations from appsettings.json and environment variables
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

// Build the connection string using environment variables
var defaultConnection = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                        $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                        $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                        $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
builder.Configuration["ConnectionStrings:DefaultConnection"] = defaultConnection;


// Set up Kestrel with the port using environment variables
var kestrelPort = Environment.GetEnvironmentVariable("APP_EXEC_PORT") ?? "5293";
builder.Configuration["Kestrel:Port"] = kestrelPort;

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(kestrelPort));
});

builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<ConeyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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
