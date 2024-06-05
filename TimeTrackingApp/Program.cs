using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TimeTrackingApp.Models;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TimeTrackingApp.Middleware;
using TimeTrackingApp.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
string allowedHosts = Environment.GetEnvironmentVariable("ALLOWED_HOSTS");
// Postgres context
string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

builder.Services.AddDbContext<TimeTrackDbContext>(options =>
{
    options.UseNpgsql(connectionString ??
        throw new InvalidOperationException("Connection String not found or invalid"));
});

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddTransient<UsersConverter>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins(allowedHosts)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials() 
    );
});

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TimeTrack API", Version = "v1" });
});

var app = builder.Build();


app.UseCors("AllowSpecificOrigin");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time Track API V1");
});

app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
