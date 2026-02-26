namespace TiendaServicio.Api.CarritoCompra.ModeloRemoto;

public class LibroRemoto
{
    public Guid? LibreriaMaterialId { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
}
