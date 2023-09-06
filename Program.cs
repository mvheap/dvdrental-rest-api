using Microsoft.EntityFrameworkCore;
using dvd_rental_api.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DvdrentalContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("dvdrentaldb")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
