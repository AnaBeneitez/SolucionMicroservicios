using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.Modelo;

namespace TiendaServicio.Api.CarritoCompra.Persistencia;

public class CarritoContexto : DbContext
{
    public CarritoContexto(DbContextOptions<CarritoContexto> options) : base(options)
    {
    }
    public DbSet<CarritoSesion> CarritoSesiones { get; set; }
    public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
}
