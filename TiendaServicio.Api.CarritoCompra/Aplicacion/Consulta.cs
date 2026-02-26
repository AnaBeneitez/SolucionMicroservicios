using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Modelo.DTOs;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion;

public class Consulta
{
    public class CarritoUnico : IRequest<CarritoDto>
    {
        public int CarritoSesionId { get; set; }
    }

    public class Manejador : IRequestHandler<CarritoUnico, CarritoDto>
    {
        private readonly CarritoContexto _context;
        private readonly ILibrosService _librosService;

        public Manejador(CarritoContexto context, ILibrosService librosService)
        {
            _context = context;
            _librosService = librosService;
        }
        public async Task<CarritoDto> Handle(CarritoUnico request, CancellationToken cancellationToken)
        {
            CarritoSesion? carritoSesion = await _context.CarritoSesiones
                                                .FirstOrDefaultAsync(c => c.CarritoSesionId == request.CarritoSesionId);

            if (carritoSesion == null)
            {
                return null;
            }

            List<CarritoSesionDetalle> carritoSesionDetalles = await _context.CarritoSesionDetalles
                                                                        .Where(d => d.CarritoSesionId == carritoSesion.CarritoSesionId)
                                                                        .ToListAsync();

            List<CarritoDetalleDto> listaDetallesDto = new();

            foreach (CarritoSesionDetalle detalle in carritoSesionDetalles)
            {
                var response = await _librosService.GetLibro(new Guid(detalle.ProductoSeleccionado));

                if(response.resultado)
                {
                    var objetoLibro = response.libro;

                    CarritoDetalleDto detalleDto = new()
                    {
                        LibroId = objetoLibro.LibreriaMaterialId,
                        TituloLibro = objetoLibro.Titulo,
                        AutorLibro = objetoLibro.AutorLibro.ToString(),
                        FechaPublicacion = objetoLibro.FechaPublicacion
                    };

                    listaDetallesDto.Add(detalleDto);
                }
            }

            CarritoDto carritoDto = new ()
            {
                CarritoId = carritoSesion.CarritoSesionId,
                FechaCreacionSesion = carritoSesion.FechaCreacion,
                ListaProductos = listaDetallesDto
            };

            return carritoDto;
        }
    }
}
