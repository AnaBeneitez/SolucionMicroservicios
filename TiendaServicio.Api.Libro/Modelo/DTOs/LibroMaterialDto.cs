namespace TiendaServicio.Api.Libro.Modelo.DTOs;

public class LibroMaterialDto
{
    public Guid? LibreriaMaterialID { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public Guid? AutorLibro { get; set; }
}
