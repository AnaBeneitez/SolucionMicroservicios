namespace TiendaServicio.Api.CarritoCompra.Modelo.DTOs;

public class CarritoDto
{
    public int CarritoId { get; set; }
    public DateTime? FechaCreacionSesion { get; set; }
    public List<CarritoDetalleDto> ListaProductos { get; set; }
}
    