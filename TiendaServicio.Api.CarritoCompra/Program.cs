using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.Aplicacion;
using TiendaServicio.Api.CarritoCompra.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CarritoContexto>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase"));
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
