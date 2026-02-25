using MediatR;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public DateTime? FechaCreacion { get; set; }
        public List<DetalleSesionDto> Productos { get; set; }
    }

    public class DetalleSesionDto
    {
        public string ProductoSeleccionado { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }

    public class Manejador : IRequestHandler<Ejecuta>
    {
        private readonly CarritoContexto _context;

        public Manejador(CarritoContexto context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
        {
            var carritoSesion = new CarritoSesion
            {
                FechaCreacion = request.FechaCreacion
            };

            _context.CarritoSesiones.Add(carritoSesion);
            var valor = await _context.SaveChangesAsync();

            if (valor == 0)
            {
                throw new Exception("No se pudo insertar el carrito de compras");
            }

            foreach (var producto in request.Productos)
            {
                var detalle = new CarritoSesionDetalle
                {
                    CarritoSesionId = carritoSesion.CarritoSesionId,
                    ProductoSeleccionado = producto.ProductoSeleccionado,
                    FechaCreacion = producto.FechaCreacion
                };
                _context.CarritoSesionDetalles.Add(detalle);
            }

            var valorDetalle = await _context.SaveChangesAsync();

            if (valorDetalle > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el detalle del carrito de compras");
        }
    }
}
