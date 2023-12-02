using Microsoft.EntityFrameworkCore;
using SOCIAL_NETWORK_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.11-mariadb");
builder.Services.AddEntityFrameworkMySql().AddDbContext<SocialNetworkContext>(opt => opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

var misReglasCors = "ReglasCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
