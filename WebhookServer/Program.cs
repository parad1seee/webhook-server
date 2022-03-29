using Microsoft.EntityFrameworkCore;
using System.Net;
using WebhookServer.DAL;
using WebhookServer.Services.Interfaces;
using WebhookServer.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUsersService, UsersService>();

builder.WebHost.ConfigureKestrel(builder =>
{
    builder.Listen(IPAddress.Any, 5000);
    builder.Listen(IPAddress.Any, 5001);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/users", async (IUsersService usersService) => await usersService.GetUsers());

app.Run();
