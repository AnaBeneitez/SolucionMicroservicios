using MediatR;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion;

public class Nuevo
{
    public class Ejecuta : IRequest
    {
        public DateTime? FechaCreacion { get; set; }
        public List<string> ProductoLista { get; set; }
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
            CarritoSesion carritoSesion = new ()
            {
                FechaCreacion = request.FechaCreacion
            };

            _context.CarritoSesiones.Add(carritoSesion);
            int valor = await _context.SaveChangesAsync();

            if (valor == 0)
            {
                throw new Exception("No se pudo insertar el carrito de compras");
            }

            foreach (string producto in request.ProductoLista)
            {
                CarritoSesionDetalle detalle = new ()
                {
                    FechaCreacion = DateTime.Now,
                    CarritoSesionId = carritoSesion.CarritoSesionId,
                    ProductoSeleccionado = producto
                };
                _context.CarritoSesionDetalles.Add(detalle);
            }

            valor = await _context.SaveChangesAsync();

            if (valor > 0)
            {
                return Unit.Value;
            }

            throw new Exception("No se pudo insertar el detalle del carrito de compras");
        }
    }
}
