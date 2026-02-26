using TiendaServicio.Api.CarritoCompra.ModeloRemoto;

namespace TiendaServicio.Api.CarritoCompra.InterfazRemota;

public interface ILibrosService
{
    Task<(bool resultado, LibroRemoto libro, string errorMessage)> GetLibro(Guid libroId);
}
